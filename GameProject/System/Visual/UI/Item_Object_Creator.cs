using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Item_Object_Creator
    {
        Item_Object_Creator()
        {
        }
        private static readonly object padlock = new object();
        private static Item_Object_Creator instance = null;
        public static Item_Object_Creator Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Item_Object_Creator();
                    }
                    return instance;
                }
            }
        }

        Dictionary<string, Texture2D> itemTextures;
        TeaGame game;

        public void setItemTextures(Dictionary<string, Texture2D> itemTextures) {
            this.itemTextures = itemTextures;
        }

        public void setTeaGame(TeaGame game) {
            this.game = game;
        }

        public Item createItem(string name, int stack = 1) {
            Item item;
            List<Animation> tempList;
            Animation newAnimation;
            Sprite newSprite;
            Plant plant;

            if (name.Equals("Apple")) {
                tempList = new List<Animation>();
                newAnimation = new Animation(itemTextures[name], 1);
                newAnimation.setFrameHeight(16);
                newAnimation.setFrameWidth(16);
                newAnimation.setFrameSpeed(0);
                newAnimation.setCurrentFrame(0);
                tempList.Add(newAnimation);
                newSprite = new Sprite(tempList, 16, 16, 0, 0, 0, 0, 0);
                newSprite.ManualUpdate();
                item = new Item(game, itemTextures[name], name, stack, 64, newSprite, true, false, true);
                return item;
            }
            else if (name.Equals("HarvestingTool")) {
                tempList = new List<Animation>();
                newAnimation = new Animation(itemTextures[name], 1);
                newAnimation.setFrameHeight(16);
                newAnimation.setFrameWidth(16);
                newAnimation.setFrameSpeed(0);
                newAnimation.setCurrentFrame(0);
                tempList.Add(newAnimation);
                newSprite = new Sprite(tempList, 16, 16, 0, 0, 0, 0, 0);
                newSprite.ManualUpdate();
                item = new Item(game, itemTextures[name], name, 1, 1, newSprite);
                return item;
            }
            else if (name.Equals("Green_Plant_Tea")) {
                tempList = new List<Animation>();
                newAnimation = new Animation(itemTextures[name], 4);
                newAnimation.setFrameHeight(64);
                newAnimation.setFrameWidth(32);
                newAnimation.setFrameSpeed(0);
                newAnimation.setCurrentFrame(0);
                tempList.Add(newAnimation);
                newSprite = new Sprite(tempList, 32, 64, 0, 0, 0, 0, 0);
                newSprite.ManualUpdate();
                plant = new Plant(game, true, 3, 5, "Green_Tea_Leaf", 3, 5);

                item = new Item(game, itemTextures[name], name, stack, 5, newSprite, true, true, true, false, null, false, true, true, plant);
                return item;
            }
            else if (name.Equals("Green_Tea_Leaf")) {
                tempList = new List<Animation>();
                newAnimation = new Animation(itemTextures[name], 1);
                newAnimation.setFrameHeight(32);
                newAnimation.setFrameWidth(32);
                newAnimation.setFrameSpeed(0);
                newAnimation.setCurrentFrame(0);
                tempList.Add(newAnimation);
                newSprite = new Sprite(tempList, 32, 32, 0, 0, 0, 0, 0);
                newSprite.ManualUpdate();
                item = new Item(game, itemTextures[name], name, stack, 64, newSprite, true, false, true);
                return item;
            }
            else if (name.Equals("Kettle"))
            {
                tempList = new List<Animation>();
                newAnimation = new Animation(itemTextures[name], 1);
                newAnimation.setFrameHeight(32);
                newAnimation.setFrameWidth(32);
                newAnimation.setFrameSpeed(0);
                newAnimation.setCurrentFrame(0);
                tempList.Add(newAnimation);
                newSprite = new Sprite(tempList, 32, 32, 0, 0, 0, 0, 0);
                newSprite.ManualUpdate();
                Crafting_Station station = new Crafting_Station(game, "Kettle");
                item = new Item(game, itemTextures[name], name, stack, 1, newSprite, true, true, true, false, null, false, 
                    true, false, null, true, station);
                return item;
            }
            else if (name.Equals("Green_Tea"))
            {
                tempList = new List<Animation>();
                newAnimation = new Animation(itemTextures[name], 1);
                newAnimation.setFrameHeight(32);
                newAnimation.setFrameWidth(32);
                newAnimation.setFrameSpeed(0);
                newAnimation.setCurrentFrame(0);
                tempList.Add(newAnimation);
                newSprite = new Sprite(tempList, 32, 32, 0, 0, 0, 0, 0);
                newSprite.ManualUpdate();
                List<Item> req = new List<Item>();
                req.Add(createItem("Green_Tea_Leaf", 5));
                item = new Item(game, itemTextures[name], name, stack, 5, newSprite, true, false, true, true, req);
                return item;
            }
            else if (name.Equals("Apple_Tea"))
            {
                tempList = new List<Animation>();
                newAnimation = new Animation(itemTextures[name], 1);
                newAnimation.setFrameHeight(32);
                newAnimation.setFrameWidth(32);
                newAnimation.setFrameSpeed(0);
                newAnimation.setCurrentFrame(0);
                tempList.Add(newAnimation);
                newSprite = new Sprite(tempList, 32, 32, 0, 0, 0, 0, 0);
                newSprite.ManualUpdate();
                List<Item> req = new List<Item>();
                req.Add(createItem("Apple", 5));
                item = new Item(game, itemTextures[name], name, stack, 5, newSprite, true, false, true, true, req);
                return item;
            }
            return null;
        }
    }
}
