using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaMedicaAPI;

public class Pessoa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    
    [NotMapped]
    public int Idade
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