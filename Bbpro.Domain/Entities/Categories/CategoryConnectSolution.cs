using Bbpro.Domain.Commons;
using Bbpro.Domain.Entities.Solutions;

namespace Bbpro.Domain.Entities.Categories;

public class CategoryConnectSolution : Auditable
{
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public Solution Solution { get; set; }
    public int SolutionId { get; set; }
}
