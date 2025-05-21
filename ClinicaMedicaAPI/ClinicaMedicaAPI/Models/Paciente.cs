using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedicaAPI;

[Table("Paciente")]
public class Paciente:Pessoa
{
    [Column("Id")]
    public int Id { get; set; }
    
    [Required]
    [Column("numeroCarteirinha")]
    public int numeroCarteirinha { get; set; }

    [ForeignKey("Id")] 
    public Pessoa Pessoa { get; set; } = null;
}