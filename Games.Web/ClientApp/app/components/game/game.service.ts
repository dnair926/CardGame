import { Inject, Injectable } from '@angular/core';
import { Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Game } from './game.types';

@Injectable()
export class GameService {
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {

    }

    initGame(playerCount: number) : Promise<Game> {        
        return this.http
            .get(this.baseUrl + 'api/gameapi/getgame?playerCount=' + playerCount)
            .toPromise()
            .then(result => { result.json() as Game })
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

}