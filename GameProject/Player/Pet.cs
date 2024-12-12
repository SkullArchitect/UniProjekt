using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameProject
{
    public class Pet
    {
        private Sprite sprite;
        private DepthDraw depthDraw;

        public Pet(TeaGame game, List<Animation> petAnimations) {
            sprite = new Sprite(petAnimations, 50, 26, 1, 50, 50, 50, 50);
            depthDraw = DepthDraw.Instance;
        }
        
        public void moveTo(GameTime gameTime, Vector2 playerPos) {
            if (sprite.getPosition().X+25 > playerPos.X+30)
            {
                sprite.addMovementX((sprite.getSpeed() * (gameTime.ElapsedGameTime.Milliseconds / 16)) * -1);
            }
            if (sprite.getPosition().X+25 < playerPos.X+30)
            {
                sprite.addMovementX(sprite.getSpeed() * (gameTime.ElapsedGameTime.Milliseconds / 16));
            }
            if (sprite.getPosition().Y+12 > playerPos.Y+64)
            {
                sprite.addMovementY((sprite.getSpeed() * (gameTime.ElapsedGameTime.Milliseconds / 16)) * -1);
            }
            if (sprite.getPosition().Y+12 < playerPos.Y+64)
            {
                sprite.addMovementY(sprite.getSpeed() * (gameTime.ElapsedGameTime.Milliseconds / 16));
            }
        }

        public void update(GameTime gameTime) {
            sprite.TimeUpdate(gameTime);
            depthDraw.InsertSprite(sprite);
        }

        public Sprite getSprite() {
            return sprite;
        }
    }
}
