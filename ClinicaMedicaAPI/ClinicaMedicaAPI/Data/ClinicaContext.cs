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
            .HasOne(m => m.Pessoa)
            .WithOne()
            .HasForeignKey<Pessoa>(m => m.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Paciente>().ToTable("Paciente")
            .HasOne(p => p.Pessoa)
            .WithOne()
            .HasForeignKey<Paciente>(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Consulta>().ToTable("Consulta")
            .HasOne(c => c.Paciente)
            .WithMany()
            .HasForeignKey(c => c.PacienteId);
        modelBuilder.Entity<StatusConsulta>().ToTable("statusConsulta")
            .HasKey(sc =>sc.Id);
    }
}
