using System.ComponentModel.DataAnnotations;
using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.DTOs
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="Title is too short")]
        [MaxLength(250, ErrorMessage = "Title is too long")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content is too short")]
        [MaxLength(250, ErrorMessage = "Content is too long")]
        public string Content { get; set; } = string.Empty;

    }
}
