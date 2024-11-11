using Microsoft.AspNetCore.Identity;

namespace WeatherApiTeddySmith.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
