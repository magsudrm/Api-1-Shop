using Api.Apps.AdminApi.Dtos;
using Core.Entities;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Apps.AdminApi.Controllers
{
	[Route("admin/api/[controller]")]
	[ApiController]
	public class BrandsController : ControllerBase
	{
		private readonly ShopDbContext _context;
		public BrandsController(ShopDbContext context) 
		{
			_context=context;
		}

		[HttpGet("")]
		public IActionResult GetAll()
		{
			var data = _context.Brands.ToList();
			List<BrandGetAllItemDto> items = data.Select(x => new BrandGetAllItemDto
			{
				Id= x.Id,
				Name= x.Name,
			}).ToList();
			return Ok(items);
		}

		[HttpPost("")]
		public IActionResult Create(BrandDto brandDto)
		{
			if(_context.Brands.Any(x=>x.Name== brandDto.Name))
			{
				ModelState.AddModelError("Name", "Brand Already Exist");
				return BadRequest(ModelState);
			}
			Brand brand= new Brand { Name= brandDto.Name };
			_context.Brands.Add(brand);
			_context.SaveChanges(); 
			return Ok(brandDto);
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var data = _context.Brands.Include(x=>x.Products).FirstOrDefault(x=>x.Id==id);
			if(data==null) { NotFound(); }
			BrandGetDto getDto= new BrandGetDto { Name= data.Name ,Id=data.Id,ProductCount=_context.Products.Count()};
			return Ok(getDto);
		}

		[HttpPost("{id}")]
		public IActionResult Update(int id,BrandDto brandDto)
		{
			var existBrand= _context.Brands.Find(id);
			if (existBrand==null) { NotFound(); }
			if(existBrand.Name!= brandDto.Name && _context.Brands.Any(x => x.Name == brandDto.Name))
			{
				ModelState.AddModelError("Name", "Brand Already Exist");
				return BadRequest(ModelState);
			}
			existBrand.Name= brandDto.Name;
			_context.SaveChanges();
			return Ok(existBrand);
		}

		[HttpDelete("{id")]
		public IActionResult Delete(int id)
		{
			var data = _context.Brands.Find(id);
			if (data == null) { NotFound(); }
			_context.Brands.Remove(data);
			_context.SaveChanges();
			return NoContent();
		}
	}
}
