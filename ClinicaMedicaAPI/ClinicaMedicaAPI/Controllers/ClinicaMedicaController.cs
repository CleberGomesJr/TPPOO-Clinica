using ClinicaMedicaAPI.Services;
using Microsoft.AspNetCore.Mvc;

/*
 
 Essa classe aqui é o controller da API, ou seja, ela que cria e envia os métodos http para a API, tudo o que possamos
 acessar dentro da API está aqui.
    Dentro do conceito de métodos HTTP, temos vários (pesquisa dps), mas aqui os mais usados e mais importantes são
    os métodos GET, POST E Delete.
    GET - Usado para obter dados do banco de dados
    POST - Usado para escrever/modificar/criar coisas dentro do banco de dados
    DELETE - Usado para apagar dados do banco de dados.
    Dentro dos métodos HTTP, temos os códigos de resposta, sendo o código 200 o mais importante, visto que ele é o 
    código de sucesso, além dele temos o código 500, que normalmente se refere a algum erro e o método 400 que mostra
    que o que foi requerido não foi encontrado. Tem mais métodos também, mas normalmente esses são os que mais aparecem.
    
    Dito isso, os métodos da classe controller são todos de retorno IActionResult, que é uma interface pronta da 
    aplicação que tem como retorno esses códigos anteriormente citados, agora o que cada método dentro do método faz 
    fui eu que criei nas classes medicoService e pacienteService, como, por exemplo, os métodos de cadastrar,listar,etc.
    
 */


namespace ClinicaMedicaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClinicaMedicaController : ControllerBase
{
    private readonly MedicoService _medicoService;
    private readonly PacienteService _pacienteService;

    public ClinicaMedicaController(MedicoService medicoService, PacienteService pacienteService)
    {
        _medicoService = medicoService;
        _pacienteService = pacienteService;
    }

    // MÉTODOS PARA OS MÉDICOS

    /*Método utilizado para cadastrar médicos no banco de dados, esse FromBody mostra pra api que os dados que serão
     enviados para o banco serão recebidos do corpo da requisição, ou seja, do front-end*/ 
    [HttpPost("medicos")]
    public async Task<IActionResult> CadastrarMedico([FromBody] MedicoCreateDTO medico)
    {
        try
        {
            await _medicoService.Cadastrar(medico);
            return Ok("Médico cadastrado com sucesso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao cadastrar médico: {ex.Message}");
        }
    }
    //Método de listar todos os médicos cadastrados no banco de dados.
    [HttpGet("medicos")]
    public async Task<IActionResult> ListarMedicos()
    {
        try
        {
            var medicos = await _medicoService.Listar();
            return Ok(medicos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Não foi possível listar os médicos");
        }
    }
    //Método para buscar as consultas de acordo com o número do Crm do médico.
    [HttpGet("medicos/{crm}/consultas")]
    public async Task<IActionResult> GetConsultasPorCrm(int crm)
    {
        try
        {
            var consultas = await _medicoService.ListarConsultasPorCrm(crm);

            if (!consultas.Any())
                return NotFound("Nenhuma consulta encontrada para esse CRM");

            return Ok(consultas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar consultas: {ex.Message}");
        }
    }

    // MÉTODOS PARA CONSULTAS
    //Método para criar consultas, segue a mesma lógica do método de criar médicos.
    [HttpPost("consultas")]
    public async Task<IActionResult> CadastrarConsulta([FromBody] ConsultaCreateDTO consulta)
    {
        try
        {
            var (sucesso, mensagem) = await _medicoService.MarcarConsulta(consulta);

            if (!sucesso)
                return BadRequest(mensagem);

            return Ok(mensagem);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao cadastrar consulta: {ex.Message}");
        }
    }
    //Método feito pra confirmar consulta, basicamente só altera o status da consulta de 1 pra 2 
    [HttpPost("consultas/{id}/confirmar")]
    public async Task<IActionResult> ConfirmarConsulta(int id)
    {
        try
        {
            await _medicoService.ConfirmarConsulta(id);
            return Ok("Consulta confirmada com sucesso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao confirmar consulta: {ex.Message}");
        }
    }
    //Método para listar todas as consultas do banco de dados
    [HttpPost("consultas/listarConsultas")]
    public async Task<IActionResult> ListarConsultas()
    {
        try
        {
            var consultas = await _medicoService.ListarConsultas();
            return Ok(consultas);
        }
        catch (Exception ex)
        {
            return StatusCode(500,$"Erro ao listar consultas: {ex.Message}");
        }
    }
    //Métodos para obter os dados da consulta desejada, precisa do número da consulta para trazer os dados
    [HttpGet("consultas/{id}/dadosConsulta")]
    public async Task<IActionResult> GetDadosConsulta(int id)
    {
        try
        {
            var consultas = await _medicoService.ObterDadosDaConsulta(id);
            return Ok(consultas);
        }
        catch (Exception ex)
        {
            return StatusCode(500,$"Erro ao mostrar os dados da consulta: {ex.Message}");
        }
    }
    //Método para apagar a consulta do banco de dados
    [HttpDelete("consultas/{id}")]
    public async Task<IActionResult> CancelarConsulta(int id)
    {
        try
        {
            await _medicoService.CancelarConsulta(id);
            return Ok("Consulta cancelada com sucesso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao cancelar consulta: {ex.Message}");
        }
    }

    // MÉTODOS PARA OS PACIENTES
    
    //Método para cadastrar paciente, segue a mesma lógica dos outros métodos de cadastro. 
    [HttpPost("pacientes")]
    public async Task<IActionResult> CadastrarPaciente([FromBody] PacienteCreateDTO paciente)
    {
        try
        {
            await _pacienteService.Cadastrar(paciente);
            return Ok("Paciente cadastrado com sucesso");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao cadastrar paciente: {ex.Message}");
        }
    }
    // Método para listar todos os pacientes do banco de dados
    [HttpGet("pacientes")]
    public async Task<IActionResult> ListarPacientes()
    {
        try
        {
            var pacientes = await _pacienteService.Listar();
            return Ok(pacientes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Não foi possível listar os pacientes: {ex.Message}");
        }
    }
    // Método para listar as consultas de acordo com o número da carteirinha do cliente.
    [HttpGet("pacientes/{numeroCarteirinha}/consultas")]
    public async Task<IActionResult> ListarConsultasPaciente(int numeroCarteirinha)
    {
        try
        {
            var consultas = await _pacienteService.ListarConsultaPacientePorNumeroCarteirinha(numeroCarteirinha);

            if (!consultas.Any())
                return NotFound("Nenhuma consulta encontrada para este paciente");

            return Ok(consultas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao listar consultas do paciente: {ex.Message}");
        }
    }
}




