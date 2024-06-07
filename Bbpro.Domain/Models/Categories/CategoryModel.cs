using Bbpro.Domain.Entities.Categories;
using Bbpro.Domain.Entities.Multilanguage;
using Bbpro.Domain.Models.Projects;
using Bbpro.Domain.Models.Solutions;

namespace Bbpro.Domain.Models.Categories;


public class CategoryModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    //public List<SolutionModel> Solutions { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual CategoryModel MapFromEntity(Category entity)
    {
        return new CategoryModel
        {
            Id = entity.Id,
            Title = entity.Title,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
