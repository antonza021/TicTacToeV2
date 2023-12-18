using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace TicTacToeV2.Model
{
    class Game
    {
        public ObservableCollection<string> MovesHistory { get; } = new ObservableCollection<string>();
        //дефолт значения игрока
        public Game(int boardSize)
        {
            Board = new Board(boardSize);
            Player = new Player("V");
            Opponent = new Player("Z");
            Winner = null;
            Finished = false;
            MovesHistory = new ObservableCollection<string>();
        }
        //доступы игрока
        public Player Player
        {
            get; private set;
        }

        private Player Opponent
        {
            get; set;
        }

        public Player Winner
        {
            get; private set;
        }

        private Board Board
        {
            get; set;
        }

        public bool Finished
        {
            get; private set;
        }

        public List<Cell> Cells
        {
            get
            {
                return Board.Cells;
            }
        }
        //разрешение на ход
        public bool MakeMove(int cellId)
        {
            Cell cell = Board.Cells[cellId];

            if (cell.Player == null && !Finished)
            {
                Player previousPlayer = Player;
                cell.Player = Player;
                Player = Opponent;
                Opponent = cell.Player;

                MovesHistory.Add($"Ход {Player.Id} в ячейку {cell.Id}");

                Finished = CheckWinCondition();
                return true;
            }

            return false;
        }
        public void UndoLastMove()
        {
            if (MovesHistory.Count > 0)
            {
                string lastMove = MovesHistory[MovesHistory.Count - 1];

                int cellId = GetCellIdFromMoveString(lastMove);

                if (cellId >= 0 && cellId < Board.Cells.Count)
                {
                    Cell cell = Board.Cells[cellId];

                    Opponent = Player;
                    Player = (cell.Player == Opponent) ? Opponent : null;

                    cell.Player = null;

                    Winner = null;

                    MovesHistory.RemoveAt(MovesHistory.Count - 1);

                    Finished = false;
                }
            }
        }

        private int GetCellIdFromMoveString(string move)
        {
            int start = move.IndexOf("в ячейку ") + 9;
            int end = move.Length;
            return int.Parse(move.Substring(start, end - start));
        }

        // чек фор вин 
        private bool CheckWinCondition()
        {
            bool result = true;
            foreach (Cell cell in Cells)
            {
                if (cell.Player == null)
                {
                    result = false;
                    break;
                }
            }


            return HasWon(Player) || HasWon(Opponent) || result;
        }
        //проверка по столбам диагоналям и строкам
        private bool HasWon(Player player)
        {
            bool result;


            for (int row = 0; row < Board.Size; ++row)
            {
                result = true;
                for (int col = 0; col < Board.Size; ++col)
                {
                    if (Cells[row * Board.Size + col] == null ||
                        Cells[row * Board.Size + col].Player == null ||
                        !player.Equals(Cells[row * Board.Size + col].Player))
                    {
                        result = false;
                        break;
                    }
                }
                if (result)
                {
                    Winner = player;
                    return true;
                }
            }

            // Проверяем по столбцам
            for (int col = 0; col < Board.Size; ++col)
            {
                result = true;
                for (int row = 0; row < Board.Size; ++row)
                {
                    if (Cells[row * Board.Size + col] == null ||
                        Cells[row * Board.Size + col].Player == null ||
                        !player.Equals(Cells[row * Board.Size + col].Player))
                    {
                        result = false;
                        break;
                    }
                }
                if (result)
                {
                    Winner = player;
                    return true;
                }
            }

            // Проверяем по диагонали (слева направо)
            result = true;
            for (int cell = 0; cell < Board.Size; ++cell)
            {
                if (Cells[cell * Board.Size + cell] == null ||
                    Cells[cell * Board.Size + cell].Player == null ||
                    !player.Equals(Cells[cell * Board.Size + cell].Player))
                {
                    result = false;
                    break;
                }
            }
            if (result)
            {
                Winner = player;
                return true;
            }

            // Проверяем по диагонали (справа налево)
            result = true;
            for (int cell = 0; cell < Board.Size; ++cell)
            {
                if (Cells[(Board.Size - 1 - cell) * Board.Size + cell] == null ||
                    Cells[(Board.Size - 1 - cell) * Board.Size + cell].Player == null ||
                    !player.Equals(Cells[(Board.Size - 1 - cell) * Board.Size + cell].Player))
                {
                    result = false;
                    break;
                }
            }
            if (result)
            {
                Winner = player;
                return true;
            }

            return false;
        }
    }
}

