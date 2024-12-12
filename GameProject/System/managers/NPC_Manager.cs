using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class NPC_Manager
    {
        private TeaGame game;
        private UI_Manager uiManager;
        private Quest_Manager questManager;
        private List<NPC> npcs;
        private List<NPC> renderedNPCs;

        private Rectangle renderPos;
        private Sprite playerSprite;


        public NPC_Manager(TeaGame game, Dictionary<string, List<Animation>> animations, List<Quest> listOfQuests) {
            this.game = game;
            questManager = Quest_Manager.Instance;
            playerSprite = game.playerManager.getPlayer().getSprite();
            uiManager = game.ui;
            npcs = new List<NPC>();
            renderedNPCs = new List<NPC>();
            npcCreator(animations, listOfQuests);
        }

        public void Update(GameTime gameTime) {
            renderPos = new Rectangle((int)playerSprite.getCentrePosition().X - 600, (int)playerSprite.getCentrePosition().Y - 400, 1200, 800);
            renderedNPCs.Clear();

            foreach (NPC singleNPC in npcs) {
                singleNPC.Update(gameTime);

                if (singleNPC.getSprite().getPosition().Intersects(renderPos)) {
                    singleNPC.AddToDraw();
                    renderedNPCs.Add(singleNPC);
                }
            }
        }

        public void LMB(Vector2 mousePos) {
            foreach (NPC singleNPC in renderedNPCs)
            {
                if (singleNPC.getSprite().getPosition().Contains(mousePos))
                {
                    if (singleNPC.getAvaliableQuest() != null) {
                        questManager.setSelectedQuest(singleNPC.getAvaliableQuest());
                        uiManager.setIsDialogueOpen(true);
                    }
                }
            }
        }

        public void RMB(Vector2 mousePos) {
            foreach (NPC singleNPC in renderedNPCs)
            {
                if (singleNPC.getSprite().getPosition().Contains(mousePos))
                {
                    questManager.checkForAcceptedQuestsInAnNPC(singleNPC.getQuestList());
                }
            }
        }

        private void npcCreator(Dictionary<string, List<Animation>> animations, List<Quest> listOfQuests) {
            NPC tempNpc;
            // Guard NPC
            tempNpc = new NPC("Guard_1", animations["Guard_1"], 48, 112, 6, 100, 100, 100, 100, 100, 100);
            npcs.Add(tempNpc);
            tempNpc = new NPC("Guard_2", animations["Guard_2"], 48, 112, 6, 100, 100, 100, 100, 150, 100);
            npcs.Add(tempNpc);
            tempNpc = new NPC("Guard_3", animations["Guard_3"], 48, 112, 6, 100, 100, 100, 100, 200, 100);
            npcs.Add(tempNpc);
            tempNpc = new NPC("Guard_4", animations["Guard_4"], 48, 112, 6, 100, 100, 100, 100, 250, 100);
            npcs.Add(tempNpc);

            foreach (Quest quest in listOfQuests) {
                foreach (NPC npc in npcs) {
                    if (npc.getName().Equals(quest.getQuestNPC())) {
                        quest.setCanBeAccepted(true);
                        npc.addQuest(quest);
                    }
                }
            }
        }
    }
}
