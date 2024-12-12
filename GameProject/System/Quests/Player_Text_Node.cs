using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Player_Text_Node
    {
        private string playerResponse;
        private NPC_Text_Node npcTextNode;
        private bool doesAcceptQuest;

        public Player_Text_Node(bool doesAcceptQuest = false) {
            this.doesAcceptQuest = doesAcceptQuest;
        }

        public void setPlayerResponse(string playerResponse) {
            this.playerResponse = playerResponse;
        }

        public string getPlayerResponse() {
            return playerResponse;
        }

        public void setNpcTextNode(NPC_Text_Node npcTextNode) {
            this.npcTextNode = npcTextNode;
        }

        public NPC_Text_Node getNpcTextNode() {
            return npcTextNode;
        }

        public void setDoesAcceptQuest(bool doesAcceptQuest) {
            this.doesAcceptQuest = doesAcceptQuest;
        }

        public bool getDoesAcceptQuest() {
            return doesAcceptQuest;
        }
    }
}
