using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedicaAPI;

[Table("statusConsulta")]
public class StatusConsulta
{
    [Key]
    [Column("numeroProtocolo")]
    public int Id { get; set; }
    
    [Column("descricao")]
    public string descricao { get; set; }
}