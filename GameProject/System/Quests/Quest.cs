using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Quest
    {
        private string questNPC;
        private string questName;
        private string questDesc;
        private bool canBeAccepted;
        private bool isAccepted;
        private bool isFinished;
        private NPC_Text_Node startingNode;
        private NPC_Text_Node completionNode;
        private List<Item> reward;
        private List<Item> requirement;
        private List<Item> givenRequirements;

        public Quest() {
            reward = new List<Item>();
            requirement = new List<Item>();
            startingNode = new NPC_Text_Node();
            givenRequirements = new List<Item>();
            isAccepted = false;
        }

        public void setQuestNPC(string questNPC) {
            this.questNPC = questNPC;
        }

        public string getQuestNPC() {
            return questNPC;
        }

        public void setQuestName(string questName) {
            this.questName = questName;
        }

        public string getQuestName() {
            return questName;
        }

        public void setStartingNode(NPC_Text_Node startingNode) {
            this.startingNode = startingNode;
        }

        public NPC_Text_Node getStartingNode() {
            return startingNode;
        }

        public void setQuestDesc(string questDesc) {
            this.questDesc = questDesc;
        }

        public string getQuestDesc() {
            return questDesc;
        }

        public void setCanBeAccepted(bool canBeAccepted) {
            this.canBeAccepted = canBeAccepted;
        }

        public bool getCanBeAccpeted() {
            return canBeAccepted;
        }

        public void addReward(Item item) {
            reward.Add(item);
        }

        public List<Item> getRewardList() {
            return reward;
        }

        public void addRequirement(Item item) {
            requirement.Add(item);
        }

        public List<Item> getRequirementList() {
            return requirement;
        }

        public void setIsFinished(bool isFinished) {
            this.isFinished = isFinished;
        }

        public bool getIsFinished() {
            return isFinished;
        }

        public void setIsAccepted(bool accepted) {
            this.isAccepted = accepted;
        }

        public bool getIsAccepted() {
            return isAccepted;
        }

        public bool giveInItem(int i) {
            givenRequirements.Add(requirement[i]);
            requirement.RemoveAt(i);

            if (requirement.Count() == 0)
            {
                isFinished = true;
                return true;
            }
            
            return false;
        }
    }
}
