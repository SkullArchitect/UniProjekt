using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class Pixel
    {
        private Texture2D tex;
        private Rectangle pixel;
        private Color color;
        private Vector2 pos;
        //Stages of pixel. E.G. health has 7 stages corresponding to different animations.
        private int stage;

        public Pixel(Vector2 pos, Texture2D tex, int stage = 0) {
            pixel = new Rectangle((int)pos.X, (int)pos.Y, 5, 5);
            this.pos = pos;
            this.stage = stage;
            this.tex = tex;
            color = new Color(50, 50, 50);
        }

        public void addRectangleSize(int incerase) {
            pixel.Height += incerase;
            pixel.Width += incerase;
        }

        public void setRectangleSize(int size) {
            pixel.Height = size;
            pixel.Width = size;
        }

        public Rectangle getRectangle() {
            return pixel;
        }

        public void setColor(Color color) {
            this.color = color;
        }

        public Color getColor() {
            return color;
        }

        public void setStage(int stage) {
            this.stage = stage;
        }

        public int getStage() {
            return stage;
        }

        public void setTexture(Texture2D tex) {
            this.tex = tex;
        }

        public Texture2D getTexture() {
            return tex;
        }
    }
}
