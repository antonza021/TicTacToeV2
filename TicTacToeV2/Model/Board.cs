using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace TicTacToeV2.Model
{
    class Board
    {
        public ObservableCollection<string> MovesHistory { get; } = new ObservableCollection<string>();

        //размер игровой доски
        public Board(int size)
        {
            Size = size;
            Cells = new List<Cell>();
            for (int i = 0; i < size * size; ++i)
            {
                Cells.Add(new Cell(i));
            }
        }
        //size-доступ к размеру доски, Cells-доступ к листу ячеек
        public int Size
        {
            get; set;
        }

        public List<Cell> Cells
        {
            get; private set;
        }
    }
}
