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
    public class Dialogue
    {
        private Quest_Manager questManager;
        private UI_Manager uiManager;
        private Texture2D texture;
        private SpriteFont font;
        private Rectangle backgroundPos;
        private Rectangle[] playerAnswerPos;
        private bool isUIOpen;
        private bool isDialogueFinished;
        private bool canUserRespond;
        private bool isUserResponding;
        private List<string> npcDialogue;
        private List<string> playerAnswers;
        private int currentLine;

        public Dialogue(Texture2D texture, SpriteFont font, UI_Manager uiManager) {
            questManager = Quest_Manager.Instance;
            this.uiManager = uiManager;
            this.texture = texture;
            this.font = font;
            isUIOpen = false;
            backgroundPos = new Rectangle(60, 310, 680, 280);
            playerAnswerPos = new Rectangle[4];
            playerAnswerPos[0] = new Rectangle(80, 500, 330, 40);
            playerAnswerPos[1] = new Rectangle(80, 540, 330, 40);
            playerAnswerPos[2] = new Rectangle(160, 500, 330, 40);
            playerAnswerPos[3] = new Rectangle(160, 540, 330, 40);
            currentLine = 0;
            canUserRespond = false;
            isUserResponding = false;
            npcDialogue = new List<string>();
            playerAnswers = new List<string>();
        }

        public void Update() {

        }

        public void Draw(SpriteBatch sp) {
            sp.Draw(texture, backgroundPos, Color.White);
            sp.DrawString(font, npcDialogue[currentLine], new Vector2(backgroundPos.X+25, backgroundPos.Y+25), Color.White);

            int i = 0;
            if (isUserResponding) {
                foreach (string response in playerAnswers) {
                    sp.DrawString(font, response, new Vector2(playerAnswerPos[i].X, playerAnswerPos[i].Y), Color.White);
                    i++;
                }
            }
        }

        public void LMB(Vector2 mousePos) {
            if (!checkIfLastLine())
            {
                currentLine++;
            }
            if (checkIfLastLine())
            {
                if (canUserRespond && !isUserResponding)
                {
                    isUserResponding = true;
                }
                else if (isUserResponding) {
                    for (int i = 0; i < playerAnswers.Count(); i++)
                    {
                        if (playerAnswerPos[i].Contains(mousePos))
                        {
                            questManager.playerResponse(i);
                            canUserRespond = false;
                            isUserResponding = false;
                        }
                    }
                }
                else
                {
                    uiManager.setIsDialogueOpen(false);
                    npcDialogue.Clear();
                    playerAnswers.Clear();
                    currentLine = 0;
                    isUIOpen = false;
                }
            }
        }

        public void closeDialouge() {
            uiManager.setIsDialogueOpen(false);
            npcDialogue.Clear();
            playerAnswers.Clear();
            currentLine = 0;
            isUIOpen = false;
        }

        private bool checkIfLastLine() {
            if (currentLine < npcDialogue.Count()-1) {
                return false;
            }
            return true;
        }

        public void setIsUIOpen(bool isUIOpen) {
            this.isUIOpen = isUIOpen;
        }

        public bool getIsUIOpen() {
            return isUIOpen;
        }

        public void setIsDialogueFinished(bool isDialogueFinished) {
            this.isDialogueFinished = isDialogueFinished;
        }

        public bool getIsDialogueFinished() {
            return isDialogueFinished;
        }

        public void setTexture(Texture2D texture) {
            this.texture = texture;
        }

        public Texture2D getTexture() {
            return texture;
        }

        public void setNpcDialouge(List<string> npcDialogue) {
            currentLine = 0;
            this.npcDialogue = npcDialogue;
        }

        public List<string> getNpcDialogue() {
            return npcDialogue;
        }
        
        public void setPlayerAnswers(List<string> userAnswers) {
            this.playerAnswers = userAnswers;
            canUserRespond = true;
        }

        public List<string> getPlayerAnswers() {
            return playerAnswers;
        }

        public void addPlayerAnswer(string answer) {
            playerAnswers.Add(answer);
            canUserRespond = true;
        }
    }
}
