namespace BlazorApp1.Pages.src
{
    public interface IBoardFactory
    {
        GameBoard CreateBoard();
    }

    public class SavedBoardFactory : IBoardFactory
    {
        private const string _path = "D:\\Microsoft Visual Studio\\Projects\\OP_2nd_Course\\Course_work\\BlazorApp1.Pages\\BlazorApp1.Pages\\data\\save.json";

        public GameBoard CreateBoard()
        {
            string json = String.Empty;

            using (StreamReader sr = new StreamReader(_path))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    json += line;
                }
            }

            return new DefaultBoard(json);
        }
    }

    public class BoardFactory : IBoardFactory
    {
        public GameBoard CreateBoard()
        {
            return new DefaultBoard();
        }
    }
}
