using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProject
{
    public class TeaGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spWorld;
        SpriteBatch spStatic;

        public PlayerManager playerManager;
        public Camera camera;
        public MapManager mapManager;
        public ContentLoader load;
        public UI_Manager ui;
        public MouseObserver mouseObs;
        public DepthDraw depthDraw;
        public NPC_Manager npcManager;
        public Item_Object_Creator itemCreator;
        public Quest_Manager questManager;
        public MainMenu mainMenu;

        private Input input;
        private Matrix scaleMatrix;
        private MouseState mState;
        private MouseState oldMState;

        private int screenH;
        private int screenW;

        public bool isInMainMenu;

        public TeaGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            load = new ContentLoader(Content);

            // Creating classes:
            input = Input.Instance;
            mouseObs = new MouseObserver(this);
            isInMainMenu = true;

            mState = new MouseState();
            oldMState = new MouseState();
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.IsMouseVisible = true;
            this.IsFixedTimeStep = true;
            //graphics.IsFullScreen = true;

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            float scaleX = (float)graphics.PreferredBackBufferWidth / 800;
            float scaleY = (float)graphics.PreferredBackBufferHeight / 600;

            scaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);
            input.setScaleMatrix(scaleMatrix);

            screenH = graphics.PreferredBackBufferWidth;
            screenW = graphics.PreferredBackBufferWidth;

            graphics.PreferMultiSampling = false;
            graphics.ApplyChanges();
        }
        
        protected override void LoadContent()
        {

            spWorld = new SpriteBatch(GraphicsDevice);
            spStatic = new SpriteBatch(GraphicsDevice);

            Dictionary<string, Texture2D> staticUI = load.loadStaticUI();
            Dictionary<string, SpriteFont> fonts = load.loadFonts();
            playerManager = new PlayerManager(this, load.loadPlayer(), load.loadPet());
            camera = new Camera(this);
            input.setCamera(camera);
            mapManager = new MapManager(this, load.loadTileset(), scaleMatrix);
            itemCreator = Item_Object_Creator.Instance;
            itemCreator.setItemTextures(load.loadItemTextures());
            itemCreator.setTeaGame(this);
            ui = new UI_Manager(this, staticUI, fonts);
            mainMenu = new MainMenu(this, staticUI["Main_Menu_Button"], fonts["MenuButtonFont"], fonts["TitleFont"], staticUI["Rock_Background"]);
            depthDraw = DepthDraw.Instance;
            npcManager = new NPC_Manager(this, load.loadNPCs(), load.loadQuests());
            questManager = Quest_Manager.Instance;
            questManager.setTeaGame(this);
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            input.Update();
            camera.Update();
            if (isInMainMenu)
            {
                mState = input.getMState();
                if (mState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released)
                {
                    mainMenu.LMB(input.getStaticMousePos());
                }
                mainMenu.Update();
                oldMState = mState; 
            }
            else {
                depthDraw.Update();
                mapManager.Update(gameTime);
                playerManager.Update(gameTime);
                ui.Update(gameTime);
                npcManager.Update(gameTime);
            }
            base.Update(gameTime);
        }
        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spStatic.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, scaleMatrix);

            base.Draw(gameTime);

            if (isInMainMenu)
            {
                mainMenu.Draw(spStatic);
            }
            else
            {
                //World draw methods go here
                spWorld.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null,
                camera.getTransform());

                mapManager.Draw(spWorld);
                depthDraw.Draw(spWorld);

                // Draw end
                spWorld.End();

                //Static draw methods go here
                

                ui.Draw(spStatic);
            }

            

            spStatic.End();
            // Draw end
        }

        public int getScreenWidth() {
            return screenW;
        }

        public int getScreenHeight() {
            return screenH;
        }

        public Matrix getScaleMatrix() {
            return scaleMatrix;
        }
    }
}
