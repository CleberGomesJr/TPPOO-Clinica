using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedicaAPI;

public class Pessoa
{
    
    public string nome { get; set; }
    public string sexo { get; set; }
    public string cpf { get; set; }
    DateTime dataNascimento { get; set; }
    public string email { get; set; }
    public string numero { get; set; }
    public string? telefoneFixo { get; set; }
    
    [NotMapped]
    public int idade
    {
        get
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;

            if (dataNascimento.Date > hoje.AddYears(-idade))
                idade--;
      
            return idade;
        }
    }
}