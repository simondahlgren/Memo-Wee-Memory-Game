using Npgsql;
using Sub_6.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Sub_6.Models
{
    public class HighScoreModel : ConnectionToDatabase
    {
        /// <summary>
        /// Retrives the current players rank from DB by checking the players ID with DB ID
        /// </summary>
        /// <param name="playerID"></param>
        /// <returns>string of players rank</returns>
        public string GetPlayerRank(int playerID)
        {
            string stmt = "select cast(row_number() over(order by score desc) as integer), name, score, id from scores;";
            string playerrank = null;
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using var command = new NpgsqlCommand(stmt, conn);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if ((int)reader["id"] == playerID)
                {
                    playerrank = $"{(int)reader["row_number"]}. {(string)reader["name"]} {(int)reader["score"]} P";
                }
            }
            return playerrank;
        }

        /// <summary>
        /// Retrieves a top 5 list from DB, where row_number is Rank and list is order by score
        /// </summary>
        /// <returns>ObervableCollection of Players</returns>
        public ObservableCollection<PlayerViewModel> GetTop5()
        {
            string stmt = "select cast(row_number() over(order by score desc) as integer), name, score from scores limit 5;"; ///https://learnsql.com/blog/how-to-rank-rows-in-sql/
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using var command = new NpgsqlCommand(stmt, conn);

            using var reader = command.ExecuteReader();
            PlayerViewModel h;
            var highscores = new ObservableCollection<PlayerViewModel>();
            while (reader.Read())
            {
                h = new PlayerViewModel
                {
                    
                    Name = (string)reader["name"],
                    Score = (int)reader["score"], 
                    Rank = (int)reader["row_number"]

                };
                highscores.Add(h);
            }
            return highscores;
        }

        /// <summary>
        /// Retrives the Player with the highest score from DB
        /// </summary>
        /// <returns>Top 1 Player in list</returns>
        public PlayerViewModel GetTop1()
        {
            string stmt = "select cast(row_number() over(order by score desc) as integer), name, score from scores limit 1;"; ///https://learnsql.com/blog/how-to-rank-rows-in-sql/
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            using var command = new NpgsqlCommand(stmt, conn);
           
            using var reader = command.ExecuteReader();
            PlayerViewModel Top1Player = null;
            var highscores = new ObservableCollection<PlayerViewModel>();
            while (reader.Read())
            {
                Top1Player = new PlayerViewModel
                {

                    Name = (string)reader["name"],
                    Score = (int)reader["score"],
                    Rank = (int)reader["row_number"]

                };
                highscores.Add(Top1Player);
            }
            return Top1Player;
        }


       
        /// <summary>
        /// Add Score to the Highscore list in DB, player retrives max ID in DB
        /// </summary>
        /// <param name="player"></param>
        /// <returns>Current Players ID</returns>
        public int AddHigh(PlayerViewModel player)
        {
            using var conn = new NpgsqlConnection(connectionString);
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO scores(name, score) VALUES (@name, @score) returning id; select max (id) from scores;";

                    cmd.Parameters.AddWithValue("name", player.Name);
                    cmd.Parameters.AddWithValue("score", player.Score);
                    int id = (int)cmd.ExecuteScalar();
                    player.ID = id;
                    return id;
                }

            }
        }
    }

}

