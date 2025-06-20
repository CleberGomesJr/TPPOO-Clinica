fetch('/src/front/components/header.html')
    .then(res => res.text())
    .then(data => document.getElementById('header').innerHTML = data);

fetch('/src/front/components/footer.html')
    .then(res => res.text())
    .then(data => document.getElementById('footer').innerHTML = data);

document.addEventListener('DOMContentLoaded', () => {
    const dataHoraInput = document.querySelector('input[type="datetime-local"]');
    if (dataHoraInput) {
        const agora = new Date();
        dataHoraInput.min = agora.toISOString().slice(0, 16);
    }

    carregarConsultas();
});

const modal = document.getElementById('modal-consulta');
const btnAbrir = document.getElementById('adicionar-consulta');
const btnCancelar = document.getElementById('cancelar-modal');
const form = document.getElementById('form-consulta');
const tabela = document.getElementById('tabela-consultas');

const API_BASE = 'https://clinica-medica-api-dth7b4c7auencgbg.brazilsouth-01.azurewebsites.net/api/ClinicaMedica/consultas';

// Abrir modal
btnAbrir.addEventListener('click', () => {
    modal.style.display = 'flex';
});

// Fechar modal
btnCancelar.addEventListener('click', () => {
    modal.style.display = 'none';
    form.reset();
});

// Cadastrar consulta
form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const medicoId = parseInt(form.medicoId.value);
    const pacienteId = parseInt(form.pacienteId.value);
    const crm = parseInt(form.crm.value);
    const numeroCarteirinha = parseInt(form.numeroCarteirinha.value);
    const statusConsultaId = parseInt(form.statusConsultaId.value);
    const descricao = form.descricao.value.trim();
    const dataStr = form.dataConsulta.value;

    const dataConsulta = new Date(dataStr);
    const agora = new Date();
    if (dataConsulta < agora) {
        alert('A data da consulta não pode estar no passado!');
        return;
    }

    const consulta = {
        medicoId,
        pacienteId,
        crm,
        numeroCarteirinha,
        dataConsulta: dataConsulta.toISOString(),
        statusConsultaId,
        descricao
    };

    try {
        const response = await fetch(`${API_BASE}/cadastrar`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(consulta)
        });

        if (!response.ok) {
            const erro = await response.text();
            alert('Erro ao cadastrar consulta: ' + erro);
            return;
        }

        alert('Consulta cadastrada com sucesso!');
        modal.style.display = 'none';
        form.reset();
        carregarConsultas();

    } catch (error) {
        console.error('Erro no cadastro da consulta:', error);
        alert('Erro ao cadastrar consulta.');
    }
});

// Carregar consultas (Exemplo: GET)
async function carregarConsultas() {
    try {
        const response = await fetch(`${API_BASE}/listarConsultas`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({})
        });

        if (!response.ok) {
            alert('Erro ao carregar consultas.');
            return;
        }

        const consultas = await response.json();
        tabela.innerHTML = '';

        consultas.forEach(consulta => {
            const tr = document.createElement('tr');

            tr.innerHTML = `
                <td>${consulta.id}</td>
                <td>${consulta.medicoId}</td>
                <td>${consulta.pacienteId}</td>
                <td>${consulta.crm}</td>
                <td>${consulta.numeroCarteirinha}</td>
                <td>${new Date(consulta.dataConsulta).toLocaleString()}</td>
                <td>${consulta.statusConsultaId}</td>
                <td>${consulta.descricao}</td>
                <td><button class="remover-btn" data-id="${consulta.id}">Cancelar</button></td>
            `;

            tabela.appendChild(tr);

            tr.querySelector('.remover-btn').addEventListener('click', () => {
                removerConsulta(consulta.id, tr);
            });
        });

    } catch (error) {
        console.error('Erro ao carregar consultas:', error);
        alert('Erro inesperado ao carregar consultas.');
    }
}

// Cancelar consulta
async function removerConsulta(id, linhaTabela) {
    if (!confirm('Tem certeza que deseja cancelar esta consulta?')) return;

    try {
        const response = await fetch(`${API_BASE}/${id}/cancelar`, {
            method: 'DELETE'
        });

        if (!response.ok) {
            const erro = await response.text();
            alert('Erro ao cancelar consulta: ' + erro);
            return;
        }

        linhaTabela.remove();
        alert('Consulta cancelada com sucesso.');

    } catch (error) {
        console.error('Erro ao cancelar consulta:', error);
        alert('Erro ao cancelar consulta.');
    }
}

// Filtro de pesquisa
document.getElementById('pesquisar-consulta').addEventListener('input', function () {
    const filtro = this.value.toLowerCase();
    const linhas = tabela.querySelectorAll('tr');
    linhas.forEach(linha => {
        const texto = linha.innerText.toLowerCase();
        linha.style.display = texto.includes(filtro) ? '' : 'none';
    });
});
