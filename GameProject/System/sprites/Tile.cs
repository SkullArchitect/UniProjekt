using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class Tile
    {
        private Texture2D tile;
        private string name;
        private Collider collider;
        private Rectangle position;
        private bool isPassable;

        public Tile(Texture2D tile, Rectangle position, string name, bool isPassable = true, Collider collider = null) {
            this.tile = tile;
            this.name = name;
            this.isPassable = isPassable;
            this.collider = collider;
            this.position = position;
        }

        public void setTile(Texture2D tile) {
            this.tile = tile;
        }

        public Texture2D getTile() {
            return tile;
        }

        public void setPosition(Rectangle position) {
            this.position = position;
        }

        public Rectangle getPosition() {
            return position;
        }

        public void setCollider(Collider collider) {
            this.collider = collider;
        }

        public Collider getCollider() {
            return collider;
        }

        public string getName() {
            return name;
        }

        public void setIsPassable(bool isPassable) {
            this.isPassable = isPassable;
        }

        public bool getIsPassable() {
            return isPassable;
        }
    }
}
