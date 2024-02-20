using System.Text.Json;

namespace BlazorApp1.Pages.src
{
    public abstract class GameBoard : IBoardPrototype, IObserve
    {
        protected const string _path = "D:\\Microsoft Visual Studio\\Projects\\OP_2nd_Course\\Course_work\\BlazorApp1.Pages\\BlazorApp1.Pages\\data\\save.json";

        protected List<List<int>> _board;

        public List<List<int>> Board => _board;

        private List<IObserver> _observers = new List<IObserver>();

        public string Clone()
        {
            return JsonSerializer.Serialize(this);
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(this._board);
            using (StreamWriter sr = new StreamWriter(_path))
            {
                sr.WriteLine(json);
            }
        }

        public string Parse()
        {
            using (StreamReader sr = new StreamReader(_path))
            {
                string output = String.Empty;
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    output += line;
                }

                return output;
            }
        }

        public void PrintBoard()
        {
            foreach (List<int> list in _board)
            {
                foreach (int number in list)
                {
                    if (number < 10)
                    {
                        Console.Write(" {0} ", number);
                    }
                    else
                    {
                        Console.Write("{0} ", number);
                    }
                }
                Console.WriteLine();
            }
        }

        public bool IsGameEnded()
        {
            int size = _board.Count;
            int lastEmptySquare = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (_board[i][j] != (i * size + j + 1))
                    {
                        if (_board[size - 1][size - 1] == lastEmptySquare && i == size - 1 && j == size - 1)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }

            return true;
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update(this);
            }
        }

        public abstract void Move(int fromX, int fromY, int toX, int toY);

        public IMemento CreateBackup()
        {
            return new CurrentBoardMemento(this);
        }

        public void Restore(IMemento memento)
        {
            _board = memento.State;
        }
    }


    public class DefaultBoard : GameBoard
    {
        public DefaultBoard()
        {
            _board = new List<List<int>>();

            Random random = new Random();

            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };

            for (int i = 0; i < numbers.Count; i++)
            {
                int randomIndex = random.Next(i, numbers.Count);

                int temp = numbers[i];
                numbers[i] = numbers[randomIndex];
                numbers[randomIndex] = temp;
            }

            _board.Add(new List<int>() { numbers[0], numbers[1], numbers[2], numbers[3] });
            _board.Add(new List<int>() { numbers[4], numbers[5], numbers[6], numbers[7] });
            _board.Add(new List<int>() { numbers[8], numbers[9], numbers[10], numbers[11] });
            _board.Add(new List<int>() { numbers[12], numbers[13], numbers[14], numbers[15] });
        }

        public DefaultBoard(string json)
        {
            _board = JsonSerializer.Deserialize<List<List<int>>>(json);
        }

        public override void Move(int fromX, int fromY, int toX, int toY)
        {
            // TODO: may be move backup caretaker here and save snapshot before we move elements
            int temp = _board[toY][toX];
            _board[toY][toX] = _board[fromY][fromX];
            _board[fromY][fromX] = temp;
            Notify();
        }
    }
}
