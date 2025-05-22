using ClinicaMedicaAPI.Services;
using Microsoft.AspNetCore.Mvc;

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

    // MÉDICOS

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

    // CONSULTAS

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

    // PACIENTES

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




