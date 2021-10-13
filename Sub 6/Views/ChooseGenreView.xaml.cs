
using GalaSoft.MvvmLight.Messaging;
using Sub_6.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sub_6.Views
{
    /// <summary>
    /// Interaction logic for ChooseGenreView.xaml
    /// </summary>
    public partial class ChooseGenreView : UserControl
    {
        public ChooseGenreView()
        {
            InitializeComponent();
            Storyboard stbMove = (Storyboard)FindResource("ContentReveal");
            stbMove.Begin();
        }

       

       
    }
}
