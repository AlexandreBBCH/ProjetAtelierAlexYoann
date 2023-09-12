using ForestSurvivor.AllGlobals;
using ForestSurvivor.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ForestSurvivor
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public OptionPause _optionPause;
        public MainMenu _mainMenu;
        

        public Game1()
        {
            Globals.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Globals.graphics.PreferredBackBufferWidth = 2560; // Largeur
            Globals.graphics.PreferredBackBufferHeight = 1440; // Hauteur
            //Globals.graphics.IsFullScreen = true;
            Globals.graphics.ApplyChanges();
            _optionPause = new OptionPause();
            _mainMenu = new MainMenu();

        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalsTexture.titleFont = Content.Load<SpriteFont>("Font/title");
            

            GlobalsTexture.MainMenu2D = Content.Load<Texture2D>("Ui/MainMenu/MainMenu");
            GlobalsTexture.Slime2D = Content.Load<Texture2D>("Monster/Slime/Slime01");
            Globals.SpriteBatch = _spriteBatch;

        }
        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if(Globals.Exit)Exit();
            MouseState mouseState = Mouse.GetState();

            _mainMenu.UpdateMainMenu(gameTime,mouseState);
            _optionPause.OptionUpdate(gameTime,mouseState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(default,null,SamplerState.PointClamp);
            _optionPause.DrawOption();
            _mainMenu.DrawMainMenu();
            //Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, "text", new Vector2(499, 400), Color.Red);
            //Globals.SpriteBatch.Draw(GlobalsTexture.MainMenu2D, new Rectangle(0,0, Globals.graphics.PreferredBackBufferWidth, Globals.graphics.PreferredBackBufferHeight), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}