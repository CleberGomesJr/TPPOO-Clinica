fetch('../components/header.html')
    .then(res => res.text())
    .then(data => document.getElementById('header').innerHTML = data);

fetch('../components/footer.html')
    .then(res => res.text())
    .then(data => document.getElementById('footer').innerHTML = data);

const modal = document.getElementById('modal-medico');
const btnAbrir = document.getElementById('adicionar-medico');
const btnCancelar = document.getElementById('cancelar-modal');
const form = document.getElementById('form-medico');
const tabela = document.getElementById('tabela-medicos');

// Exibe modal
btnAbrir.addEventListener('click', () => {
    modal.style.display = 'flex';
});

// Fecha modal
btnCancelar.addEventListener('click', () => {
    modal.style.display = 'none';
    form.reset();
});

// Adiciona novo médico à tabela (simulado)
// TEM QUE ALTERAR PARA IMPLEMENTAR A API
form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const medico = {
        nome: form.nome.value,
        sexo: form.sexo.value,
        cpf: form.cpf.value,
        dataNascimento: form.dataNascimento.value,
        email: form.email.value,
        numero: form.numero.value,
        crm: parseInt(form.crm.value),
        especialidade: form.especialidade.value
    };

    try {
        const response = await fetch('https://clinica-medica-api-dth7b4c7auencgbg.brazilsouth-01.azurewebsites.net/api/ClinicaMedica/medicos/cadastrar', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(medico)
        });

        if (!response.ok) {
            throw new Error(`Erro: ${response.status}`);
        }

        const novoMedico = await response.json();

        // Adiciona novo médico à tabela
        const novaLinha = document.createElement('tr');
        novaLinha.innerHTML = `
            <td>${novoMedico.nome}</td>
            <td>${novoMedico.sexo}</td>
            <td>${novoMedico.cpf}</td>
            <td>${new Date(novoMedico.dataNascimento).toLocaleDateString()}</td>
            <td>${novoMedico.email}</td>
            <td>${novoMedico.numero}</td>
            <td>${novoMedico.crm}</td>
            <td>${novoMedico.especialidade}</td>
            <td><button class="remover-btn">Remover</button></td>
        `;
        tabela.appendChild(novaLinha);

        // Evento de remover
        novaLinha.querySelector('.remover-btn').addEventListener('click', () => {
            novaLinha.remove();
            // Aqui pode adicionar o DELETE futuramente
        });

        modal.style.display = 'none';
        form.reset();

    } catch (erro) {
        console.error('Erro ao cadastrar médico:', erro);
        alert('Erro ao cadastrar médico. Verifique se a API está funcionando corretamente.');
    }
});



// Simular ação de remover médico
function ativarRemocao() {
    document.querySelectorAll('.remover-btn').forEach(button => {
        button.addEventListener('click', async function () {
            const id = this.getAttribute('data-id');
            if (!confirm('Tem certeza que deseja excluir este médico?')) return;

            try {
                const response = await fetch(`https://clinica-medica-api-dth7b4c7auencgbg.brazilsouth-01.azurewebsites.net/api/ClinicaMedica/medicos/${id}/apagar`, {
                    method: 'DELETE'
                });

                if (!response.ok) throw new Error(`Erro ${response.status}`);

                // Remove a linha da tabela
                this.closest('tr').remove();
            } catch (erro) {
                console.error('Erro ao remover médico:', erro);
                alert('Erro ao excluir médico.');
            }
        });
    });
}

// Filtro básico de pesquisa
document.getElementById('pesquisar-medico').addEventListener('input', function () {
    const filtro = this.value.toLowerCase();
    const linhas = document.querySelectorAll('tbody tr');
    linhas.forEach(linha => {
        const texto = linha.innerText.toLowerCase();
        linha.style.display = texto.includes(filtro) ? '' : 'none';
    });
});