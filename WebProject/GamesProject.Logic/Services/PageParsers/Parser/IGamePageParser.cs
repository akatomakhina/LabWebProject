namespace GamesProject.Logic.Services.PageParsers
{
    public interface IGamePageParser
    {
        string Parse(string html);

        string Host { get; }
    }
}
