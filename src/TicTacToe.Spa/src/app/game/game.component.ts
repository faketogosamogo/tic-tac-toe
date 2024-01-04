import { Component, inject } from '@angular/core';
import { GameService } from '../game.service';
import { Turn } from '../turn';
import { Sign } from '../sign';
import { Coordinates } from '../coordinates';
import { Game } from '../game';
import { GameState } from '../gameState';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent {
  gameService: GameService = inject(GameService);
  map: string[][] = [
    ['', '', ''],
    ['', '', ''],
    ['', '', '']
  ];
  game!: Game;
  playerSign: Sign = Sign.X;
  startSign: Sign = Sign.X;
  loadGameToken!: string;

  ngOnInit(): void {
  
  }
  constructor(){
  }

  createGame(): void {
    this.gameService.createGame(this.playerSign, this.startSign).subscribe(resp => {
      this.game = resp;
      this.clearMap();
      this.renderMap(this.game.turns);
    });
  }

  loadGame(): void {
    this.gameService.getGame(this.loadGameToken).subscribe(resp => {
      this.game = resp;
      this.clearMap();
      this.renderMap(this.game.turns);
    });
  }
  
  makeTurn(x: number, y: number){
    const coordinates: Coordinates = { x: x, y: y };
    
    this.gameService.makeTurn(this.game.token, coordinates)
      .subscribe(resp => {
        this.game = resp;
        this.renderMap(resp.turns);

        if (this.game.state == GameState.XWon){
          alert("X выиграл");
        }
        if(this.game.state == GameState.OWon)
        {
          alert("O выиграл");
        }
      })
  }

  renderMap(turns: Turn[]){
    turns.forEach((turn: Turn, index: number): void => {
      this.map[turn.coordinates.x][turn.coordinates.y] = turn.sign == Sign.X ? 'X' : 'O';
    })
  }

  clearMap(): void {
    this.map = [
      ['', '', ''],
      ['', '', ''],
      ['', '', '']
    ];
  }
}
