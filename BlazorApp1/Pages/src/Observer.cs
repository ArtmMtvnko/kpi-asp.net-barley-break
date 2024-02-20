namespace BlazorApp1.Pages.src
{
    public interface IObserver
    {
        void Update(GameBoard gameBoard);
    }

    public interface IObserve
    {
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify();
    }

    public class MoveObserver : IObserver
    {
        public void Update(GameBoard gameBoard)
        {
            gameBoard.Save();
        }
    }
}
