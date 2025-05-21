using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedicaAPI;

public class ConsultaCreateDTO
{
    public int Crm { get; set; }
    public int numeroCarteirinha { get; set; }
    public DateTime DataConsulta { get; set; }
    public int StatusConsultaId { get; set; }
    
    public string descricao  { get; set; }
}