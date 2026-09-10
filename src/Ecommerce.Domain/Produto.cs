namespace Ecommerce.Domain;

// Entidade principal representando um produto no sistema
public class Produto
{
    // Identificador unico do produto
    public Guid Id { get; private set; }

    // Nome comercial do produto
    public string Nome { get; private set; } = string.Empty;

    // Descricao detalhada do produto
    public string Descricao { get; private set; } = string.Empty;

    // Preco de venda
    public decimal Preco { get; private set; }

    // Quantidade disponivel no estoque
    public int Estoque { get; private set; }

    // Data de cadastro no sistema
    public DateTime DataCriacao { get; private set; }

    // Construtor privado exigido por frameworks de ORM
    private Produto() { }

    // Construtor principal para criacao de um novo produto com validacoes simples
    public Produto(string nome, string descricao, decimal preco, int estoque)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do produto nao pode ser vazio.");

        if (preco <= 0)
            throw new ArgumentException("O preco deve ser maior que zero.");

        if (estoque < 0)
            throw new ArgumentException("O estoque nao pode ser negativo.");

        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Estoque = estoque;
        DataCriacao = DateTime.UtcNow;
    }

    // Metodo para atualizar o estoque de forma controlada
    public void AtualizarEstoque(int quantidade)
    {
        if (Estoque + quantidade < 0)
            throw new InvalidOperationException("Quantidade em estoque insuficiente.");

        Estoque += quantidade;
    }
}