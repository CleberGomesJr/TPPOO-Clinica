using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedicaAPI;

public class ConsultaDTO
{
    public int Crm { get; set; }
    public int numeroCarteirinha { get; set; }
    
    public DateTime DataConsulta { get; set; }
    public int statusConsultaId { get; set; }
    
    public string  descricao { get; set; }
}
