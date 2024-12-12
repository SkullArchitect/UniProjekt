using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Quest_Manager
    {
        Quest_Manager()
        {
        }
        private static readonly object padlock = new object();
        private static Quest_Manager instance = null;
        public static Quest_Manager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Quest_Manager();
                    }
                    return instance;
                }
            }
        }

        private List<Quest> acceptedQuests = new List<Quest>();
        private List<Quest> finishedQuests = new List<Quest>();
        private Quest selectedQuest;
        private NPC_Text_Node currentTextNode;
        private TeaGame game;
        private UI_Manager uiManager;
        private int acceptedQuestsNumber = 0;

        public void playerResponse(int i) {
            if (currentTextNode.getPlayerNodes()[i].getDoesAcceptQuest())
            {
                acceptedQuests.Add(selectedQuest);
                selectedQuest.setIsAccepted(true);
                Debug.WriteLine("Accepted " + acceptedQuests.Count());
            }
            currentTextNode = currentTextNode.getPlayerNodes()[i].getNpcTextNode();
            passStringsToDialogue();
        }

        public void passStringsToDialogue() {
            uiManager.getDialogue().setNpcDialouge(currentTextNode.getNpcLines());
            if (currentTextNode.getPlayerNodes() != null || currentTextNode.getPlayerNodes().Count() != 0) {
                foreach (Player_Text_Node playerNode in currentTextNode.getPlayerNodes()) {
                    uiManager.getDialogue().addPlayerAnswer(playerNode.getPlayerResponse());
                }
            }
        }

        public void setSelectedQuest(Quest selectedQuest) {
            this.selectedQuest = selectedQuest;
            currentTextNode = selectedQuest.getStartingNode();
            passStringsToDialogue();
        }

        public void checkForAcceptedQuestsInAnNPC(List<Quest> quests) {
            foreach (Quest quest in quests) {
                if (quest.getIsAccepted()) {
                    int p = quest.getRequirementList().Count();
                    for (int i = 0; i < p; i++) {
                        if (uiManager.inv.checkIfInvHasItem(quest.getRequirementList()[i]))
                        {
                            uiManager.inv.removeStackFromInv(quest.getRequirementList()[i]);
                            if (quest.giveInItem(i))
                            {
                                Debug.WriteLine("I worked!");
                                finishedQuests.Add(quest);
                                acceptedQuests.Remove(quest);
                                foreach (Item item in quest.getRewardList()) {
                                    uiManager.inv.addToInv(item.getName(), item.getStack());
                                }
                            }
                        }
                    }
                }
            }
        }

        public void emptySelectedQuest() {
            selectedQuest = null;
        }

        public void setTeaGame(TeaGame game) {
            this.game = game;
            uiManager = game.ui;
        }

        public void addAcceptedQuest(Quest quest) {
            acceptedQuests.Add(quest);
            acceptedQuestsNumber++;
        }

        public List<Quest> getAcceptedQuessts() {
            return acceptedQuests;
        }

        public void addFinishedQuest(Quest quest) {
            finishedQuests.Add(quest);
        }

        public List<Quest> getFinishedQuests() {
            return finishedQuests;
        }

        public void setAcceptedQuestsNumber(int i) {
            acceptedQuestsNumber = i;
        }

        public int getAcceptedQuestNumber() {
            return acceptedQuestsNumber;
        }
    }
}
