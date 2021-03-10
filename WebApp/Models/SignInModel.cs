using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage ="Vui long nhap ten dang nhap")]
        public string Usr { get; set; }
        [Required(ErrorMessage ="Vui lon nhap mat khau")]
        public string Pwd { get; set; }
        public bool Rem { get; set; }
    }
}
