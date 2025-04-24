using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamComicBook.Data;
using System.Linq;
using ExamComicBook.Models;

public class ReportsController : Controller
{
    private readonly ComicDbContext _context;

    public ReportsController(ComicDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(DateTime? fromDate, DateTime? toDate)
    {
        var query = from d in _context.RentalDetails
                    join r in _context.Rentals on d.RentalID equals r.RentalID
                    join c in _context.Customers on r.CustomerID equals c.CustomerID
                    join b in _context.ComicBooks on d.ComicBookID equals b.ComicBookID
                    where (!fromDate.HasValue || r.RentalDate >= fromDate)
                       && (!toDate.HasValue || r.RentalDate <= toDate)
                    select new RentalReportViewModel
                    {
                        BookName = b.Title,
                        RentalDate = r.RentalDate,
                        ReturnDate = r.ReturnDate,
                        CustomerName = c.FullName,
                        Quantity = d.Quantity
                    };

        return View(query.ToList());
    }
}
