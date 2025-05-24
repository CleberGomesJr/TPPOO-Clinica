using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedicaAPI;

public class PacienteCreateDTO:Paciente
{
    public int Id { get; set; }
    public int numeroCarteirinha { get; set; }
    
}