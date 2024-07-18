using Edtech.Data;
using Edtech.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Edtech.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly AppDbContext _context;

        public DashBoardController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> Index()
        {
            return _context.Students2 != null ?
                        View(await _context.Students2.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Courses'  is null.");
        }

        public async Task<IActionResult> AdminIndex()
        {
            return _context.Students2 != null ?
                        View(await _context.Students2.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Courses'  is null.");
        }
        
        [HttpPost]
        public async Task<IActionResult> Enroll(string courseTitle, string instructorName, string description, string duration, int price, string ProfileVideo1, string ProfileVideo2, string ProfileVideo3, string ProfileVideo4, string ProfileVideo5)
        {
            var userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var merchantOrder = await _context.MerchantOrders.FirstOrDefaultAsync(o => o.Email == userEmail);
            var coursename = await _context.MerchantOrders.FirstOrDefaultAsync(o => o.CourseTitle == courseTitle);

            if (merchantOrder != null && coursename!=null)
            {
                var model = new Student2
                {
                    CourseTitle = courseTitle,
                    InstructorName = instructorName,
                    Description = description,
                    Duration = duration,
                    Price = price,
                    ProfileVideo1 = ProfileVideo1,
                    ProfileVideo2 = ProfileVideo2,
                    ProfileVideo3 = ProfileVideo3,
                    ProfileVideo4 = ProfileVideo4,
                    ProfileVideo5 = ProfileVideo5
                };
                return RedirectToAction("Enrolled", model);
            }
            else
            {
                return RedirectToAction("Index", "Payment");
            }
        }

        public IActionResult Enrolled(Student2 model)
        {
            return View(model);
        }
    }
}
