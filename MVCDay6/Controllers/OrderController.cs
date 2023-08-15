using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCDay6.Models;
using MVCDay6.Repositories.CustomerRepositories;
using MVCDay6.Repositories.OrderRepositories;
using MVCDay6.Repositories.ProductRepositories;

namespace MVCDay6.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public OrderController(IOrderRepository orderRepository, ICustomerRepository customerRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }
        public ActionResult Index()
        {
            var customers = _customerRepository.GetAll();
            SelectList selectLists = new SelectList(customers, "ID", "Name", -1);
            ViewBag.Customers = selectLists;
            var model = _orderRepository.GetAllOrdersWithCustomerAndProducts();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int CustID)
        {
            if (CustID != -1)
            {
                var customers = _customerRepository.GetAll();
                SelectList selectLists = new SelectList(customers, "ID", "Name", CustID);
                ViewBag.Customers = selectLists;
                return View(_orderRepository.GetCustomerOrders(CustID));

            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        public ActionResult Details(int id)
        {
            var order = _orderRepository.GetOrderByIdWithProductsAndCustomer(id);
            return View(order);
        }
        public ActionResult Create()
        {
            var customers = _customerRepository.GetAll();
            SelectList selectLists = new SelectList(customers, "ID", "Name");
            ViewBag.Customers = selectLists;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (order.CustID == -1)
                ModelState.AddModelError("CustID", "You have to choose a customer");
            if (ModelState.IsValid)
            {
                _orderRepository.Add(order);
                return RedirectToAction("Index");
            }
            else
            {
                var customers = _customerRepository.GetAll();
                SelectList selectLists = new SelectList(customers, "ID", "Name", order.CustID);
                ViewBag.Customers = selectLists;
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var customers = _customerRepository.GetAll();
                SelectList selectLists = new SelectList(customers, "ID", "Name", order.CustID);
                ViewBag.Customers = selectLists;
                var products = _orderRepository.GetOrderProducts(id);
                return View(new OrderViewModel() { Order = order, OrderProducts = products });
            }

        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (order.CustID == -1)
                ModelState.AddModelError("CustID", "You have to choose a customer");
            if (ModelState.IsValid)
            {
                _orderRepository.Update(order);
                return RedirectToAction("Index");
            }
            else
            {
                var customers = _customerRepository.GetAll();
                SelectList selectLists = new SelectList(customers, "ID", "Name", order.CustID);
                ViewBag.Customers = selectLists;
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            _orderRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult AddProduct(int id)
        {
            var products = _productRepository.GetAll();
            SelectList selectLists = new SelectList(products, "Id", "Name");
            ViewBag.Products = selectLists;
            ViewBag.orderId = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(OrderProduct orderProduct)
        {
            _orderRepository.AddOrderProduct(orderProduct.OrderId, orderProduct.ProductId, orderProduct.Quantity);
            return RedirectToAction("Details", new { id = orderProduct.OrderId });
        }
        public ActionResult DeleteProduct(int orderId, int productId)
        {
            _orderRepository.DeleteOrderProduct(orderId, productId);
            return RedirectToAction("Details", new { id = orderId });


        }

    }
}
