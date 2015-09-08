using System;
using SFML.Window;
using SFML.Graphics;

namespace Reversi
{
    class AssetLoader
    {
        private Window window;

        public AssetLoader(Window passedWindow)
        {
            window = passedWindow;
        }

        //loads in the staring assests
        public void loadBaseAssets()
        {
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
        }
    }
}
