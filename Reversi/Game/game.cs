using System;
using SFML.Window;
using SFML.Graphics;

namespace Reversi
{
    class Game
    {
        private Window window;

        private string messageGameOut = string.Empty;
        private string tempStr = string.Empty;
        private Char[,] pieces = new Char[8,8];
        private string turn = "Black";
        private int piecesBlack = 0;
        private int piecesWhite = 0;

        public float gameSpeed { get; private set; }

        public Game(Window passedWindow)
        {
            window = passedWindow;
            gameSpeed = 30;
            resetBoard();
        }

        //sets up the board on screen
        public void setupBoard()
        {
            for (int i = 0; i < 64; i++)
            {
                window.moveEntity("square" + (i + 1), new Vector2f((i % 8) * 60 + 1, (i / 8) * 60 + 1));
                window.moveEntity("piece" + (i + 1), new Vector2f((i % 8) * 60 + 5, (i / 8) * 60 + 5));
                window.makeInvisible("piece" + (i + 1));
            }
        }

        //resets the board to starting positions
        private void resetBoard()
        {
            turn = "Black";
            piecesBlack = 2;
            piecesWhite = 2;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    pieces[x, y] = 'o';
                    if ((x == 3 && y == 3) || (x == 4 && y == 4))
                    {
                        pieces[x, y] = 'W';
                    }
                    else if ((x == 4 && y == 3) || (x == 3 && y == 4))
                    {
                        pieces[x, y] = 'B';
                    }
                }
            }
        }

        //adds a piece onto the board if it is a legal move
        private void placePiece(int index)
        {
            if (index < 0)
            {
                return;
            }
            if (pieces[(index % 8), (index / 8)] == 'o' && lineChange((index % 8), (index / 8)))
            {
                if (turn == "White")
                {
                    pieces[(index % 8), (index / 8)] = 'W';
                }
                else if (turn == "Black")
                {
                    pieces[(index % 8), (index / 8)] = 'B';
                }
                switchTurn();
            }
        }
        
        //returns if a piece has been placed
        private bool getPlaced(int index)
        {
            if (pieces[(index % 8), (index / 8)] == 'o')
            {
                return false;
            }

            return true;
        }
        
        //returns the color of a piece
        private string getColor(int index)
        {
            if (pieces[(index % 8), (index / 8)] == 'B')
            {
                return "Black";
            }
            else if (pieces[(index % 8), (index / 8)] == 'W')
            {
                return "White";
            }
            return "empty";
        }

        //updates the number of pieces on the board
        private void updatePieceAmount()
        {
            piecesBlack = 0;
            piecesWhite = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (pieces[x, y] == 'B')
                    {
                        piecesBlack++;
                    }
                    else if (pieces[x, y] == 'W')
                    {
                        piecesWhite++;
                    }
                }
            }
        }

        //changes the turn from player to player
        private void switchTurn()
        {
            if (turn == "Black")
            {
                turn = "White";
            }
            else if (turn == "White")
            {
                turn = "Black";
            }
        }
        
        //changes a line of pieces if they are sandwitched
        //if they can't be sandwitched returns nothing
        private bool lineChange(int indexX, int indexY)
        {
            tempStr = string.Empty;
            
            if (isSandwitchedRight(indexX, indexY))
            {
                tempStr += "Right";
            }
            if (isSandwitchedLeft(indexX, indexY))
            {
                tempStr += "Left";
            }
            if (isSandwitchedDown(indexX, indexY))
            {
                tempStr += "Down";
            }
            if (isSandwitchedUp(indexX, indexY))
            {
                tempStr += "Up";
            }
            if (isSandwitchedDownRight(indexX, indexY))
            {
                tempStr += "DR";
            }
            if (isSandwitchedDownLeft(indexX, indexY))
            {
                tempStr += "DL";
            }
            if (isSandwitchedUpLeft(indexX, indexY))
            {
                tempStr += "UL";
            }
            if (isSandwitchedUpRight(indexX, indexY))
            {
                tempStr += "UR";
            }
            if (tempStr == string.Empty)
            {
                return false;
            }
            if (tempStr.Contains("Right"))
            {
                swapPiecesRight(indexX, indexY);
            }
            if (tempStr.Contains("Left"))
            {
                swapPiecesLeft(indexX, indexY);
            }
            if (tempStr.Contains("Down"))
            {
                swapPiecesDown(indexX, indexY);
            }
            if (tempStr.Contains("Up"))
            {
                swapPiecesUp(indexX, indexY);
            }
            if (tempStr.Contains("DR"))
            {
                swapPiecesDownRight(indexX, indexY);
            }
            if (tempStr.Contains("DL"))
            {
                swapPiecesDownLeft(indexX, indexY);
            }
            if (tempStr.Contains("UL"))
            {
                swapPiecesUpLeft(indexX, indexY);
            }
            if (tempStr.Contains("UR"))
            {
                swapPiecesUpRight(indexX, indexY);
            }

            updatePieceAmount();

            return true;
        }

        //finds if a there is a sandwitch to the right
        private bool isSandwitchedRight(int indexX, int indexY)
        {
            if (indexX + 1 < 8)
            {
                if (pieces[indexX + 1, indexY] == 'o' ||
                    (pieces[indexX + 1, indexY] == 'W' && turn == "White") ||
                    (pieces[indexX + 1, indexY] == 'B' && turn == "Black"))
                {
                    return false;
                }
            }
            for (int i = 2; i < 8; i++)
            {
                if (indexX + i < 8)
                {
                    if (pieces[indexX + i, indexY] == 'o')
                    {
                        break;
                    }
                    else if (pieces[indexX + i, indexY] == 'B' && turn == "Black")
                    {
                        return true;
                    }
                    else if (pieces[indexX + i, indexY] == 'W' && turn == "White")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //finds if a there is a sandwitch to the left
        private bool isSandwitchedLeft(int indexX, int indexY)
        {
            if (indexX - 1 >= 0)
            {
                if (pieces[indexX - 1, indexY] == 'o' ||
                    (pieces[indexX - 1, indexY] == 'W' && turn == "White") ||
                    (pieces[indexX - 1, indexY] == 'B' && turn == "Black"))
                {
                    return false;
                }
            }
            for (int i = 2; i < 8; i++)
            {
                if (indexX - i >= 0)
                {
                    if (pieces[indexX - i, indexY] == 'o')
                    {
                        break;
                    }
                    else if (pieces[indexX + (i * -1), indexY] == 'B' && turn == "Black")
                    {
                        return true;
                    }
                    else if (pieces[indexX + (i * -1), indexY] == 'W' && turn == "White")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //finds if a there is a sandwitch to down
        private bool isSandwitchedDown(int indexX, int indexY)
        {
            if (indexY + 1 < 8)
            {
                if (pieces[indexX, indexY + 1] == 'o' ||
                    (pieces[indexX, indexY + 1] == 'W' && turn == "White") ||
                    (pieces[indexX, indexY + 1] == 'B' && turn == "Black"))
                {
                    return false;
                }
            }
            for (int i = 2; i < 8; i++)
            {
                if (indexY + i < 8)
                {
                    if (pieces[indexX, indexY + i] == 'o')
                    {
                        break;
                    }
                    else if (pieces[indexX, indexY + i] == 'B' && turn == "Black")
                    {
                        return true;
                    }
                    else if (pieces[indexX, indexY + i] == 'W' && turn == "White")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //finds if a there is a sandwitch to up
        private bool isSandwitchedUp(int indexX, int indexY)
        {
            if (indexY - 1 >= 0)
            {
                if (pieces[indexX, indexY - 1] == 'o' ||
                   (pieces[indexX, indexY - 1] == 'W' && turn == "White") ||
                   (pieces[indexX, indexY - 1] == 'B' && turn == "Black"))
                {
                    return false;
                }
            }
            for (int i = 2; i < 8; i++)
            {
                if (indexY - i >= 0)
                {
                    if (pieces[indexX, indexY - i] == 'o')
                    {
                        break;
                    }
                    if (pieces[indexX, indexY + (i * -1)] == 'B' && turn == "Black")
                    {
                        return true;
                    }
                    else if (pieces[indexX, indexY + (i * -1)] == 'W' && turn == "White")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //finds if a there is a sandwitch down right
        private bool isSandwitchedDownRight(int indexX, int indexY)
        {
            if (indexX + 1 < 8 && indexY + 1 < 8)
            {
                if (pieces[indexX + 1, indexY + 1] == 'o' ||
                    (pieces[indexX + 1, indexY + 1] == 'W' && turn == "White") ||
                    (pieces[indexX + 1, indexY + 1] == 'B' && turn == "Black"))
                {
                    return false;
                }
            }
            for (int i = 2; i < 8; i++)
            {
                if (indexX + i < 8 && indexY + i < 8)
                {
                    if (pieces[indexX + i, indexY + i] == 'o')
                    {
                        break;
                    }
                    else if (pieces[indexX + i, indexY + i] == 'B' && turn == "Black")
                    {
                        return true;
                    }
                    else if (pieces[indexX + i, indexY + i] == 'W' && turn == "White")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //finds if a there is a sandwitch down left
        private bool isSandwitchedDownLeft(int indexX, int indexY)
        {
            if (indexX - 1 >= 0 && indexY + 1 < 8)
            {
                if (pieces[indexX - 1, indexY + 1] == 'o' ||
                    (pieces[indexX - 1, indexY + 1] == 'W' && turn == "White") ||
                    (pieces[indexX - 1, indexY + 1] == 'B' && turn == "Black"))
                {
                    return false;
                }
            }
            for (int i = 2; i < 8; i++)
            {
                if (indexX - i >= 0 && indexY + i < 8)
                {
                    if (pieces[indexX - i, indexY + i] == 'o')
                    {
                        break;
                    }
                    else if (pieces[indexX - i, indexY + i] == 'B' && turn == "Black")
                    {
                        return true;
                    }
                    else if (pieces[indexX - i, indexY + i] == 'W' && turn == "White")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //finds if a there is a sandwitch up left
        private bool isSandwitchedUpLeft(int indexX, int indexY)
        {
            if (indexX - 1 >= 0 && indexY - 1 >= 0)
            {
                if (pieces[indexX - 1, indexY - 1] == 'o' ||
                    (pieces[indexX - 1, indexY - 1] == 'W' && turn == "White") ||
                    (pieces[indexX - 1, indexY - 1] == 'B' && turn == "Black"))
                {
                    return false;
                }
            }
            for (int i = 2; i < 8; i++)
            {
                if (indexX - i >= 0 && indexY - i >= 0)
                {
                    if (pieces[indexX - i, indexY - i] == 'o')
                    {
                        break;
                    }
                    else if (pieces[indexX - i, indexY - i] == 'B' && turn == "Black")
                    {
                        return true;
                    }
                    else if (pieces[indexX - i, indexY - i] == 'W' && turn == "White")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //finds if a there is a sandwitch up right
        private bool isSandwitchedUpRight(int indexX, int indexY)
        {
            if (indexX + 1 < 8 && indexY - 1 >= 0)
            {
                if (pieces[indexX + 1, indexY - 1] == 'o' ||
                    (pieces[indexX + 1, indexY - 1] == 'W' && turn == "White") ||
                    (pieces[indexX + 1, indexY - 1] == 'B' && turn == "Black"))
                {
                    return false;
                }
            }
            for (int i = 2; i < 8; i++)
            {
                if (indexX + i < 8 && indexY - i >= 0)
                {
                    if (pieces[indexX + i, indexY - i] == 'o')
                    {
                        break;
                    }
                    else if (pieces[indexX + i, indexY - i] == 'B' && turn == "Black")
                    {
                        return true;
                    }
                    else if (pieces[indexX + i, indexY - i] == 'W' && turn == "White")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //changes a sandwitched set of colors two their opposite to the right
        private void swapPiecesRight(int indexX, int indexY)
        {
            for (int i = 1; i < 8; i++)
            {
                if (indexX + i < 8)
                {
                    if (turn == "Black" && pieces[indexX + i, indexY] == 'W')
                    {
                        pieces[indexX + i, indexY] = 'B';
                    }
                    else if (turn == "White" && pieces[indexX + i, indexY] == 'B')
                    {
                        pieces[indexX + i, indexY] = 'W';
                    }
                    else if (turn == "Black" && pieces[indexX + i, indexY] == 'B')
                    {
                        break;
                    }
                    else if (turn == "White" && pieces[indexX + i, indexY] == 'W')
                    {
                        break;
                    }
                }
            }
        }

        //changes a sandwitched set of colors two their opposite to the left
        private void swapPiecesLeft(int indexX, int indexY)
        {
            for (int i = 1; i < 8; i++)
            {
                if (indexX - i >= 0)
                {
                    if (turn == "Black" && pieces[indexX - i, indexY] == 'W')
                    {
                        pieces[indexX - i, indexY] = 'B';
                    }
                    else if (turn == "White" && pieces[indexX - i, indexY] == 'B')
                    {
                        pieces[indexX - i, indexY] = 'W';
                    }
                    else if (turn == "Black" && pieces[indexX - i, indexY] == 'B')
                    {
                        break;
                    }
                    else if (turn == "White" && pieces[indexX - i, indexY] == 'W')
                    {
                        break;
                    }
                }
            }
        }

        //changes a sandwitched set of colors two their opposite down
        private void swapPiecesDown(int indexX, int indexY)
        {
            for (int i = 1; i < 8; i++)
            {
                if (indexY + i < 8)
                {
                    if (turn == "Black" && pieces[indexX, indexY + i] == 'W')
                    {
                        pieces[indexX, indexY + i] = 'B';
                    }
                    else if (turn == "White" && pieces[indexX, indexY + i] == 'B')
                    {
                        pieces[indexX, indexY + i] = 'W';
                    }
                    else if (turn == "Black" && pieces[indexX, indexY + i] == 'B')
                    {
                        break;
                    }
                    else if (turn == "White" && pieces[indexX, indexY + i] == 'W')
                    {
                        break;
                    }
                }
            }
        }

        //changes a sandwitched set of colors two their opposite up
        private void swapPiecesUp(int indexX, int indexY)
        {
            for (int i = 1; i < 8; i++)
            {
                if (indexY - i >= 0)
                {
                    if (turn == "Black" && pieces[indexX, indexY - i] == 'W')
                    {
                        pieces[indexX, indexY - i] = 'B';
                    }
                    else if (turn == "White" && pieces[indexX, indexY - i] == 'B')
                    {
                        pieces[indexX, indexY - i] = 'W';
                    }
                    else if (turn == "Black" && pieces[indexX, indexY - i] == 'B')
                    {
                        break;
                    }
                    else if (turn == "White" && pieces[indexX, indexY - i] == 'W')
                    {
                        break;
                    }
                }
            }
        }

        //changes a sandwitched set of colors two their opposite down right
        private void swapPiecesDownRight(int indexX, int indexY)
        {
            for (int i = 1; i < 8; i++)
            {

                if (indexX + i < 8 && indexY + i < 8)
                {
                    if (turn == "Black" && pieces[indexX + i, indexY + i] == 'W')
                    {
                        pieces[indexX + i, indexY + i] = 'B';
                    }
                    else if (turn == "White" && pieces[indexX + i, indexY + i] == 'B')
                    {
                        pieces[indexX + i, indexY + i] = 'W';
                    }
                    else if (turn == "Black" && pieces[indexX + i, indexY + i] == 'B')
                    {
                        break;
                    }
                    else if (turn == "White" && pieces[indexX + i, indexY + i] == 'W')
                    {
                        break;
                    }
                }
            }
        }

        //changes a sandwitched set of colors two their opposite down left
        private void swapPiecesDownLeft(int indexX, int indexY)
        {
            for (int i = 1; i < 8; i++)
            {
                if (indexX - i >= 0 && indexY + i < 8)
                {
                    if (turn == "Black" && pieces[indexX - i, indexY + i] == 'W')
                    {
                        pieces[indexX - i, indexY + i] = 'B';
                    }
                    else if (turn == "White" && pieces[indexX - i, indexY + i] == 'B')
                    {
                        pieces[indexX - i, indexY + i] = 'W';
                    }
                    else if (turn == "Black" && pieces[indexX - i, indexY + i] == 'B')
                    {
                        break;
                    }
                    else if (turn == "White" && pieces[indexX - i, indexY + i] == 'W')
                    {
                        break;
                    }
                }
            }
        }

        //changes a sandwitched set of colors two their opposite up left
        private void swapPiecesUpLeft(int indexX, int indexY)
        {
            for (int i = 1; i < 8; i++)
            {
                if (indexX - i >= 0 && indexY - i >= 0)
                {
                    if (turn == "Black" && pieces[indexX - i, indexY - i] == 'W')
                    {
                        pieces[indexX - i, indexY - i] = 'B';
                    }
                    else if (turn == "White" && pieces[indexX - i, indexY - i] == 'B')
                    {
                        pieces[indexX - i, indexY - i] = 'W';
                    }
                    else if (turn == "Black" && pieces[indexX - i, indexY - i] == 'B')
                    {
                        break;
                    }
                    else if (turn == "White" && pieces[indexX - i, indexY - i] == 'W')
                    {
                        break;
                    }
                }
            }
        }

        //changes a sandwitched set of colors two their opposite up right
        private void swapPiecesUpRight(int indexX, int indexY)
        {
            for (int i = 1; i < 8; i++)
            {
                if (indexX + i < 8 && indexY - i >= 0)
                {
                    if (turn == "Black" && pieces[indexX + i, indexY - i] == 'W')
                    {
                        pieces[indexX + i, indexY - i] = 'B';
                    }
                    else if (turn == "White" && pieces[indexX + i, indexY - i] == 'B')
                    {
                        pieces[indexX + i, indexY - i] = 'W';
                    }
                    else if (turn == "Black" && pieces[indexX + i, indexY - i] == 'B')
                    {
                        break;
                    }
                    else if (turn == "White" && pieces[indexX + i, indexY - i] == 'W')
                    {
                        break;
                    }
                }
            }
        }

        //returns true if the game has ended
        public bool gameEnd()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if ((isSandwitchedRight(x, y) || isSandwitchedLeft(x, y) ||
                        isSandwitchedUp(x, y) || isSandwitchedDown(x, y) ||
                        isSandwitchedDownRight(x, y) || isSandwitchedDownLeft(x, y) ||
                        isSandwitchedUpLeft(x, y) || isSandwitchedUpRight(x, y)) &&
                        pieces[x,y] == 'o')
                    {
                        return false;
                    }
                }
            }
            switchTurn();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if ((isSandwitchedRight(x, y) || isSandwitchedLeft(x, y) ||
                        isSandwitchedUp(x, y) || isSandwitchedDown(x, y) ||
                        isSandwitchedDownRight(x, y) || isSandwitchedDownLeft(x, y) ||
                        isSandwitchedUpLeft(x, y) || isSandwitchedUpRight(x, y)) && 
                        pieces[x,y] == 'o')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //takes in mouse input for the game
        public void mouseInputGame()
        {
            if (window.inputMouseClick() == "Leftbutton")
            {
                tempStr = string.Empty;
                tempStr = window.batchIsWithin("square", window.mousePositionView());
                if (tempStr != string.Empty && tempStr.Contains("square"))
                {
                    placePiece(window.batchNumber(tempStr) - 1);
                }
                else if (window.isWithin("passbutton", window.mousePositionView()))
                {
                    switchTurn();
                }
                else if (window.isWithin("resetbutton", window.mousePositionView()))
                {
                    resetBoard();
                }
            }
        }

        //takes in mouse input for the reset button
        public void mouseInputReset()
        {
            if (window.inputMouseClick() == "Leftbutton")
            {
                tempStr = string.Empty;
                tempStr = window.batchIsWithin("square", window.mousePositionView());
                if (window.isWithin("resetbutton", window.mousePositionView()))
                {
                    resetBoard();
                }
            }
        }

        //sets the marquee if the game is won
        public void setMarqueeWon()
        {
            if (piecesBlack > piecesWhite)
            {
                window.setText("turn", "Black Player Wins!");
                window.setColor("turn", Color.Black);
            }
            else if (piecesBlack < piecesWhite)
            {
                window.setText("turn", "White Player Wins!");
                window.setColor("turn", Color.White);
            }
        }

        //updates the piece graphics 
        public void updatePieceGraphics()
        {
            for (int i = 0; i < 64; i++)
            {
                if (getPlaced(i))
                {
                    window.makeVisible("piece" + (i + 1));
                }
                if (getColor(i) == "White")
                {
                    window.setColor("piece" + (i + 1), Color.White);
                }
                else if (getColor(i) == "Black")
                {
                    window.setColor("piece" + (i + 1), Color.Black);
                }
                else if (getColor(i) == "empty")
                {
                    window.makeInvisible("piece" + (i + 1));
                }
            }
        }

        //updates the UI
        public void updateUI()
        {
            //updates the UI
            window.setText("blackpieces", "Black Pieces " + piecesBlack);
            window.setText("whitepieces", "White Pieces " + piecesWhite);

            if (turn == "Black")
            {
                window.setText("turn", "Black Players Turn");
                window.setColor("turn", Color.Black);
            }
            else if (turn == "White")
            {
                window.setText("turn", "White Players Turn");
                window.setColor("turn", Color.White);
            }
        }
    }
}
