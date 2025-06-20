fetch('../components/header.html')
    .then(res => res.text())
    .then(data => document.getElementById('header').innerHTML = data);

fetch('../components/footer.html')
    .then(res => res.text())
    .then(data => document.getElementById('footer').innerHTML = data);


document.addEventListener('DOMContentLoaded', () => {
    const API_BASE = 'https://clinica-medica-api-dth7b4c7auencgbg.brazilsouth-01.azurewebsites.net/api/ClinicaMedica/consultas';

    // Carregar histórico de consultas
    async function carregarHistorico() {
        try {
            const response = await fetch(`${API_BASE}/listarConsultas`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({})
            });

            if (!response.ok) {
                alert('Erro ao carregar o histórico de consultas.');
                return;
            }

            const consultas = await response.json();
            const tabela = document.getElementById('tabela-historico');
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
                `;

                tabela.appendChild(tr);
            });

        } catch (error) {
            console.error('Erro ao carregar histórico:', error);
            alert('Erro inesperado ao carregar histórico.');
        }
    }

    carregarHistorico();

    // Filtro de pesquisa no histórico
    const campoPesquisa = document.getElementById('pesquisar-historico');
    if (campoPesquisa) {
        campoPesquisa.addEventListener('input', function () {
            const filtro = this.value.toLowerCase();
            const linhas = document.querySelectorAll('#tabela-historico tr');
            linhas.forEach(linha => {
                const texto = linha.innerText.toLowerCase();
                linha.style.display = texto.includes(filtro) ? '' : 'none';
            });
        });
    }
});
