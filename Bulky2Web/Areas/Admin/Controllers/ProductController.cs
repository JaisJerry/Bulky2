
using Bulky2.Data;
using Bulky2.DataAccess.Repository.IRepository;
 
using Bulky2.Models.Models;
using Bulky2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Bulky2Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objCategories = (List<Product>)_unitOfWork.Product.GetAll();
           
            return View(objCategories);
        }
        public IActionResult Upsert(int? id)
        {
            
            //ViewBag.CategoryList = CategoryList;    
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryId.ToString()
                }),
                product = new Product()

            };
            if(id== null || id==0)//Create
                {
                return View(productVM); 
            }
            else//update
            {
                productVM.product = _unitOfWork.Product.GetFirstOrDefault(u=>u.Id==id);
                return View(productVM);
            }
           
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string filename = Guid.NewGuid().ToString(); 
                    string upload = Path.Combine(wwwRootPath, @"images\products");
                    string extention = Path.GetExtension(file.FileName);

                    if(!string.IsNullOrEmpty (productVM.product.ImageUrl))
                    {
                        //image URL not preset delete

                        var oldImagePath = Path.Combine(wwwRootPath + productVM.product.ImageUrl);//Trim('\\')) ; ;
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var filestreams = new FileStream(Path.Combine(upload, filename + extention), FileMode.Create))
                    {
                        file.CopyTo(filestreams);
                    }
                    productVM.product.ImageUrl = @"\Images\Products\" + filename + extention;


                }
                if(productVM.product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.product);
                }
                
                _unitOfWork.Save();
                TempData["Success"] = "Created";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryId.ToString()
                });

                return View(productVM);
            }
           

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
