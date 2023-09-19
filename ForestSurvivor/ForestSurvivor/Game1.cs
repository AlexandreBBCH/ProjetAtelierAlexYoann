using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using ForestSurvivor.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ForestSurvivor
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public OptionPause _optionPause;
        public MainMenu _mainMenu;
        Player player;
        SpawnManager spawnManager;


        public Game1()
        {
            Globals.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Globals.graphics.PreferredBackBufferWidth = 1920; // Largeur
            Globals.graphics.PreferredBackBufferHeight = 1080; // Hauteur

            Globals.ScreenHeight = Globals.graphics.PreferredBackBufferHeight;
            Globals.ScreenWidth = Globals.graphics.PreferredBackBufferWidth;
            //Globals.graphics.IsFullScreen = true;
            Globals.graphics.ApplyChanges();
 



        }
        protected override void Initialize()
        {

            player = new Player(120, 120, 500, 500, 8, 10);
            spawnManager = new SpawnManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalsTexture.titleFont = Content.Load<SpriteFont>("Font/title");
            

            GlobalsTexture.MainMenu2D = Content.Load<Texture2D>("Ui/MainMenu/MainMenu");
            GlobalsTexture.PauseBackground2D = Content.Load<Texture2D>("Ui/MainMenu/BackgroundPause");

            GlobalsTexture.Minus = Content.Load<Texture2D>("Ui/Button/minus");
            GlobalsTexture.Plus = Content.Load<Texture2D>("Ui/Button/plus");

            GlobalsTexture.Slime2D = Content.Load<Texture2D>("Monster/Slime/Slime01");
            Globals.SpriteBatch = _spriteBatch;

            GlobalsTexture.listTexturesPlayer = new List<Texture2D>
            {
                Content.Load<Texture2D>("Player/HunterBot"),
                Content.Load<Texture2D>("Player/HunterBotLeft"),
                Content.Load<Texture2D>("Player/HunterBotRight"),
                Content.Load<Texture2D>("Player/HunterLeft"),
                Content.Load<Texture2D>("Player/HunterRight"),
                Content.Load<Texture2D>("Player/HunterTop"),
                Content.Load<Texture2D>("Player/HunterTopLeft"),
                Content.Load<Texture2D>("Player/HunterTopRight"),
            };
            GlobalsTexture.shootTexture = Content.Load<Texture2D>("Player/HunterTopRight");


            player.Texture = GlobalsTexture.listTexturesPlayer[0];
            _optionPause = new OptionPause();
            _mainMenu = new MainMenu();
        }
       

        protected override void Update(GameTime gameTime)
        {
            if(Globals.Exit)Exit();
            MouseState mouseState = Mouse.GetState();

            _mainMenu.UpdateMainMenu(gameTime,mouseState);
            _optionPause.OptionUpdate(gameTime,mouseState);

            if (!_optionPause.IsResume && Globals.LauchGame)
            {
                spawnManager.Update(gameTime, this);
                player.Update(gameTime);
                foreach (Shoot shoot in Globals.listShoots)
                {
                    shoot.Update();
                }
                foreach (Ennemies ennemies in Globals.listEnnemies)
                {
                    ennemies.Update(player);
                }
            }
  

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            GraphicsDevice.Clear(Color.LightGreen);
            _spriteBatch.Begin(default,null,SamplerState.PointClamp);
            if (Globals.LauchGame)
            {
                player.Draw();

                foreach (Ennemies ennemies in Globals.listEnnemies)
                {
                    ennemies.Draw();
                }

                foreach (Shoot shoot in Globals.listShoots)
                {
                    shoot.Draw();
                }
            }
            _mainMenu.DrawMainMenu();
            _optionPause.DrawOption();

            //Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, "text", new Vector2(499, 400), Color.Red);
            //Globals.SpriteBatch.Draw(GlobalsTexture.MainMenu2D, new Rectangle(0,0, Globals.graphics.PreferredBackBufferWidth, Globals.graphics.PreferredBackBufferHeight), Color.White);




            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}