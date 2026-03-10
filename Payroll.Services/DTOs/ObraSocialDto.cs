namespace Payroll.Services.DTOs;

public class ObraSocialDto
{
    public int Id { get; set; }
    public string CodigoARCA { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Sigla { get; set; } = string.Empty;
}