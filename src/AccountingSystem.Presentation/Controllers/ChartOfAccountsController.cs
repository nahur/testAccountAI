using Microsoft.AspNetCore.Mvc;
using AccountingSystem.Application.DTOs;
using AccountingSystem.Application.Services;

namespace AccountingSystem.Presentation.Controllers
{
    public class ChartOfAccountsController : Controller
    {
        private readonly IChartOfAccountService _service;
        private readonly ILogger<ChartOfAccountsController> _logger;

        public ChartOfAccountsController(IChartOfAccountService service, ILogger<ChartOfAccountsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: ChartOfAccounts
        public async Task<IActionResult> Index()
        {
            try
            {
                var accounts = await _service.GetAllAccountsAsync();
                return View(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving accounts");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: ChartOfAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var account = await _service.GetAccountByIdAsync(id.Value);
                if (account == null)
                    return NotFound();

                return View(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving account details");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: ChartOfAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChartOfAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountCode,AccountName,AccountType,ParentAccountId")] ChartOfAccountDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.CreateAccountAsync(dto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating account");
                    ModelState.AddModelError("", "Error creating account");
                }
            }
            return View(dto);
        }

        // GET: ChartOfAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var account = await _service.GetAccountByIdAsync(id.Value);
                if (account == null)
                    return NotFound();

                return View(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving account for edit");
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: ChartOfAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountCode,AccountName,AccountType,IsActive,ParentAccountId")] ChartOfAccountDTO dto)
        {
            if (id != dto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAccountAsync(id, dto);
                    return RedirectToAction(nameof(Index));
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating account");
                    ModelState.AddModelError("", "Error updating account");
                }
            }
            return View(dto);
        }

        // GET: ChartOfAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var account = await _service.GetAccountByIdAsync(id.Value);
                if (account == null)
                    return NotFound();

                return View(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving account for deletion");
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: ChartOfAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _service.DeleteAccountAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting account");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}