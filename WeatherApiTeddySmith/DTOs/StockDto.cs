using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.DTOs
{
    public class StockDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol is too long")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "Company name is too long")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1,10000000)]
        public decimal Purchase { get; set; }     
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry is too long")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}
