using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using P3.Resources.Models;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class OrderViewModel
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingName", ErrorMessageResourceType = (typeof(Order)))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingAddress", ErrorMessageResourceType = (typeof(Order)))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingCity", ErrorMessageResourceType = (typeof(Order)))]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingZipCode", ErrorMessageResourceType = (typeof(Order)))]
        public string Zip { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingCountry", ErrorMessageResourceType = (typeof(Order)))]
        public string Country { get; set; }

        [BindNever]
        public DateTime Date { get; set; }
    }
}
