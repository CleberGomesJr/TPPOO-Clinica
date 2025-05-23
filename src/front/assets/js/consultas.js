document.getElementById('form-consulta').addEventListener('submit', function (event) {
    event.preventDefault();

    const nome = document.getElementById('nome').value.trim();
    const especialidade = document.getElementById('especialidade').value;
    const data = document.getElementById('data').value;
    const hora = document.getElementById('hora').value;

    const hoje = new Date().toISOString().split('T')[0];
    if (data < hoje) {
        alert('A data da consulta não pode ser anterior à data atual.');
        return;
    }

    if (hora < '08:00' || hora > '18:00') {
        alert('Horário inválido. As consultas são permitidas entre 08:00 e 18:00.');
        return;
    }

    alert(`Consulta agendada com sucesso para ${nome}, em ${data} às ${hora}, na especialidade ${especialidade}.`);
    this.reset();
});