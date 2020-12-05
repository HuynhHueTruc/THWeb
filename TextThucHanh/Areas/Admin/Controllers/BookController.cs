using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using TextThucHanh.Areas.Admin.Models;

namespace TextThucHanh.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        TextThucHanhDbContext db = new TextThucHanhDbContext();
        // GET: Admin/Book
        public ActionResult getListBook(int page = 1, int pagesize = 2)
        {
            var listPage = new Model.AccountModel();
            var model = listPage.ListAllPage(page, pagesize);
            //var listBook = from s in db.Books select s;
            //return View(listBook);
            return View(model);

        }
        // GET: Books/Create
        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost, ActionName("CreateBook")]
        [ValidateInput(false)]
        public ActionResult CreateBook(Book book, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //Upload file
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Console.WriteLine(fileName);
                    //Lưu đường dẫn file ảnh 
                    var path = Path.Combine(Server.MapPath("~/Content/Image"), fileName);
                    //Kiểm tra file đã tồn tại
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    //Them Sach Moi
                    book.CoverPage = fileUpload.FileName;
                    db.Books.Add(book);
                    db.SaveChanges();
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            //Cập nhật lại danh sách hiển thị
            var listBook = from s in db.Books select s;
            return View(book);
        }


        //Hiển thị chi tiết
        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //Chỉnh sửa thông tin sách
        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.SingleOrDefault(s => s.BookId == id);
            return View(book);
        }

        [HttpPost, ActionName("EditBook")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(int id)
        {
            var bookUpdate = db.Books.Find(id);
            if (TryUpdateModel(bookUpdate, "", new string[] { "Title", "AuthorName", "Price", "Year", "CoverPage" }))
            {
                try
                {
                    db.Entry(bookUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }
            }

            return RedirectToAction("getListBook");
        }

        //Hiển thị thông tin một sách cần xoá
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book book = db.Books.SingleOrDefault(n => n.BookId == id);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(book);
        }

        //Xác nhận xoá
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Book book = db.Books.SingleOrDefault(n => n.BookId == id);
            var path = Path.Combine(Server.MapPath("~/Content/Image"), book.CoverPage);

            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Xoá ảnh trong thư mục ~/Content/Image
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("getListBook");
        }

        //Tiến trình thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public JsonResult AddToCart(int id)
        {
            //Process Add To Cart
            List<CartItem> listCartItem;
            if (Session["ShoppingCart"] == null)
            {
                //Create New Shopping Cart Session 
                listCartItem = new List<CartItem>();
                listCartItem.Add(new CartItem { Quality = 1, productOrder = db.Books.Find(id) });
                Session["ShoppingCart"] = listCartItem;
            }
            else
            {
                bool flag = false;
                listCartItem = (List<CartItem>)Session["ShoppingCart"];
                foreach (CartItem item in listCartItem)
                {
                    if (item.productOrder.BookId == id)
                    {
                        item.Quality++; flag = true;
                        break;
                    }
                }

                if (!flag)
                    listCartItem.Add(new CartItem { Quality = 1, productOrder = db.Books.Find(id) });

                Session["ShoppingCart"] = listCartItem;
            }
            //Count item in shopping cart 
            int cartcount = 0;
            List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
            foreach (CartItem item in ls)
            {
                cartcount += item.Quality;
            }
            return Json(new { ItemAmount = cartcount });
        }

        public ActionResult ShoppingCart()
        {
            return View();
        }

    }
}