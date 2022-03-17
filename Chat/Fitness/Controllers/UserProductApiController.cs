using Fitness.Models;
using Fitness.Models.Repositries;
using Fitness.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductApiController : ControllerBase
    {
        private readonly IFitnessRepositry<UserProduct> db;
        private readonly IFitnessRepositry<Product> productdb;
        private readonly IFitnessRepositry<Wallet> walletDb;
        public UserProductApiController(IFitnessRepositry<UserProduct> db, IFitnessRepositry<Wallet> walletDb, IFitnessRepositry<Product> productdb)
        {
            this.db = db;
            this.productdb = productdb;
            this.walletDb = walletDb;

        }
        [HttpPost("UserProducts")]
        public List<UserProductViewModel> AllLevels(UserInfoModel user)
        {
            return db.IList(user.ApplicationUserId).Select(a => new UserProductViewModel { Id = a.Id, ApplicationUserId = a.ApplicationUserId, ProductId = a.ProductId, ImgUrl = a.Product.ImgUrl, Price = a.Product.Price, ProductName = a.Product.ProductName, DaysCount = a.Product.DaysCount, Used=a.Used,date=a.date}).ToList();
        }
        [HttpPost("AddProduct2User")]
        public async Task<object> AddProduct2User(UserProduct p)
        {
            try
            {
                bool IncreaseDaysCount = false;
                if (p.ProductId > 0 && !string.IsNullOrEmpty(p.ApplicationUserId) && !string.IsNullOrEmpty(p.NewUserId))
                {
                    UserProduct product = new UserProduct();
                    UserProduct checkProduct = new UserProduct();
                    if (p.ApplicationUserId.Equals(p.NewUserId))
                    {
                        checkProduct = db.firstOrdefault(0, p.ApplicationUserId, p.ProductId.ToString());
                        if ( checkProduct != null && checkProduct.IsDeleted==false)
                        {
                            IncreaseDaysCount = true;
                        }
                        product.UserDis = p.ApplicationUserId;                  
                    }
                    else
                    {
                        checkProduct = db.firstOrdefault(0, p.NewUserId, p.ProductId.ToString());
                        if (checkProduct != null && checkProduct.IsDeleted==false)
                        {
                            IncreaseDaysCount = true;
                        }
                        product.UserDis = p.ApplicationUserId;   
                    }
                    if (IncreaseDaysCount)
                    {
                      //  checkProduct.UserDis = p.ApplicationUserId;
                        checkProduct.DaysCount = checkProduct.DaysCount+ productdb.firstOrdefault(p.ProductId).DaysCount;
                        if (await db.update(checkProduct ))
                        {
                            Product pro = productdb.firstOrdefault(p.ProductId);
                            Wallet w = walletDb.firstOrdefault(0, p.ApplicationUserId);
                            if (w != null && pro != null)
                            {
                            
                                if (w.Balance >= pro.Price)
                                {
                                    //Take price form user wallet
                                    w.Balance -= pro.Price;
                                    if (await walletDb.update(w))
                                    {
                                        return (new { message = "Success" });

                                    }
                                    else
                                    {
                                        return (new { message = "Error : Wallet Issue" });
                                    }

                                  }
                                }
                            else
                            {
                                return (new { message = "ApplicationUserId" + p.ApplicationUserId });

                            }

                        }
                    }
                    else
                    {
                        product.ProductId = p.ProductId;
                        product.Used = false;
                        product.IsReceived = false;
                        product.ApplicationUserId = p.ApplicationUserId;
                        product.NewUserId = p.NewUserId;
                        product.DaysCount = productdb.firstOrdefault(p.ProductId).DaysCount;
                        product.date = DateTime.Now;
                        if (await db.Add(product))
                        {
                            return (new { message = "Success" });
                        }
                    }
                }
                return (new { message = "Error : Not added" });
            }
            catch (Exception ex )
            {
                return (new { message = "Error : Execption" + ex.InnerException });
            }
        }
        [HttpPost("UsingProduct")]
        public async Task<object> UsingProduct(UserProduct p)
        {
            try
            {
                if (p.ProductId > 0 && !string.IsNullOrEmpty(p.ApplicationUserId))
                {
                    List<UserProduct> products = db.list().Where(a=>a.ApplicationUserId==p.ApplicationUserId).ToList();   
                    if (products.Where(a=>a.ProductId==p.ProductId).Count()== 0)
                    {
                        return (new { message = "Product not available for this user" });
                    }
                    List<Product> _product = productdb.list();  
                    int catId = _product.FirstOrDefault(a => a.Id == p.ProductId).CategoryId;
                    foreach (var prod in _product.Where(a => a.CategoryId == catId).ToList())
                    {
                        foreach (var item in products)
                        {
                            if (item.ProductId == prod.Id)
                            {
                                if (item.ProductId != p.ProductId)
                                {
                                    item.Used = false;
                                    await db.update(item);
                                    break;
                                }
                                else
                                {
                                    item.Used = true;
                                    await db.update(item);
                                    break;
                                }
                            }
                        }
                    }
                    return (new { message = "Success" });
                }
                return (new { message = "Error : Data not valid" });
            }
            catch (Exception x)
            {
                return (new { message = "Error : Execption "+x.InnerException.Message });
            }
        }

        [HttpPost("ReceiveProduct")]
        public async Task<object> ReceiveProduct(UserProduct p)
        {
            try
            {
                if (p.ProductId > 0 && !string.IsNullOrEmpty(p.ApplicationUserId))
                {
                    List<UserProduct> products = db.list().Where(a => a.IsReceived==false && a.ProductId==p.ProductId &&(a.ApplicationUserId == p.ApplicationUserId || a.NewUserId == p.NewUserId)).ToList();// منتجات المستخدمين
                    if (products.Count() == 0)
                    {
                        return (new { message = "Product not available for this user" });
                    }
                    products[0].IsReceived = true;
                    await db.update(products[0]);
                    return (new { message = "Success" });
                }
                return (new { message = "Error : Data not valid" });
            }
            catch (Exception x)
            {
                return (new { message = "Error : Execption " + x.InnerException.Message });
            }
        }
        [HttpPost("UnUsingProduct")]
        public async Task<object> UnUsingProduct(UserProduct p)
        {
            try
            {
                if (p.ProductId > 0 && !string.IsNullOrEmpty(p.ApplicationUserId))
                {
                    List<UserProduct> products = db.list().Where(a => a.ApplicationUserId == p.ApplicationUserId).ToList();   
                    if (products.Where(a => a.ProductId == p.ProductId).Count() == 0)
                    {
                        return (new { message = "Product not available for this user" });
                    }
                    List<Product> _product = productdb.list();  
                    int catId = _product.FirstOrDefault(a => a.Id == p.ProductId).CategoryId;
                    foreach (var prod in _product.Where(a => a.CategoryId == catId).ToList())
                    {
                        foreach (var item in products)
                        {
                            if (item.ProductId == prod.Id)
                            {
                                    item.Used = false;
                                    await db.update(item);
                                    break;
                            }
                        }
                    }
                    return (new { message = "Success" });
                }
                return (new { message = "Error : Data not valid" });
            }
            catch (Exception x)
            {
                return (new { message = "Error : Execption " + x.InnerException.Message });
            }
        }
    }
}