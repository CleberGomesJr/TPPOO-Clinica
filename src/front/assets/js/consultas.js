document.getElementById('form-consulta').addEventListener('submit', function (event) {
    event.preventDefault();

    const nome = document.getElementById('nome').value.trim();
    const especialidade = document.getElementById('especialidade').value;
    const data = document.getElementById('data').value;
    const hora = document.getElementById('hora').value;

});
