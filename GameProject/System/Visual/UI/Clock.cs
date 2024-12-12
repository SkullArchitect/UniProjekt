using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Clock
    {
        private Dictionary<string, SpriteFont> fonts;
        private string time;
        private int hours;
        private int minutes;
        private float timer;

        public Clock(Dictionary<string, SpriteFont> fonts) {
            this.fonts = fonts;
            hours = 0;
            minutes = 0;
            timer = 0;
            time = "";
        }

        public void Update(GameTime gameTime) {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= 2) {
                timer = 0;
                minutes++;
                if (minutes >= 60) {
                    minutes = 0;
                    hours++;
                    if (hours >= 24) {
                        hours = 0;
                    }
                }
            }

            if (hours < 10)
            {
                time = "0" + hours + ":";
            }
            else {
                time = hours + ":";
            }

            if (minutes < 10)
            {
                time += "0" + minutes;
            }
            else {
                time += minutes;
            }
        }

        public void Draw(SpriteBatch sp) {
            sp.DrawString(fonts["ClockFont"], time, new Vector2(750, 10), Color.White);
        }
    }
}
