using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.Core.Entities;

namespace Payroll.Data.Configurations;

public class ConvenioConfiguration : IEntityTypeConfiguration<Convenio>
{
    public void Configure(EntityTypeBuilder<Convenio> builder)
    {
        // Nombre de la tabla
        builder.ToTable("Convenios");

        // Llave primaria
        builder.HasKey(c => c.Id);

        // Índice único para el Numero (como vimos en tu SQL)
        builder.HasIndex(c => c.Numero)
               .IsUnique()
               .HasDatabaseName("UQ_Convenios_Numero");
    }
}