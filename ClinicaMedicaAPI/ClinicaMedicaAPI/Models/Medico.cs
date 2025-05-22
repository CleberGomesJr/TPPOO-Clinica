using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedicaAPI;

[Table("Medico")]
public class Medico:Pessoa
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    
    [Column("crm")]
    public int Crm { get; set; }
    
    [Column("especialidade")]
    public string especialidade { get; set; }
}