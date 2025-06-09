using ClinicaMedicaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaMedicaAPI.Services;

//Classe que armazena os métodos do paciente
public class PacienteService
{
    
    private readonly ClinicaContext _context;

    public PacienteService(ClinicaContext context)
    {
        _context = context;
    }
    
    public async Task Cadastrar(PacienteCreateDTO paciente)
    {
        var cpfExistente = await _context.Pacientes.AnyAsync(p => p.cpf == paciente.cpf);

        if (cpfExistente)
            throw new Exception("Esse CPF já está cadastrado para outro usuário, tente novamente.");
        
        var novoPaciente = new Paciente
        {
            dataNascimento = paciente.dataNascimento,
            nome = paciente.nome,
            cpf = paciente.cpf,
            numero = paciente.numero,
            email = paciente.email,
            sexo = paciente.sexo,
            numeroCarteirinha = paciente.numeroCarteirinha
        };

        _context.Pacientes.Add(novoPaciente);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<PacienteDTO>> Listar()
    {
        return await _context.Pacientes.Select(p => new PacienteDTO
        {
            nome = p.nome,
            numeroCarteirinha = p.numeroCarteirinha,
            cpf = p.cpf,
            
        }).ToListAsync();
    }

    public async Task<List<ConsultaDTO>> ListarConsultaPacientePorNumeroCarteirinha(int numeroCarteirinha)
    {
        var consultas = await _context.Consultas
            .Include(c => c.StatusConsulta)
            .Where(c => c.numeroCarteirinha == numeroCarteirinha)
            .Select(c => new ConsultaDTO
            {
                Crm = c.Crm,
                DataConsulta = c.DataConsulta,
                descricao = c.descricao,
                statusConsultaId = c.StatusConsultaId,
                numeroCarteirinha = c.numeroCarteirinha
            })
            .ToListAsync();

        return consultas;
    }
}