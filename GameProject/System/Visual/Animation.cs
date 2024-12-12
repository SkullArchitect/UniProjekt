using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace GameProject
{
    public class Animation
    {
        private int currentFrame;
        private int frameCount;
        private int frameHeight;
        private int frameWidth;
        private float frameSpeed;
        private bool isLooping;
        Texture2D sprite;


        public Animation(Texture2D sprite, int frameCount) {
            this.sprite = sprite;
            this.frameCount = frameCount;
            currentFrame = 0;
            isLooping = true;
        }

        public void setCurrentFrame(int currentFrame) {
            this.currentFrame = currentFrame;
        }

        public int getCurrentFrame() {
            return currentFrame;
        }

        public void setFrameCount(int frameCount) {
            this.frameCount = frameCount;
        }

        public int getFrameCount() {
            return frameCount;
        }

        public void setFrameHeight(int frameHeight) {
            this.frameHeight = frameHeight;
        }

        public int getFrameHeight() {
            return frameHeight;
        }

        public void setFrameWidth(int frameWidth) {
            this.frameWidth = frameWidth;
        }

        public int getFrameWidth() {
            return frameWidth;
        }

        public void setFrameSpeed(float frameSpeed) {
            this.frameSpeed = frameSpeed;
        }

        public float getFrameSeed() {
            return frameSpeed;
        }

        public void setIsLooping(bool isLooping) {
            this.isLooping = isLooping;
        }

        public bool getIsLooping() {
            return isLooping;
        }

        public void setSprite(Texture2D sprite) {
            this.sprite = sprite;
        }

        public Texture2D getTexture() {
            return sprite;
        }


    }
}
