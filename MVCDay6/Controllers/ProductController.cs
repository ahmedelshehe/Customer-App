using Microsoft.AspNetCore.Mvc;
using MVCDay6.Models;
using MVCDay6.Repositories.ProductRepositories;

namespace MVCDay6.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }

        public ActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var product = _productRepository.GetById(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(Product product)
        {
            _productRepository.Delete(product.Id);
            return RedirectToAction("Index");
        }
    }
}
