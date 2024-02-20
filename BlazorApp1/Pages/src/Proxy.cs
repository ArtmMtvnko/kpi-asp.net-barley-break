namespace BlazorApp1.Pages.src
{
    public class DefaulBoardController : GameBoard
    {
        private GameBoard _board;
        private BackupCaretaker _backupCaretaker;

        public GameBoard Board => _board;

        public void SetBoard(GameBoard board) { _board = board; }

        public DefaulBoardController(GameBoard board)
        {
            _board = board;
            _backupCaretaker = new BackupCaretaker(board);
        }

        public override void Move(int x, int y, int toX = 0, int toY = 0)
        {
            x -= 1;
            y -= 1;
            if (_board.Board[y][x] == 0)
            {
                throw new Exception("You try to move empty square");
            }
            else if (y < 3 && _board.Board[y + 1][x] == 0)
            {
                _backupCaretaker.Backup();
                _board.Move(x, y, x, y + 1);
            }
            else if (y > 0 && _board.Board[y - 1][x] == 0)
            {
                _backupCaretaker.Backup();
                _board.Move(x, y, x, y - 1);
            }
            else if (x < 3 && _board.Board[y][x + 1] == 0)
            {
                _backupCaretaker.Backup();
                _board.Move(x, y, x + 1, y);
            }
            else if (x > 0 && _board.Board[y][x - 1] == 0)
            {
                _backupCaretaker.Backup();
                _board.Move(x, y, x - 1, y);
            }
            else
            {
                throw new Exception("Use can't move this square because there are no empty squares around");
            }
        }

        public void ShowBackups()
        {
            _backupCaretaker.ShowHistory();
        }

        public void Undo()
        {
            _backupCaretaker.Undo();
        }
    }
}
