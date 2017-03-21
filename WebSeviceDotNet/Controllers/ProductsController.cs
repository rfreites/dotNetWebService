using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSeviceDotNet.Models;

namespace WebSeviceDotNet.Controllers
{
    [Produces("application/json")]
    //[Produces("application/xml")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private static List<Product> _products = new List<Product>(new[] {
                new Product() { ID = 1, Name = "Fanta", Quantity = 3 },
                new Product() { ID = 2, Name = "Pepsi", Quantity = 4 },
                new Product() { ID = 3, Name = "Colita", Quantity = 10 },
                new Product() { ID = 4, Name = "Leche", Quantity = 8 },
                new Product() { ID = 5, Name = "Azucar", Quantity = 11 }
            });

        //[HttpGet]
        //public string Get() => "Hello API";


        [HttpGet]
        public List<Product> Get()
        {
            return _products; //pretend to go to the database
        }

        [HttpGet("{id}")] //capture route parameter
        public IActionResult Get(int id)
        {
            var product = _products.SingleOrDefault(p => p.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            _products.Add(product);

            return CreatedAtAction(nameof(Get),
                new { id = product.ID }, product);
        }

    }
}