using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class MainMenu
    {
        TeaGame game;

        public Texture2D mainMenuButton;
        public SpriteFont font;
        public SpriteFont mainFont;
        public Texture2D background_rock;

        public bool isPlaying;

        public MainMenu(TeaGame game, Texture2D mainMenuButton, SpriteFont font, SpriteFont mainFont, Texture2D background_rock) {
            this.game = game;
            this.mainMenuButton = mainMenuButton;
            this.font = font;
            this.mainFont = mainFont;
            this.background_rock = background_rock;
            isPlaying = false;
        }

        public void Update() {

        }

        public void LMB(Vector2 mousePos) {
            if (new Rectangle(304, 200, 192, 96).Contains(mousePos)) {
                game.isInMainMenu = false;
                isPlaying = true;
            }
            if (new Rectangle(304, 496, 192, 96).Contains(mousePos)) {
                game.Exit();
            }
        }

        public void Draw(SpriteBatch sp) {
            int startingX = 0, startingY = 0;
            for (int y = 0; y < 14; y++)
            {
                for (int x = 0; x < 18; x++)
                {
                    sp.Draw(background_rock, new Rectangle(startingX, startingY, 48, 28), Color.White);
                    startingX += 48;
                }
                startingY += 56;
                startingX = 0;
            }
            startingX = -24;
            startingY = 28;
            for (int y = 0; y < 14; y++)
            {
                for (int x = 0; x < 18; x++)
                {
                    sp.Draw(background_rock, new Rectangle(startingX, startingY, 48, 28), Color.White);
                    startingX += 48;
                }
                startingY += 56;
                startingX = -24;
            }

            sp.DrawString(mainFont, "TeaGame - Demo", new Vector2(250, 50), Color.Black);
            if (!isPlaying)
            {
                sp.Draw(mainMenuButton, new Rectangle(304, 200, 192, 96), Color.White);
                sp.DrawString(font, "New Game", new Vector2(324, 230), Color.White);
            }
            else {
                sp.Draw(mainMenuButton, new Rectangle(304, 200, 192, 96), Color.White);
                sp.DrawString(font, "Continue", new Vector2(324, 230), Color.White);
            }
            sp.Draw(mainMenuButton, new Rectangle(304, 496, 192, 96), Color.White);
            sp.DrawString(font, "Exit", new Vector2(364, 526), Color.White);
        }
    }
}
