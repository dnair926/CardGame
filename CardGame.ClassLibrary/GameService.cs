using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.ClassLibrary
{
    public class GameService
    {

        public Game CreateGame(int playerCount)
        {
            var game = new Game();

            game.Players = GetPlayers(playerCount)?.ToList();

            foreach (var player in game.Players)
            {
                Console.WriteLine($"{player.ID} {player.UserInfo.Name}");
            }

            var cardService = new CardService();
            var shuffledDeck = cardService.GetShuffledDeck();

            foreach (var card in shuffledDeck)
            {
                Console.WriteLine($"{card.Value.DisplayText}{card.Suit.DisplayText}");
            }

            DistributeCards(game, shuffledDeck);

            return game;
        }

        public void PlayCard(Player player, Card card)
        {

        }

        private void DistributeCards(Game game, IEnumerable<Card> cards)
        {
            var cardsToDeal = cards.ToArray();
            var dealtCardIndex = 0;
            var cardCount = cardsToDeal.Length;

            while (dealtCardIndex < cardCount)
            {
                foreach (var player in game.Players)
                {
                    var cardToDeal = cardsToDeal[dealtCardIndex];
                    cardToDeal.CardImageName = $"card{cardToDeal.Suit.Suit}{cardToDeal.Value.DisplayText}";
                    player.Cards.Add(cardsToDeal[dealtCardIndex]);
                    dealtCardIndex++;
                    
                    if (dealtCardIndex > (cardsToDeal.Length -1))
                    {
                        break;
                    }
                }
            }

            foreach (var player in game.Players)
            {
                player.Cards = player.Cards.OrderBy(c => c.Suit.Suit).ThenBy(c => c.Value.Value)?.ToList();
            }
        }

        private IEnumerable<Player> GetPlayers(int playerCount)
        {
            var players = new List<Player>();
            players.Add(new Player() { ID = 1, UserInfo = new User() { ID = 1, Name = "Current User" }, CurrentPlayer = true });

            var numberOfPlayers = playerCount;
            for (var i = 0; i < numberOfPlayers - 1; i++)
            {
                players.Add(new Player() { ID = 1, UserInfo = new User() { ID = i + 2, Name = "User " + (i + 2) } });
            }

            return players;
        }
    }

    public class CardService
    {
        public IEnumerable<Card> GetShuffledDeck()
        {
            var deck = GetDeck().ToArray();
            return Shuffle(deck);
        }

        public IEnumerable<Card> GetDeck()
        {
            var numericValues = Enumerable.Range(2, 9).Select(r => new CardValue() { Value = r, DisplayText = r.ToString() });
            var alphaNumericValues = new CardValue[] {
            new CardValue() { Value = 11, DisplayText = "J" },
            new CardValue() { Value = 12, DisplayText = "Q" },
            new CardValue() { Value = 13, DisplayText = "K" },
            new CardValue() { Value = 14, DisplayText = "A" },
        };
            var values = numericValues.Concat(alphaNumericValues);
            var suits = new SuitInformation[]
            {
            new SuitInformation() { Suit = Suit.Spades, DisplayText = "Spades", Value = 1 },
            new SuitInformation() { Suit = Suit.Diamonds, DisplayText = "Diamonds", Value = 0 },
            new SuitInformation() { Suit = Suit.Hearts, DisplayText = "Hearts", Value = 0 },
            new SuitInformation() { Suit = Suit.Clubs, DisplayText = "Clubs", Value = 0 },
            };

            var i = 0;
            foreach (var suit in suits)
            {
                foreach (var card in values)
                {
                    i++;
                    yield return new Card()
                    {
                        ID = i,
                        Suit = suit,
                        Value = card,
                    };
                }
            }
        }

        static Random r = new Random();
        //  Based on Java code from wikipedia:
        //  http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
        public IEnumerable<Card> Shuffle(IEnumerable<Card> deckToShuffle)
        {
            var deck = deckToShuffle.ToArray();
            for (int n = deck.Length - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);
                Card temp = deck[n];
                deck[n] = deck[k];
                deck[k] = temp;
            }

            return deck;
        }
    }
}
