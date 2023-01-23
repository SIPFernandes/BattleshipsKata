// See https://aka.ms/new-console-template for more information
using BattleshipsKata;

var game = new Game();

game.Players[0] = game.Commands.AddPlayer();

game.Players[1] = game.Commands.AddPlayer();

game.Commands.Start(game.Players);

game.Commands.Print(game.Players[0]);
game.Commands.Print(game.Players[1]);
