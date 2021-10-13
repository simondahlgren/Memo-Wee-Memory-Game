using Sub_6.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sub_6.Stores
{
    public class NavigationStore
    // use of navigationstore and main logic behind navigation originated from SingeltonSean: https://www.youtube.com/playlist?list=PLA8ZIAm2I03ggP55JbLOrXl6puKw4rEb2
    {
        //event to alert UI to a change in current view 
        public event Action CurrentViewModelChanged;

        private GameViewModel _currentViewModel;
        
        //Sets the currentviewmodel to the value that is sent as <> property in the navigation command
        public GameViewModel CurrentViewModel 
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            //if viewmodel changes, invoke to all subscribers of event (in our case MainWindow binding)
            CurrentViewModelChanged?.Invoke();
        }
    }
}
