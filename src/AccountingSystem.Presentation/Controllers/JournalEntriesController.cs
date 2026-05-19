using Microsoft.AspNetCore.Mvc;
using AccountingSystem.Application.DTOs;
using AccountingSystem.Application.Services;

namespace AccountingSystem.Presentation.Controllers
{
    public class JournalEntriesController : Controller
    {
        private readonly IJournalEntryService _service;
        private readonly ILogger<JournalEntriesController> _logger;

        public JournalEntriesController(IJournalEntryService service, ILogger<JournalEntriesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: JournalEntries
        public async Task<IActionResult> Index()
        {
            try
            {
                var entries = await _service.GetAllEntriesAsync();
                return View(entries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving journal entries");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: JournalEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var entry = await _service.GetEntryByIdAsync(id.Value);
                if (entry == null)
                    return NotFound();

                return View(entry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving journal entry");
                return NotFound();
            }
        }

        // GET: JournalEntries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JournalEntries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,EntryDate,DebitAmount,CreditAmount,Reference,ChartOfAccountId")] JournalEntryDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.CreateEntryAsync(dto);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating journal entry");
                    ModelState.AddModelError("", "Error creating entry");
                }
            }
            return View(dto);
        }
    }
}