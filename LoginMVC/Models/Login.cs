using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginMVC.Models
{
    public class Login
    {
        [Display(Name ="Username")]
        public string Username
        {
            get;
            set;
        }
        [Display(Name = "Password")]
        public string Password
        {
            get;
            set;
        }
        [Display(Name = "Fullname")]
        public string Fullname
        {
            get;
            set;
        }

        public Login account
        {
            get;
            set;
        }
    }
}