using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Item
    {
        private Texture2D tex;
        private Vector2 positionInInv;

        private string name;

        private int stack;
        private int maxStack;
        private bool isStackVisible;

        public bool isPlant;

        private bool isPlacable;
        private bool isPlaced;
        private bool isDepthDraw;

        private bool isCraftable;
        private List<Item> requirements;

        private bool isCraftingTable;
        Crafting_Station station;

        private Sprite sprite;
        private Plant plant;

        public Item(TeaGame game, Texture2D tex, string name, int stack = 1, int maxStack = 64, Sprite sprite = null, bool isStackVisible = false,
                                                                bool isPlacable = false, bool isStackable = false, bool isCraftable = false, 
                                                                List<Item> requirements = null, bool isPlaced = false,
                                                                bool isDepthDraw = false, bool isPlant = false, Plant plant = null, 
                                                                bool isCraftingTable = false, Crafting_Station station = null) {
            this.tex = tex;
            this.positionInInv = Vector2.Zero;
            this.name = name;
            this.stack = stack;
            this.maxStack = maxStack;
            this.isStackVisible = isStackVisible;
            this.isPlacable = isPlacable;
            this.isPlaced = isPlaced;
            this.isDepthDraw = isDepthDraw;
            this.sprite = sprite;
            this.isPlant = isPlant;
            this.plant = plant;
            this.isCraftable = isCraftable;
            this.requirements = requirements;
            this.isCraftingTable = isCraftingTable;
            this.station = station;
        }

        public void LMB() {
            if (isPlant && isPlaced) {
                plant.LMB();
            }
            if (isCraftingTable && isPlaced) {
                station.LMB();
            }
        }

        public void Update(GameTime gameTime) {
            if (isPlant && isPlaced) {
                plant.Update(gameTime);
                sprite.getAnimationManager().setFrame(plant.getStage());
            }
            if (isCraftingTable && isPlaced)
            {
                sprite.TimeUpdate(gameTime);
            }
            else {
                sprite.ManualUpdate();
            }
        }

        public void setPositionInInventory(Vector2 pos) {
            this.positionInInv = pos;
        }

        public Vector2 getPositionInInventory() {
            return positionInInv;
        }

        public void setTexture(Texture2D tex) {
            this.tex = tex;
        }

        public Texture2D getTexture() {
            return tex;
        }

        public void setSprite(Sprite sprite) {
            this.sprite = sprite;
        }

        public Sprite getSprite() {
            return sprite;
        }

        public void addStack(int stack) {
            this.stack += stack;
        }

        public void removeStack(int stack)
        {
            this.stack -= stack;
        }

        public void setStack(int stack) {
            this.stack = stack;
        }

        public int getStack() {
            return stack;
        }

        public void setMaxStack(int maxStack) {
            this.maxStack = maxStack;
        }

        public int getMaxStack() {
            return maxStack;
        }

        public void setIsStackVisible(bool isStackVisible) {
            this.isStackVisible = isStackVisible;
        }

        public bool getIsStackVisible() {
            return isStackVisible;
        }

        public void setIsPlacable(bool isPlacable) {
            this.isPlacable = isPlacable;
        }

        public bool getIsPlacable() {
            return isPlacable;
        }

        public void setName(string name) {
            this.name = name;
        }

        public string getName() {
            return name;
        }

        public void setIsDepthDraw(bool isDepthDraw) {
            this.isDepthDraw = isDepthDraw;
        }

        public bool getIsDepthDraw() {
            return isDepthDraw;
        }

        public void setPlant(Plant plant) {
            this.plant = plant;
        }

        public Plant getPlant() {
            return plant;
        }

        public void setIsPlaced(bool isPlaced) {
            this.isPlaced = isPlaced;
        }

        public bool getIsPlaced() {
            return isPlaced;
        }

        public void setIsCraftable(bool isCraftable) {
            this.isCraftable = isCraftable;
        }

        public bool getIsCraftable() {
            return isCraftable;
        }

        public void addCraftingReq(Item item) {
            requirements.Add(item);
        }

        public void setCraftingReq(List<Item> requirements) {
            this.requirements = requirements;
        }

        public List<Item> getCraftingReq() {
            return requirements;
        }
    }
}
