using Microsoft.AspNetCore.Mvc;
using AccountingSystem.Application.Services;

namespace AccountingSystem.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChartOfAccountService _accountService;

        public HomeController(ILogger<HomeController> logger, IChartOfAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return View(accounts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}