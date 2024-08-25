using Microsoft.AspNetCore.Mvc;
using web_api.Model;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }
    // GET: api/products
    [HttpGet]
    public IEnumerable<Product> GetProducts()
    {
        return _context.Products.ToList();
    }

    // GET: api/products/1
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {  
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound(); // Return 404 if the product is not found
        }
        return product;
    }

    // POST: api/products
    [HttpPost]
    public ActionResult<Product> PostProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
    }

    // PUT: api/products/1
    [HttpPut("{id}")]
    public IActionResult PutProduct(int id, Product updatedProduct)
    {
        var product= _context.Products.Find(id);
        if(product==null)
            return NotFound();
        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        _context.SaveChanges();
        return NoContent();
    }

    // DELETE: api/products/1
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product=_context.Products.Find(id);
        if (product == null)
            return NotFound();
        _context.Products.Remove(product);
        _context.SaveChanges();
        return NoContent();
    }
}
