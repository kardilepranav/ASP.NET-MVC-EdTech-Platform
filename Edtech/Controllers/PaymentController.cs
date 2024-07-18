using Microsoft.AspNetCore.Mvc;
using Edtech.Models;
using Edtech.Service;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Edtech.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Edtech.Controllers
{
    public class PaymentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _service;
        private IHttpContextAccessor _httpContextAccessor;

        public PaymentController(AppDbContext context, ILogger<PaymentController> logger, IPaymentService service, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessRequestOrder(PaymentRequest _paymentRequest)
        {
            MerchantOrder _marchantOrder = await _service.ProcessMerchantOrder(_paymentRequest);
            return View("Payment", _marchantOrder);
        }

        public async Task<IActionResult> CompleteOrderProcess(string orderId,string razorpayKey,int amount,string currency,string name,string email,string phoneNumber,string address,string description ,string courseTitle)
        {
            string paymentMessage = await _service.CompleteOrderProcess(_httpContextAccessor);

            if (paymentMessage == "captured")
            {
                var merchantOrder = new MerchantOrder
                {
                    OrderId = orderId,
                    RazorpayKey = razorpayKey,
                    Amount = amount,
                    Currency = currency,
                    Name = name,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    Description = description,
                    CourseTitle = courseTitle
                };

                _context.MerchantOrders.Add(merchantOrder);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View("Failed");
            }
        }
        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Failed()
        {
            return View();
        }
    }
}
