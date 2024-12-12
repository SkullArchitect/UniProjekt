using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class AnimationManager
    {
        private Animation anim;

        private float timer;
        private Rectangle frameRectangle;

        public AnimationManager(Animation anim) {
            this.anim = anim;
            frameRectangle = new Rectangle();
        }

        public void Play(Animation anim) {

            //If the animation is already playing do nothing.
            if (this.anim == anim) {
                return;
            }

            this.anim = anim;

            anim.setCurrentFrame(0);

            timer = 0;
        }

        public void Stop() {
            timer = 0;
            anim.setCurrentFrame(0);
        }

        public void setFrame(int frame) {
            if (frame <= anim.getFrameCount()) {
                anim.setCurrentFrame(frame);
            }
        }

        public void TimeUpdate(GameTime gameTime) {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > anim.getFrameSeed()) {
                timer = 0;

                anim.setCurrentFrame(anim.getCurrentFrame()+1);

                if (anim.getCurrentFrame() >= anim.getFrameCount()) {
                    anim.setCurrentFrame(0);
                }
            }

            frameRectangle = new Rectangle(anim.getCurrentFrame() * anim.getFrameWidth(),
                0, anim.getFrameWidth(), anim.getFrameHeight());
        }

        public void ManualUpdate() {
            frameRectangle = new Rectangle(anim.getCurrentFrame() * anim.getFrameWidth(),
                0, anim.getFrameWidth(), anim.getFrameHeight());
        }

        public void Draw(SpriteBatch sp, Rectangle pos) {
            sp.Draw(anim.getTexture(), pos, 
                new Rectangle(anim.getCurrentFrame()* anim.getFrameWidth(), 
                0, anim.getFrameWidth(), anim.getFrameHeight()), Color.White);
        }

        public Rectangle getFrameRectangle() {
            return frameRectangle;
        }

        public Texture2D getTexture() {
            return anim.getTexture();
        }
    }
}
