const historicoAtendimentos = [
    {
        paciente: "Cleber Gomes",
        especialidade: "Cardiologia",
        data: "2025-05-10",
        hora: "09:30"
    },
    {
        paciente: "Ana Souza",
        especialidade: "Dermatologia",
        data: "2025-05-15",
        hora: "14:00"
    },
    {
        paciente: "Carlos Lima",
        especialidade: "Ortopedia",
        data: "2025-05-17",
        hora: "11:15"
    }
];

const corpoTabela = document.querySelector('#tabela-historico tbody');

historicoAtendimentos.forEach(atendimento => {
    const linha = document.createElement('tr');

    linha.innerHTML = `
        <td>${atendimento.paciente}</td>
        <td>${atendimento.especialidade}</td>
        <td>${atendimento.data}</td>
        <td>${atendimento.hora}</td>
      `;

    corpoTabela.appendChild(linha);
});

fetch('components/header.html')
            .then(res => res.text())
            .then(data => document.getElementById('header').innerHTML = data);

        fetch('components/footer.html')
            .then(res => res.text())
            .then(data => document.getElementById('footer').innerHTML = data);