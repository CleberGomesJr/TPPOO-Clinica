document.getElementById('form-consulta').addEventListener('submit', function (event) {
    event.preventDefault();

    const nome = document.getElementById('nome').value.trim();
    const especialidade = document.getElementById('especialidade').value;
    const data = document.getElementById('data').value;
    const hora = document.getElementById('hora').value;

});

fetch('components/header.html')
            .then(res => res.text())
            .then(data => document.getElementById('header').innerHTML = data);

        fetch('components/footer.html')
            .then(res => res.text())
            .then(data => document.getElementById('footer').innerHTML = data);