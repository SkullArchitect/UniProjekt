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
    public class Journal
    {
        private UI_Manager uiManager;
        private Quest_Manager questManager;
        private Texture2D background;
        private Texture2D questFrame;
        private Texture2D rightArrow;
        private Texture2D leftArrow;
        private SpriteFont font;
        private Rectangle backgroundPos;
        private List<Rectangle> questPositions;
        private List<Rectangle> buttonPositions;
        private List<bool> areButtonsEnabled;
        private List<Quest> quests;
        private Quest selectedQuest;
        private int questIndex;
        private bool isQuestSelected;

        public Journal(Texture2D background, Texture2D questFrame, Texture2D rightArrow, Texture2D leftArrow, SpriteFont font, UI_Manager uiManager) {
            this.background = background;
            this.uiManager = uiManager;
            this.questFrame = questFrame;
            this.font = font;
            this.leftArrow = leftArrow;
            this.rightArrow = rightArrow;
            questManager = Quest_Manager.Instance;
            backgroundPos = new Rectangle(500, 112, 288, 375);
            questPositions = new List<Rectangle>();
            buttonPositions = new List<Rectangle>();
            quests = new List<Quest>();
            areButtonsEnabled = new List<bool>();
            questPositions.Add(new Rectangle(520, 132, 248, 75));
            questPositions.Add(new Rectangle(520, 208, 248, 75));
            questPositions.Add(new Rectangle(520, 284, 248, 75));
            questPositions.Add(new Rectangle(520, 360, 248, 75));
            buttonPositions.Add(new Rectangle(520, 438, 40, 40));
            buttonPositions.Add(new Rectangle(728, 438, 40, 40));
            areButtonsEnabled.Add(false);
            areButtonsEnabled.Add(false);
            questIndex = 0;
            isQuestSelected = false;
            selectedQuest = null;
        }

        public void LMB(Vector2 mousePos) {
            for(int i = 0; i < 4; i++) {
                if (questPositions[i].Contains(mousePos) && i < quests.Count()) {
                    isQuestSelected = true;
                    selectedQuest = quests[i];
                    areButtonsEnabled[0] = true;
                }
            }
            if (buttonPositions[0].Contains(mousePos) && areButtonsEnabled[0])
            {
                previousQuests();
            }
            if (buttonPositions[1].Contains(mousePos) && areButtonsEnabled[1])
            {
                nextQuests();
            }
        }

        public void openJournal() {
            quests = questManager.getAcceptedQuessts();
            questIndex = 0;
            isQuestSelected = false;
            selectedQuest = null;
            checkIfButtonsShouldBeEnabled();
        }

        public void Draw(SpriteBatch sp) {
            sp.Draw(background, backgroundPos, Color.White);

            if (!isQuestSelected)
            {

                for (int i = questIndex; i < questIndex + 4; i++)
                {
                    //Debug.WriteLine(i + " " + quests.Count());
                    if (i < quests.Count())
                    {
                        //Debug.WriteLine("I worked!");
                        sp.Draw(questFrame, questPositions[i], Color.White);
                        sp.DrawString(font, quests[i].getQuestName(), new Vector2(questPositions[i].X + 20, questPositions[i].Y + 37), Color.White);
                    }
                }
                if (areButtonsEnabled[1])
                    sp.Draw(rightArrow, buttonPositions[1], Color.White);
                if (areButtonsEnabled[0])
                    sp.Draw(leftArrow, buttonPositions[0], Color.White);
            }
            else {
                sp.Draw(leftArrow, buttonPositions[0], Color.White);
                sp.DrawString(font, selectedQuest.getQuestName(), new Vector2(backgroundPos.X+20, backgroundPos.Y+20), Color.White);
                sp.DrawString(font, selectedQuest.getQuestDesc(), new Vector2(backgroundPos.X + 20, backgroundPos.Y + 40), Color.White);
                sp.DrawString(font, "Requirements: ", new Vector2(backgroundPos.X + 20, backgroundPos.Y + 100), Color.White);
                int y = 0;
                foreach (Item item in selectedQuest.getRequirementList()) {
                    y += 20;
                    sp.DrawString(font, item.getStack() + "x" + item.getName(), new Vector2(backgroundPos.X + 20, backgroundPos.Y + 100 + y), Color.White);
                }
                sp.DrawString(font, "Rewards: ", new Vector2(backgroundPos.X + 20, backgroundPos.Y + 140 + y), Color.White);
                foreach (Item item in selectedQuest.getRewardList())
                {
                    y += 20;
                    sp.DrawString(font, item.getStack() + "x" + item.getName(), new Vector2(backgroundPos.X + 20, backgroundPos.Y + 140 + y), Color.White);
                }
            }
        }

        public void nextQuests() {
            if (questIndex < quests.Count())
            {
                questIndex += 4;
                checkIfButtonsShouldBeEnabled();
            }
        }
        public void previousQuests() {
            if (isQuestSelected)
            {
                isQuestSelected = false;
                checkIfButtonsShouldBeEnabled();
            }
            else {
                if (questIndex > 0)
                {
                    questIndex -= 4;
                    checkIfButtonsShouldBeEnabled();
                }
            }
        }
        public void checkIfButtonsShouldBeEnabled() {
            if (questIndex == 0)
            {
                areButtonsEnabled[0] = false;
            }
            else
            {
                areButtonsEnabled[0] = true;
            }

            if (questIndex+4 >= quests.Count())
            {
                areButtonsEnabled[1] = false;
            }
            else
            {
                areButtonsEnabled[1] = true;
            }
        }
    }
}
