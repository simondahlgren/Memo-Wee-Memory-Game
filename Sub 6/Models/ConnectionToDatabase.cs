using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sub_6.Models
{
    public class ConnectionToDatabase
    {
        #region Database connectionstring

        protected static readonly string connectionString = "Server=studentpsql.miun.se;Port=5432;Database=sup_db6;User ID=sup_g6;Password=spin;sslmode=Require;Trust Server Certificate=true;";

        /// <summary>
        /// This method converts a string to a Uri and then creates a bitmap to set as the ImageSource for an ImageBrush.
        /// </summary>
        /// <param name="image">A url for an image.</param>
        /// <returns>The image as ImageBrush.</returns>
        protected ImageBrush GetImage(string image)
        {
            var b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(image);
            b.EndInit();
            return new ImageBrush { ImageSource = b };
        }
        #endregion
    }
}
