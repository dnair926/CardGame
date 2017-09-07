import { Component} from '@angular/core';
import { Assets } from '../assets/assets.component';
import { GameService } from './game.service';

import { Game, Player, Card } from './game.types';

@Component({
    selector: 'game',
    templateUrl: 'game.component.html',
    styleUrls: ['./game.component.css'],
    providers: [GameService]
})
export class GameComponent {
    cards: any;
    game: Game;
    playerCount: number = 0;
    selectedCard: Card | null;

    constructor(private gameService : GameService) {
        this.initImages();
    }

    initImages() {
        var assets : Assets | any = new Assets();
        this.cards = {};
        this.cards["cardBack"] = assets.cardBack;

        var suits = ["Clubs", "Spades", "Diamonds", "Hearts"],
            values = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"],
            imageName = "",
            imagePath = "";
        for (var i = 0; i < suits.length; i++) {
            for (var j = 0; j < values.length; j++) {
                imageName = "card" + suits[i] + values[j]; 
                this.cards[imageName] = assets[imageName];
            }
        }
    }

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
        var indexOfCardPlayed = this.findCardIndex(player, this.selectedCard);
        if (indexOfCardPlayed > -1) {
            var cardPlayed = player.cards.splice(indexOfCardPlayed, 1);
            this.game.cardsInPlay = this.game.cardsInPlay || [];
            this.game.cardsInPlay.push(cardPlayed[0]);
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
        this.gameService.initGame(this.playerCount)
            .then(game => this.game = game);
    }
}

/*
    1. Game Starts
    2. Create Players
    3. Distribute Cards
        4. Decide first Player
    5. First player Play card
    6. If computer
        7. play card automatically
    8. else
        9. wait for player to play card
    10. Get Next player
        11. If no more players
                end round
                create new round
                set first player
        12. else
                move to next player
    13. Go to 6
*/