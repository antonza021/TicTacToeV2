using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TicTacToeV2.Model;

namespace TicTacToeV2
{
    class TextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string currentPlayerText = values[0] != null ? ((Player)values[0]).Id + " Ходи" : string.Empty;
            string winnerText = values[1] != null ? ((Player)values[1]).Id + " Победил!" : " Ничья!";
            bool gameFinished = (bool)values[2];
            return gameFinished ? winnerText : currentPlayerText;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
