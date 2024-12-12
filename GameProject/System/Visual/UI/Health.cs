using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class Health
    {
        public TeaGame game;
        public PlayerClass player;



        //Variables to keep track if health was lost
        private int maxHealth;
        private int health;

        private int healthDiff;
        
        private Texture2D pixelTex;

        private Dictionary<int, Pixel> pixels;
        

        public Health(TeaGame game, Texture2D tex) {
            this.game = game;
            player = game.playerManager.player;

            pixels = new Dictionary<int, Pixel>();

            maxHealth = 100;
            health = maxHealth;
            healthDiff = 0;
            pixelTex = tex;


            createHealthPixels();
        }

        // Methods that constructor calls, it creates new instances of Pixel class.
        public void createHealthPixels() {
            int i = 1;
            for (int x = 0; x < 25; x++)
            {
                for (int y = 3; y >= 0; y--)
                {
                    pixels.Add(i, new Pixel(new Vector2((x * 5), (y * 5)), pixelTex));
                    i++;
                }
            }

            for (int b = 1; b <= health; b++)
            {
                pixels[b].setColor(new Color(210, 60, 80));
            }
        }


        // Checks if the health is changed, sets the stages to indicate animation.
        public void Update()
        {
            if (health != player.getSprite().getHealth())
            {
                healthDiff = player.getSprite().getHealth() - health;
                if (healthDiff < 0)
                {
                    for (int i = 0; i > healthDiff; i--)
                    {
                        pixels[health + i].setStage(1);
                    }
                }
                else {
                    for (int i = 1; i <= healthDiff; i++)
                    {
                        pixels[health + i].setStage(3);
                    }
                }
                
                health = player.getSprite().getHealth();
            }
        }

        /**
         * Health stages consists of:
         * 0 - Red, normal
         * 
         * 1 - White
         * 
         * 2 - Reduces alpha till 0
         * 
         * 3 - Green, starts small
         * 
         * 4 - Incerases in size
         * 
         **/


        public void Draw(SpriteBatch sp) {
            for (int i = 1; i <= maxHealth; i++) {
                switch (pixels[i].getStage()) {
                    case 0:
                        sp.Draw(pixels[i].getTexture(), pixels[i].getRectangle(), new Color(210, 60, 80, 255));
                        break;
                    case 1:
                        pixels[i].setColor(new Color(255,255,255,255));
                        sp.Draw(pixels[i].getTexture(), pixels[i].getRectangle(), pixels[i].getColor());
                        pixels[i].setStage(2);
                        break;
                    case 2:
                        if (pixels[i].getColor().A > 5)
                        {
                            pixels[i].setColor(pixels[i].getColor()*0.8f);
                            sp.Draw(pixels[i].getTexture(), pixels[i].getRectangle(), pixels[i].getColor());
                        }
                        break;
                    case 3:
                        pixels[i].setRectangleSize(1);
                        pixels[i].setColor(new Color(64, 207, 202, 255));
                        pixels[i].setStage(4);
                        sp.Draw(pixels[i].getTexture(), pixels[i].getRectangle(), pixels[i].getColor());
                        break;
                    case 4:
                        if (pixels[i].getRectangle().Height < 5)
                        {
                            pixels[i].addRectangleSize(1);
                        }
                        else {
                            pixels[i].setStage(0);
                        }
                        sp.Draw(pixels[i].getTexture(), pixels[i].getRectangle(), pixels[i].getColor());
                        break;

                }
            }
        }


    }
}
