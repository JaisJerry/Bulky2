 
using Bulky2.Data;
using Bulky2.DataAccess.Repository.IRepository;
using Bulky2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky2Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategories = (List<Category>)_unitOfWork.Category.GetAll();
            return View(objCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Created";
                return RedirectToAction("Index", "Category");
            }
            return View(obj);
            
                }

        public IActionResult Edit(int? id)
        {
            if(id == null || id==0)
            {
                return NotFound();
            }
            Category? CategoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            if (CategoryFromDb == null)
                return NotFound();

            return View(CategoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Updated";
                return RedirectToAction("Index", "Category");
            }
            return View(obj);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CategoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            if (CategoryFromDb == null)
                return NotFound();

            return View(CategoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)
        {
            Category obj = _unitOfWork.Category.GetByID(id);
            if (obj == null)
                return NotFound();
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Deleted";
            return RedirectToAction("Index", "Category");

            

        }
    }
}
