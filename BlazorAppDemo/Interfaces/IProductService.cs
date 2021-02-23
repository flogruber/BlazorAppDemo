using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAppDemo.Models;

namespace BlazorAppDemo.Interfaces
{
    interface IProductService
    {
        public Task<ICollection<DevProduct>> GetProducts();
        public Task<DevProduct> GetProduct(string id);
        public Task<bool> AddProduct(DevProduct product);
        public Task<bool> DeleteProduct(string productID);
        public Task UpdateProduct(DevProduct product);
        public Task<bool> AddComment(string productID, DevProductComment comment);
        public Task<bool> DeleteComment(string productID, string commentID);
        public Task UpdateComment(string productID, DevProductComment comment);
        public Task<ICollection<DevProductComment>> GetComments2Product(string productID);
    }
}
