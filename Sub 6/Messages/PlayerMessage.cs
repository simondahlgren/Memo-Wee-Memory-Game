using System;
using System.Collections.Generic;
using System.Text;

namespace Sub_6.Messages
{
    class PlayerMessage
    {
        //the int that represents the final score of a round of TimeGame
        public int PlayerScore { get; set; }

        //Messages send properties rather than loose int's to ensure buildability. If game is expanded, additional proerties can be added without needing to send several messages.
    }
}
