//Reversi 1.1
//
//Programer Douglas Harvey-Marose
//
//  Version Changes 1.1
//  updated the used engine to version 1.2
//  game speed is now independent from the frame rate
//  
// - Known isues -
//   none that I could find

using SFML.Window;
using SFML.Graphics;
using System;

namespace Reversi
{
    class main
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Window window = new Window();
            GameTimer gameTimer = new GameTimer();
            LoadINI loadINI = new LoadINI("engine.ini");

            string tempStr = string.Empty;
            float gameSpeed;
            window.setFont("arial.ttf");
            window.setBackgroundColor(Color.Black);
            window.addRectangle("sidebar", new Vector2f(480, 0), 160, 480, Color.Magenta);
            window.addRectangle("resetbutton", new Vector2f(485, 425), 150, 50, Color.Blue);
            window.addRectangle("passbutton", new Vector2f(485, 370), 150, 50, Color.Blue);
            window.addText("turn", new Vector2f(490, 200), Color.Black, 17, "Black Players Turn");
            window.addText("blackpieces", new Vector2f(490, 50), Color.Black, 17, "Black Pieces 0");
            window.addText("whitepieces", new Vector2f(490, 100), Color.White, 17, "White Pieces 0");
            window.addText("reset", new Vector2f(525, 430), Color.Yellow, 26, "Reset");
            window.addText("pass", new Vector2f(530, 380), Color.Yellow, 26, "Pass");
            window.batchAddCircle(64, "piece", new Vector2f(), Color.Black, 25);
            window.batchAddRectangle(64, "square", new Vector2f(), 58, 58, Color.Green);

            //loads game speed
            if (loadINI.inFile("GameSpeed"))
            {
                gameSpeed = Convert.ToInt32(loadINI.getValue("GameSpeed"));
                gameSpeed = 1000 / gameSpeed;
            }
            else
            {
                gameSpeed = 30;
                gameSpeed = 1000 / gameSpeed;
            }
            
            //moves all the pieces into place
            for (int i = 0; i < 64; i++)
            {
                window.moveEntity("square" + (i + 1), new Vector2f((i % 8) * 60 + 1, (i / 8) * 60 + 1));
                window.moveEntity("piece" + (i + 1), new Vector2f((i % 8) * 60 + 5, (i / 8) * 60 + 5));
                window.makeInvisible("piece" + (i + 1));
            }

            gameTimer.restartWatch();

            while (window.isOpen())
            {
                //game logic
                if (gameTimer.getTimeMilliseconds() >= gameSpeed)
                {

                    //tells if the game has ended
                    if (game.gameEnd() == false)
                    {
                        //mouse input
                        if (window.inputMouseClick() == "Leftbutton")
                        {
                            tempStr = string.Empty;
                            tempStr = window.batchIsWithin("square", window.mousePositionView());
                            if (tempStr != string.Empty && tempStr.Contains("square"))
                            {
                                game.placePiece(window.batchNumber(tempStr) - 1);
                            }
                            else if (window.isWithin("passbutton", window.mousePositionView()))
                            {
                                game.switchTurn();
                            }
                            else if (window.isWithin("resetbutton", window.mousePositionView()))
                            {
                                game.resetBoard();
                            }
                        }
                        //update the pieces
                        for (int i = 0; i < 64; i++)
                        {
                            if (game.getPlaced(i))
                            {
                                window.makeVisible("piece" + (i + 1));
                            }
                            if (game.getColor(i) == "White")
                            {
                                window.setColor("piece" + (i + 1), Color.White);
                            }
                            else if (game.getColor(i) == "Black")
                            {
                                window.setColor("piece" + (i + 1), Color.Black);
                            }
                            else if (game.getColor(i) == "empty")
                            {
                                window.makeInvisible("piece" + (i + 1));
                            }
                        }

                        //updates the UI
                        window.setText("blackpieces", "Black Pieces " + game.getPiecesBlack());
                        window.setText("whitepieces", "White Pieces " + game.getPiecesWhite());

                        if (game.getTurn() == "Black")
                        {
                            window.setText("turn", "Black Players Turn");
                            window.setColor("turn", Color.Black);
                        }
                        else if (game.getTurn() == "White")
                        {
                            window.setText("turn", "White Players Turn");
                            window.setColor("turn", Color.White);
                        }
                    }
                    else if (game.gameEnd() == true)
                    {
                        if (game.getPiecesBlack() > game.getPiecesWhite())
                        {
                            window.setText("turn", "Black Player Wins!");
                        }
                        else if (game.getPiecesBlack() < game.getPiecesWhite())
                        {
                            window.setText("turn", "White Player Wins!");
                        }
                    }

                    gameTimer.restartWatch();
                }

                //updates the window
                window.drawAll();
            }
        }
    }
}