using Ecommerce.Domain;
using Ecommerce.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Controllers;

// DTO (Data Transfer Object) para receber os dados do frontend de forma segura
public record CriarProdutoRequest(string Nome, string Descricao, decimal Preco, int Estoque);

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    // Injecao de dependencia do nosso banco de dados
    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    // Rota GET: api/produtos
    // Retorna a lista de todos os produtos cadastrados
    [HttpGet]
    public async Task<IActionResult> GetProdutos()
    {
        var produtos = await _context.Produtos.ToListAsync();
        return Ok(produtos);
    }

    // Rota POST: api/produtos
    // Cadastra um novo produto no banco de dados
    [HttpPost]
    public async Task<IActionResult> CriarProduto([FromBody] CriarProdutoRequest request)
    {
        // Criando a entidade usando o construtor que possui nossas validacoes de negocio
        var produto = new Produto(request.Nome, request.Descricao, request.Preco, request.Estoque);

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return Ok(produto);
    }
}