using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.api.Dto;
using web.api.Service;

namespace web.api.Controllers
{
    [Route("api/[Controller]")]
    public class ProductController:Controller
    {
        private readonly ILogger<ProductController> logger;
        private readonly IMailService localMailService1;

        public ProductController(ILogger<ProductController> logger,IMailService localMailService)
        {
            this.logger = logger;
            localMailService1 = localMailService;
        }

        [HttpGet]
        public IActionResult GetProduct()
        {

            return Json(ProductService.Current.Products);
        }

        [HttpGet]
        [Route("{id}",Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            try
            {
               
                var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
                if (product == null)
                {
                    logger.LogInformation("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception)
            {
                logger.LogCritical("cccccccccccccccccccccccccccc");
                return StatusCode(500, "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            }
            
            

        }

        [HttpPost("create")]
        public IActionResult Post([FromBody] ProductCreation product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maxId = ProductService.Current.Products.Max(x => x.Id);
            var newProduct = new ProductDto
            {
                Id = ++maxId,
                Name = product.Name,
                Price = product.Price
            };
            ProductService.Current.Products.Add(newProduct);

           

            //newProduct 为要返回到界面的值
            //路由名称对应方法的参数列表
            //nameof(GetProduct)路由名称 这么设定[Route(Name = "GetProduct")]
            return CreatedAtRoute(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }


        [HttpPut("put/{id}")]
        public IActionResult Put(int id, [FromBody] ProductModification product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (product.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            model.Name = product.Name;
            model.Price = product.Price;

            // return Ok(model);
            return NoContent();
        }


        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<ProductModification> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            var toPatch = new ProductModification
            {
                Name = model.Name,
               
                Price = model.Price
            };

            //更新
            patchDoc.ApplyTo(toPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (toPatch.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }
            TryValidateModel(toPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Name = toPatch.Name;
           
            model.Price = toPatch.Price;

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            ProductService.Current.Products.Remove(model);
            localMailService1.Send("Product Deleted", $"Id为{id}的产品被删除了");
            return NoContent();
        }

    }

}
