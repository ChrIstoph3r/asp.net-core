using System.Threading.Tasks;
using InterviewTest.Data;
using InterviewTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using System;

namespace InterviewTest.Controllers
{
    public class RealLotteryController : Controller
    {
        private readonly Context _context;

        public RealLotteryController(Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            if( !await _context.Players.AnyAsync())
            {
                Console.WriteLine("AddPlayer");
                await addPlayer("Marie", "Stromodden");
            }
            if( !await _context.Prizes.AnyAsync())
            {
               await addPrize("PS4");
            }

            var players = await _context.Players.ToListAsync();

            var prizes = await _context.Prizes.ToListAsync();

           // PlayersAndPrizes playersAndPrizes = new PlayersAndPrizes() { Prizes = prizes, Players = players};

            return View(/*playersAndPrizes*/);
        }

        public async Task addPlayer(string firstName, string lastName)
        {
            var player = new Player(){ FirstName = firstName, LastName = lastName};
            _context.Players.Add(player);

            await _context.SaveChangesAsync();
        }

        public async Task addPrize(string name)
        {
            var prize = new Prize(){ Name = name};
            _context.Prizes.Add(prize);

            await _context.SaveChangesAsync();
        }
    }
}