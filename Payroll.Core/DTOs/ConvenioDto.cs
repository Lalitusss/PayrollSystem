namespace Payroll.Core.DTOs;
public class ConvenioDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
}