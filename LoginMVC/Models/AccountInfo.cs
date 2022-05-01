using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace LoginMVC.Models
{
    public class AccountInfo
    {
        public string sessionID
        {
            get; set;
        }

        public string accountID
        {
            get; set;
        }
    }
}