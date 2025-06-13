// Carregar header e footer
fetch('../components/header.html')
    .then(res => res.text())
    .then(data => document.getElementById('header').innerHTML = data);

fetch('../components/footer.html')
    .then(res => res.text())
    .then(data => document.getElementById('footer').innerHTML = data);

// Variáveis principais
const modal = document.getElementById('modal-medico');
const btnAbrir = document.getElementById('adicionar-medico');
const btnCancelar = document.getElementById('cancelar-modal');
const form = document.getElementById('form-medico');
const tabela = document.getElementById('tabela-medicos');

// Ao carregar a página, busca médicos e popula a tabela
window.onload = () => {
    carregarMedicos();
};

// Abrir modal
btnAbrir.addEventListener('click', () => {
    modal.style.display = 'flex';
});

// Fechar modal e resetar formulário
btnCancelar.addEventListener('click', () => {
    modal.style.display = 'none';
    form.reset();
});

// Enviar formulário para cadastrar médico
form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const medico = {
        nome: form.nome.value.trim(),
        sexo: form.sexo.value.trim(),
        cpf: form.cpf.value.trim(),
        dataNascimento: new Date(form.dataNascimento.value).toISOString(),
        email: form.email.value.trim(),
        numero: form.numero.value.trim(),
        crm: parseInt(form.crm.value),
        especialidade: form.especialidade.value.trim()
    };

    console.log('Payload enviado:', JSON.stringify(medico));

    try {
        const response = await fetch('https://clinica-medica-api-dth7b4c7auencgbg.brazilsouth-01.azurewebsites.net/api/ClinicaMedica/medicos/cadastrar', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(medico)
        });

        if (!response.ok) {
            const erroDetalhado = await response.text();
            console.error('Erro detalhado da API:', erroDetalhado);
            alert('Erro ao cadastrar médico: ' + erroDetalhado);
            throw new Error(`Erro ${response.status}`);
        }

        const resultado = await response.text();
        alert('Cadastro realizado: ' + resultado);

        modal.style.display = 'none';
        form.reset();

        // Recarregar médicos para atualizar tabela
        carregarMedicos();

    } catch (erro) {
        console.error('Erro ao cadastrar médico:', erro);
        alert('Falha no cadastro. Verifique os dados e tente novamente.');
    }
});

// Função para carregar médicos da API e mostrar na tabela
async function carregarMedicos() {
    try {
        const response = await fetch('https://clinica-medica-api-dth7b4c7auencgbg.brazilsouth-01.azurewebsites.net/api/ClinicaMedica/medicos/listar');

        if (!response.ok) {
            const erroDetalhado = await response.text();
            console.error('Erro ao listar médicos:', erroDetalhado);
            alert('Erro ao carregar médicos: ' + erroDetalhado);
            return;
        }

        const medicos = await response.json();

        tabela.innerHTML = ''; // limpa tabela antes de preencher

        medicos.forEach(medico => {
            const tr = document.createElement('tr');

            tr.innerHTML = `
        <td>${medico.nome}</td>
        <td>${medico.crm}</td>
        <td>${medico.especialidade}</td>
        <td>
            <button class="remover-btn" data-id="${medico.crm}">Excluir</button>
        </td>
    `;

            tabela.appendChild(tr);

            const btnRemover = tr.querySelector('.remover-btn');
            btnRemover.addEventListener('click', () => {
                const id = btnRemover.getAttribute('data-id');
                removerMedico(id, tr);
            });
        });

    } catch (erro) {
        console.error('Falha ao buscar médicos:', erro);
        alert('Erro inesperado ao buscar médicos.');
    }
}



// Função para ativar evento nos botões de remover
async function removerMedico(id, linhaTabela) {
    if (!id) {
        alert('ID do médico não encontrado.');
        return;
    }

    if (!confirm('Tem certeza que deseja excluir este médico?')) return;

    try {
        const response = await fetch(`https://clinica-medica-api-dth7b4c7auencgbg.brazilsouth-01.azurewebsites.net/api/ClinicaMedica/medicos/${id}/apagar`, {
            method: 'DELETE'
        });

        if (!response.ok) {
            const erroDetalhado = await response.json();
            alert('Erro ao excluir médico: ' + JSON.stringify(erroDetalhado));
            throw new Error(`Erro ${response.status}`);
        }

        linhaTabela.remove();
        alert('Médico removido com sucesso.');

    } catch (erro) {
        console.error('Erro ao remover médico:', erro);
        alert('Falha ao excluir médico. Tente novamente.');
    }
}

