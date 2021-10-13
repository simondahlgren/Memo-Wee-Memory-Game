using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media;

namespace Sub_6.Models
{
    public class CardModel
    {
        /// <summary>
        /// Used to prevent a card from being "flipped" twice.
        /// </summary>
        public int CardID { get; set; }

        /// <summary>
        /// Used to determine if two flipped cards are a match.
        /// </summary>
        public int PairID { get; set; }

        /// <summary>
        /// Used to get a CardModel from its corresponding CardView.
        /// </summary>
        public Point Coordinates { get; set; }

        /// <summary>
        /// This property will be null if the card is an image-card. The CardView's CardImage-property will be set from the GenreModel's DefaultAudioImage when audio plays.
        /// </summary>
        public ImageBrush Image { get; set; }

        /// <summary>
        /// This property will be null if the card is an image-card.
        /// </summary>
        public Uri Audio { get; set; }
    }
}
