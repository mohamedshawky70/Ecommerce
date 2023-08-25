using Ecommerce.DTOs;
using Ecommerce.Models;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepo<Products> Prepo;
        private readonly IRepo<ProductOptions> Porepo;
        private readonly IRepo<Options> Orepo;
        private readonly ProductsAndOpitonsDTO dto;
        

        public ProductsController(IRepo<Products> Prepo, IRepo<ProductOptions> Porepo, IRepo<Options> Orepo, ProductsAndOpitonsDTO dto)
        {
            this.Prepo = Prepo;
            this.Porepo = Porepo;
            this.Orepo = Orepo;
            this.dto = dto;
        }
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await Prepo.GetById(id));
        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await Prepo.GetAll());
        }
        [HttpGet("GetProductByName")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            return Ok(await Prepo.FindMatch(p=>p.Name==name));
        }
        [HttpGet("GetAllProductsByName")]
        public async Task<IActionResult> GetAllProductsByName(string name)
        {
            return Ok(await Prepo.FindAllMatch(p=>p.Name==name));
        }
        [HttpGet("GetAllProductsByPrice")]
        public async Task<IActionResult> GetAllProductsByName(int price)
        {
            return Ok(await Prepo.FindAllMatch(p=>p.Price==price ));
        }
        [HttpGet("GetAllProductsAndProductsOptonsByName")]
        public async Task<IActionResult> GetAllProductsAndProductsOptonsByName(string name)
        {
            return Ok(await Prepo.FindAllMatchInclude(p => p.Name == name, new[] { "productOptions" } ));
        }
        [HttpGet("GetProductAndProductsOptonsByName")]
        public async Task<IActionResult> GetProductAndProductsOptonsByName(string name)
        {
            //return Ok(await Prepo.FindMatchInclude(p => p.Name == name, new[] { "productOptions" } ));
            //var pro = await Prepo.GetByName(name);
            //var ids=await Prepo.GetAll
            var product=await Prepo.FindMatch(p => p.Name == name);                    //product
            var Productoptions=await Porepo.FindAllMatch(po => po.ProductsId == product.Id);//ProductOptions
            IEnumerable<Options> options = new List<Options>();
            foreach (var item in Productoptions)
            {
                options = await Orepo.FindAllMatch(o => o.Id == item.OptionsId);
            }
            foreach (var item in options)
            {
                dto.Name = item.Name; 
            }
            //var opions = await Orepo.FindMatch(o => o.Id == Productoptions.OptionsId );//Options

            dto.ProductId = product.Id;
            dto.Name = product.Name;
            dto.Price = product.Price;

            //dto.OptionName = opions.Name;
            return Ok(dto);
           

                   
                  

        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm]Products products)
        {
            await Prepo.Add(products);
            return Ok("Succeded add");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Products products)
        {
            await Prepo.Update(products);
            return Ok("Succeded Updated");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Products products)
        {
             Prepo.Delete(products);
            return Ok("Succeded Deleted");
        }
    }
}
