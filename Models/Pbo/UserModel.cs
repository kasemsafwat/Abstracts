using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abstracts.Models.Pbo
{
    public class UserModel
    {
        [Required(ErrorMessage = "أدخل إسم المستخدم")]
        [DataType(DataType.Text)]
        [Display(Name = "إسم المستخدم")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "أدخل كلمة المرور")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        
        [Display(Name = "نوع الدخول")]
        public int type { get; set; }
    }
}