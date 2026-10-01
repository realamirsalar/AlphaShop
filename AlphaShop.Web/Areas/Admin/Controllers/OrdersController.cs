using AlphaShop.Application.Common.Utitlity;
using AlphaShop.Application.Services.Implementations;
using AlphaShop.Web.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlphaShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View(_orderService.GetAllOrders());
        }
        public IActionResult Detail(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                TempData["Error"] = "سفارش مد نظر پیدا نشد!";
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var orderVM = new UpdateOrderVM(id)
            {
                Statuses = SD.Stutuses.Select(s => new SelectListItem
                {
                    Text = s,
                    Value = s
                })
            };
            return View(orderVM);
        }
        [HttpPost]
        public IActionResult Update(UpdateOrderVM vm)
        {
            if (ModelState.IsValid)
            {
                var order = _orderService.GetOrderById(vm.Id);
                if (order == null)
                {
                    TempData["Error"] = "سفارش مد نظر پیدا نشد!";
                    return RedirectToAction(nameof(Index));
                }
                order.RefPostId = vm.RefPostId;
                order.OrderStatus = vm.Status;
                _orderService.UpdateOrder(order);
                TempData["Success"] = "سفارش با موفقیت ویرایش شد!";
                return RedirectToAction(nameof(Index));
            }
            var updatevm = new UpdateOrderVM()
            {
                Statuses = SD.Stutuses.Select(s => new SelectListItem
                {
                    Text = s,
                    Value = s
                })
            }; 
            updatevm.Id = vm.Id;

            return View(updatevm);
        }
    }
}
