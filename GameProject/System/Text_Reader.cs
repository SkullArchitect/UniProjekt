using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GameProject
{
    public class Text_Reader
    {
        private Item_Object_Creator itemCreator;

        public Text_Reader() {
        }

        public List<Quest> loadQuests() {
            StreamReader sr = new StreamReader("System/NPC/Text/NPC_Quests.txt");
            List<Quest> listOfLoadedQuests = new List<Quest>();
            Quest tempQuest = new Quest();
            itemCreator = Item_Object_Creator.Instance;
            string line;
            string[] splitLine;

            while ((line = sr.ReadLine()) != null) {
                splitLine = line.Split(':');

                switch (splitLine[0])
                {
                    case "NEW":
                        tempQuest = new Quest();
                        break;
                    case "END":
                        listOfLoadedQuests.Add(tempQuest);
                        break;
                    case "QUEST_NPC":
                        tempQuest.setQuestNPC(splitLine[1]);
                        break;
                    case "QUEST_NAME":
                        tempQuest.setQuestName(splitLine[1]);
                        break;
                    case "QUEST_DESC":
                        tempQuest.setQuestDesc(splitLine[1]);
                        break;
                    case "ITEM":
                        addItemRequirement(splitLine[1], tempQuest);
                        break;
                    case "REWARD":
                        addReward(splitLine[1], tempQuest);
                        break;
                    default:
                        createNodes(splitLine, tempQuest);
                        break;
                }
            }
            return listOfLoadedQuests;
        }

        private void createNodes(string[] lines, Quest tempQuest)
        {
            string[] splitCommand = lines[0].Split('_');
            string[] tree = splitCommand[1].Split('.');
            int[] numbers = new int[tree.Count()];
            

            for (int i = 0; i < tree.Count(); i++) {
                numbers[i] = Int32.Parse(tree[i]);
            }


            // Check if line belongs to NPC
            if (splitCommand[0].Equals("NPC")) {
                if (numbers.Count() == 1)
                {
                    tempQuest.getStartingNode().addNpcLine(lines[1]);
                    return;
                }
                else {
                    NPC_Text_Node tempNpcNode = tempQuest.getStartingNode();
                    Player_Text_Node tempPlayerNode;

                    for (int i = 1; i < numbers.Count();)
                    {
                        tempPlayerNode = tempNpcNode.getPlayerNodes()[numbers[i]];
                        tempNpcNode = tempPlayerNode.getNpcTextNode();
                        i += 2;
                    }

                    tempNpcNode.addNpcLine(lines[1]);
                }
            }
            else if (splitCommand[0].Equals("USER")) {
                if (numbers.Count() == 1)
                {
                    Player_Text_Node newPlayerNode = new Player_Text_Node(); 
                    newPlayerNode.setNpcTextNode(new NPC_Text_Node());
                    newPlayerNode.setPlayerResponse(lines[1]);
                    tempQuest.getStartingNode().addPlayerNode(newPlayerNode);
                    return;
                }
                else
                {
                    Player_Text_Node newPlayerNode = new Player_Text_Node();
                    newPlayerNode.setNpcTextNode(new NPC_Text_Node());
                    newPlayerNode.setPlayerResponse(lines[1]);

                    NPC_Text_Node tempNpcNode = tempQuest.getStartingNode();
                    Player_Text_Node tempPlayerNode = new Player_Text_Node();

                    for (int i = 1; i < numbers.Count();)
                    {
                        if (i == numbers.Count() - 1)
                        {
                            tempNpcNode.addPlayerNode(newPlayerNode);
                        }
                        else
                        {
                            tempPlayerNode = tempNpcNode.getPlayerNodes()[numbers[i]];
                            tempNpcNode = tempPlayerNode.getNpcTextNode();
                        }
                        i += 2;
                    }
                }
            }
            else if (splitCommand[0].Equals("USERA"))
            {
                if (numbers.Count() == 1)
                {
                    Player_Text_Node newPlayerNode = new Player_Text_Node();
                    newPlayerNode.setNpcTextNode(new NPC_Text_Node());
                    newPlayerNode.setPlayerResponse(lines[1]);
                    newPlayerNode.setDoesAcceptQuest(true);
                    tempQuest.getStartingNode().addPlayerNode(newPlayerNode);
                    return;
                }
                else
                {
                    Player_Text_Node newPlayerNode = new Player_Text_Node();
                    newPlayerNode.setNpcTextNode(new NPC_Text_Node());
                    newPlayerNode.setPlayerResponse(lines[1]);
                    newPlayerNode.setDoesAcceptQuest(true);

                    NPC_Text_Node tempNpcNode = tempQuest.getStartingNode();
                    Player_Text_Node tempPlayerNode = new Player_Text_Node();

                    for (int i = 1; i < numbers.Count();)
                    {
                        if (i == numbers.Count() - 1)
                        {
                            tempNpcNode.addPlayerNode(newPlayerNode);
                        }
                        else
                        {
                            tempPlayerNode = tempNpcNode.getPlayerNodes()[numbers[i]];
                            tempNpcNode = tempPlayerNode.getNpcTextNode();
                        }
                        i += 2;
                    }
                }
            }
        }

        private void addReward(string line, Quest tempQuest)
        {
            string[] splitLine = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string str in splitLine) {
                int stack = Int32.Parse(str.Split('x')[0]);
                string name = str.Split('x')[1];

                tempQuest.addReward(itemCreator.createItem(name, stack));
            }
        }

        private void addItemRequirement(string line, Quest tempQuest)
        {
            string[] splitLine = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string str in splitLine)
            {
                int stack = Int32.Parse(str.Split('x')[0]);
                string name = str.Split('x')[1];

                
                tempQuest.addRequirement(itemCreator.createItem(name, stack));
            }
        }
    }
}
