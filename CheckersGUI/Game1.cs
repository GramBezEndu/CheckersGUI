using CheckersLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using CheckersGUI.Draw;
using CheckersGUI.Update;
using CheckersLogic.States;

namespace CheckersGUI
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static InputManager inputManager = new InputManager();
        State nextState;
        State currentState;
        public static Game1 GameReference;
        Vector2 statePosition = new Vector2(0, 0);
        //Board Board;
        //Vector2 boardPosition = new Vector2(50, 50);
        /// <summary>
        /// All textures
        /// </summary>
        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public static SpriteFont Font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameReference = this;
        }

        public void ChangeState(State s)
        {
            nextState = s;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            base.Initialize();
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            LoadTextures();
            Font = Content.Load<SpriteFont>("Font");
            currentState = new MenuState();
            currentState.Init();
        }

        private void LoadTextures()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Content.RootDirectory);
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException();
            FileInfo[] files = directoryInfo.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);
                if (key == "Font")
                    continue;
                Textures[key] = Content.Load<Texture2D>(Directory.GetCurrentDirectory() + "/Content/" + key);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(nextState != null)
            {
                currentState = nextState;
                currentState.Init();
                nextState = null;
            }

            // TODO: Add your update logic here
            inputManager.Update(gameTime);
            currentState.Update(statePosition, gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.OldLace);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            currentState.Draw(statePosition, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
