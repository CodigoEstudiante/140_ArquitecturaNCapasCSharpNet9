using _2.BusinessLayer;
using _3.DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductBL _productBL;

        public ProductController(ProductBL productBL)
        {
            _productBL = productBL;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productBL.GetAll();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Post(Product value)
        {
            try
            {
                var created = _productBL.Create(value);

                return created == true ? StatusCode(201, new { message = "ok" }) :
                    StatusCode(409, new { message = "error" });
            }
            catch (ArgumentException ex)
            {
                // Retornar BadRequest (400) para errores de validación
                return BadRequest(new { message = ex.Message, detail = "" });
            }
            catch (Exception ex) {
                // Retornar InternalServerError (500) para otros errores
                return StatusCode(500, new { message = "Error interno al crear el producto", detail = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult Put(Product value)
        {
            try
            {
                var edited = _productBL.Edit(value);

                return edited == true ? StatusCode(200, new { message = "ok" }) :
                    StatusCode(409, new { message = "error" });
            }
            catch (ArgumentException ex)
            {
                // Retornar BadRequest (400) para errores de validación
                return BadRequest(new { message = ex.Message, detail = "" });
            }
            catch (Exception ex)
            {
                // Retornar InternalServerError (500) para otros errores
                return StatusCode(500, new { message = "Error interno al editar el producto", detail = ex.Message });
            }
        }

        //[HttpDelete("{idProduct:int}")]
        [HttpDelete]
        public IActionResult Delete(int idProduct)
        {
            try
            {
                var deleted = _productBL.Delete(idProduct);

                return deleted == true ? StatusCode(200, new { message = "ok" }) :
                    StatusCode(409, new { message = "error" });
            }
            catch (ArgumentException ex)
            {
                // Retornar BadRequest (400) para errores de validación
                return BadRequest(new { message = ex.Message, detail = "" });
            }
            catch (Exception ex)
            {
                // Retornar InternalServerError (500) para otros errores
                return StatusCode(500, new { message = "Error interno al eliminar el producto", detail = ex.Message });
            }
        }
    }
}
