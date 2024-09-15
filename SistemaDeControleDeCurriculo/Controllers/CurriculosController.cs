using Microsoft.AspNetCore.Mvc;
using CurriculoMVC.Data;
using CurriculoMVC.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CurriculoMVC.Controllers
{
    public class CurriculosController : Controller
    {
        private readonly CurriculoContext _context;
        private readonly ILogger<CurriculosController> _logger;

        public CurriculosController(CurriculoContext context, ILogger<CurriculosController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Método Index chamado");
            try
            {
                var curriculos = await _context.Curriculos.ToListAsync();
                _logger.LogInformation($"Número de currículos recuperados: {curriculos.Count}");
                return View(curriculos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar currículos");
                return View(new List<Curriculo>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,CPF,Endereco,Telefone,Email,PretensaoSalarial,CargoPretendido,FormacaoAcademica,ExperienciasProfissionais,Idiomas")] Curriculo curriculo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(curriculo);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Currículo criado com sucesso. ID: {curriculo.Id}");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao criar currículo");
                    ModelState.AddModelError("", "Ocorreu um erro ao salvar o currículo. Por favor, tente novamente.");
                }
            }
            return View(curriculo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Tentativa de editar currículo com ID nulo");
                return NotFound();
            }

            var curriculo = await _context.Curriculos.FindAsync(id);
            if (curriculo == null)
            {
                _logger.LogWarning($"Currículo não encontrado para edição. ID: {id}");
                return NotFound();
            }
            return View(curriculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CPF,Endereco,Telefone,Email,PretensaoSalarial,CargoPretendido,FormacaoAcademica,ExperienciasProfissionais,Idiomas")] Curriculo curriculo)
        {
            if (id != curriculo.Id)
            {
                _logger.LogWarning($"ID fornecido ({id}) não corresponde ao ID do currículo ({curriculo.Id})");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curriculo);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Currículo atualizado com sucesso. ID: {curriculo.Id}");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!CurriculoExists(curriculo.Id))
                    {
                        _logger.LogWarning($"Tentativa de atualizar currículo inexistente. ID: {curriculo.Id}");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(ex, $"Erro de concorrência ao atualizar currículo. ID: {curriculo.Id}");
                        throw;
                    }
                }
            }
            return View(curriculo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Tentativa de excluir currículo com ID nulo");
                return NotFound();
            }

            var curriculo = await _context.Curriculos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curriculo == null)
            {
                _logger.LogWarning($"Currículo não encontrado para exclusão. ID: {id}");
                return NotFound();
            }

            return View(curriculo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curriculo = await _context.Curriculos.FindAsync(id);
            if (curriculo != null)
            {
                _context.Curriculos.Remove(curriculo);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Currículo excluído com sucesso. ID: {id}");
            }
            else
            {
                _logger.LogWarning($"Tentativa de excluir currículo inexistente. ID: {id}");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CurriculoExists(int id)
        {
            return _context.Curriculos.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Tentativa de visualizar detalhes de currículo com ID nulo");
                return NotFound();
            }

            var curriculo = await _context.Curriculos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curriculo == null)
            {
                _logger.LogWarning($"Currículo não encontrado para visualização de detalhes. ID: {id}");
                return NotFound();
            }

            return View(curriculo);
        }
    }
}