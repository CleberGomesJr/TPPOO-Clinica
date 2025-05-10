namespace ClinicaMedicaAPI;

public class Consulta
{
    public int Id { get; set; }

    // FK para Médico
    public int MedicoId { get; set; }
    public Medico Medico { get; set; }

    // FK para Paciente
    public int PacienteId { get; set; }
    public Paciente Paciente { get; set; }

    public DateTime DataConsulta { get; set; }
    public int StatusConsultaId { get; set; }
    public StatusConsulta StatusConsulta { get; set; }
}
