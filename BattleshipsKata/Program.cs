// See https://aka.ms/new-console-template for more information
using BattleshipsKata;

var game = new Game();

game.Players[0] = Commands.AddPlayer();

game.Players[1] = Commands.AddPlayer();

Commands.Start(game.Players);

Commands.Print(game.Players[0]);
Commands.Print(game.Players[1]);
