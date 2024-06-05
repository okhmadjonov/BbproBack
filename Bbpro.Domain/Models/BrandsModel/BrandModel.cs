using Bbpro.Domain.Entities.Brands;

namespace Bbpro.Domain.Models.BrandsModel;


public class BrandModel
{
    public int Id { get; set; }
    public List<string> ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual BrandModel MapFromEntity(Brand entity)
    {
        return new BrandModel
        {
            Id = entity.Id,
            ImageUrl = entity.ImageUrl,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
