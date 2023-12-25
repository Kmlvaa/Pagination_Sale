using System;
using P335_BackEnd.Entities;

namespace P335_BackEnd.Models
{
    public class ShopIndexVM
    {
        public List<Product> Products { get; set; }
        public List<ShopProducts> ShopProducts { get; set; }
        public List<Product> LatestProducts { get; set; }
        public List<Product> SaleOffProducts { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }

       public int PageCount()
       {
           return ShopProducts.Count * TotalPageCount;
       }
    }
}