using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Collider
    {
        private Rectangle rectangle;
        private bool enabled;

        private int offsetX;
        private int offsetY;

        /// <summary>
        /// Constructor for the collider
        /// </summary>
        /// <param name="rectangle">takes collider rectangle</param>
        /// <param name="offsetX">Takes the offset X of the collider</param>
        /// <param name="offsetY">Takes the offset of Y of the collider</param>
        public Collider(Rectangle rectangle, int offsetX = 0, int offsetY = 0) {
            this.rectangle = rectangle;
            enabled = true;

            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public Rectangle getRectangle() {
            return rectangle;
        }

        /// <summary>
        /// sets position of the collider with the offset
        /// </summary>
        /// <param name="rectangle"></param>
        public void setRectangle(Rectangle rectangle) {
            this.rectangle = rectangle;

            addColliderOffset();
        }

        /// <summary>
        /// sets position of the collider with the offset
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setRectanglePos(int x, int y)
        {
            this.rectangle.X = x;
            this.rectangle.Y= y;

            addColliderOffset();
        }

        public bool getBool() {
            return enabled;
        }

        public void setBool(bool enabled) {
            this.enabled = enabled;
        }

        public void addColliderOffset() {
            rectangle.X += offsetX;
            rectangle.Y += offsetY;
        }
    }
}
