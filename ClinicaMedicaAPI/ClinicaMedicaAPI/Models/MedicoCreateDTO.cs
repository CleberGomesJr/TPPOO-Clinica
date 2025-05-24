using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedicaAPI;

public class MedicoCreateDTO:Pessoa
{
    public int Id { get; set; }
    public int crm { get; set; }
    public string especialidade { get; set; }
    
}