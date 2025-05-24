using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedicaAPI;

// Classe com os atributos necessários para criar uma consulta, não possui dados por mera convenção de uso

public class ConsultaCreateDTO
{
    public int medicoId  { get; set; }
    public int pacienteId  { get; set; }
    public int Crm { get; set; }
    public int numeroCarteirinha { get; set; }
    public DateTime DataConsulta { get; set; }
    public int StatusConsultaId { get; set; }
    public string descricao  { get; set; }
}