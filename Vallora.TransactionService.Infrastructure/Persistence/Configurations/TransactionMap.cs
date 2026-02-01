#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionService.Domain.Aggregates.Transaction;
using TransactionService.Domain.ValueObjects;

#endregion

namespace TransactionService.Infrastructure.Persistence.Configurations;

public sealed class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        // ======================
        // Primary Key (Value Object)
        // ======================
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new TransactionId(value)
            )
            .ValueGeneratedNever()
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // ======================
        // PortfolioId (Value Object)
        // ======================
        builder.Property(t => t.PortfolioId)
            .HasColumnName("portfolio_id")
            .HasConversion(
                id => id.Value,
                value => new PortfolioId(value)
            )
            .IsRequired();

        // ======================
        // Primitive properties
        // ======================
        builder.Property(t => t.Ticker)
            .HasColumnName("ticker")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(t => t.Quantity)
            .HasColumnName("quantity")
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(t => t.Price)
            .HasColumnName("price")
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(t => t.CompletedAt)
            .HasColumnName("completed_at");

        builder.Property(t => t.CancelledAt)
            .HasColumnName("cancelled_at");

        // ======================
        // Enums
        // ======================
        builder.Property(t => t.Type)
            .HasColumnName("type")
            .HasConversion<short>()
            .IsRequired();

        builder.Property(t => t.Status)
            .HasColumnName("status")
            .HasConversion<short>()
            .IsRequired();

        // ======================
        // Ignore computed properties
        // ======================
        builder.Ignore(t => t.TotalAmount);

        //TODO: Criar esses índices futuramente?
        // builder.HasIndex(t => t.PortfolioId);
        // builder.HasIndex(t => t.CreatedAt);
    }
}