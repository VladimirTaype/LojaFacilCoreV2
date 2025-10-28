using LojaFacilCoreV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaFacilCoreV2.Controllers
{
    public class ProdutosController : BaseController
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produtos.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produtos == null)
                return NotFound();

            var produto = await _context.Produtos.FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
                return NotFound();

            return View(produto);
        }

        // GET: Produtos/Create
        [AutorizacaoGerente]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutorizacaoGerente]
        public async Task<IActionResult> Create([Bind("Id,Nome,Categoria,Preco,Estoque")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Edit/5
        [AutorizacaoGerente]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtos == null)
                return NotFound();

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                return NotFound();

            return View(produto);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutorizacaoGerente]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Categoria,Preco,Estoque")] Produto produto)
        {
            if (id != produto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var produtoExistente = await _context.Produtos.FindAsync(id);
                    if (produtoExistente == null)
                        return NotFound();

                    // Atualiza apenas os campos permitidos
                    produtoExistente.Nome = produto.Nome;
                    produtoExistente.Categoria = produto.Categoria;
                    produtoExistente.Preco = produto.Preco;
                    produtoExistente.Estoque = produto.Estoque;

                    _context.Update(produtoExistente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(produto);
        }

        // GET: Produtos/Delete/5
        [AutorizacaoGerente]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtos == null)
                return NotFound();

            var produto = await _context.Produtos.FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
                return NotFound();

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AutorizacaoGerente]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produtos == null)
                return Problem("Entity set 'AppDbContext.Produtos' is null.");

            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
                _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
