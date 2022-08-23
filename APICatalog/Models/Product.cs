using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(maximumLength: 80, MinimumLength = 1, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
    public string? Name { get; set; }

    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(maximumLength: 300, MinimumLength = 8, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
    public string? Description { get; set; }

    [Display(Name = "Preço")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Column(TypeName = "decimal(10,2)")]
    [Range(100.0, 50000.0, ErrorMessage = "O campo {0} deve ter um valor entre {1} e {2}")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Display(Name = "URL da Imagem")]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(maximumLength: 300, MinimumLength = 8, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
    public string? ImageURL { get; set; }

    [Display(Name = "Estoque")]
    public float Inventory { get; set; }

    [Display(Name = "Data de Criação")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime CreateDate { get; set; }
    
    public int CategoryId { get; set; }

    [JsonIgnore]
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}
