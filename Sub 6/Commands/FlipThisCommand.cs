using Sub_6.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace Sub_6.Commands
{
    public class FlipThisCommand : ICommand
    {
        private readonly GameViewModel gameViewModel;

        public FlipThisCommand(GameViewModel gVM)
        {
            this.gameViewModel = gVM;
        }
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        /// <param name="parameter">Parameter is CardView.ID.</param>
        public void Execute(object parameter)
        {
            var cardView = gameViewModel.CardsOnBoard[(int)parameter];
            gameViewModel.FlipCard(cardView);
            gameViewModel.OnPropertyChanged(nameof(gameViewModel.CardsOnBoard));
        }
    }
}
