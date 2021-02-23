using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAppDemo.Interfaces;
using BlazorAppDemo.Models;
using BlazorAppDemo.DB;
using BlazorAppDemo.Helpers;

namespace BlazorAppDemo.Services
{
    public class ProductService: IProductService
    {
        private productDBContext _dbContext = null;

        public ProductService(productDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Product
        public async Task<bool> AddProduct(DevProduct product)
        {
            if (string.IsNullOrEmpty(product.productID) && !GuidHelper.IsGuid(product.productID)) { return false; }
            
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProduct(string productID)
        {
            if(string.IsNullOrEmpty(productID) && !GuidHelper.IsGuid(productID))
            {
                return false;
            }
            var product = _dbContext.Products.Where(x => x.productID == productID).FirstOrDefault();
            if(product == null) { return false; }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<DevProduct> GetProduct(string id)
        {
            if(string.IsNullOrEmpty(id) && !GuidHelper.IsGuid(id))
            {
                return Task.FromResult(new DevProduct());
            }

            var product = _dbContext.Products.Where(x => x.productID == id).FirstOrDefault();
            return Task.FromResult(product);
        }

        public async Task<ICollection<DevProduct>> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public async Task UpdateProduct(DevProduct product)
        {
            if (string.IsNullOrEmpty(product.productID) && !GuidHelper.IsGuid(product.productID)) { return; }
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region Comment
        public async Task<bool> AddComment(string productID, DevProductComment comment)
        {
            if (!GuidHelper.IsGuid(productID) && !GuidHelper.IsGuid(comment.commentID)) { return false; }
            var product = _dbContext.Products.Where(x => x.productID == productID).FirstOrDefault();
            if (product == null) { return false; }
            
            product.Comments.Add(comment);

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdateComment(string productID, DevProductComment comment)
        {
            if (!GuidHelper.IsGuid(productID) && !GuidHelper.IsGuid(comment.commentID)) { return; }
            var product = _dbContext.Products.Where(x => x.productID == productID).FirstOrDefault();
            if (product == null) { return; }
            var lComment = product.Comments.Where(x => x.commentID == comment.commentID).FirstOrDefault();
            if (lComment == null) { return; }
            
            lComment.Author = comment.Author;
            lComment.Content = comment.Content;
            lComment.Approved = comment.Approved;

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteComment(string productID, string commentID)
        {
            if (!GuidHelper.IsGuid(productID) && !GuidHelper.IsGuid(commentID)) { return false; }
            var product = _dbContext.Products.Where(x => x.productID == productID).FirstOrDefault();
            if (product == null) { return false; }
            var lComment = product.Comments.Where(x => x.commentID == commentID).FirstOrDefault();
            if (lComment == null) { return false; }
            
            product.Comments.Remove(lComment);

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<DevProductComment>> GetComments2Product(string productID)
        {
            if (!GuidHelper.IsGuid(productID)) { return null; }

            var product = _dbContext.Products.Where(x => x.productID == productID).FirstOrDefault();
            if (product == null){ return null; }

            return product.Comments;
        }
        #endregion
    }
}
