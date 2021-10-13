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
    /// Interaction logic for CardView.xaml
    /// </summary>
    public partial class CardView : UserControl
    {
        /// <summary>
        /// Used as commandparameter in FlipThisCommand.
        /// </summary>
        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(int), typeof(CardView), new PropertyMetadata(0));
        
        /// <summary>
        /// Sets gridcolumn location through binding. Is - together with Y - used to match CardView to its corresponding CardModel.
        /// </summary>
        public int X
        {
            get { return (int)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }


        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(int), typeof(CardView), new PropertyMetadata(0));

        /// <summary>
        /// Sets gridrow location through binding. Is - together with X - used to match CardView to its corresponding CardModel.
        /// </summary>
        public int Y
        {
            get { return (int)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }


        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(int), typeof(CardView), new PropertyMetadata(0));

        public ImageBrush CardImage
        {
            get { return (ImageBrush)GetValue(CardImageProperty); }
            set { SetValue(CardImageProperty, value); }
        }


        public static readonly DependencyProperty CardImageProperty =
            DependencyProperty.Register("CardImage", typeof(ImageBrush), typeof(CardView), new PropertyMetadata(null));

        public CardView()
        {
            InitializeComponent();
        }
    }
}
