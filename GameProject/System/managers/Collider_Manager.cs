using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace GameProject
{
    public class ColliderManager
    {
        TeaGame game;
        private Dictionary<int, Rectangle> listOfDetectedColliders;

        public ColliderManager(TeaGame game) {
            this.game = game;
            listOfDetectedColliders = new Dictionary<int, Rectangle>();
        }

        /// <summary>
        /// Checks for any collisions, takes a collider and then cycles through different tiles in the map. If collision is found,
        /// it then calculates the depth of collision and subtracts it from the X and Y.
        /// 
        /// 1. Move sprite in X direction
        /// 2. Check collisions
        /// 3. Move sprite in Y direction
        /// 4. Check collisions
        /// 5. Return modified position
        /// 
        /// </summary>
        /// <param name="newPosition">Sprite position to be changed</param>
        /// <param name="oldPosition">Previous sprite position</param><>
        /// <param name="colliderA">Collider of the sprite</param>
        /// <param name="direction">Direction the sprite is moving</param>
        /// <returns></returns>
        public Rectangle Collision(Rectangle newPosition, Rectangle oldPosition, Collider colliderA, Vector2 direction) {
            Rectangle revisedPosition = oldPosition;
            
            Rectangle colliderRectangleB;

            Dictionary<int, Tile> renderedCollisionTiles = game.mapManager.getCollisionTiles();
            
            // Check X
            if (direction.X != 0) {

                revisedPosition.X = newPosition.X;

                colliderA.setRectanglePos(revisedPosition.X, revisedPosition.Y);

                for (int i = 0; i < renderedCollisionTiles.Count(); i++)
                {
                    colliderRectangleB = renderedCollisionTiles[i].getCollider().getRectangle();

                    if (colliderA.getRectangle().Intersects(colliderRectangleB))
                    {
                        if (colliderA.getRectangle().Center.X <= colliderRectangleB.Center.X && colliderA.getRectangle().Right > colliderRectangleB.Left) {
                            revisedPosition.X -= colliderA.getRectangle().Right - colliderRectangleB.Left;
                            colliderA.setRectanglePos(revisedPosition.X, revisedPosition.Y);
                        }
                        else if (colliderA.getRectangle().Center.X > colliderRectangleB.Center.X && colliderA.getRectangle().Left < colliderRectangleB.Right) {
                            revisedPosition.X += colliderRectangleB.Right - colliderA.getRectangle().Left;
                            colliderA.setRectanglePos(revisedPosition.X, revisedPosition.Y);
                        }
                    }
                }
            }

            // Check Y
            if (direction.Y != 0)
            {
                revisedPosition.Y = newPosition.Y;

                colliderA.setRectanglePos(revisedPosition.X, revisedPosition.Y);

                for (int i = 0; i < renderedCollisionTiles.Count(); i++)
                {
                    colliderRectangleB = renderedCollisionTiles[i].getCollider().getRectangle();

                    if (colliderA.getRectangle().Intersects(colliderRectangleB))
                    {
                        if (colliderA.getRectangle().Center.Y <= colliderRectangleB.Center.Y && colliderA.getRectangle().Bottom > colliderRectangleB.Top)
                        {
                            revisedPosition.Y -= colliderA.getRectangle().Bottom - colliderRectangleB.Top;
                            colliderA.setRectanglePos(revisedPosition.X, revisedPosition.Y);
                        }
                        else if (colliderA.getRectangle().Center.Y > colliderRectangleB.Center.Y && colliderA.getRectangle().Top < colliderRectangleB.Bottom)
                        {
                            revisedPosition.Y += colliderRectangleB.Bottom - colliderA.getRectangle().Top;
                            colliderA.setRectanglePos(revisedPosition.X, revisedPosition.Y);
                        }
                    }
                }
            }

            return revisedPosition;
        }

    }
}
