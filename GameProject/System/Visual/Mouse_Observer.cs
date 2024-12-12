using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class MouseObserver
    {
        public TeaGame game;
        private Input input;

        // Set of booleans checking for keys.
        private bool isShift;
        private bool isCtrl;
        private bool isAlt;

        public MouseObserver(TeaGame game) {
            isShift = false;
            isCtrl = false;
            isAlt = false;

            this.game = game;
            input = Input.Instance;
        }

        public void LMB(Vector2 localMousePos, Vector2 globalMousePos) {
            if (game.ui.getIsPlayerInUi()) {
                game.ui.mouseClickedLMB(localMousePos);
            }
            else
            {
                if (game.mapManager.LMB(globalMousePos))
                {
                    return;
                }
                else {
                    game.npcManager.LMB(globalMousePos);
                }
            }
        }

        public void RMB(Vector2 localMousePos, Vector2 globalMousePos) {
            if (game.ui.getIsPlayerInUi())
            {
                game.ui.mouseClickedRMB(localMousePos);
            }
            else {
                game.npcManager.RMB(globalMousePos);
            }
        }

        public void setShift(bool isShift = true) {
            this.isShift = isShift;
        }

        public void setCtrl(bool isCtrl = true) {
            this.isCtrl = isCtrl;
        }

        public void setAlt(bool isAlt = true) {
            this.isAlt = isAlt;
        }
    }
}
