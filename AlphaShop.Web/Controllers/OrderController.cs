using AlphaShop.Application.Common.DTOs;
using AlphaShop.Application.Common.Interfaces;
using AlphaShop.Application.Common.Utitlity;
using AlphaShop.Application.Services.Implementations;
using AlphaShop.Web.ViewModels.Shopping;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlphaShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;

        public OrderController(IOrderService orderService, IPaymentService paymentService)
        {
            _orderService = orderService;
            this._paymentService = paymentService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddAddress()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAddress(AddressVM address)
        {
            var claimsIdentity = (ClaimsIdentity)HttpContext.User.Identity;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = new AddressDTO()
            {
                State = address.State,
                StreetAddress = address.StreetAddress,
                City = address.City,
                PostalCode = address.PostalCode,
                UserId = userId,
            };
            var result = _orderService.CreateOrder(order);
            var returnUrl = Url.Action(action: "VerifyOrder", controller: "Order" , values : null ,protocol: Request.Scheme );
            var requestResult = await _paymentService.ZarinpalRequestSend(result.Id , result.TotalCost , returnUrl );
            if (requestResult.IsSuccess)
            {
                _orderService.UpdatePaymentStatus(result.Id, requestResult.Authority, SD.ZarinPalGateway);
                return Redirect(requestResult.PaymentUrl);
            }
            return RedirectToAction(nameof(OrderFaild));
        }


        public async Task<IActionResult> VerifyOrder(string authority, string status)
        {
            if (status != "OK")
            {
                TempData["Error"] = "پرداخت توسط شما لغو شد.";
                return RedirectToAction(nameof(OrderFaild), new { message = "پرداخت شما لغو شد." });
            }

            var orderDto = _orderService.GetOrderByAuthority(authority);
            if (orderDto == null)
            {
                TempData["Error"] = "سفارش مرتبط با این پرداخت یافت نشد.";
                return RedirectToAction(nameof(OrderFaild), new { message = "سفارش مرتبط با این پرداخت یافت نشد." });
            }
            if (orderDto.State == SD.StatusApproved)
            {
                TempData["Success"] = "این سفارش قبلا پرداخت شده است.";
                return RedirectToAction(nameof(OrderConfirmation), new { id = orderDto.Id });
            }

            var verifyResult = await _paymentService.ZarinPalVerifyData(authority, (int)orderDto.OrderTotal);
            if (verifyResult.IsSuccess)
            {
                _orderService.UpdatePaymentStatus(orderDto.Id, authority, SD.ZarinPalGateway, verifyResult.RefId);

                return RedirectToAction(nameof(OrderConfirmation), new { id = orderDto.Id });
            }

            TempData["Error"] = verifyResult.ErrorMessage;
            return RedirectToAction(nameof(OrderFaild), new { message = verifyResult.ErrorMessage });
        }

        [HttpGet]
        public IActionResult OrderConfirmation(int orderId)
        {
            return View();
        }
        public IActionResult OrderFaild(string message)
        {
            return View(message);
        }
    }
}
