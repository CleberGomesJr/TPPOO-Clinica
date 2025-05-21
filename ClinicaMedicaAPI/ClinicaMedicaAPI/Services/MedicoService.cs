using System.Diagnostics;
using ClinicaMedicaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Diagnostics.Metrics;

namespace ClinicaMedicaAPI.Services;

//Classe que armazena os métodos do médico
public class MedicoService
{
    private readonly ClinicaContext _context;

    public MedicoService(ClinicaContext context)
    {
        _context = context;
    }

    public bool Cadastrar (Medico medico)
    {
        try
        {
            _context.Medicos.Add(medico);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    
    /*public async Task<bool> Listar(MedicoDTO medicodto)
    {
        
    }*/
    
   /*private ConsultaDTO MapConsultaToDto(Consulta consulta)
    {
        return new ConsultaDTO
        {
            Crm = consulta.Medico.Crm,
            DataConsulta = consulta.DataConsulta,
            descricao = consulta.descricao,
            statusConsultaId = consulta.StatusConsultaId,
            numeroCarteirinha = consulta.Paciente.numeroCarteirinha
        };
    }*/
    
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

    
    public async Task<(bool Sucesso, string Mensagem)> MarcarConsulta(ConsultaCreateDTO dto)
    {
        var consultasNoDia = await _context.Consultas
            .Where(c => c.Crm == dto.Crm && c.DataConsulta.Date == dto.DataConsulta.Date)
            .CountAsync();

        if (consultasNoDia >= 10)
            return (false, "Limite de consultas por dia atingido para este médico.");

        var consulta = new Consulta
        {
            Crm = dto.Crm,
            numeroCarteirinha = dto.numeroCarteirinha,
            DataConsulta = dto.DataConsulta,
            StatusConsultaId = dto.StatusConsultaId, 
            descricao = dto.descricao
        };

        _context.Consultas.Add(consulta); 
        _context.SaveChanges();

        return (true, "Consulta criada.");
    }


}
