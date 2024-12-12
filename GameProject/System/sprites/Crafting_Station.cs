using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Crafting_Station
    {
        private TeaGame game;
        private string nameOfCraftingBlock;

        public Crafting_Station(TeaGame game, string nameOfCraftingBlock) {
            this.game = game;
            this.nameOfCraftingBlock = nameOfCraftingBlock;
        }

        public void LMB() {
            if (nameOfCraftingBlock.Equals("Kettle")) {
                game.ui.openCraftingStation("Kettle");
            }
        }
    }
}
