using Bbpro.Domain.Entities.Multilanguage;
using Bbpro.Domain.Entities.Solutions;

namespace Bbpro.Domain.Models.Solutions;

public  class SolutionFilterCategoryModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public Language Title { get; set; }
    public Language Description { get; set; }
  
   
    public virtual SolutionFilterCategoryModel MapFromEntity(Solution? entity)
    {
        Id = entity.Id;
        ImageUrl = entity.ImageUrl;
        Title = entity.Title;
        Description = entity.Description;
        return this;
    }
}
