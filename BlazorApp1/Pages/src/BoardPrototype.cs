namespace BlazorApp1.Pages.src
{
    public interface IBoardPrototype
    {
        string Clone();

        void Save();

        string Parse();
    }
}
