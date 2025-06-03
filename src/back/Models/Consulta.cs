using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClinicaMedicaAPI;

/*
    Classe geral das consultas, traz todos os dados que estão presentes no banco de dados
 */

[Table("Consulta")]
public class Consulta
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("MedicoId")]
    public int MedicoId { get; set; }

    [Column("PacienteID")]
    public int PacienteId { get; set; }

    [Column("dataConsulta")]
    public DateTime DataConsulta { get; set; }

    [Column("statusConsulta")]
    public int StatusConsultaId { get; set; }

    [Column("crm")]
    public int Crm { get; set; }

    [Column("numeroCarteirinha")]
    public int numeroCarteirinha { get; set; }

    [Column("descricao")]
    public string descricao { get; set; }

    // Relações
    [ForeignKey("StatusConsultaId")]
    public StatusConsulta StatusConsulta { get; set; }

    [ForeignKey("PacienteId")]
    public Paciente Paciente { get; set; }

    [ForeignKey("MedicoId")]
    public Medico Medico { get; set; }
}


