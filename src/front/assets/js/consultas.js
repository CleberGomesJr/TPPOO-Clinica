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
        dataHoraInput.min = agora.toISOString().slice(0,16);
    }
});

const modal = document.getElementById('modal-consulta');
const btnAbrir = document.getElementById('adicionar-consulta');
const btnCancelar = document.getElementById('cancelar-modal');
const form = document.getElementById('form-consulta');
const tabela = document.getElementById('tabela-consultas');

// Exibe modal
btnAbrir.addEventListener('click', () => {
    modal.style.display = 'flex';
});

// Fecha modal
btnCancelar.addEventListener('click', () => {
    modal.style.display = 'none';
    form.reset();
});

//Add a lista
form.addEventListener('submit', (e) => {
    e.preventDefault();

    const protocolo = document.getElementById('protocolo').value;
    const crm = document.getElementById('crm').value;
    const status = document.getElementById('status').value;
    const cpf = document.getElementById('cpf').value;
    const dataStr = document.getElementById('dataConsulta').value;

    // Validação: data não pode ser no passado
    const dataConsulta = new Date(dataStr + "T00:00"); // adiciona horário 00:00 para comparar
    const hoje = new Date();
    hoje.setHours(0, 0, 0, 0); // zera a hora atual para comparar apenas a data

    if (dataConsulta < hoje) {
        alert('A data da consulta não pode estar no passado!');
        return;
    }

    const novaLinha = document.createElement('tr');
    novaLinha.innerHTML = `
        <td>${protocolo}</td>
        <td>${crm}</td>
        <td>${status}</td>
        <td>${cpf}</td>
        <td>${dataStr}</td>
        <td><button class="remover-btn">Remover</button></td>
    `;
    tabela.appendChild(novaLinha);

    novaLinha.querySelector('.remover-btn').addEventListener('click', () => {
        novaLinha.remove();
    });

    modal.style.display = 'none';
    form.reset();
});


// Simular ação de remover 
document.querySelectorAll('.remover-btn').forEach(button => {
    button.addEventListener('click', function () {
        const row = this.closest('tr');
        row.remove();
        // AQUI O MÉTODO DA API PARA REMOVER 
    });
});

// Filtro básico de pesquisa
document.getElementById('pesquisar-consulta').addEventListener('input', function () {
    const filtro = this.value.toLowerCase();
    const linhas = document.querySelectorAll('tbody tr');
    linhas.forEach(linha => {
        const texto = linha.innerText.toLowerCase();
        linha.style.display = texto.includes(filtro) ? '' : 'none';
    });
});