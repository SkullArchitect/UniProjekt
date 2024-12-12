using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class DepthDraw
    {
        DepthDraw()
        {
        }
        private static readonly object padlock = new object();
        private static DepthDraw instance = null;
        public static DepthDraw Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DepthDraw();
                    }
                    return instance;
                }
            }
        }

        private TeaGame game;
        private List<Sprite> sprites = new List<Sprite>();


        public void setTeaGame(TeaGame game)
        {
            this.game = game;
        }
        /// <summary>
        /// Insert a sprite and sort the algorithm using selection sort.
        /// </summary>
        /// <param name="sprite">Sprite class of the object</param>
        public void InsertSprite(Sprite sprite) {
            sprites.Add(sprite);

            Sprite tempSprite;

            if (sprites.Count() > 0) {
                for (int i = 0; i < sprites.Count()-1; i++) {
                    for (int j = i+1; j < sprites.Count(); j++)
                    {
                        if (sprites[i].getPosition().Bottom > sprites[j].getPosition().Bottom) {
                            tempSprite = sprites[i];
                            sprites[i] = sprites[j];
                            sprites[j] = tempSprite;
                        }
                    }
                }
            }
        }

        public void Update() {
            sprites.Clear();
        }

        public void Draw(SpriteBatch sp) {
            if (sprites.Count() != 0) {
                foreach (Sprite sprite in sprites)
                {
                    sp.Draw(sprite.getTexture(), sprite.getPosition(), sprite.getFrameRectangle(), Color.White);
                }
            }
        }
    }
}
