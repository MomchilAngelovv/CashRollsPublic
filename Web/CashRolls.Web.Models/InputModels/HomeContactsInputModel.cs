using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CashRolls.Web.Models.InputModels
{
    public class HomeContactsInputModel
    {
        [Required]
        [MaxLength(250)]
        public string Sender { get; set; }
        [Required]
        [MaxLength(450)]
        public string Message { get; set; }
    }
}
