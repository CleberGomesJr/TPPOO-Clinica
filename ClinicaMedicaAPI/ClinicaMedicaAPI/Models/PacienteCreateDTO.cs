using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedicaAPI;

public class PacienteCreateDTO
{
    public int Id { get; set; }
    
    [Column("nomeCompleto")]
    public string nome { get; set; }
    
    [Column("sexo")]
    public string sexo { get; set; }
    
    [Column("cpf")]
    public string cpf { get; set; }
    
    [Column("dataNascimento")]
    public DateTime dataNascimento { get; set; }
    
    [Column("email")]
    public string email { get; set; }
    
    [Column("telefone")]
    public string numero { get; set; }
    
    [Column("numeroCarteirinha")]
    public int numeroCarteirinha { get; set; }
    
}