using Microsoft.AspNetCore.Mvc;
using LojaFacilCoreV2.Models;
using Microsoft.EntityFrameworkCore;
using LojaFacilCoreV2.Filters; // 🔒 Importante para os filtros de autorização

namespace LojaFacilCoreV2.Controllers
{
    // 🔹 Exige login em TODAS as ações (qualquer usuário autenticado)
    [AutorizacaoUsuario]
    public class VendasController : Controller
    {
        private readonly AppDbContext _context;

        public VendasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Vendas
        // 🔹 Todos os usuários autenticados podem visualizar
        public async Task<IActionResult> Index()
        {
            var vendas = await _context.Vendas
                .Include(v => v.Itens)
                .ThenInclude(i => i.Produto)
                .ToListAsync();

            return View(vendas);
        }

        // GET: /Vendas/Create
        // 🔒 Apenas Gerente pode registrar novas vendas
        [AutorizacaoGerente]
        public async Task<IActionResult> Create()
        {
            ViewBag.Produtos = await _context.Produtos.ToListAsync();
            return View(new Venda());
        }

        // POST: /Vendas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AutorizacaoGerente]
        public async Task<IActionResult> Create(Venda venda)
        {
            if (venda == null || venda.Itens == null || !venda.Itens.Any())
            {
                ModelState.AddModelError("", "Adicione pelo menos um item à venda.");
                ViewBag.Produtos = await _context.Produtos.ToListAsync();
                return View(venda ?? new Venda());
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Produtos = await _context.Produtos.ToListAsync();
                return View(venda);
            }

            // 🔹 Corrige cálculo do total
            venda.Total = venda.Itens.Sum(i => Convert.ToDecimal(i.Quantidade) * Convert.ToDecimal(i.PrecoUnitario));

            // 🔹 Valida e atualiza estoque
            if (venda.Itens != null)
            {
                foreach (var item in venda.Itens)
                {
                    var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                    if (produto != null)
                    {
                        if (item.Quantidade > produto.Estoque)
                        {
                            TempData["MensagemErro"] = $"❌ O produto '{produto.Nome}' não possui estoque suficiente!";
                            ViewBag.Produtos = await _context.Produtos.ToListAsync();
                            return View(venda);
                        }

                        produto.Estoque -= item.Quantidade;
                        _context.Produtos.Update(produto);
                    }
                }
            }

            // 🔹 Salva a venda no banco
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();

            TempData["MensagemSucesso"] = "✅ Venda registrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Vendas/Details/5
        // 🔹 Qualquer usuário logado pode ver detalhes
        public async Task<IActionResult> Details(int id)
        {
            var venda = await _context.Vendas
                .Include(v => v.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venda == null)
                return NotFound();

            return View(venda);
        }

        // GET: /Vendas/Delete/5
        // 🔒 Apenas gerente pode excluir
        [AutorizacaoGerente]
        public async Task<IActionResult> Delete(int id)
        {
            var venda = await _context.Vendas
                .Include(v => v.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venda == null)
                return NotFound();

            return View(venda);
        }

        // POST: /Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AutorizacaoGerente]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venda = await _context.Vendas
                .Include(v => v.Itens)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venda != null)
            {
                // 🔹 Devolve o estoque antes de excluir
                foreach (var item in venda.Itens)
                {
                    var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                    if (produto != null)
                    {
                        produto.Estoque += item.Quantidade;
                        _context.Update(produto);
                    }
                }

                _context.ItensVenda.RemoveRange(venda.Itens);
                _context.Vendas.Remove(venda);
                await _context.SaveChangesAsync();
            }

            TempData["MensagemSucesso"] = "🗑️ Venda excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}
