import { Injectable } from '@angular/core';
import { Game } from './game';
import { Coordinates } from './coordinates';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Sign } from './sign';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  url = 'http://localhost:5020/Games';

  constructor(private http: HttpClient) {}

  makeTurn(token: string, coordinates: Coordinates): Observable<Game> {
    return this.http.post<Game>(`${this.url}/${token}/turn`, coordinates);
  }

  getGame(token: string): Observable<Game> {
    return this.http.get<Game>(`${this.url}/${token}`);
  }

  createGame(playerSign: Sign, startSign: Sign): Observable<Game> {
    const params = { playerSign: playerSign.toString(), startSign: startSign.toString() };

    return this.http.post<any>(`${this.url}`, {}, { params });
  }
}
