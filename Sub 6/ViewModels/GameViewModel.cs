using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sub_6.Models;
using Sub_6.Views;
using GalaSoft.MvvmLight.Messaging;
using Sub_6.Messages;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Sub_6.Stores;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Sub_6.Commands;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Sub_6.ViewModels
{
    public class GameViewModel : BaseViewModel
    {

        #region Objects
        public int genreID;
        protected readonly Random random;
        public int score = 0;
        readonly TimerStore timerStore = new TimerStore();
        public GenreModel genreModel;
        public TimerStore _timerStore;
        public ObservableCollection<CardView> cardsOnBoard = new ObservableCollection<CardView>();
        public List<CardModel> flippedCards = new List<CardModel>();
        public List<CardView> displayedCards = new List<CardView>();
        public MediaPlayer mediaPlayer = new MediaPlayer();
        #endregion

        #region Properties
        public CardsModel Cards { get; set; }
        public PlayerViewModel Player { get; set; } = new PlayerViewModel();
        public string BackGroundByGenre
        {
            get
            {
                genreModel = new GenreModel(genreID);
                SetBoard();
                return genreModel.Background;
            }
            set { OnPropertyChanged("BackGroundByGenre"); }
        }
        public ObservableCollection<CardView> CardsOnBoard
        {
            get
            {
                return cardsOnBoard;
            }
            set {; }
        }
        public Uri Gif1 { get; set; }
        public Uri Gif2 { get; set; }
        public Uri GifCel1 { get; set;}
        public Uri GifCel2 { get; set; }
        public string MatchOrNoMatch { get; set; }
        public string BorderColorMatch { get; set; }
        public string BackgroundMatchBox { get; set; }
        #endregion

        #region Commands
        public ICommand FlipThisCommand { get; }
        #endregion

        #region ctor
        public GameViewModel()
        {
            Messenger.Default.Register<GenreMessage>(this, (genreMessage) =>
            {
                genreID = genreMessage.GenrePick;

            });
            random = new Random();
            GenreModel genreModel = new GenreModel(genreID);
            FlipThisCommand = new FlipThisCommand(this);
            _timerStore = timerStore;
        }
        protected int GenreID
        {
            set { genreID = value; }
            get {return genreID; }
        }

        #endregion

        #region Background music
        public async void PlayBackgroundMusic()
        {
            var backroundmusic = Cards.GetBackgroundMusics();
            mediaPlayer.Open(backroundmusic.Audio);
            mediaPlayer.Play();

            await Task.Delay(500);
            mediaPlayer.Volume = 0.45;
            await Task.Delay(250);
            mediaPlayer.Volume = 0.4;
            await Task.Delay(125);
            mediaPlayer.Volume = 0.35;
            await Task.Delay(64);
            mediaPlayer.Volume = 0.3;
            await Task.Delay(32);
            mediaPlayer.Volume = 0.25;
            await Task.Delay(16);
            mediaPlayer.Volume = 0.2;
            await Task.Delay(8);
            mediaPlayer.Volume = 0.15;
            await Task.Delay(4);
            mediaPlayer.Volume = 0.1;
            await Task.Delay(2);
            mediaPlayer.Stop();
        }


        public void StopMusic()
        {
            {
                mediaPlayer.Dispatcher.Invoke(() =>
                {
                    mediaPlayer.Stop();
                });

            }

        }

        #endregion

        #region SettingUpGame
        /// <summary>
        /// Sets the board by getting cards from the database and filling the gameboard with 24 CardViews with unique coordinates.
        /// </summary>
        public void SetBoard()
        {
            Cards = new CardsModel(genreID);
            timerStore.Start(120);
            int i = 0;
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    CardView card = new CardView
                    {
                        ID = i,
                        X = x,
                        Y = y,
                        CardImage = genreModel.BackOfCard
                    };
                    cardsOnBoard.Add(card);
                    i++;
                }
            }
        }

        #endregion
        
        #region Game
        public CardModel GetCorrespondingCardModel(CardView cardView) //gets the chosen CardView's corresponding CardModel by matching placement in grid to coordinates
        {
            return Cards.AllCards.Where(c => c.Coordinates.X == cardView.X && c.Coordinates.Y == cardView.Y).FirstOrDefault();
        }

        /// <summary>
        /// Runs methods in a specific sequence to flip cards, display content and check if match.
        /// </summary>
        /// <param name="cardView">The clicked CardView that player wants to flip.</param>
        public void FlipCard (CardView cardView)
        {
            CardModel cardModel = GetCorrespondingCardModel(cardView);
            if (IsFlippedCardsFull() == true && IsDisplayedCardsFull() == true)
            {
                mediaPlayer.Stop();
                SetPreviousPair(); //only runs if "flippedCards" and "displayedCards" are full
            }
            AddToFlippedCards(cardModel);
            AddToDisplayedCards(cardView);
            DisplayMedia(cardView, cardModel);
            if (IsFlippedCardsFull() == true && IsDisplayedCardsFull() == true)
            {
                IsPair();
            }
            FinalPair();
        }

        /// <summary>
        /// Checks if flippedCards is full. The list is allowed to hold max 2 items.
        /// </summary>
        /// <returns>True if full, false if list holds less than 2 items.</returns>
        public bool IsFlippedCardsFull ()
        {
            if (flippedCards.Count == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  Checks if displayedCards is full.The list is allowed to hold max 2 items.
        /// </summary>
        /// <returns>True if full, false if list holds less than 2 items.</returns>
        public bool IsDisplayedCardsFull()
        {
            if (displayedCards.Count == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks player.IsMatch. Removes matching pair from AllCards and sets CardImage to null if match. Sets CardImage to "flip back" if no match. Always clears flippedCards and displayedCards.
        /// </summary>
        public void SetPreviousPair() 
        {
            if (Player.IsMatch == true)
            {
                Cards.AllCards.RemoveAll(cm => cm.PairID == flippedCards[0].PairID);
                foreach (var cardView in cardsOnBoard)
                {
                    if (cardView.ID == displayedCards[0].ID || cardView.ID == displayedCards[1].ID) 
                    {
                        cardView.CardImage = null;
                    }
                }
                flippedCards.Clear(); 
                displayedCards.Clear();
            }
            else
            {
                foreach (var cardView in cardsOnBoard)
                {
                    if (cardView.ID == displayedCards[0].ID || cardView.ID == displayedCards[1].ID) //unmatched pair's cardViews' CardImage-property are set to BackOfCard-image
                    {
                        cardView.CardImage = genreModel.BackOfCard;
                    }
                }
                flippedCards.Clear();
                displayedCards.Clear();
            }
            OnPropertyChanged(nameof(CardsOnBoard));
        }

        /// <summary>
        /// CardModel that corresponds to the clicked CardView is added to flippedCards. Won't add if same card is clicked twice.
        /// </summary>
        /// <param name="cardModel"></param>
        public void AddToFlippedCards(CardModel cardModel)
        {
            if (flippedCards.Count == 0)
            {
                flippedCards.Add(cardModel);
            }
            else if (cardModel.CardID == flippedCards[0].CardID) //if the same card is clicked more than once nothing happens
            {

            }
            else
            {
                flippedCards.Add(cardModel);
            }
        }

        /// <summary>
        /// Clicked CardView is added to displayedCards. Won't add if same card is clicked twice.
        /// </summary>
        /// <param name="cardView"></param>
        public void AddToDisplayedCards(CardView cardView) //clicked CardView is added to "displayedCards"
        {
            if (displayedCards.Count == 0)
            {
                displayedCards.Add(cardView);
            }
            else if (cardView.ID == displayedCards[0].ID) //if the same card is clicked more than once nothing happens
            {

            }
            else
            {
                displayedCards.Add(cardView);
            }
        }

        /// <summary>
        /// Sets player.IsMatch to true or false. Runs AddToScore() if true. Runs DisplayMatchOrNoMatch().
        /// </summary>
        public void IsPair ()
        {
        if (flippedCards[0].PairID == flippedCards[1].PairID)
            {
                Player.IsMatch = true;
                AddToScore();
                DisplayMatchOrNoMatch();
            }
            else
            {
                Player.IsMatch = false;
                DisplayMatchOrNoMatch();
            }
        }

        /// <summary>
        /// Adds points to player's score, virtual for override in TimeGameViewModel
        /// </summary>
        public virtual void AddToScore()
        {
            score += 10;
            Player.Score = score;
            OnPropertyChanged("ScoreBind");
        }

        /// <summary>
        /// When two cards are left on the board they are a pair and AllCards is cleared after a 2,5 s delay - activates TimeIsUp().
        /// </summary>
        public virtual async void FinalPair()

        {
            if (Cards.AllCards.Count == 2 && flippedCards.Count ==2)
            {
                Player.IsMatch = true;
                AddToScore();
                DisplayMatchOrNoMatch();
                await Task.Delay(2500);
                Cards.AllCards.Clear();
                mediaPlayer.Stop();
            }
        }
        #endregion

        #region Displays

        /// <summary>
        /// Plays audio and sets a genre specific default image to CardImage if CardModel.Audio is not null. Otherwise sets CardImage to CardModel.Image.
        /// </summary>
        /// <param name="cardView"></param>
        /// <param name="cardModel"></param>
        public void DisplayMedia(CardView cardView, CardModel cardModel)
        {
            
            foreach (var cV in cardsOnBoard)
            {
                if (cV.ID == cardView.ID)
                {
                    if (cardModel.Audio != null)
                    {
                        
                        cV.CardImage = genreModel.DefaultAudioImage;
                        mediaPlayer.Open(cardModel.Audio);
                        mediaPlayer.Play();
                    }
                    else
                    {
                        cV.CardImage = cardModel.Image;
                    }
                }
            }
            OnPropertyChanged(nameof(cardsOnBoard));
        }

        /// <summary>
        /// Displays text and gifs depending on whether pair was match or no match.
        /// </summary>
        public void DisplayMatchOrNoMatch()
        {
            
            MatchOrNoMatch = Player.MatchText;
            BorderColorMatch = Player.BorderColor;
            BackgroundMatchBox = Player.BackgroundMatchBox;
            if (Player.IsMatch == true)
            {
                Gif1 = new Uri(genreModel.MatchGifs[random.Next(genreModel.MatchGifs.Count)]);
                Gif2 = new Uri(genreModel.MatchGifs[random.Next(genreModel.MatchGifs.Count)]);
            }
            else
            {
                Gif1 = new Uri(genreModel.NoMatchGifs[random.Next(genreModel.NoMatchGifs.Count)]);
                Gif2 = new Uri(genreModel.NoMatchGifs[random.Next(genreModel.NoMatchGifs.Count)]);
            }
            OnPropertyChanged("Gif1");
            OnPropertyChanged("Gif2");
            OnPropertyChanged("MatchOrNoMatch");
            OnPropertyChanged("BorderColorMatch");
            OnPropertyChanged("BackgroundMatchBox");
        }
        public virtual void DisplayCelebratoryGif()
        {
            GifCel1 = new Uri("https://c.tenor.com/pi_bSmpi_BUAAAAC/leonardo-dicaprio-cheers.gif");
            GifCel2 = new Uri("https://c.tenor.com/5p3sBDEuk2gAAAAC/bugs-bug.gif");
        }
        #endregion
    }
}
