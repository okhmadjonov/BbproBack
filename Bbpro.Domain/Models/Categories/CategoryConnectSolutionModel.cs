using Bbpro.Domain.Entities.Categories;
using Bbpro.Domain.Models.Projects;
using Bbpro.Domain.Models.Solutions;

namespace Bbpro.Domain.Models.Categories;
    


public class CategoryConnectSolutionModel
{
    public SolutionModel SolutionModel { get; set; }
    public CategoryModel CategoryModel { get; set; }
    public int CategoryId { get; set; }

    public virtual CategoryConnectSolutionModel MapFromEntity(CategoryConnectSolution entity, int categoryId)
    {
        SolutionModel = new SolutionModel().MapFromEntity(entity.Solution, categoryId);
        CategoryModel = entity.Category != null ? new CategoryModel().MapFromEntity(entity.Category) : null;
        CategoryId = categoryId;
        return this;
    }
}
