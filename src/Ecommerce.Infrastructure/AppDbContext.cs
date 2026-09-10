using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure;

// O AppDbContext herda de DbContext do Entity Framework
public class AppDbContext : DbContext
{
    // Construtor que recebe as opcoes (como a string de conexao) e passa para a classe base
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Representa a tabela de Produtos no banco de dados
    public DbSet<Produto> Produtos { get; set; }

    // Metodo chamado quando o modelo de banco de dados esta sendo criado
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuracao da tabela Produto (Mapeamento via Fluent API)
        modelBuilder.Entity<Produto>(builder =>
        {
            // Define o Id como Chave Primaria
            builder.HasKey(p => p.Id);

            // Define regras para o campo Nome
            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Define regras para a Descricao
            builder.Property(p => p.Descricao)
                .HasMaxLength(500);

            // Define regras e precisao matematica para o Preco (18 digitos, 2 decimais)
            builder.Property(p => p.Preco)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(p => p.Estoque)
                .IsRequired();
        });
    }
}