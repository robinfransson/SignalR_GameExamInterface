export interface Message {
    user: string,
    text: string,
    timeStamp: string
}


export interface Player {
    name: string,
    averageGuesses: Number,
    totalGuesses: Number,
    gamesPlayer: Number
}