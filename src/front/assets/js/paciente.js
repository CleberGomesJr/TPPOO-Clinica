fetch('../components/header.html')
    .then(res => res.text())
    .then(data => document.getElementById('header').innerHTML = data);

fetch('../components/footer.html')
    .then(res => res.text())
    .then(data => document.getElementById('footer').innerHTML = data);

const modal = document.getElementById('modal-paciente');
const btnAbrir = document.getElementById('adicionar-paciente');
const btnCancelar = document.getElementById('cancelar-modal');
const form = document.getElementById('form-paciente');
const tabela = document.getElementById('tabela-pacientes');


btnAbrir.addEventListener('click', () => {
    modal.style.display = 'flex';
});


btnCancelar.addEventListener('click', () => {
    modal.style.display = 'none';
    form.reset();
});


form.addEventListener('submit', (e) => {
    e.preventDefault();
    const inputs = form.querySelectorAll('input, select');
    const values = Array.from(inputs).map(input => input.value);

    const novaLinha = document.createElement('tr');
    novaLinha.innerHTML = `
                <td>${values[0]}</td>
                <td>${values[1]}</td>
                <td>${values[2]}</td>
                <td>${values[3]}</td>
                <td>${values[4]}</td>
                <td>${values[5]}</td>
                <td>${values[6]}</td>
                <td><button class="remover-btn">Remover</button></td>
            `;
    tabela.appendChild(novaLinha);

    novaLinha.querySelector('.remover-btn').addEventListener('click', () => {
        novaLinha.remove();
    });

    modal.style.display = 'none';
    form.reset();
});


// Simular ação de remover médico
document.querySelectorAll('.remover-btn').forEach(button => {
    button.addEventListener('click', function () {
        const row = this.closest('tr');
        row.remove();
        // AQUI O MÉTODO DA API PARA REMOVER O MÉDICO
    });
});

// Filtro básico de pesquisa
document.getElementById('pesquisar-paciente').addEventListener('input', function () {
    const filtro = this.value.toLowerCase();
    const linhas = document.querySelectorAll('tbody tr');
    linhas.forEach(linha => {
        const texto = linha.innerText.toLowerCase();
        linha.style.display = texto.includes(filtro) ? '' : 'none';
    });
});