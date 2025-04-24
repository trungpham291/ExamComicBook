﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamComicBook.Data;
using ExamComicBook.Models;

namespace ExamComicBook.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly ComicDbContext _context;

        public ComicBooksController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: ComicBooks
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComicBooks.ToListAsync());
        }

        // GET: ComicBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBooks
                .FirstOrDefaultAsync(m => m.ComicBookID == id);
            if (comicBook == null)
            {
                return NotFound();
            }

            return View(comicBook);
        }

        // GET: ComicBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComicBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComicBookID,Title,Author,PricePerDay")] ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comicBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // GET: ComicBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBooks.FindAsync(id);
            if (comicBook == null)
            {
                return NotFound();
            }
            return View(comicBook);
        }

        // POST: ComicBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComicBookID,Title,Author,PricePerDay")] ComicBook comicBook)
        {
            if (id != comicBook.ComicBookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comicBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicBookExists(comicBook.ComicBookID))
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
            return View(comicBook);
        }

        // GET: ComicBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBooks
                .FirstOrDefaultAsync(m => m.ComicBookID == id);
            if (comicBook == null)
            {
                return NotFound();
            }

            return View(comicBook);
        }

        // POST: ComicBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comicBook = await _context.ComicBooks.FindAsync(id);
            if (comicBook != null)
            {
                _context.ComicBooks.Remove(comicBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComicBookExists(int id)
        {
            return _context.ComicBooks.Any(e => e.ComicBookID == id);
        }
    }
}
