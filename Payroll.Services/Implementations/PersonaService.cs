using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.DTOs;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class PersonaService 
    : GenericService<Persona>, IPersonaService
{
    public PersonaService(PayrollDbContext context) 
        : base(context)
    {
    }
}