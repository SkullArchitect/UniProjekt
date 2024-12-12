using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class NPC_Text_Node
    {
        private List<string> npcLines;
        private List<Player_Text_Node> playerNodes;

        public NPC_Text_Node() {
            npcLines = new List<string>();
            playerNodes = new List<Player_Text_Node>();
        }

        public void setNpcLines(List<string> npcLines) {
            this.npcLines = npcLines;
        }

        public List<string> getNpcLines() {
            return npcLines;
        }

        public void addNpcLine(string npcLine) {
            npcLines.Add(npcLine);
        }

        public void setPlayerNodes(List<Player_Text_Node> playerNodes)
        {
            this.playerNodes = playerNodes;
        }

        public List<Player_Text_Node> getPlayerNodes()
        {
            return playerNodes;
        }

        public void addPlayerNode(Player_Text_Node playerNode)
        {
            playerNodes.Add(playerNode);
        }
    }
}
