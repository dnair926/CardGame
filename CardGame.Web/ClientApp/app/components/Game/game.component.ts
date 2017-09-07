import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
require('../../../images/cardback.png');

@Component({
    selector: 'game',
    templateUrl: 'game.component.html',
    styleUrls: ['./game.component.css']
})
export class GameComponent {
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
    }

    game: Game;
    playerCount: number = 0;
    selectedCard: Card | null;

    cardClicked(player: Player, card: Card) {
        var currentlySelectedCard = player.cards.find(c => c.selected),
            sameCardClicked = currentlySelectedCard && currentlySelectedCard.id == card.id;
        if (sameCardClicked) {
            card.selected = !card.selected;
        } else {
            if (currentlySelectedCard) {
                currentlySelectedCard.selected = false;
            }
            card.selected = true;
        }
        this.selectedCard = card.selected ? card : null;
    }

    playCard(player: Player) {
        var self = this;
        this.http.post(this.baseUrl + 'api/gameapi/playcard', { player: player, card: this.selectedCard }).subscribe(result => {
            
        });
        var indexOfCardPlayed = this.findCardIndex(player, this.selectedCard);
        if (indexOfCardPlayed > -1) {
            player.cards.splice(indexOfCardPlayed, 1);
        }
    }

    findCardIndex(player: Player, card: Card | null): number {
        var cards = player.cards;
        if (!cards || !card) {
            return 0;
        }
        return cards.findIndex(c => c.id == card.id);
    }

    playerCountChanged() {
        this.http.get(this.baseUrl + 'api/gameapi/getgame?playerCount=' + this.playerCount).subscribe(result => {
            this.game = result.json() as Game;
        }, error => console.error(error));
    }
}

export class Game {
    players: Array<Player>;
}
export class Player {
    cards: Array<Card>;
}
export class Card {
    id: number;
    selected: boolean;
}