using DChat.Data;
using DChat.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DChat.Controllers
{
	[Authorize]
    public class HomeController : Controller
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

		public async Task<IActionResult> Index()
		{
			var currentUser = await _userManager.GetUserAsync(User);

			if(User.Identity.IsAuthenticated)
			{
				ViewBag.CurrentUserName = currentUser.UserName;
			}
			

			var messages = await _context.Messages.ToListAsync();
			return View(messages);
		}



		[HttpPost]
		public async Task<IActionResult> Create(Message message)
		{
			if (ModelState.IsValid)
			{
				message.UserName = User.Identity.Name;
				var Sender = await _userManager.GetUserAsync(User);
				message.UserID = Sender.Id;
				await _context.Messages.AddAsync(message);
				await _context.SaveChangesAsync();

				await _context.Messages.AddAsync(message);
				await _context.SaveChangesAsync();
				return Ok();
			}
			return Error();

		}
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}