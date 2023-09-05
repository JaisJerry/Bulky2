
using Bulky2.Data;
using Bulky2.DataAccess.Repository.IRepository;
 
using Bulky2.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky2Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objCategories = (List<Product>)_unitOfWork.Product.GetAll();
            return View(objCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Created";
                return RedirectToAction("Index", "Product");
            }
            return View(obj);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? ProductFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (ProductFromDb == null)
                return NotFound();

            return View(ProductFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Updated";
                return RedirectToAction("Index", "Product");
            }
            return View(obj);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? ProductFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (ProductFromDb == null)
                return NotFound();

            return View(ProductFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteProduct(int? id)
        {
            Product obj = _unitOfWork.Product.GetByID(id);
            if (obj == null)
                return NotFound();
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Deleted";
            return RedirectToAction("Index", "Product");



        }
    }
}
