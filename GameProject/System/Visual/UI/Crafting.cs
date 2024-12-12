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
    public class Crafting
    {
        private UI_Manager uiManager;
        private Item_Object_Creator itemCreator;

        private Texture2D background;
        private Texture2D frame;
        private Texture2D rightArrow;
        private Texture2D leftArrow;
        private SpriteFont font;

        private Rectangle backgroundPos;
        private Rectangle rightArrowPos;
        private Rectangle leftArrowPos;
        private bool isLeftButtonEnabled;
        private bool isRightButtonEnabled;
        private Rectangle[] craftingItemPosArr;

        private Dictionary<string, List<Item>> craftableItems;
        private string currentCraftableStation;
        private int visibleItems;

        public Crafting(Texture2D background, Texture2D frame, Texture2D rightArrow, Texture2D leftArrow, SpriteFont font, UI_Manager uiManager) {
            this.uiManager = uiManager;
            this.background = background;
            this.frame = frame;
            this.rightArrow = rightArrow;
            this.leftArrow = leftArrow;
            isLeftButtonEnabled = false;
            isRightButtonEnabled = false;
            this.font = font;

            currentCraftableStation = null;
            visibleItems = 0;

            itemCreator = Item_Object_Creator.Instance;

            backgroundPos = new Rectangle(256, 150, 288, 375);

            leftArrowPos = new Rectangle(backgroundPos.X+20, backgroundPos.Y + backgroundPos.Height-60, 40, 40);
            rightArrowPos = new Rectangle(backgroundPos.X + backgroundPos.Width - 20, backgroundPos.Y + backgroundPos.Height - 60, 40, 40);

            craftingItemPosArr = new Rectangle[4];

            craftingItemPosArr[0] = new Rectangle(backgroundPos.X + 20, backgroundPos.Y + 20, 248, 75);
            craftingItemPosArr[1] = new Rectangle(backgroundPos.X + 20, backgroundPos.Y + 20 + 75, 248, 75);
            craftingItemPosArr[2] = new Rectangle(backgroundPos.X + 20, backgroundPos.Y + 20 + 160, 248, 75);
            craftingItemPosArr[3] = new Rectangle(backgroundPos.X + 20, backgroundPos.Y + 20 + 235, 248, 75);

            craftableItems = new Dictionary<string, List<Item>>();

            List<Item> kettleItems = new List<Item>();
            kettleItems.Add(itemCreator.createItem("Green_Tea"));
            kettleItems.Add(itemCreator.createItem("Apple_Tea"));
            craftableItems.Add("Kettle", kettleItems);
        }

        public void LMB(Vector2 mousePos) {
            //if (isRightButtonEnabled && rightArrowPos.Contains(mousePos)) {
            //    nextCraftingItems();
            //}
            //else if (isLeftButtonEnabled && leftArrowPos.Contains(mousePos))
            //{
            //    previousCraftingItems();
            //}
            bool flag = true;
            for (int i = 0; i < 2; i++) {
                if (craftingItemPosArr[i].Contains(mousePos)) {
                    if (currentCraftableStation.Equals("Kettle")) {
                        foreach (Item req in craftableItems["Kettle"][i].getCraftingReq())
                        {
                            if (!uiManager.inv.checkIfInvHasItem(req))
                            {
                                Debug.WriteLine(req.getName() + " " + req.getStack());
                                flag = false;
                            }
                        }

                        Debug.WriteLine(flag);

                        if (flag) {
                            uiManager.inv.addToInv(craftableItems["Kettle"][i].getName(), 1);
                            foreach (Item req in craftableItems["Kettle"][i].getCraftingReq())
                            {
                                uiManager.inv.removeStackFromInv(req);
                            }
                        }
                    }
                }
            }
        }

        public void Update() {
            //checkIfButtonsShouldBeEnabled();
        }

        public void Draw(SpriteBatch sp) {
            sp.Draw(background, backgroundPos, Color.White);

            int b = 0;
            if (currentCraftableStation.Equals("Kettle")) {
                for (int i = 0; i < visibleItems+2; i++) {
                    sp.Draw(frame, craftingItemPosArr[i], Color.White);
                    sp.Draw(craftableItems["Kettle"][i].getSprite().getTexture(), new Rectangle(craftingItemPosArr[b].X, craftingItemPosArr[b].Y+20, 
                        craftableItems["Kettle"][i].getSprite().getSpriteWidth(), craftableItems["Kettle"][i].getSprite().getSpriteHeight()),
                        craftableItems["Kettle"][i].getSprite().getFrameRectangle(), Color.White);
                    sp.DrawString(font, craftableItems["Kettle"][i].getStack().ToString(), new Vector2(craftingItemPosArr[b].X+30, 
                        craftingItemPosArr[b].Y + 30), Color.White);
                    sp.DrawString(font, "Needs: ", new Vector2(craftingItemPosArr[b].X + 80, craftingItemPosArr[b].Y + 20), Color.White);

                    int x = 0;
                    foreach (Item item in craftableItems["Kettle"][i].getCraftingReq()) {
                        sp.Draw(item.getSprite().getTexture(), new Rectangle(craftingItemPosArr[b].X+140 + x, craftingItemPosArr[b].Y+20, 
                            32, 32), item.getSprite().getFrameRectangle(), Color.White);
                        sp.DrawString(font, item.getStack().ToString(), new Vector2(craftingItemPosArr[b].X + 170 + x, 
                            craftingItemPosArr[b].Y + 50), checkForItem(item));
                        x += 40;
                    }
                    b++;
                }
            }

            //if (isRightButtonEnabled) {
            //    sp.Draw(rightArrow, rightArrowPos, Color.White);
            //}
            //if (isLeftButtonEnabled)
            //{
            //    sp.Draw(leftArrow, leftArrowPos, Color.White);
            //}
        }

        public Color checkForItem(Item item) {
            if (uiManager.inv.checkIfInvHasItem(item))
            {
                return Color.White;
            }
            else {
                return Color.Red;
            }
        }

        public void setCraftingStation(string craftingStation) {
            this.currentCraftableStation = craftingStation;
        }

        public void checkIfButtonsShouldBeEnabled() {

        }

        public void nextCraftingItems() {

        }
        public void previousCraftingItems() {

        }
    }
}
