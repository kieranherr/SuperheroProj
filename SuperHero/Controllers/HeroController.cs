using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperHero.Data;
using SuperHero.Models;

namespace SuperHero.Controllers
{
    public class HeroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HeroController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hero
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hero.ToListAsync());
        }

        // GET: Hero/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hero = await _context.Hero
                .FirstOrDefaultAsync(m => m.Name == id);
            if (hero == null)
            {
                return NotFound();
            }

            return View(hero);
        }

        // GET: Hero/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hero/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,AlterEgo,PrimeAbility,SecondAbility,CatchPhrase")] Hero hero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hero);
        }

        // GET: Hero/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hero = await _context.Hero.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }
            return View(hero);
        }

        // POST: Hero/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,AlterEgo,PrimeAbility,SecondAbility,CatchPhrase")] Hero hero)
        {
            if (id != hero.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeroExists(hero.Name))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hero);
        }

        // GET: Hero/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hero = await _context.Hero
                .FirstOrDefaultAsync(m => m.Name == id);
            if (hero == null)
            {
                return NotFound();
            }

            return View(hero);
        }

        // POST: Hero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hero = await _context.Hero.FindAsync(id);
            _context.Hero.Remove(hero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeroExists(string id)
        {
            return _context.Hero.Any(e => e.Name == id);
        }
    }
}
