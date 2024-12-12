using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class MapManager
    {
        private TeaGame game;
        private DepthDraw depthDraw;
        private Input input;
        private Item_Object_Creator itemCreator;

        // Maps
        private TestMap1 testMap1;
        private TestMap2 testMap2;

        private Matrix scale;
        private Vector2 playerCentrePosition;
        private Rectangle renderPos;
        private Rectangle nearbyColliders;

        // Set of textures
        private Dictionary<string, Texture2D> tileSet;
        // Set of constructed tiles on map
        private Dictionary<int, Tile> staticMap;
        // set of tiles placed by the player
        private List<Tile> playerPlacedTiles;
        // set of items placed by the player
        private List<Item> playerPlacedItems;
        // Set of rendered tiles
        private Dictionary<int, Tile> renderedTiles;
        // set of rendered items
        private Dictionary<int, Item> renderedItems;
        // Set of nearby tiles with colliders
        private Dictionary<int, Tile> nearbyCollisionTiles;
        // Preview of placable item when player holds specific item
        private Sprite placableItem;
        private int placableItemPosIndex;
        private bool isDisplayingPlacableItem;

        private Dictionary<int, Rectangle> chunks;
        private Dictionary<int, List<Rectangle>> placableLocationsInChunks;
        // set of booleans that check if something was placed on the tile
        private Dictionary<int, List<bool>> isSomethingPlacedHere;
        // private 
        private string mapName;

        // This class is responsible for drawing the tilesets on screen.
        public MapManager(TeaGame game, Dictionary<string, Texture2D> tileSet, Matrix scale, string mapName = "testMap1")
        {
            this.game = game;
            this.depthDraw = DepthDraw.Instance;
            this.input = Input.Instance;
            this.itemCreator = Item_Object_Creator.Instance;
            this.tileSet = tileSet;
            this.scale = scale;

            testMap1 = new TestMap1();
            testMap2 = new TestMap2();

            staticMap = new Dictionary<int, Tile>();
            renderedTiles = new Dictionary<int, Tile>();
            renderedItems = new Dictionary<int, Item>();
            nearbyCollisionTiles = new Dictionary<int, Tile>();
            isSomethingPlacedHere = new Dictionary<int, List<bool>>();

            chunks = new Dictionary<int, Rectangle>();
            placableLocationsInChunks = new Dictionary<int, List<Rectangle>>();
            placableItem = null;
            isDisplayingPlacableItem = false;

            this.mapName = mapName;

            LoadMap();
            calcRenderDistance();

            // Create chunks
            int i = 0;
            for (int x = 0; x < 10; x++) {
                for (int y = 0; y < 10; y++) {
                    chunks.Add(i, new Rectangle(x * 256, y * 256, 256, 256));
                    i++;
                }
            }

            i = 0;
            // Create placable tile positions in designated chunks
            for (int c = 0; c < chunks.Count(); c++) {
                List<Rectangle> listOfPositions = new List<Rectangle>();
                List<bool> listOfPosBooleans = new List<bool>();
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        listOfPositions.Add(new Rectangle(chunks[c].X + (x * 32), chunks[c].Y + (y * 32), 32, 32));
                        listOfPosBooleans.Add(false);
                    }
                }
                placableLocationsInChunks.Add(c, listOfPositions);
                isSomethingPlacedHere.Add(c, listOfPosBooleans);
            }
        }

        // Creates a test map
        public void LoadMap() {
            staticMap.Clear();

            string[] vectors;
            char[] separators = new char[] { '[', ']' };

            switch (mapName) {
                case "testMap1":
                    vectors = testMap1.getTestMap().Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    playerPlacedItems = testMap1.getPlayerPlacedItems();
                    playerPlacedTiles = testMap1.getPlayerPlacedTiles();
                    break;
                case "testMap2":
                    vectors = testMap2.getTestMap().Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    playerPlacedItems = testMap2.getPlayerPlacedItems();
                    playerPlacedTiles = testMap2.getPlayerPlacedTiles();
                    break;
                default:
                    vectors = testMap1.getTestMap().Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    playerPlacedItems = testMap1.getPlayerPlacedItems();
                    playerPlacedTiles = testMap1.getPlayerPlacedTiles();
                    break;
            }

            separators = new char[] { ',' };

            foreach (string str in vectors)
            {
                string[] tokens = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int[] values = new int[3];

                for (int i = 0; i < 3; i++)
                {
                    values[i] = Int32.Parse(tokens[i]);
                }
                switch (values[2])
                {
                    case 1:
                        staticMap.Add(staticMap.Count(), new Tile(tileSet["Water1"], new Rectangle(values[0] * 32, values[1] * 32, 32, 32), "Water", false, new Collider(new Rectangle(values[0] * 32, values[1] * 32, 32, 32))));
                        break;
                    case 2:
                        staticMap.Add(staticMap.Count(), new Tile(tileSet["Grass1"], new Rectangle(values[0] * 32, values[1] * 32, 32, 32), "Grass"));
                        break;
                }
            }
        }

        public void setMap(string mapName) {
            this.mapName = mapName;
            LoadMap();
        }

        public void calcRenderDistance() {
            playerCentrePosition = game.playerManager.player.getSprite().getCentrePosition();

            /*
             * Creates new rectangles based on player position
             */
            renderPos = new Rectangle((int)playerCentrePosition.X - 600, (int)playerCentrePosition.Y - 400, 1200, 800);
            nearbyColliders = new Rectangle((int)playerCentrePosition.X - 200, (int)playerCentrePosition.Y - 200, 400, 400);

            renderedTiles.Clear();
            renderedItems.Clear();
            nearbyCollisionTiles.Clear();

            // Detects if any tiles are withing render rectangle range and adds them to the list.
            int x = 0;
            for (int i = 0; i < staticMap.Count; i++)
            {
                if (renderPos.Intersects(staticMap[i].getPosition()))
                {
                    renderedTiles.Add(x++, staticMap[i]);
                    
                }
            }
            foreach (Tile tile in playerPlacedTiles) {
                if (renderPos.Intersects(tile.getPosition()))
                {
                    renderedTiles.Add(x++, tile);
                }
            }
            x = 0;
            foreach (Item item in playerPlacedItems) {
                if (renderPos.Intersects(item.getSprite().getPosition()))
                {
                    renderedItems.Add(x++, item);
                }
            }

            /* 
             * Takes list from above and checks if any tiles have collision enabled and are within nearbyColliders range, add tiles to the list.
             * It is used in collider manager to reduce optimize computation.
             */ 
            x = 0;
            for (int i = 0; i < renderedTiles.Count; i++)
            {
                if (!renderedTiles[i].getIsPassable() && nearbyColliders.Intersects(renderedTiles[i].getCollider().getRectangle()))
                {
                    nearbyCollisionTiles.Add(x++, renderedTiles[i]);
                }
            }
        }

        public bool LMB(Vector2 mousePos) {
            if (game.ui.inv.checkIfHoldingPlacableItem())
            {
                for (int i = 0; i < chunks.Count(); i++)
                {
                    if (chunks[i].Contains(mousePos))
                    {
                        for (int z = 0; z < placableLocationsInChunks[i].Count(); z++)
                        {
                            if (placableLocationsInChunks[i][z].Contains(mousePos))
                            {
                                if (!isSomethingPlacedHere[i][z]) {
                                    Debug.WriteLine(isSomethingPlacedHere[i][z] + " " + i + " " + z);
                                    Item tempItem = itemCreator.createItem(game.ui.inv.getCurrentHotbarItem().getName(), 1);
                                    game.ui.inv.removeStackFromInv(tempItem);

                                    int differenceX = 0, differenceY = 0;
                                    if (tempItem.getSprite().getPosition().Height > 32)
                                    {
                                        differenceY = placableItem.getPosition().Height - 32;
                                    }
                                    if (tempItem.getSprite().getPosition().Width > 32)
                                    {
                                        differenceX = placableItem.getPosition().Width - 32;
                                    }

                                    tempItem.getSprite().setPosition(placableLocationsInChunks[i][z].X - differenceX, placableLocationsInChunks[i][z].Y - differenceY);
                                    tempItem.setIsPlaced(true);
                                    playerPlacedItems.Add(tempItem);
                                    isSomethingPlacedHere[i][z] = true;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            else {
                for(int i = 0; i < renderedItems.Count(); i++) {
                    if (renderedItems[i].getSprite().getPosition().Contains(mousePos)) {
                        renderedItems[i].LMB();
                        return true;
                    }
                }
            }
            return false;
        }

        public void Update(GameTime gameTime) {
            if (game.ui.inv.checkIfHoldingPlacableItem())
            {
                for (int i = 0; i < chunks.Count(); i++)
                {
                    if (chunks[i].Contains(input.getGlobalMousePos()))
                    {
                        for (int z = 0; z < placableLocationsInChunks[i].Count(); z++)
                        {
                            if (placableLocationsInChunks[i][z].Contains(input.getGlobalMousePos())) {
                                placableItem = game.ui.inv.getCurrentHotbarItem().getSprite();
                                int differenceX = 0, differenceY = 0;
                                if (placableItem.getPosition().Height > 32) {
                                    differenceY = placableItem.getPosition().Height - 32;
                                }
                                if (placableItem.getPosition().Width > 32)
                                {
                                    differenceX = placableItem.getPosition().Width - 32;
                                }

                                placableItem.setPosition(placableLocationsInChunks[i][z].X - differenceX, placableLocationsInChunks[i][z].Y - differenceY);
                                isDisplayingPlacableItem = true;
                            }
                        }
                    }
                }
            }
            else {
                isDisplayingPlacableItem = false;
            }
            foreach (Item item in playerPlacedItems) {
                item.Update(gameTime);
            }
            calcRenderDistance();
            for (int i = 0; i < renderedItems.Count(); i++) {
                depthDraw.InsertSprite(renderedItems[i].getSprite());
            }
        }

        public void Draw(SpriteBatch sp) {
            for (int i = 0; i < renderedTiles.Count; i++) {
                sp.Draw(renderedTiles[i].getTile(), renderedTiles[i].getPosition(), Color.White);
            }
            if (isDisplayingPlacableItem) {
                sp.Draw(placableItem.getTexture(), placableItem.getPosition(), placableItem.getFrameRectangle(), Color.LightGreen);
            }
        }

        public Dictionary<int, Tile> getRenderedTiles() {
            return renderedTiles;
        }

        public Dictionary<int, Tile> getCollisionTiles() {
            return nearbyCollisionTiles;
        }
    }
}
