using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sub_6.Views
{
    /// <summary>
    /// Interaction logic for ChooseGameModeView.xaml
    /// </summary>
    public partial class ChooseGameModeView : UserControl
    {
        public ChooseGameModeView()
        {
            InitializeComponent();
        }

        public object ChooseTimeGameCommand { get; internal set; }
    }
}
