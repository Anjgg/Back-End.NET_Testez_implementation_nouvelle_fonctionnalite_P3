using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace P3.Models.ViewModels
{
    public class OrderViewModel
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingName", ErrorMessageResourceType = (typeof(Resources.Models.Order)))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingAddress", ErrorMessageResourceType = (typeof(Resources.Models.Order)))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingCity", ErrorMessageResourceType = (typeof(Resources.Models.Order)))]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingZipCode", ErrorMessageResourceType = (typeof(Resources.Models.Order)))]
        public string Zip { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingCountry", ErrorMessageResourceType = (typeof(Resources.Models.Order)))]
        public string Country { get; set; }

        [BindNever]
        public DateTime Date { get; set; }
    }
}
