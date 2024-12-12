using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class UI_Manager
    {
        private Dictionary<string, Texture2D> staticUI;
        public TeaGame game;
        public Health health;
        public Mana mana;
        public Inventory inv;
        public Clock clock;
        public Dialogue dialogue;
        public Journal journal;
        public Crafting crafting;

        private bool isPlayerInUI;
        private bool isJournalOpen;
        private bool isInvOpen;
        private bool isDialogueOpen;
        private bool isCraftingOpen;

        public UI_Manager(TeaGame game, Dictionary<string, Texture2D> staticUI, Dictionary<string, SpriteFont> fonts) {
            this.staticUI = staticUI;
            this.game = game;
            health = new Health(game, staticUI["Pixel"]);
            mana = new Mana(game, staticUI["Pixel"]);
            inv = new Inventory(game, staticUI["InvBackground"], staticUI["Box"], staticUI["DropButton"], staticUI["SelectedToorbar"], fonts);
            clock = new Clock(fonts);
            dialogue = new Dialogue(staticUI["Dialogue_box"], fonts["ClockFont"], this);
            journal = new Journal(staticUI["Journal_Background"], staticUI["Journal_Quest_Frame"], staticUI["Journal_Right_Arrow"], 
                                    staticUI["Journal_Left_Arrow"], fonts["ClockFont"], this);
            crafting = new Crafting(staticUI["Journal_Background"], staticUI["Journal_Quest_Frame"], staticUI["Journal_Right_Arrow"],
                                    staticUI["Journal_Left_Arrow"], fonts["ClockFont"], this);
            isInvOpen = false;
            isDialogueOpen = false;
            isJournalOpen = false;
            isPlayerInUI = false;
            isCraftingOpen = false;
        }

        public void Update(GameTime gameTime) {
            health.Update();
            mana.Update();
            clock.Update(gameTime);
            dialogue.Update();
            if (isInvOpen) {
                inv.UpdateAnimations(gameTime);
            }
        }

        public void Draw(SpriteBatch sp) {
            health.Draw(sp);
            mana.Draw(sp);
            clock.Draw(sp);
            inv.Draw(sp, isInvOpen);
            if (isDialogueOpen)
            {
                dialogue.Draw(sp);
            }
            if (isJournalOpen) {
                journal.Draw(sp);
            }
            if (isCraftingOpen) {
                crafting.Draw(sp);
            }
        }

        public void setIsInvOpen(bool isInvOpen) {
            this.isInvOpen = isInvOpen;
            checkIfPlayerInUI();
        }

        public bool getIsInvOpen() {
            return isInvOpen;
        }

        public void toggleInv() {
            if (isInvOpen)
            {
                setIsInvOpen(false);
            }
            else {
                setIsInvOpen(true);
            }
        }

        public void setIsDialogueOpen(bool isDialogueOpen) {
            this.isDialogueOpen = isDialogueOpen;
            checkIfPlayerInUI();
        }

        public bool getIsDialogueOpen() {
            return isDialogueOpen;
        }

        public void setIsJournalOpen(bool isJournalOpen) {
            this.isJournalOpen = isJournalOpen;
            checkIfPlayerInUI();
        }

        public bool getIsJournalOpen() {
            return isJournalOpen;
        }

        public bool getIsPlayerInUi() {
            return isPlayerInUI;
        }

        public void toggleJournal() {
            if (isJournalOpen)
            {
                setIsJournalOpen(false);
            }
            else {
                setIsJournalOpen(true);
                journal.openJournal();
            }
        }

        public void checkIfPlayerInUI() {
            if (isInvOpen || isJournalOpen || isDialogueOpen)
            {
                isPlayerInUI = true;
            }
            else isPlayerInUI = false;
        }

        public void mouseClickedLMB(Vector2 mousePos) {
            if (isInvOpen) {
                inv.LMB(mousePos);
            }
            if (isDialogueOpen) {
                dialogue.LMB(mousePos);
            }
            if (isJournalOpen) {
                journal.LMB(mousePos);
            }
            if (isCraftingOpen) {
                crafting.LMB(mousePos);
            }
        }

        public void mouseClickedRMB(Vector2 mousePos)
        {
            inv.RMB(mousePos);
        }

        public void KeyboardNumericalInput(int input) {
            inv.setToolbar(input);
        }

        public Dialogue getDialogue() {
            return dialogue;
        }

        public void openCraftingStation(string craftingStation) {
            crafting.setCraftingStation(craftingStation);
            isCraftingOpen = true;
            isPlayerInUI = true;
        }

        public void closeCraftingStation() {
            isCraftingOpen = false;
            isPlayerInUI = false;
        }

        public void closeAllUI() {
            isInvOpen = false;
            isJournalOpen = false;
            isCraftingOpen = false;
            isPlayerInUI = false;
        }
    }
}
