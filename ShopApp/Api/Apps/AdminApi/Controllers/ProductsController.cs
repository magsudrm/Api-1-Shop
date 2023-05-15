using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Apps.AdminApi.Controllers
{
	[Route("admin/api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly ShopDbContext _context;

		public ProductsController(ShopDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var data = _context.Products.ToList();
			return Ok(data);
		}

		[HttpGet("{id")]
		public IActionResult Get(int id)
		{
			var data = _context.Products.Find(id);
			if (data == null)
			{
				return NotFound();
			}
			return Ok(data);
		}
	}
}
