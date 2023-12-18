using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeV2.Model
{
    class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Player player;

        public Cell(int id)
        {
            Player = null;
            Id = id;
        }
        //изменения Player
        public Player Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
                OnPropertyChanged("Player");
            }
        }

        public int Id
        {
            get; private set;
        }
        //че та случилось с Player
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}