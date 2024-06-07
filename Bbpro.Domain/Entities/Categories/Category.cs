using Bbpro.Domain.Commons;
using Bbpro.Domain.Entities.Multilanguage;

namespace Bbpro.Domain.Entities.Categories;

public class Category : Auditable
{
    public string Title { get; set; }
    public List<CategoryConnectSolution> Solutions { get; set; }
}
