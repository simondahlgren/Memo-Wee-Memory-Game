using Npgsql;
using Sub_6.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sub_6.Models
{
    public class CardsModel : ConnectionToDatabase
    {
        #region Variables
        /// <summary>
        /// The genre chosen by the player. 0 = animals, 1 = music, 2 = sci-fi.
        /// </summary>
        private int genre;

        /// <summary>
        /// Total amount of pairs in a given genre.
        /// </summary>
        private int pairsCount;

        /// <summary>
        /// A string consisting of twelve random pairs.
        /// </summary>
        private string pairsToRetrieve;
        #endregion

        #region Objects
        private static readonly Random random = new Random();
        #endregion 

        #region Properties
        public List<CardModel> AllCards { get; set; } = new List<CardModel>();
        public ObservableCollection<Point> Coordinates { get; set; } = new ObservableCollection<Point>();
        #endregion

        #region Constructor
        public CardsModel(int chosengenre)
        {
            genre = chosengenre;
            SetPairsCount();
            SetPairsToRetrieve();
            GetCards();
        }
        #endregion

        #region Prelude
        /// <summary>
        /// Connects to database to set variable pairsCount to the total amount of pairs in the chosen genre.
        /// </summary>
        private void SetPairsCount()
        {
            string statement = $"SELECT CAST(COUNT(id) AS integer) FROM card{genre};";
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            using var command = new NpgsqlCommand(statement, conn);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                pairsCount = (int)reader["count"]/2;
            }
        }

        /// <summary>
        /// Sets a string by joining an IEnumberable of 12 pairs in randomized order, ranomizes 12 pairs from database.
        /// </summary>
        private void SetPairsToRetrieve()
        {
            List<int> allpairs = new List<int>();
            for (int i = 0; i < pairsCount-1; i++)
            {
                allpairs.Add(i);
            }
            IEnumerable<int> random12 = allpairs.OrderBy(x => random.Next()).Take(12); //https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.take?view=net-5.0
            pairsToRetrieve = string.Join(", ", random12);
        }
        #endregion

        #region Get background music
        /// <summary>
        /// Returns a CardModel with its Audio-property set to the Cantina Song.
        /// </summary>
        /// <returns></returns>
        public CardModel GetBackgroundMusics()
        {
            string statement = $"SELECT * FROM card2 WHERE id = 11;";
            CardModel newCard = null;
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            using var command = new NpgsqlCommand(statement, conn);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                newCard = new CardModel
                {
                    CardID = (int)reader["id"],
                    PairID = (int)reader["pairid"]
                };
                newCard.Audio = new Uri(reader["audio"].ToString());
            }
            return newCard;
        }
        #endregion

        #region Getting the cards
        /// <summary>
        /// Fills List<CardModel> "AllCards" with cards of random pairs from database.
        /// </summary>
        private void GetCards()
        {
            string statement = $"SELECT * FROM card{genre} WHERE pairid IN ({pairsToRetrieve});";
            CardModel newCard;
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            using var command = new NpgsqlCommand(statement, conn);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                newCard = new CardModel
                {
                    CardID = (int)reader["id"],
                    PairID = (int)reader["pairid"],
                };

                if (reader["image"].ToString().Length > 5)
                {
                    newCard.Image = GetImage(reader["image"].ToString());
                    newCard.Image.Stretch = Stretch.UniformToFill;
                }
                else
                {
                    newCard.Audio = new Uri(reader["audio"].ToString());
                }
                AllCards.Add(newCard);
            }
            GetCoordinatesFromCollection();
        }
        
        /// <summary>
        /// Fills the ObservableCollection<Point> "Coordinates" with 24 points, 6*4.
        /// </summary>
        private void SetCoordinates()
        {
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Point point = new Point { X = x, Y = y };
                    Coordinates.Add(point);
                }
            }
        }

        /// <summary>
        /// Randomizes and index in ObservableCollection<Point> "Coordinates" from where to get a point for a CardModel's Coordinate-property.
        /// </summary>
        private void GetCoordinatesFromCollection()
        {
            int rando;
            SetCoordinates();
            for (int i = 0; i < AllCards.Count; i++)
            {
                rando = random.Next(Coordinates.Count);
                AllCards[i].Coordinates = Coordinates[rando];
                Coordinates.RemoveAt(rando);
            }
        }
        #endregion

    }
}
