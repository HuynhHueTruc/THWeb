using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TextThucHanh.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Tên đăng nhập không được để trống")]
        public string Username { set; get; }
        [Required(ErrorMessage = "Password không được để trống")]
        [MinLength(8, ErrorMessage = "Password phải có tối thiểu 8 ký tự!")]
        public string Password { set; get; }
        public string Remenberme { set; get; }

    }
}