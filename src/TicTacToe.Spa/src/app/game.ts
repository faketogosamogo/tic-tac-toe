import { GameState } from "./gameState";
import { Turn } from "./turn";

export interface Game {
    token: string;
    turns: Turn[];
    state: GameState;
}
