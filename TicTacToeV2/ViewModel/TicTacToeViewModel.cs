using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TicTacToeV2.Model;

namespace TicTacToeV2.ViewModel
{
    class TicTacToeViewModel : INotifyPropertyChanged
    {
        //размер по умолчанию
        public event PropertyChangedEventHandler PropertyChanged;
        private const int DEFAULT_SIZE = 3;
        private bool isDarkTheme;
        private Game game;


        public TicTacToeViewModel()
        {
            Game = new Game(DEFAULT_SIZE);
            IsDarkTheme = true;
        }
        //доступы
        public Game Game
        {
            get
            {
                return game;
            }
            private set
            {
                game = value;
                OnPropertyChanged("Game");
                OnPropertyChanged("GameFinished");
                OnPropertyChanged("CurrentPlayer");
                OnPropertyChanged("Winner");
                OnPropertyChanged(nameof(ButtonBackground));
                OnPropertyChanged(nameof(ButtonForeground));
            }
        }
        public bool IsDarkTheme
        {
            get { return isDarkTheme; }
            set
            {
                if (isDarkTheme != value)
                {
                    isDarkTheme = value;
                    OnPropertyChanged(nameof(IsDarkTheme));
                    ApplyTheme();
                }
            }
        }

        public SolidColorBrush ButtonBackground
        {
            get { return IsDarkTheme ? new SolidColorBrush(Colors.Gray) : new SolidColorBrush(Colors.LightGray); }
        }

        public SolidColorBrush ButtonForeground
        {
            get { return IsDarkTheme ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black); }
        }

        private void ApplyTheme()
        {
            OnPropertyChanged(nameof(ButtonBackground));
            OnPropertyChanged(nameof(ButtonForeground));
            // Другие логические шаги для изменения темы.
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Player Winner
        {
            get
            {
                return Game.Winner;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return Game.Player;
            }
        }

        public bool GameFinished
        {
            get
            {
                return Game.Finished;
            }
        }
        //кнопки
        public ICommand NewGame
        {
            get
            {
                return new RelayCommand<int>(HandleNewGame);
            }
        }

        public ICommand CellClick
        {
            get
            {
                return new RelayCommand<int>(HandleCellClick);
            }
        }
        //нью гей м
        public void HandleNewGame(int boardSize)
        {
            Game = new Game(boardSize);
        }
        //обработка нажатия
        public void HandleCellClick(int cellId)
        {
            if (Game.MakeMove(cellId))
            {
                OnPropertyChanged("CurrentPlayer");
                if (Game.Finished)
                {
                    OnPropertyChanged("GameFinished");
                    OnPropertyChanged("Winner");
                }
            }
        }
        public ICommand UndoMove
        {
            get
            {
                return new RelayCommand<object>(HandleUndoMove, CanUndoMove);
            }
        }

        private bool CanUndoMove(object parameter)
        {
            // Разрешить отмену хода только если есть ходы в истории
            return Game.MovesHistory.Count > 0 && !Game.Finished;
        }

        private void HandleUndoMove(object parameter)
        {
            Game.UndoLastMove();
            OnPropertyChanged("CurrentPlayer");
            OnPropertyChanged("GameFinished");
            OnPropertyChanged("Winner");
            OnPropertyChanged("MovesHistory");
        }
        public ICommand ToggleTheme
        {
            get
            {
                return new RelayCommand<object>(HandleToggleTheme);
            }
        }

        private void HandleToggleTheme(object parameter)
        {
            IsDarkTheme = !IsDarkTheme;
        }
    }
}
