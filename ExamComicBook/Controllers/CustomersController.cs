using Microsoft.AspNetCore.Mvc;
using ExamComicBook.Data;
using ExamComicBook.Models;

namespace ExamComicBook.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ComicDbContext _context;

        public CustomersController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,PhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Registration = DateTime.Now; // Tự động gán ngày đăng ký
                _context.Add(customer); // Thêm khách hàng vào DbContext
                await _context.SaveChangesAsync(); // Lưu vào cơ sở dữ liệu
                return RedirectToAction("Index", "Home"); // Sau khi lưu thành công, chuyển hướng về trang chủ
            }
            return View(customer); // Nếu có lỗi, quay lại form
        }
    }
}
