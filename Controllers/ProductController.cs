using CrudDapper.Interface;
using CrudDapper.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudDapper.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _productRepsitory;

        public ProductController(IProduct productRepository)
        {
            _productRepsitory = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepsitory.GetProducts();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.IsEdit = false;
            return View("CreateorEdit", new Product());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepsitory.Add(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.IsEdit = false;
            return View("CreateorEdit", product);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var product = await _productRepsitory.FindProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.IsEdit = true;
            return View("CreateorEdit", product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            await _productRepsitory.Update(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productRepsitory.Delete(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
