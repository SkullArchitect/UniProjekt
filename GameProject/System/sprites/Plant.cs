using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Plant
    {
        TeaGame game;
        Random random;
        private bool isStagable;
        private int stage;
        private int maxStage;

        private float timer;
        private float CTimer;

        private string givesItem;
        private int minStack;
        private int maxStack;

        public Plant(TeaGame game, bool isStagable, int maxStage, float timer, string givesItemName, int minStack, int maxStack) {
            this.game = game;
            this.isStagable = isStagable;
            stage = 0;
            this.maxStage = maxStage;
            this.timer = timer;
            CTimer = timer;
            givesItem = givesItemName;
            this.minStack = minStack;
            this.maxStack = maxStack;

            random = new Random();
        }

        public void Update(GameTime gameTime) {
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer < 0) {
                if ((stage + 1) > maxStage)
                {

                }
                else {
                    stage++;
                }
                timer = CTimer;
            }
        }

        public void LMB() {
            if (stage >= maxStage) {
                game.ui.inv.addToInv(givesItem, random.Next(minStack, maxStack));
                stage = 0;
            }
        }

        public void setIsStagable(bool isStagable)
        {
            this.isStagable = isStagable;
        }

        public bool getIsStagable()
        {
            return isStagable;
        }

        public void setStage(int stage)
        {
            this.stage = stage;
        }

        public int getStage()
        {
            return stage;
        }

        public void addToStage(int i)
        {
            stage += i;
        }
    }
}
