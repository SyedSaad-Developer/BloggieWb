using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;



namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        /*Retrieving Category*/
        public IActionResult Index()
        {
            List<Category> objCategoryList = _UnitOfWork.Category.GetAll().ToList(); /* Retrieving Category Items*/
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        /* Adding Category*/
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid) /*Data Valiadtion: If any field is empty so it won't store the data to db else goes back to the category list*/
            {
                _UnitOfWork.Category.Add(obj); /* Just keeping track that actually what it has to add*/
                _UnitOfWork.Save(); /* In this statement it will go to the db and create that category*/
                TempData["success"] = "Category created successfully.!";
                return RedirectToAction("Index"); /*As we are in the same controller so we just have to tell the action otherwise we have to tell the controller name as well*/
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _UnitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid) /*Data Valiadtion: If any field is empty so it won't store the data to db else goes back to the category list*/
            {
                _UnitOfWork.Category.Update(obj); /* Just keeping track that actually what it has to add*/
                _UnitOfWork.Save(); /* In this statement it will go to the db and create that category*/
                TempData["success"] = "Category updated successfully.!";
                return RedirectToAction("Index"); /*As we are in the same controller so we just have to tell the action otherwise we have to tell the controller name as well*/
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _UnitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _UnitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Category.Remove(obj);
            _UnitOfWork.Save();
            TempData["success"] = "Category deleted successfully.!";
            return RedirectToAction("Index");
        }


    }
}
