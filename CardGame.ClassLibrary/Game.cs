using System.Collections.Generic;

namespace CardGame.ClassLibrary
{
    public class Game
    {
        public IEnumerable<Player> Players { get; set; }

        public IEnumerable<Card> CardsInPlay { get; set; }

        public IEnumerable<Card> CardsOutOfPlay { get; set; }

    }
}
