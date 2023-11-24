namespace TestAvtobus.Interfaces;

public interface ICounterLinkService
{
    Task<string> CountJumpLink(long id);
}