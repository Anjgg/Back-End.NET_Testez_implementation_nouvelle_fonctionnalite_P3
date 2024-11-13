using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;


namespace P3.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "MissingName", ErrorMessageResourceType = (typeof(Resources.Models.Services.ProductService)))]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [Required(ErrorMessageResourceName = "MissingStock", ErrorMessageResourceType = (typeof(Resources.Models.Services.ProductService)))]
        [RegularExpression(@"^[-]?\d*$", ErrorMessageResourceName = "StockNotAnInteger", ErrorMessageResourceType = (typeof(Resources.Models.Services.ProductService)))]
        [Range(1,1000000,ErrorMessageResourceName = "StockNotGreaterThanZero", ErrorMessageResourceType = (typeof(Resources.Models.Services.ProductService)))]
        public string Stock { get; set; }


        [Required(ErrorMessageResourceName = "MissingPrice", ErrorMessageResourceType = (typeof(Resources.Models.Services.ProductService)))]
        [RegularExpression(@"^[-]?\d*([\.]\d{1,2})?$", ErrorMessageResourceName = "PriceNotANumber", ErrorMessageResourceType = (typeof(Resources.Models.Services.ProductService)))]
        [Range(0.01, 1000000.00, ErrorMessageResourceName = "PriceNotGreaterThanZero", ErrorMessageResourceType = (typeof(Resources.Models.Services.ProductService)))]
        public string Price { get; set; }
    }
}

