using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Models;
using Fitness.Models.Repositries;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/categoryProduct")]
    [ApiController]
    public class CategoryProductApiController : ControllerBase
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IFitnessRepositry<Category> catDb;
        private readonly IFitnessRepositry<Product> pDb;
        private readonly IFitnessRepositry<Gold> gDb;

        public CategoryProductApiController(IHostingEnvironment hostingEnvironment, IFitnessRepositry<Category> catDb, IFitnessRepositry<Product> pDb, IFitnessRepositry<Gold> gDb)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.catDb = catDb;
            this.pDb = pDb;
            this.gDb = gDb;
        }
        [HttpGet("AllCategories")] 
        public List<categoryViewModel> GetAllCategories()
        {
            try
            {                
                var ddd = catDb.IList("EldahbDefault").Where(a => !a.IsGift).Select(a => new categoryViewModel
                {
                    CatName = a.CatName,
                    Id = a.Id,
                    ImgUrl = a.ImgUrl,
                    products = a.Products.Select(p => new ProductViewModel { ImgUrl = p.ImgUrl, Id = p.Id, Price = p.Price, ProductName = p.ProductName ,DaysCount=p.DaysCount}).ToList()
                    ,IsGift=false
                }).ToList();
                return ddd;
            }
            catch (Exception)
            {
                return new List<categoryViewModel>();
            }
        }
        [HttpGet("AllGift")] 
        public List<categoryViewModel> AllGift()
        {
            try
            {
                var ddd = catDb.IList().Where(a=>a.IsGift).Select(a => new categoryViewModel
                {
                    CatName = a.CatName,
                    Id = a.Id,
                    ImgUrl = a.ImgUrl,
                    products = a.gifts.Select(p => new ProductViewModel { ImgUrl = p.ImgUrl, Id = p.Id, Price = p.Price, ProductName = p.GiftName , SoundUrl=p.SoundUrl}).ToList()
                    ,IsGift=true
                }).ToList();
                return ddd;
            }
            catch (Exception)
            {
                return new List<categoryViewModel>();
            }
        }
        [HttpGet("AllGolds")]
        public List<Gold> GetAllGold()
        {
            try
            {
                return gDb.list();
            }
            catch (Exception)
            {
                return new List<Gold>();
            }
        }
      
    }
    public class categoryViewModel
    {
        public int Id { get; set; }
        public string CatName { get; set; }
        public string ImgUrl { get; set; }
        public List<ProductViewModel> products { get; set; }
        public List<GiftViewModel> gifts { get; set; }
        public bool IsGift { get; set; }
    }
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public int DaysCount { get; set; }
           public int CategoryId { get; set; }
        public DateTime date { get; set; }
        public bool Used { get; set; }
        public bool IsDeleted { get; set; }
        public string SoundUrl { get; set; }

    }
    public class GiftViewModel
    {

        public int Id { get; set; }
        public string GiftName { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public string SoundUrl { get; set; }
    }
}