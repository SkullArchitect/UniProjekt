using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Inventory
    {
        public TeaGame game;
        private Input input;
        private Item holdingItem;
        private Item_Object_Creator itemCreator;

        private int itemsInInv;
        private int currentSelectedToolbar;
        private int invSize;
        private int invCollumns;
        private int invRows;

        private Texture2D backgroundTex;
        private Texture2D boxTex;
        private Texture2D dropButtonTex;
        private Texture2D selectedToolbarItemTex;
        
        Dictionary<string, SpriteFont> fonts;
        private Dictionary<int, Item> inv;

        private Rectangle[] invPositions;
        private Rectangle[] toolbarPos;
        private Rectangle dropPos;
        private bool isHolding;
    

        public Inventory(TeaGame game, Texture2D backgroundTex, 
                                        Texture2D boxTex, Texture2D dropButtonTex, Texture2D selectedToolbarItemTex, 
                                        Dictionary<string, SpriteFont> fonts) {
            this.game = game;
            itemsInInv = 0;
            invSize = 16;
            currentSelectedToolbar = 0;

            invCollumns = 4;
            invRows = 4;

            this.backgroundTex = backgroundTex;
            this.boxTex = boxTex;
            this.dropButtonTex = dropButtonTex;
            this.selectedToolbarItemTex = selectedToolbarItemTex;
            this.fonts = fonts;

            invPositions = new Rectangle[invSize];
            toolbarPos = new Rectangle[9];
            inv = new Dictionary<int, Item>();

            input = Input.Instance;
            itemCreator = Item_Object_Creator.Instance;

            isHolding = false;

            int i = 0;

            for (int y = 0; y < invCollumns; y++)
            {
                for (int x = 0; x < invRows; x++)
                {
                    invPositions[i] = new Rectangle((175 + (x * 100)+25), (75 + (y * 100)+25), 100, 100);
                    i++;
                }
            }

            toolbarPos[0] = new Rectangle(197, 555, 45, 45);
            toolbarPos[1] = new Rectangle(242, 555, 45, 45);
            toolbarPos[2] = new Rectangle(287, 555, 45, 45);
            toolbarPos[3] = new Rectangle(332, 555, 45, 45);
            toolbarPos[4] = new Rectangle(377, 555, 45, 45);
            toolbarPos[5] = new Rectangle(422, 555, 45, 45);
            toolbarPos[6] = new Rectangle(467, 555, 45, 45);
            toolbarPos[7] = new Rectangle(512, 555, 45, 45);
            toolbarPos[8] = new Rectangle(557, 555, 45, 45);

            dropPos = new Rectangle(655, 288, 35, 75);

            //Temp adding items to inv
            addToInv("Apple", 1);
            addToInv("Green_Plant_Tea", 1);
            addToInv("Kettle", 1);
            addToInv("Green_Tea", 1);
        }

        public Item getCurrentHotbarItem() {
            if (inv.ContainsKey(currentSelectedToolbar)) {
                return inv[currentSelectedToolbar];
            }
            return null;
        }

        public bool checkIfHoldingPlacableItem() {
            if (inv.ContainsKey(currentSelectedToolbar)) return inv[currentSelectedToolbar].getIsPlacable();
            return false;
        }

        // Add new items to the inventory without set position
        public void addToInv(string name, int stack) {
            Debug.WriteLine("Adding " + name + " " + stack);
            if (itemsInInv < invSize)
            {
                for (int i = 0; i < invSize; i++)
                {
                    if (inv.ContainsKey(i) && name.Equals(inv[i].getName()) && inv[i].getStack() < inv[i].getMaxStack())
                    {
                        int checkEmptySpace = inv[i].getMaxStack() - inv[i].getStack();

                        if (stack <= checkEmptySpace)
                        {
                            inv[i].addStack(stack);
                            return;
                        }
                        if (checkEmptySpace != 0) {
                            stack -= checkEmptySpace;
                            inv[i].addStack(checkEmptySpace);
                        }
                    }
                }

                // If there's empty space in inventory add new item
                for (int i = 0; i < invSize; i++)
                {
                    if (!inv.ContainsKey(i))
                    {
                        inv.Add(i, itemCreator.createItem(name, stack));
                        Debug.WriteLine(inv[i]);
                        inv[i].getSprite().setPosition(invPositions[i].X + 5, invPositions[i].Y + 5);
                        itemsInInv++;
                        return;
                    }
                }
            }
        }

        // Add exisiting items to the inventory with set position
        public void addToInv(Item item, int pos)
        {
            inv.Add(pos, item);
            inv[pos].getSprite().setPosition(invPositions[pos].X + 5, invPositions[pos].Y + 5);
            itemsInInv++;
        }

        public void removeFromInv(int i) {
            if (inv.ContainsKey(i)) {
                inv.Remove(i);
            }
        }

        public void removeStackFromInv(Item item)
        {
            int[] array = inv.Keys.ToArray();
            for (int i = 0; i < inv.Count(); i++)
            {
                if (inv.ContainsKey(array[i])) {
                    if (inv[array[i]].getName().Equals(item.getName()))
                    {
                        if (item.getStack() < inv[array[i]].getStack())
                        {
                            inv[i].removeStack(item.getStack());
                            Debug.WriteLine("Removed " + item.getName());
                        }
                        else if (item.getStack() == inv[array[i]].getStack())
                        {
                            removeFromInv(array[i]);
                            Debug.WriteLine("Removed all " + item.getName());
                        }
                    }
                }
            }
        }

        public void Update() {
            if (isHolding) {
                holdingItem.setPositionInInventory(input.getStaticMousePos());
            }
        }

        public void UpdateAnimations(GameTime gameTime) {
            for (int i = 0; i < invSize; i++ ) {
                if (inv.ContainsKey(i))
                {
                    inv[i].Update(gameTime);
                }
            }
        }

        // Checks each inventory rectangle for mouse input. If there's something in the slot it will remmove it and assign it to mouse.
        // If something is already inside the slot it will switch it around.
        public void LMB(Vector2 mousePos) {

            if (dropPos.Contains(mousePos)) {
                if (isHolding) {
                    isHolding = false;
                    holdingItem = null;
                    return;
                }
            }

            for (int i = 0; i < invSize; i++) {
                if (invPositions[i].Contains(mousePos)) {
                    if (inv.ContainsKey(i)) {
                        if (isHolding)
                        {
                            if (inv[i].getName().Equals(holdingItem.getName()))
                            {
                                int checkEmptySpace = inv[i].getMaxStack() - inv[i].getStack();

                                if (holdingItem.getStack() <= checkEmptySpace)
                                {
                                    inv[i].addStack(holdingItem.getStack());
                                    isHolding = false;
                                    holdingItem = null;
                                }
                                else if (checkEmptySpace <= 0) {
                                    Item tempItem = inv[i];
                                    inv[i] = holdingItem;
                                    holdingItem = tempItem;
                                    inv[i].setPositionInInventory(new Vector2(invPositions[i].X + 5, invPositions[i].Y + 5));
                                    break;
                                }
                                else
                                {
                                    inv[i].addStack(checkEmptySpace);
                                    holdingItem.removeStack(checkEmptySpace);
                                }
                            }
                            else
                            {
                                Item tempItem = inv[i];
                                inv[i] = holdingItem;
                                holdingItem = tempItem;
                                inv[i].setPositionInInventory(new Vector2(invPositions[i].X + 5, invPositions[i].Y + 5));
                                break;
                            }
                        }
                        else
                        {
                            isHolding = true;
                            holdingItem = inv[i];
                            removeFromInv(i);
                            itemsInInv--;
                            break;
                        }
                    } else if (isHolding) {
                        // If the there's empty space add to inventory and remove holdingItem
                        addToInv(holdingItem, i);
                        isHolding = false;
                        holdingItem = null;
                    }
                }
            }
        }

        public void RMB(Vector2 mousePos) {
            if (isHolding)
            {
                for (int i = 0; i < invSize; i++)
                {
                    if (invPositions[i].Contains(mousePos))
                    {
                        if (inv.ContainsKey(i))
                        {
                            if (inv[i].getName().Equals(holdingItem.getName()))
                            {
                                // If there's an item in click poition remove stack from holdingItem to the inventory item
                                if (inv[i].getMaxStack() > inv[i].getStack())
                                {
                                    holdingItem.removeStack(1);
                                    inv[i].addStack(1);

                                    // If there's no more stack inside the item
                                    if (holdingItem.getStack() <= 0)
                                    {
                                        isHolding = false;
                                        holdingItem = null;
                                    }
                                }

                            }
                        }
                        // If there's only one stack left
                        else if (holdingItem.getStack() <= 1)
                        {
                            addToInv(holdingItem, i);
                            isHolding = false;
                            holdingItem = null;
                        }
                        // Create new item and remove 1 stack from holding item
                        else
                        {
                            holdingItem.removeStack(1);
                            addToInv(itemCreator.createItem(holdingItem.getName(), 1), i);
                        }
                    }
                }
            }
            else {
                for (int i = 0; i < invSize; i++)
                {
                    if (invPositions[i].Contains(mousePos))
                    {
                        if (inv.ContainsKey(i) && inv[i].getIsStackVisible())
                        {
                            if (inv[i].getStack() == 1)
                            {
                                isHolding = true;
                                holdingItem = itemCreator.createItem(inv[i].getName(), 1);
                                removeFromInv(i);
                            }
                            else
                            {
                                int half = inv[i].getStack() / 2;
                                inv[i].removeStack(half);
                                isHolding = true;
                                holdingItem = itemCreator.createItem(inv[i].getName(), half);
                            }
                        }
                    }
                }
            }
        }
        
        public void setToolbar(int x) {
            currentSelectedToolbar = x - 1;
        }

        /// <summary>
        /// Checks if inventory has an item with a set stack
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool checkIfInvHasItem(Item item) {
            if (itemsInInv != 0) {
                foreach (Item invItem in inv.Values) {
                    if (invItem.getName().Equals(item.getName()) && item.getStack() <= invItem.getStack())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Draw(SpriteBatch sp, bool isInvOpen) {
            // Draw toolbar
            sp.Draw(boxTex, toolbarPos[0], Color.White);
            sp.Draw(boxTex, toolbarPos[1], Color.White);
            sp.Draw(boxTex, toolbarPos[2], Color.White);
            sp.Draw(boxTex, toolbarPos[3], Color.White);
            sp.Draw(boxTex, toolbarPos[4], Color.White);
            sp.Draw(boxTex, toolbarPos[5], Color.White);
            sp.Draw(boxTex, toolbarPos[6], Color.White);
            sp.Draw(boxTex, toolbarPos[7], Color.White);
            sp.Draw(boxTex, toolbarPos[8], Color.White);

            sp.Draw(selectedToolbarItemTex, toolbarPos[currentSelectedToolbar], Color.White);
            for (int i = 0; i < 9; i++) {
                if (inv.ContainsKey(i)) {
                    sp.Draw(inv[i].getSprite().getTexture(), new Rectangle(toolbarPos[i].X + 12, toolbarPos[i].Y + 12, 20, 20), inv[i].getSprite().getFrameRectangle(), Color.White);
                    if (inv[i].getIsStackVisible()) {
                        sp.DrawString(fonts["ClockFont"], inv[i].getStack().ToString(), new Vector2(toolbarPos[i].X + 30,
                                                                                            toolbarPos[i].Y + 30), Color.White);
                    }
                }
            }



            // Draw inventory only if it's open
            if (isInvOpen) {
                sp.Draw(backgroundTex, new Rectangle(165, 65, 470, 470), Color.White);
                sp.Draw(dropButtonTex, dropPos, Color.White);
                for (int i = 0; i < invSize; i++) {
                    sp.Draw(boxTex, invPositions[i], Color.White);
                    if (inv.ContainsKey(i))
                    {
                        sp.Draw(inv[i].getSprite().getTexture(), new Rectangle((int)invPositions[i].X + 25,
                                                                    (int)invPositions[i].Y + 25, 50, 50), inv[i].getSprite().getFrameRectangle(), Color.White);
                        if (inv[i].getIsStackVisible())
                        {
                            sp.DrawString(fonts["ClockFont"], inv[i].getStack().ToString(), new Vector2((int)invPositions[i].X + 75,
                                                                                                (int)invPositions[i].Y + 75), Color.White);
                        }
                    }
                }

                // Draw item next to mouse pos
                if (isHolding) {
                    sp.Draw(holdingItem.getSprite().getTexture(), new Rectangle((int)input.getStaticMousePos().X, (int)input.getStaticMousePos().Y, 20, 20), holdingItem.getSprite().getFrameRectangle(), Color.White);
                    if (holdingItem.getIsStackVisible())
                    {
                        sp.DrawString(fonts["ClockFont"], holdingItem.getStack().ToString(), new Vector2((int)input.getStaticMousePos().X + 20,
                                                                                            (int)input.getStaticMousePos().Y + 20), Color.White);
                    }
                }
            }
            // Draw item the player is holding next to the mouse pos
            else if (inv.ContainsKey(currentSelectedToolbar)) {
                sp.Draw(inv[currentSelectedToolbar].getSprite().getTexture(), new Rectangle((int)input.getStaticMousePos().X, (int)input.getStaticMousePos().Y, 20, 20), inv[currentSelectedToolbar].getSprite().getFrameRectangle(), Color.White);
                input.getStaticMousePos();
            }
        }
    }
}
