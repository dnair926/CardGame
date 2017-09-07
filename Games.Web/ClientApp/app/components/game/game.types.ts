export class Game {
    players: Array<Player>;
    cardsInPlay: Array<Card>;

}
export class Player {
    cards: Array<Card>;
}
export class Card {
    id: number;
    selected: boolean;
}