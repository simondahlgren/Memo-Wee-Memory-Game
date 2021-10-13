using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sub_6.Models
{
    public class GenreModel : ConnectionToDatabase
    {
        #region Variables
/// <summary>
/// Sets the ID-property. 0 = animals, 1 = music, 2 = sci-fi.
/// </summary>
        private int id;
        #endregion

        #region Properties
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Background { get; set; }
        public List<string> MatchGifs { get; set; }
        public List<string> NoMatchGifs { get; set; }
        public ImageBrush BackOfCard { get; set; }
        public ImageBrush DefaultAudioImage { get; set; }
        #endregion

        #region Constructor
        /// <param name="genreID">0 = animals, 1 = music, 2 = sci-fi.</param>
        public GenreModel(int genreID)
        {
            id = genreID;
            GetGenre(id);
            GetGifs(id);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the genrespecific visual elements from database.
        /// </summary>
        /// <param name="ID"></param>
        private void GetGenre(int ID)
        {
            string statement = $"SELECT background, backofcards, defaultcard FROM genre WHERE id = {ID};";
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using var command = new NpgsqlCommand(statement, conn);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Background = reader["background"].ToString();
                BackOfCard = GetImage(reader["backofcards"].ToString());
                DefaultAudioImage = GetImage(reader["defaultcard"].ToString());
            }
        }

        /// <summary>
        /// Gets the genrespecific gifs and sorts them into MatchGifs or NoMatchGifs depending on whether the attribute "match" is true (= match) or false (= no match).
        /// </summary>
        /// <param name="ID"></param>
        private void GetGifs(int ID)
        {
            MatchGifs = new List<string>();
            NoMatchGifs = new List<string>();
            string statement = $"SELECT match, gif FROM resultgif WHERE genre = {ID}";
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using var command = new NpgsqlCommand(statement, conn);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
               if ((bool)reader["match"] == true)
                {
                    MatchGifs.Add(reader["gif"].ToString());
                }
               else
                {
                    NoMatchGifs.Add(reader["gif"].ToString());
                }
            }
        }
        #endregion
    }
}
