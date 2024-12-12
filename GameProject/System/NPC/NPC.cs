using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class NPC
    {
        private Sprite sprite;
        private DepthDraw depthDraw;
        private List<Quest> quests;
        private string name;

        public NPC(string name, List<Animation> animations, int width, int height, int speed, int health, int maxHealth, int mana, int maxMana, int xPos, int yPos) {
            sprite = new Sprite(animations, width, height, speed, health, maxHealth, mana, maxMana);
            sprite.setPosition(xPos, yPos);
            depthDraw = DepthDraw.Instance;
            this.name = name;
            quests = new List<Quest>();
        }

        public void Update(GameTime gameTime) {
            sprite.TimeUpdate(gameTime);
        }

        public void AddToDraw()
        {
            depthDraw.InsertSprite(sprite);
        }

        public Quest getAvaliableQuest() {
            foreach (Quest singleQuest in quests) {
                if (singleQuest.getCanBeAccpeted() && !singleQuest.getIsAccepted()) {
                    return singleQuest;
                }
            }

            return null;
        }

        public void addQuest(Quest quest) {
            quests.Add(quest);
        }

        public List<Quest> getQuestList() {
            return quests;
        }

        public Quest checkForPossibleQuest() {
            if (quests.Count() > 0)
            {
                foreach (Quest q in quests) {
                    if (q.getCanBeAccpeted()) return q;
                }
            }
            return null;
        }

        public void setSprite(Sprite sprite) {
            this.sprite = sprite;
        }

        public Sprite getSprite() {
            return sprite;
        }

        public void setName(string name) {
            this.name = name;
        }

        public string getName() {
            return name;
        }
    }
}