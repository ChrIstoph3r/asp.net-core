using System.Threading.Tasks;
using InterviewTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewTest.ViewComponents
{
    public class PlayersViewComponent : ViewComponent
    {
        private readonly Context _context;

        public PlayersViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Default", await _context.Players.ToListAsync());
        }
        
    }
}