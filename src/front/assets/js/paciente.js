// Carregar header e footer
fetch('../components/header.html')
    .then(res => res.text())
    .then(data => document.getElementById('header').innerHTML = data);

fetch('../components/footer.html')
    .then(res => res.text())
    .then(data => document.getElementById('footer').innerHTML = data);

// Variáveis principais
const modal = document.getElementById('modal-paciente');
const btnAbrir = document.getElementById('adicionar-paciente');
const btnCancelar = document.getElementById('cancelar-modal');
const form = document.getElementById('form-paciente');
const tabela = document.getElementById('tabela-pacientes');

// URL base da API
const API_BASE = 'https://clinica-medica-api-dth7b4c7auencgbg.brazilsouth-01.azurewebsites.net/api/ClinicaMedica/pacientes';

// Carregar pacientes ao abrir a página
window.onload = () => {
    carregarPacientes();
};

// Abrir modal
btnAbrir.addEventListener('click', () => {
    modal.style.display = 'flex';
});

// Fechar modal
btnCancelar.addEventListener('click', () => {
    modal.style.display = 'none';
    form.reset();
});

// Submeter formulário (Cadastrar Paciente)
form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const paciente = {
        nome: form.nome.value.trim(),
        sexo: form.sexo.value.trim(),
        cpf: form.cpf.value.trim(),
        dataNascimento: new Date(form.dataNascimento.value).toISOString(),
        email: form.email.value.trim(),
        numero: form.numero.value.trim(),
        endereco: form.endereco.value.trim()
    };

    try {
        const response = await fetch(`${API_BASE}/cadastrar`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(paciente)
        });

        if (!response.ok) {
            const erro = await response.text();
            console.error('Erro ao cadastrar paciente:', erro);
            alert('Erro ao cadastrar paciente: ' + erro);
            return;
        }

        alert('Paciente cadastrado com sucesso!');
        modal.style.display = 'none';
        form.reset();
        carregarPacientes();

    } catch (error) {
        console.error('Erro geral no cadastro:', error);
        alert('Erro ao cadastrar paciente.');
    }
});

// Carregar lista de pacientes mostrando apenas Nome, Número da Carteirinha e CPF
async function carregarPacientes() {
    try {
        const response = await fetch(`${API_BASE}/listar`);

        if (!response.ok) {
            const erro = await response.text();
            console.error('Erro ao listar pacientes:', erro);
            alert('Erro ao carregar pacientes.');
            return;
        }

        const pacientes = await response.json();
        tabela.innerHTML = '';

        pacientes.forEach(paciente => {
            console.log(paciente);  // <-- Veja os campos reais aqui

            const tr = document.createElement('tr');

            tr.innerHTML = `
        <td>${paciente.nome}</td>
        <td>${paciente.numeroCarteirinha}</td>
        <td>${paciente.cpf}</td>
        <td><button class="remover-btn" data-numero="${paciente.numeroCarteirinha}">Remover</button></td>
    `;

            tabela.appendChild(tr);

            const btnRemover = tr.querySelector('.remover-btn');
            btnRemover.addEventListener('click', () => {
                const numero = btnRemover.getAttribute('data-numero');
                removerPaciente(numero, tr);
            });
        });


    } catch (error) {
        console.error('Erro ao carregar pacientes:', error);
        alert('Erro inesperado ao buscar pacientes.');
    }
}

// Remover paciente
async function removerPaciente(numero, linhaTabela) {
    if (!confirm('Tem certeza que deseja excluir este paciente?')) return;

    try {
        const response = await fetch(`https://clinica-medica-api-dth7b4c7auencgbg.brazilsouth-01.azurewebsites.net/api/ClinicaMedica/paciente/${numero}/apagar`, {
            method: 'DELETE'
        });

        if (!response.ok) {
            const erro = await response.text();
            console.error('Erro ao excluir paciente:', erro);
            alert('Erro ao excluir paciente: ' + erro);
            return;
        }

        linhaTabela.remove();
        alert('Paciente removido com sucesso.');

    } catch (error) {
        console.error('Erro ao excluir paciente:', error);
        alert('Falha ao excluir paciente.');
    }
}

