//Reversi 1.11
//
//Programer Douglas Harvey-Marose
//
//  Version Changes 1.11
//  updated the used engine to version 1.31
//  complete refactor of the program
//
//  Version Changes 1.1
//  updated the used engine to version 1.2
//  game speed is now independent from the frame rate
//  
// - Known isues -
//   none

using SFML.Window;
using SFML.Graphics;
using System;

namespace Reversi
{
    class main
    {
        static void Main(string[] args)
        {
            Window window = new Window();
            GameTimer gameTimer = new GameTimer();
            LoadINI loadINI = new LoadINI("engine.ini");

            Game game = new Game(window);
            AssetLoader assetLoader = new AssetLoader(window);

            assetLoader.loadBaseAssets();
            game.setupBoard();
            gameTimer.restartWatch();

            while (window.isOpen())
            {
                //game logic
                if (gameTimer.getTimeMilliseconds() >= game.gameSpeed)
                {
                    //tells if the game has ended
                    if (game.gameEnd() == false)
                    {
                        game.mouseInputGame();
                        game.updatePieceGraphics();
                        game.updateUI();
                    }
                    else
                    {
                        game.setMarqueeWon();
                    }

                    game.mouseInputReset();
                    gameTimer.restartWatch();
                }

                //updates the window
                window.drawAll();
            }
        }
    }
}