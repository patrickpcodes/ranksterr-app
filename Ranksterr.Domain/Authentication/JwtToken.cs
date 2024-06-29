namespace Ranksterr.Domain.Authentication;

public class JwtToken
{
    public string AccessToken { get; set; }
    public int ExpiryInMinutes { get; set; }
    public DateTime ExpiryDate { get; set; }
}