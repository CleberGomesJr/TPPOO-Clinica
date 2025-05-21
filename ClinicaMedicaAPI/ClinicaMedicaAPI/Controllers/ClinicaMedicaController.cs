using ClinicaMedicaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaMedicaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ClinicaMedicaController : ControllerBase
{
    private readonly MedicoService  _medicoService;

    public ClinicaMedicaController(MedicoService medicoService)
    {
        _medicoService = medicoService;
    }

    //VERIFICAR A VELOCIDADE DE RESPOSTA DESSE MÉTODO, ESTÁ 5X MAIS PESADO QUE O ESPERADO
    [HttpGet("crm/consultas")]
    public async Task<IActionResult> GetConsultas(int crm)
    {
        //vai sofrer alterações
        var consultas = await _medicoService.ListarConsultasPorCrm(crm);
        
        if(!consultas.Any())
            return  NotFound("Nenhuma consulta encontrada pára esse CRM");
        
        return Ok(consultas);
    }

    [HttpPost("consultas/cadastrar")]
    public IActionResult CadastrarConsulta([FromBody] ConsultaCreateDTO consulta)
    {
        try
        {
            _medicoService.MarcarConsulta(consulta);
            return Ok("Consulta cadastrado com sucesso");
        }
        catch (Exception ex)
        {
            return BadRequest("Erro ao cadastrar consulta");
        }
    }
}



