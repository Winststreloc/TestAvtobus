using System.Security.Cryptography;
using System.Text;
using TestAvtobus.Interfaces;

namespace TestAvtobus.Services;

public class LinkShortenerService : ILinkShortenerService
{
    public string ShortenUrl(string? originalUrl)
    {
        string shortenedUrl = "https://" + GenerateHash(originalUrl);

        return shortenedUrl;
    }

    private string GenerateHash(string? originalUrl)
    {
        var hash = BitConverter.ToString(
            MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(originalUrl))
        ).Replace("-", "").Substring(0, 8);

        return hash;
    }
}