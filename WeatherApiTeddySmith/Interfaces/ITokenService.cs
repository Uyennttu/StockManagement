using WeatherApiTeddySmith.Models;

namespace WeatherApiTeddySmith.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
