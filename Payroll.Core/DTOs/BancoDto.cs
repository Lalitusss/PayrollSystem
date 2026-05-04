namespace Payroll.Core.DTOs;
public class BancoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Codigo { get; set; } // Por si necesitas el código de entidad bancaria
}

