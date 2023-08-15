using Microsoft.AspNetCore.Mvc;
using MVCDay6.Models;
using MVCDay6.Repositories.CustomerRepositories;

namespace MVCDay6.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ActionResult Index()
        {
            var customers = _customerRepository.GetAllCustomersWithOrders();
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _customerRepository.GetCustomerWithOrders(id);
            var model = new CustomerViewModel() { Customer = customer, Orders = customer.Orders };
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.Add(customer);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var emp = _customerRepository.GetCustomerWithOrders(id);
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.Update(customer);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            _customerRepository.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
