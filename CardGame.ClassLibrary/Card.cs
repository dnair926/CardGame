namespace CardGame.ClassLibrary
{
    public class Card
    {
        public int ID { get; set; }
        public SuitInformation Suit { get; set; }
        public CardValue Value { get; set; }
        public string CardImageName { get; set; }
    }
}
