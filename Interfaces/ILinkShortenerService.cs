namespace TestAvtobus.Interfaces;

public interface ILinkShortenerService
{
    string ShortenUrl(string? originalUrl);
}