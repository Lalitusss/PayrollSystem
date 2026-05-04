namespace Payroll.Core.DTOs;

public class ProvinciaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    // Lo mapeamos como string para evitar problemas de formato en el front
    public string CodigoAfip { get; set; } = string.Empty;

    // El PaisId es útil si luego quieres filtrar provincias por país
    public int PaisId { get; set; }
}