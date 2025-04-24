using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExamComicBook.Data;
using Microsoft.EntityFrameworkCore;
using ExamComicBook.Models;

public class RentalsController : Controller
{
    private readonly ComicDbContext _context;

    public RentalsController(ComicDbContext context)
    {
        _context = context;
    }

    // GET: Rentals/Create
    public IActionResult Create()
    {
        ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName");
        ViewData["ComicBookID"] = new SelectList(_context.ComicBooks, "ComicBookID", "Title");
        return View();
    }

    // POST: Rentals/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int CustomerID, DateTime RentalDate, DateTime ReturnDate, int ComicBookID, int Quantity)
    {
        var comic = await _context.ComicBooks.FindAsync(ComicBookID);
        if (comic == null)
        {
            ModelState.AddModelError("", "Selected comic book not found.");
            return View();
        }

        var rental = new Rental
        {
            CustomerID = CustomerID,
            RentalDate = RentalDate,
            ReturnDate = ReturnDate,
            Status = "Rented"
        };

        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();

        var rentalDetail = new RentalDetail
        {
            RentalID = rental.RentalID,
            ComicBookID = ComicBookID,
            Quantity = Quantity,
            PricePerDay = comic.PricePerDay
        };

        _context.RentalDetails.Add(rentalDetail);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
}
