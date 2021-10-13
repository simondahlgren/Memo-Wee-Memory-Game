using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sub_6.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        #region Variables
        public int score;
        #endregion

        #region Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsTop5 { get; set; }
        public int Rank { get; set; }
        public bool IsMatch { get; set; }
        public int Score { get => score; set => score = value; }
        //String for color to borderbrush.
        public string BorderColor
        {
            get
            {
                if (IsMatch == true)
                {
                    return "DeepPink";
                }
                else
                {
                    return null;
                }
            }
            set { OnPropertyChanged("BorderColor"); }
        }
     
        // String to color text when you get a match.
        public string BackgroundMatchBox
        {
            get
            {
                if (IsMatch == true)
                {
                    return "GreenYellow";
                }
                else
                {
                    return null;
                }
            }
            set { OnPropertyChanged("BackgroundMatchBox"); }

        }
        #endregion

        #region Methods
        /// <summary>
        /// Displays Properties in list.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Rank}. {Name}  {""}  {Score} P";
        }
        public string MatchText
        {
            get
            {
                if (IsMatch == true)
                {
                    return "It's a match! Good job!";
                }
                else
                {
                    return "Try again!";
                }
            }
            set { OnPropertyChanged("MatchText"); }
        }
        #endregion
    }
}
