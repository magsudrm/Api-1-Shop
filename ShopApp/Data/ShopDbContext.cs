using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Data
{
	public class ShopDbContext:DbContext
	{
		public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

		public DbSet<Brand> Brands { get; set; }
		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrandConfiguration).Assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
