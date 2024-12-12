using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Camera
    {
        private Matrix transform;
        private TeaGame game;
        private PlayerClass player;

        public Camera(TeaGame game) {
            this.game = game;
            this.player = game.playerManager.player;
        }

        public void Update() {
            transform = Matrix.CreateTranslation(
               -player.getSprite().getPosition().X - (player.getSprite().getSpriteWidth() / 2),
              -player.getSprite().getPosition().Y - (player.getSprite().getSpriteHeight() / 2),
             0) * (Matrix.CreateTranslation(800 / 2, 600 / 2, 0) * game.getScaleMatrix ());
        }

        public Matrix getTransform()
         {
            return transform;
        }
    }
}
