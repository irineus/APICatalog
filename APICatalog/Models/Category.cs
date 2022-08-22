using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(maximumLength: 80, MinimumLength = 1, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
    public string? Name { get; set; }

    [Display(Name = "URL da Imagem")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(maximumLength: 300, MinimumLength = 8, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
    public string? ImageURL { get; set; }
    
    public ICollection<Product> Products { get; set; }

    public Category()
    {
        Products = new Collection<Product>();
    }
}
