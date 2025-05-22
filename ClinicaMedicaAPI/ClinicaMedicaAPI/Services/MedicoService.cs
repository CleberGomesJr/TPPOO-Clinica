using ClinicaMedicaAPI.Data;
using Microsoft.EntityFrameworkCore;


namespace ClinicaMedicaAPI.Services;

//Classe que armazena os métodos do médico
public class MedicoService
{
    private readonly ClinicaContext _context;

    public MedicoService(ClinicaContext context)
    {
        _context = context;
    }

    public async Task Cadastrar(MedicoCreateDTO medico)
    {
        var novoMedico = new Medico
        {
            dataNascimento = medico.dataNascimento,
            nome = medico.nome,
            cpf = medico.cpf,
            numero = medico.numero,
            email = medico.email,
            sexo = medico.sexo,
            Crm = medico.crm,
            especialidade = medico.especialidade
        };

        _context.Medicos.Add(novoMedico);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<MedicoDTO>> Listar()
    {
        return await _context.Medicos.Select(m => new MedicoDTO
        {
            Crm = m.Crm,
            especialidade = m.especialidade,
            Nome = m.nome
        }).ToListAsync();
    }
    
    public async Task<List<ConsultaDTO>> ListarConsultasPorCrm(int crm)
    {
        var consultas = await _context.Consultas
            .Include(c => c.StatusConsulta)
            .Where(c => c.Crm == crm)
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

    public async Task<bool> CancelarConsulta(int id)
    {
        var consulta = await _context.Consultas.FindAsync(id);
        try
        {
            _context.Consultas.Remove(consulta ?? throw new NullReferenceException());
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            Console.WriteLine("Não foi possível cancelar a consulta");
            return false;
        }
    }

    public async Task<bool> ConfirmarConsulta(int id)
    {
       var  consulta = await _context.Consultas.FindAsync(id);
       if(consulta == null) return false;

       consulta.StatusConsultaId = 2;
       _context.Consultas.Update(consulta);
       await _context.SaveChangesAsync();
       return true;
    }
    
    public async Task<(bool Sucesso, string Mensagem)> MarcarConsulta(ConsultaCreateDTO dto)
    {
        var consultasNoDia = await _context.Consultas
            .Where(c => c.Crm == dto.Crm && c.DataConsulta.Date == dto.DataConsulta.Date)
            .CountAsync();

        if (consultasNoDia >= 10)
            return (false, "Limite de consultas por dia atingido para este médico.");

        var consulta = new Consulta
        {
            MedicoId = dto.medicoId,
            PacienteId = dto.pacienteId,
            Crm = dto.Crm,
            numeroCarteirinha = dto.numeroCarteirinha,
            DataConsulta = dto.DataConsulta,
            StatusConsultaId = dto.StatusConsultaId, 
            descricao = dto.descricao
        };

        _context.Consultas.Add(consulta); 
        await _context.SaveChangesAsync();

        return (true, "Consulta criada.");
    }
}
