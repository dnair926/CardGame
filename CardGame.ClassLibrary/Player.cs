using System.Collections.Generic;

namespace CardGame.ClassLibrary
{
    public class Player
    {
        public int ID { get; set; }
        public User UserInfo { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public bool CurrentPlayer { get; internal set; }
    }
}
