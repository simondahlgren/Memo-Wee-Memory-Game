using Sub_6.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sub_6.Messages
{
    public class GenreMessage
    {
        //the int (DB id number) related to the genre selected 
        public int GenrePick { get; set; }
       
        //Messages send properties rather than loose int's to ensure buildability. If game is expanded, additional proerties can be added without needing to send several messages.
    }
}
