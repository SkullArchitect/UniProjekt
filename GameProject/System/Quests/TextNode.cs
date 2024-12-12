using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class TextNode
    {
        private Quest mainQuest;
        private List<string> npcLines;
        private List<string> userResponses;
        private List<TextNode> nodes;
        private bool canUserRespond;
        private bool doesResponseAcceptTheQuest;

        /// <summary>
        /// Node takes npcLines that are displayed before the user response.
        /// </summary>
        /// <param name="npcLines">List of NPC lines</param>
        /// <param name="canUserRespond">Checks if the user can respond</param>
        /// <param name="userResponses">Max four in the list. This is due to button limitation</param>
        public TextNode(Quest mainQuest) {
            this.mainQuest = mainQuest;
            this.npcLines = new List<string>();
            this.canUserRespond = false;
            this.nodes = new List<TextNode>();
        }

        public void setNpcLines(List<string> npcLines) {
            this.npcLines = npcLines;
        }

        public List<string> getNpcLines() {
            return npcLines;
        }

        public void addNpcLines(string npcLine) {
            npcLines.Add(npcLine);
        }

        public void setNodesList(List<TextNode> userResponses) {
            this.nodes = userResponses;
        }

        public List<TextNode> getNodesList() {
            return nodes;
        }

        public void addNode(TextNode node) {
            nodes.Add(node);
        }

        public void setCanUserResponses(bool canUserRespond) {
            this.canUserRespond = canUserRespond;
        }

        public bool getCanUserRespond() {
            return canUserRespond;
        }

        public void addUserResponse(string userResponse) {
            userResponses.Add(userResponse);
        }

        public void setDoesResponseAcceptTheQuest(bool doesResponseAcceptTheQuest) {
            this.doesResponseAcceptTheQuest = doesResponseAcceptTheQuest;
        }

        public bool getDoesResponseAcceptTheQuest() {
            return doesResponseAcceptTheQuest;
        }
    }
}
