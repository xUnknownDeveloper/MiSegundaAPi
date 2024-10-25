using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MiPrimeraAPi.Models;
using System.Reflection.Metadata.Ecma335;

namespace MiPrimeraAPi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductosController : Controller
    {
        private static List<Producto> productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Laptop", Precio = 1500},
            new Producto { Id = 2, Nombre = "Mouse", Precio = 25}
        };

        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetProducts()
        {
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetProducto(int id)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPost]
        public ActionResult<Producto> CrearProducto(Producto nuevoProducto)
        {
            nuevoProducto.Id = productos.Max(p => p.Id) + 1;
            productos.Add(nuevoProducto);
            return CreatedAtAction(nameof(GetProducto), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarProducto(int id, Producto productoActualizado)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            producto.Nombre = productoActualizado.Nombre;
            producto.Precio = productoActualizado.Precio;
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult EliminarPRoducto(int id)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            productos.Remove(producto);
            return NoContent();
        }
    }
}
