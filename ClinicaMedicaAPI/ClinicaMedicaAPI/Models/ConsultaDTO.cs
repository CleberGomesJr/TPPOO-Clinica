using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedicaAPI;

// Molde para o programa trazer os dados do banco de dados, esses são os dados consultados

public class ConsultaDTO
{
    public int Id { get; set; }
    public int Crm { get; set; }
    public int numeroCarteirinha { get; set; }
    
    public DateTime DataConsulta { get; set; }
    public int statusConsultaId { get; set; }
    
    public string  descricao { get; set; }
}
