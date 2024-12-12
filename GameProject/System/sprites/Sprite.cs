using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Sprite
    {   
        private Rectangle pos;
        private Rectangle oldPos;
        private Vector2 centre;

        private AnimationManager animationManager;
        private List<Animation> animations;

        // Height and width of player sprite
        private int spriteWidth;
        private int spriteHeight;

        // Player game variables
        private int speed;
        private int health;
        private int mana;
        private int maxHealth;
        private int maxMana;

        public Sprite(List <Animation> animations, int spriteWidth, int spriteHeight, int speed, int health, int maxHealth, 
                                                                                                   int mana, int maxMana) {
            this.animations = animations;
            animationManager = new AnimationManager(animations[0]);

            this.spriteHeight = spriteHeight;
            this.spriteWidth = spriteWidth;
            this.health = health;
            this.maxHealth = maxHealth;
            this.mana = mana;
            this.maxMana = maxMana;
            this.speed = speed;


            pos = new Rectangle(100, 100, this.spriteWidth, this.spriteHeight);
            centre = new Vector2(pos.X + (pos.Right/2), pos.Y + (pos.Bottom/2));
            oldPos = pos;
        }

        /// <summary>
        /// IDLE:
        /// 0 - idle_down
        /// 1 - idle_up
        /// 2 - idle_left
        /// 3 - idle_right
        /// MOVE:
        /// 4 - move_down
        /// 5 - move_up
        /// 6 - move_left
        /// 7 - move_right
        /// </summary>
        /// <param name="index"></param>
        public void playAnimation(int index) {
            animationManager.Play(animations[index]);
        }

        public void TimeUpdate(GameTime gameTime) {
            animationManager.TimeUpdate(gameTime);
            centre.X = pos.X + (spriteWidth / 2);
            centre.Y = pos.Y + (spriteHeight / 2);
        }

        public void ManualUpdate() {
            animationManager.ManualUpdate();
            centre.X = pos.X + (spriteWidth / 2);
            centre.Y = pos.Y + (spriteHeight / 2);
        }

        public void Draw(SpriteBatch sp) {
            animationManager.Draw(sp, pos);
        }

        public void addMovement(Vector2 movement) {
            pos.X += (int)movement.X;
            pos.Y += (int)movement.Y;
        }

        public void addMovementX(int x)
        {
            pos.X += x;
        }

        public void addMovementY(int y)
        {
            pos.Y += y;
        }

        public Vector2 getCentrePosition()
        {
            return centre;
        }

        public void setCentrePosition(Vector2 centre)
        {
            this.centre = centre;
        }

        public void setPosition(int x, int y)
        {
            pos.X = x;
            pos.Y = y;
        }

        public void setPosition(Rectangle pos)
        {
            this.pos = pos;
        }

        public void addToPosition(Vector2 movement) {
            pos.X += (int)movement.X;
            pos.Y += (int)movement.Y;
        }

        public Rectangle getPosition()
        {
            return pos;
        }

        public void setOldPosition(Rectangle oldPos) {
            this.oldPos = oldPos;
        }

        public Rectangle getOldPosition() {
            return oldPos;
        }

        public void setHealth(int health)
        {
            this.health = health;
        }

        public void addToHealth(int diff)
        {
            int temp = health + diff;
            if (temp > maxHealth)
            {
                health = maxHealth;
            }
            else if (temp < 0)
            {
                health = 0;
            }
            else
            {
                health += diff;
            }
        }

        public int getHealth()
        {
            return health;
        }

        public void setMana(int mana)
        {
            this.mana = mana;
        }

        public void addToMana(int diff)
        {
            int temp = mana + diff;
            if (temp > maxMana)
            {
                mana = maxMana;
            }
            else if (temp < 0)
            {
                mana = 0;
            }
            else
            {
                mana += diff;
            }
        }

        public int getMana()
        {
            return mana;
        }

        public Texture2D getTexture() {
            return animationManager.getTexture();
        }

        public Rectangle getFrameRectangle()
        {
            return animationManager.getFrameRectangle();
        }

        public void setSpeed(int speed) {
            this.speed = speed;
        }

        public int getSpeed() {
            return speed;
        }

        public void setSpriteWidth(int spriteWidth) {
            this.spriteWidth = spriteWidth;
        }

        public int getSpriteWidth() {
            return spriteWidth;
        }

        public void setSpriteHeight(int spriteHeight) {
            this.spriteHeight = spriteHeight;
        }

        public int getSpriteHeight() {
            return spriteHeight;
        }

        public AnimationManager getAnimationManager() {
            return animationManager;
        }
    }
}
