using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class ContentLoader
    {
        private ContentManager content;
        private Texture2D tempTex;
        private Text_Reader textReader;

        private List <Animation> petAnimations;
        private List <Animation> playerAnimations;
        private Dictionary<string, List<Animation>> npcAnimations;
        private Dictionary<string, Texture2D> tileSet;
        private Dictionary<string, Texture2D> staticUI;
        private Dictionary<string, Texture2D> itemTextures;
        private Dictionary<string, SpriteFont> fonts;
        private Dictionary<string, Quest> quests;

        public ContentLoader(ContentManager content) {
            this.content = content;
            textReader = new Text_Reader();
        }

        public List <Animation> loadPlayer() {
            // All player animations load here
            playerAnimations = new List <Animation>();
            Animation tempPlayerAnimation = new Animation(getTexture("Player/Player_Idle_Down"), 2);
            tempPlayerAnimation.setFrameHeight(28);
            tempPlayerAnimation.setFrameWidth(15);
            tempPlayerAnimation.setFrameSpeed(2);
            playerAnimations.Add(tempPlayerAnimation);

            tempPlayerAnimation = new Animation(getTexture("Player/Player_Idle_Up"), 2);
            tempPlayerAnimation.setFrameHeight(27);
            tempPlayerAnimation.setFrameWidth(15);
            tempPlayerAnimation.setFrameSpeed(2);
            playerAnimations.Add(tempPlayerAnimation);

            tempPlayerAnimation = new Animation(getTexture("Player/Player_Idle_Left"), 2);
            tempPlayerAnimation.setFrameHeight(28);
            tempPlayerAnimation.setFrameWidth(12);
            tempPlayerAnimation.setFrameSpeed(2);
            playerAnimations.Add(tempPlayerAnimation);

            tempPlayerAnimation = new Animation(getTexture("Player/Player_Idle_Right"), 2);
            tempPlayerAnimation.setFrameHeight(28);
            tempPlayerAnimation.setFrameWidth(12);
            tempPlayerAnimation.setFrameSpeed(2);
            playerAnimations.Add(tempPlayerAnimation);

            tempPlayerAnimation = new Animation(getTexture("Player/Player_Walk_Down"), 4);
            tempPlayerAnimation.setFrameHeight(28);
            tempPlayerAnimation.setFrameWidth(15);
            tempPlayerAnimation.setFrameSpeed(1);
            playerAnimations.Add(tempPlayerAnimation);

            tempPlayerAnimation = new Animation(getTexture("Player/Player_Walk_Up"), 4);
            tempPlayerAnimation.setFrameHeight(27);
            tempPlayerAnimation.setFrameWidth(15);
            tempPlayerAnimation.setFrameSpeed(1);
            playerAnimations.Add(tempPlayerAnimation);

            tempPlayerAnimation = new Animation(getTexture("Player/Player_Walk_Left"), 4);
            tempPlayerAnimation.setFrameHeight(28);
            tempPlayerAnimation.setFrameWidth(12);
            tempPlayerAnimation.setFrameSpeed(1);
            playerAnimations.Add(tempPlayerAnimation);

            tempPlayerAnimation = new Animation(getTexture("Player/Player_Walk_Right"), 4);
            tempPlayerAnimation.setFrameHeight(28);
            tempPlayerAnimation.setFrameWidth(12);
            tempPlayerAnimation.setFrameSpeed(1);
            playerAnimations.Add(tempPlayerAnimation);

            return playerAnimations;
        }

        public Dictionary<string, List<Animation>> loadNPCs() {
            npcAnimations = new Dictionary<string, List<Animation>>();
            List<Animation> guard1IdleList = new List<Animation>();
            Animation guard_1_Idle = new Animation(getTexture("NPCs/Guard_NPC_1"), 2);
            guard_1_Idle.setFrameHeight(28);
            guard_1_Idle.setFrameWidth(12);
            guard_1_Idle.setFrameSpeed(4);

            guard1IdleList.Add(guard_1_Idle);
            npcAnimations.Add("Guard_1", guard1IdleList);

            guard1IdleList = new List<Animation>();
            guard_1_Idle = new Animation(getTexture("NPCs/Guard_NPC_1"), 2);
            guard_1_Idle.setFrameHeight(28);
            guard_1_Idle.setFrameWidth(12);
            guard_1_Idle.setFrameSpeed(4);

            guard1IdleList.Add(guard_1_Idle);
            npcAnimations.Add("Guard_2", guard1IdleList);

            guard1IdleList = new List<Animation>();
            guard_1_Idle = new Animation(getTexture("NPCs/Guard_NPC_1"), 2);
            guard_1_Idle.setFrameHeight(28);
            guard_1_Idle.setFrameWidth(12);
            guard_1_Idle.setFrameSpeed(4);

            guard1IdleList.Add(guard_1_Idle);
            npcAnimations.Add("Guard_3", guard1IdleList);

            guard1IdleList = new List<Animation>();
            guard_1_Idle = new Animation(getTexture("NPCs/Guard_NPC_1"), 2);
            guard_1_Idle.setFrameHeight(28);
            guard_1_Idle.setFrameWidth(12);
            guard_1_Idle.setFrameSpeed(4);

            guard1IdleList.Add(guard_1_Idle);
            npcAnimations.Add("Guard_4", guard1IdleList);

            return npcAnimations;
        }

        public List <Animation> loadPet() {
            // Chosing and loading the pet
            petAnimations = new List <Animation>();
            Animation tempPetAnimation = new Animation(getTexture("Pet/Slime_Idle"), 2);
            tempPetAnimation.setFrameHeight(12);
            tempPetAnimation.setFrameWidth(24);
            tempPetAnimation.setFrameSpeed(1);

            petAnimations.Add(tempPetAnimation);

            return petAnimations;
        }

        public Dictionary<string, Texture2D> loadTileset() {
            //All tiles go here
            tileSet = new Dictionary<string, Texture2D>();
            tileSet.Add("Grass1", getTexture("Tiles/Sand_1"));
            tileSet.Add("Water1", getTexture("Tiles/Water_1"));

            return tileSet;
        }

        public Dictionary<string, Texture2D> loadStaticUI() {
            // All non-animated UI goes here. ?Change to animation manager?
            staticUI = new Dictionary<string, Texture2D>();
            staticUI.Add("HealthMana", getTexture("UI/HealthManaBar"));
            staticUI.Add("Toolbar", getTexture("UI/Toolbar"));
            staticUI.Add("Pixel", getTexture("UI/Pixel"));
            staticUI.Add("InvBackground", getTexture("UI/TempInv"));
            staticUI.Add("Box", getTexture("UI/TempItemHolder"));
            staticUI.Add("DropButton", getTexture("UI/DropButton"));
            staticUI.Add("SelectedToorbar", getTexture("UI/SelectedToolbar"));
            staticUI.Add("Dialogue_box", getTexture("UI/dialogue_box"));
            staticUI.Add("Journal_Background", getTexture("UI/Journal_Background"));
            staticUI.Add("Journal_Right_Arrow", getTexture("UI/Journal_Right_Arrow"));
            staticUI.Add("Journal_Left_Arrow", getTexture("UI/Journal_Left_Arrow"));
            staticUI.Add("Journal_Quest_Frame", getTexture("UI/Journal_Quest_Frame"));
            staticUI.Add("Main_Menu_Button", getTexture("UI/Main_Menu_Button"));
            staticUI.Add("Rock_Background", getTexture("UI/Rock_Background"));

            return staticUI;
        }

        public Dictionary<string, Texture2D> loadItemTextures() {
            itemTextures = new Dictionary<string, Texture2D>();
            itemTextures.Add("Apple", getTexture("Items/Apple"));
            itemTextures.Add("HarvestingTool", getTexture("Items/HarvestingTool"));
            itemTextures.Add("PlantInPot1", getTexture("Items/PlantInPot1"));
            itemTextures.Add("Green_Plant_Tea", getTexture("Plants/Green_Plant_Tea"));
            itemTextures.Add("Green_Tea_Leaf", getTexture("Items/Green_Tea_Leaf"));
            itemTextures.Add("Green_Tea", getTexture("Items/Green_Tea"));
            itemTextures.Add("Apple_Tea", getTexture("Items/Apple_Tea"));
            itemTextures.Add("Kettle", getTexture("Crafting/Kettlev2"));

            return itemTextures;
        }

        public Dictionary<string, SpriteFont> loadFonts() {
            fonts = new Dictionary<string, SpriteFont>();
            fonts.Add("ClockFont", getFont("UI/Fonts/ClockFont"));
            fonts.Add("TitleFont", getFont("UI/Fonts/TitleFont"));
            fonts.Add("MenuButtonFont", getFont("UI/Fonts/MenuButtonFont"));

            return fonts;
        }

        public Dictionary<string, Animation> addAnimationToList(Dictionary<string, Animation> list ,string path, int frameCount, int frameHeight, int frameWidth, int frameSpeed = 0) {
            tempTex = getTexture(path);
            Animation temp = new Animation(tempTex, frameCount);

            list.Add(tempTex.Name.ToString(), temp);

            return list;
        }

        public List<Quest> loadQuests() {
            return textReader.loadQuests();
        }

        public Texture2D getTexture(string path) {
            return content.Load<Texture2D>(path);
        }

        public SpriteFont getFont(string path)
        {
            return content.Load<SpriteFont>(path);
        }
    }
}
