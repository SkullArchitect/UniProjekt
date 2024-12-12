using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class PlayerManager
    {
        public TeaGame game;
        public Input input;
        public PlayerClass player;
        public Pet pet;
        private Vector2 direction;

        private KeyboardState kState;
        private KeyboardState oldKState;
        private MouseState mState;
        private MouseState oldMState;

        bool isPetMoving = false;

                
        public PlayerManager(TeaGame game, List <Animation> playerAnimations, List <Animation> petAnimations) {
            this.game = game;
            input = Input.Instance;
            player = new PlayerClass(game, playerAnimations);
            pet = new Pet(game, petAnimations);
        }

        public void Update(GameTime gameTime) {
            kState = input.getKState();
            mState = input.getMState();
            Keys[] currentPressedKeyboardKeys = kState.GetPressedKeys();

            direction = Vector2.Zero;

            if (mState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released) {
                game.mouseObs.LMB(input.getStaticMousePos(), input.getGlobalMousePos());
            }
            if (mState.RightButton == ButtonState.Pressed && oldMState.RightButton == ButtonState.Released)
            {
                game.mouseObs.RMB(input.getStaticMousePos(), input.getGlobalMousePos());
            }

            foreach (Keys key in currentPressedKeyboardKeys) {
                if (key == Keys.W)
                {
                    direction.Y = -1;
                }
                if (key == Keys.S)
                {
                    direction.Y = 1;
                }
                if (key == Keys.A)
                {
                    direction.X = -1;
                }
                if (key == Keys.D)
                {
                    direction.X = 1;
                }
                if (key == Keys.J && oldKState.IsKeyUp(Keys.J))
                {
                    game.ui.toggleJournal();
                }
                if (key == Keys.Escape && oldKState.IsKeyUp(Keys.Escape)) {
                    if (game.ui.getIsDialogueOpen()) {

                    }
                    else if (game.ui.getIsPlayerInUi())
                    {
                        game.ui.closeAllUI();
                    }
                    else {
                        game.isInMainMenu = true;
                    }
                }
                if (key == Keys.I && oldKState.IsKeyUp(Keys.I)) {
                    game.ui.toggleInv();
                }
                if (key == Keys.D1) {
                    game.ui.KeyboardNumericalInput(1);
                }
                if (key == Keys.D2)
                {
                    game.ui.KeyboardNumericalInput(2);
                }
                if (key == Keys.D3)
                {
                    game.ui.KeyboardNumericalInput(3);
                }
                if (key == Keys.D4)
                {
                    game.ui.KeyboardNumericalInput(4);
                }
                if (key == Keys.D5)
                {
                    game.ui.KeyboardNumericalInput(5);
                }
                if (key == Keys.D6)
                {
                    game.ui.KeyboardNumericalInput(6);
                }
                if (key == Keys.D7)
                {
                    game.ui.KeyboardNumericalInput(7);
                }
                if (key == Keys.D8)
                {
                    game.ui.KeyboardNumericalInput(8);
                }
                if (key == Keys.D9)
                {
                    game.ui.KeyboardNumericalInput(9);
                }
                // Testing keys
                if (key == Keys.P && oldKState.IsKeyUp(Keys.P)) {
                    player.getSprite().addToHealth(2);
                }
                if (key == Keys.O && oldKState.IsKeyUp(Keys.O)) {
                    player.getSprite().addToHealth(-2);
                }
                if (key == Keys.L && oldKState.IsKeyUp(Keys.L))
                {
                    player.getSprite().addToMana(2);
                }
                if (key == Keys.K && oldKState.IsKeyUp(Keys.K))
                {
                    player.getSprite().addToMana(-2);
                }
                if (key== Keys.M && oldKState.IsKeyUp(Keys.M)) {
                    game.ui.inv.addToInv("Apple", 5);
                }
                if (key == Keys.N && oldKState.IsKeyUp(Keys.N))
                {
                    game.ui.inv.addToInv("HarvestingTool", 5);
                }
            }
            
            
            player.Update(gameTime, direction);
            pet.update(gameTime);
            movePet(gameTime);
            oldKState = kState;
            oldMState = mState;
        }

        public void movePet(GameTime gameTime) {
            if (Vector2.Distance(pet.getSprite().getCentrePosition(), player.getSprite().getCentrePosition()) > 200)
            {
                isPetMoving = true;
            }
            if (isPetMoving == true)
            {
                pet.moveTo(gameTime, player.getSprite().getCentrePosition());
                if (Vector2.Distance(pet.getSprite().getCentrePosition(), player.getSprite().getCentrePosition()) <= 75) {
                    isPetMoving = false;
                }
            }
        }

        public PlayerClass getPlayer() {
            return player;
        }



    }
}
