using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumeWEBAPI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Proudct Name")]
        public string ProductName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Qty { get; set; }
    }
}
