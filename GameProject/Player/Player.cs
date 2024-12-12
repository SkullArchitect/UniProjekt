using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameProject
{
    public class PlayerClass
    {
        private TeaGame game;
        private Sprite sprite;
        private DepthDraw depthDraw;

        private ColliderManager colliderManager;
        private Collider collider;

        private Vector2 movement;
        private string facingDirection;

        public PlayerClass(TeaGame game, List <Animation> animations) {
            this.game = game;

            sprite = new Sprite(animations, 60, 112, 6, 100, 100, 100, 100);
            sprite.setPosition(300, 300);
            
            colliderManager = new ColliderManager(game);
            collider = new Collider(new Rectangle((int)sprite.getPosition().X, (int)sprite.getPosition().Y, 60, 48), 0, 80);
            depthDraw = DepthDraw.Instance;

            movement = Vector2.Zero;
            facingDirection = "down";
        }
        //Checks the direction vector, then sets the animaton and changes the position.
        public void move(GameTime gameTime, Vector2 direction) {
            float tempSpeed = sprite.getSpeed();

            // Checks if two kewys are pressed at the same time
            if (direction.X != 0 && direction.Y != 0)
            {
                tempSpeed = sprite.getSpeed() / 2;
            }

            sprite.setOldPosition(sprite.getPosition());
            movement = direction * (tempSpeed * (gameTime.ElapsedGameTime.Milliseconds / 16));
            sprite.addMovement(movement);
            collider.setRectanglePos((int)sprite.getPosition().X, (int)sprite.getPosition().Y);

            changeAnimation(direction);
            sprite.setPosition(colliderManager.Collision(sprite.getPosition(), sprite.getOldPosition(), collider, direction));
        }

        public void changeAnimation(Vector2 direction) {
            if (direction.X == 0 && direction.Y == 0)
            {
                if (facingDirection.Equals("down")) {
                    sprite.playAnimation(0);
                }
                if (facingDirection.Equals("up"))
                {
                    sprite.playAnimation(1);
                }
                if (facingDirection.Equals("left"))
                {
                    sprite.playAnimation(2);
                }
                if (facingDirection.Equals("right"))
                {
                    sprite.playAnimation(3);
                }
            }
            if (direction.Y == 1)
            {
                sprite.playAnimation(4);
                facingDirection = "down";
            }
            else if (direction.Y == -1)
            {
                sprite.playAnimation(5);
                facingDirection = "up";
            }
            else if (direction.X == -1 && direction.Y == 0)
            {
                sprite.playAnimation(6);
                facingDirection = "left";
            }
            else if (direction.X == 1 && direction.Y == 0) {
                sprite.playAnimation(7);
                facingDirection = "right";
            }
        }

        public void Update(GameTime gameTime, Vector2 direction) {
            move(gameTime, direction);
            sprite.TimeUpdate(gameTime);
            depthDraw.InsertSprite(sprite);
        }

        public Sprite getSprite() {
            return sprite;
        }
    }
}
