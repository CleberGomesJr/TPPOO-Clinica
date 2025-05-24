using Microsoft.EntityFrameworkCore;

namespace ClinicaMedicaAPI.Data;

public class ClinicaContext : DbContext
{
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }

    public DbSet<Consulta> Consultas { get; set; }
    public DbSet<StatusConsulta> StatusConsultas { get; set; }

    public ClinicaContext(DbContextOptions<ClinicaContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>().ToTable("Pessoa");

        modelBuilder.Entity<Medico>().ToTable("Medico")
            .HasIndex(m => m.cpf)
            .IsUnique();
        
        modelBuilder.Entity<Paciente>().ToTable("Paciente")
            .HasIndex(p => p.cpf)
            .IsUnique();

        modelBuilder.Entity<Consulta>().ToTable("Consulta")
            .HasOne(c => c.Paciente)
            .WithMany()
            .HasForeignKey(c => c.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Consulta>()
            .HasOne(c => c.Medico)
            .WithMany()
            .HasForeignKey(c => c.MedicoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StatusConsulta>().ToTable("statusConsulta")
            .HasKey(sc => sc.Id);
    }

}
