using System;

namespace Reversi
{
    class Game
    {
        private string messageGameOut = string.Empty;
        private string tempStr = string.Empty;
        private Char[,] pieces = new Char[8,8];
        private string turn = "Black";
        private int piecesBlack = 0;
        private int piecesWhite = 0;

        public Game()
        {
            //sets starting positions
            resetBoard();
        }

        //resets the board to starting positions
        public void resetBoard()
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
        public void placePiece(int index)
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
        public bool getPlaced(int index)
        {
            if (pieces[(index % 8), (index / 8)] == 'o')
            {
                return false;
            }

            return true;
        }
        
        //returns the color of a piece
        public string getColor(int index)
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

        //returns the number of black pieces on the board
        public int getPiecesBlack()
        {
            return piecesBlack;
        }

        //returns the number of black pieces on the board
        public int getPiecesWhite()
        {
            return piecesWhite;
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
        public void switchTurn()
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

        //returns the players turn
        public string getTurn()
        {
            return turn;
        }

        //changes a line of pieces if they are sandwitched
        //if they can't be sandwitched returns nothing
        private bool lineChange(int indexX, int indexY)
        {
            tempStr = string.Empty;
            
            if (isSandwitched("Right", indexX, indexY))
            {
                tempStr += "Right";
            }
            if (isSandwitched("Left", indexX, indexY))
            {
                tempStr += "Left";
            }
            if (isSandwitched("Down", indexX, indexY))
            {
                tempStr += "Down";
            }
            if (isSandwitched("Up", indexX, indexY))
            {
                tempStr += "Up";
            }
            if (isSandwitched("DownRight", indexX, indexY))
            {
                tempStr += "DR";
            }
            if (isSandwitched("DownLeft", indexX, indexY))
            {
                tempStr += "DL";
            }
            if (isSandwitched("UpLeft", indexX, indexY))
            {
                tempStr += "UL";
            }
            if (isSandwitched("UpRight", indexX, indexY))
            {
                tempStr += "UR";
            }
            if (tempStr == string.Empty)
            {
                return false;
            }
            if (tempStr.Contains("Right"))
            {
                swapPieces("Right", indexX, indexY);
            }
            if (tempStr.Contains("Left"))
            {
                swapPieces("Left", indexX, indexY);
            }
            if (tempStr.Contains("Down"))
            {
                swapPieces("Down", indexX, indexY);
            }
            if (tempStr.Contains("Up"))
            {
                swapPieces("Up", indexX, indexY);
            }
            if (tempStr.Contains("DR"))
            {
                swapPieces("DownRight", indexX, indexY);
            }
            if (tempStr.Contains("DL"))
            {
                swapPieces("DownLeft", indexX, indexY);
            }
            if (tempStr.Contains("UL"))
            {
                swapPieces("UpLeft", indexX, indexY);
            }
            if (tempStr.Contains("UR"))
            {
                swapPieces("UpRight", indexX, indexY);
            }

            updatePieceAmount();

            return true;
        }

        //finds if a there is a sandwitch in a given direction
        public bool isSandwitched(string direction,int indexX, int indexY)
        {
            switch (direction)
            {
            case "Right":
                if (indexX + 1 < 8)
                {
                    if (pieces[indexX + 1, indexY] == 'o' ||
                        (pieces[indexX + 1, indexY] == 'W' && turn == "White") ||
                        (pieces[indexX + 1, indexY] == 'B' && turn == "Black"))
                    {
                        break;
                    }
                }
                for (int i = 2; i < 8; i++)
                {
                    if (indexX + i < 8)
                    {
                        if(pieces[indexX + i, indexY] == 'o')
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
                break;

            case "Left":
                if (indexX - 1 >= 0)
                {
                    if (pieces[indexX - 1, indexY] == 'o' ||
                        (pieces[indexX - 1, indexY] == 'W' && turn == "White") ||
                        (pieces[indexX - 1, indexY] == 'B' && turn == "Black"))
                    {
                        break;
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
                break;

            case "Down":
                if (indexY + 1 < 8)
                {
                    if (pieces[indexX, indexY + 1] == 'o' ||
                        (pieces[indexX, indexY + 1] == 'W' && turn == "White") ||
                        (pieces[indexX, indexY + 1] == 'B' && turn == "Black"))
                    {
                        break;
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
                break;

            case "Up":
                if (indexY - 1 >= 0)
                {
                    if (pieces[indexX, indexY - 1] == 'o' ||
                       (pieces[indexX, indexY - 1] == 'W' && turn == "White") ||
                       (pieces[indexX, indexY - 1] == 'B' && turn == "Black"))
                    {
                        break;
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
                break;

            case "DownRight":
                if (indexX + 1 < 8 && indexY + 1 < 8)
                {
                    if (pieces[indexX + 1, indexY + 1] == 'o' ||
                        (pieces[indexX + 1, indexY + 1] == 'W' && turn == "White") ||
                        (pieces[indexX + 1, indexY + 1] == 'B' && turn == "Black"))
                    {
                        break;
                    }
                }
                for (int i = 2; i < 8; i++)
                {
                    if (indexX + i < 8 && indexY + i < 8)
                    {
                        if(pieces[indexX + i, indexY + i] == 'o')
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
                break;

            case "DownLeft":
                if (indexX - 1 >= 0 && indexY + 1 < 8)
                {
                    if (pieces[indexX - 1, indexY + 1] == 'o' ||
                        (pieces[indexX - 1, indexY + 1] == 'W' && turn == "White") ||
                        (pieces[indexX - 1, indexY + 1] == 'B' && turn == "Black"))
                    {
                        break;
                    }
                }
                for (int i = 2; i < 8; i++)
                {
                    if (indexX - i >= 0 && indexY + i < 8)
                    {
                        if(pieces[indexX - i, indexY + i] == 'o')
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
                break;

            case "UpLeft":
                if (indexX - 1 >= 0 && indexY - 1 >= 0)
                {
                    if (pieces[indexX - 1, indexY - 1] == 'o' ||
                        (pieces[indexX - 1, indexY - 1] == 'W' && turn == "White") ||
                        (pieces[indexX - 1, indexY - 1] == 'B' && turn == "Black"))
                    {
                        break;
                    }
                }
                for (int i = 2; i < 8; i++)
                {
                    if (indexX - i >= 0 && indexY - i >= 0)
                    {
                        if(pieces[indexX - i, indexY - i] == 'o')
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
                break;

            case "UpRight":
                if (indexX + 1 < 8 && indexY - 1 >= 0)
                {
                    if (pieces[indexX + 1, indexY - 1] == 'o' ||
                        (pieces[indexX + 1, indexY - 1] == 'W' && turn == "White") ||
                        (pieces[indexX + 1, indexY - 1] == 'B' && turn == "Black"))
                    {
                        break;
                    }
                }
                for (int i = 2; i < 8; i++)
                {
                    if (indexX + i < 8 && indexY - i >= 0)
                    {
                        if(pieces[indexX + i, indexY - i] == 'o')
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
                break;
            }
            return false;
        }

        //changes a sandwitched set of colors two their opposite in a given direction
        public void swapPieces(string direction, int indexX, int indexY)
        {
            switch (direction)
            {
                case "Right":
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
                    break;

                case "Left":
                    for (int i = 1; i < 8; i++)
                    {
                        if (indexX - i >= 0)
                        {
                            if (turn == "Black" && pieces[indexX - i, indexY] == 'W')
                            {
                                pieces[indexX -i , indexY] = 'B';
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
                    break;

                case "Down":
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
                    break;

                case "Up":
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
                    break;

                case "DownRight":
                    for (int i = 1; i < 8; i++)
                    {

                        if (indexX + i < 8 && indexY + i < 8)
                        {
                            if (turn == "Black" && pieces[indexX + i, indexY +i] == 'W')
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
                    break;

                case "DownLeft":
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
                    break;

                case "UpLeft":
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
                    break;

                case "UpRight":
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
                    break;
            }
        }

        //returns true if the game has ended
        public bool gameEnd()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if ((isSandwitched("Right", x, y) || isSandwitched("Left", x, y) ||
                        isSandwitched("Up", x, y) || isSandwitched("Down", x, y) ||
                        isSandwitched("DownRight", x, y) ||isSandwitched("DownLeft", x, y) ||
                        isSandwitched("UpLeft", x, y) || isSandwitched("UpRight", x, y)) &&
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
                    if ((isSandwitched("Right", x, y) || isSandwitched("Left", x, y) ||
                        isSandwitched("Up", x, y) || isSandwitched("Down", x, y) ||
                        isSandwitched("DownRight", x, y) || isSandwitched("DownLeft", x, y) ||
                        isSandwitched("UpLeft", x, y) || isSandwitched("UpRight", x, y)) && 
                        pieces[x,y] == 'o')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
