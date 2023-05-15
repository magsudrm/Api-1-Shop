using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Api.Apps.AdminApi.Dtos
{
	public class BrandDto
	{
		[MaxLength(30)]
		public string Name { get; set; }
	}
}
