
using Bulkey.Models;
using Microsoft.AspNetCore.Mvc;
using Bulkey.DataAccess.Data;
using Bulkey.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bulkey.Models.ViewModel;
using Bulkey.DataAccess.Repository;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        // Môi trường lưu ảnh
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();  
            return View(objProductList);
        }

        // Create
        public IActionResult Upsert(int? id)
        {
            // Cách lấy danh sach danh mục
            ProductVM productVM = new()
            {
                CategorList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Product = new Product()
            };
            if(id == null || id == 0)
            {
                // Create
                return View(productVM);
            } else
            {
                // Update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }

        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid) {
                // Lấy ra thư mục wwwroot
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                //Kiểm tra nếu có file được up lên thì vào if
                if(file != null)
                {
                    // Lấy tên file được up lên
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    // Lưu đường dẫn file vào thư mục gốc
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    // Xử lý việc nếu khi Update người dùng chọn ảnh mới thì tự động xóa ảnh cũ đi và hiển thị ảnh mới (nếu ko có nhu cầu cập nhật ảnh mới thì mặc định là null)
                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl)) // Như nyaf là xử lý trường hợp có dữ liệu ảnh mới
                    {
                        //delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath)) { 
                            System.IO.File.Delete(oldImagePath);
                        }
                    } // Xóa hình ảnh cũ

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    } // Tải lên 1 ảnh mới

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                // Kiểm tra có Id ko, ko có thì Add, có Id thì Update
                if(productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                } else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Save();
                 // Hiển thị thông báo đã create thành công
                 TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            } else
            {
                productVM.CategorList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
                return View(productVM);
            }     
       }    

        #region API CALLS
            public IActionResult GetAll()
            {
                List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
                return Json(new {data =  objProductList});
            }

            [HttpDelete]    

            public IActionResult Delete(int? id)
            {
                var productDeleted = _unitOfWork.Product.Get(u => u.Id == id);
                if(productDeleted == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productDeleted.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _unitOfWork.Product.Remove(productDeleted);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete Successfuly" });
            }
        #endregion
    }
}


/*
 *             //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
 // Edit Category
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
 */

// Delete Category
//public IActionResult Delete(int? id)
//{
//    if (id == null || id == 0)
//    {
//        return NotFound();
//    }
//    Product productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
//    if (productFromDb == null)
//    {
//        return NotFound();
//    }
//    return View(productFromDb);
//}

//[HttpPost, ActionName("Delete")]
//public IActionResult DeletePOST(int? id)
//{
//    Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
//    if (obj == null)
//    {
//        return NotFound();
//    }
//    if (ModelState.IsValid)
//    {
//        _unitOfWork.Product.Remove(obj);
//        _unitOfWork.Save();
//        TempData["success"] = "Product deleted successfully";
//        return RedirectToAction("Index");
//    }

//    return View();
//}
