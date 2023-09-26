using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using ForestSurvivor.AllItems;
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
        MusicManager musicManager;
        Items apple;
        public Game1()
        {
            Globals.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Globals.graphics.PreferredBackBufferWidth = 1500; // Largeur
            Globals.graphics.PreferredBackBufferHeight = 800; // Hauteur

            Globals.ScreenHeight = Globals.graphics.PreferredBackBufferHeight;
            Globals.ScreenWidth = Globals.graphics.PreferredBackBufferWidth;
            //Globals.graphics.IsFullScreen = true;
            Globals.graphics.ApplyChanges();
        }
        protected override void Initialize()
        {

            player = new Player(120, 120, 500, 500, 8, 10, Color.White);
            spawnManager = new SpawnManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalsTexture.titleFont = Content.Load<SpriteFont>("Font/title");
            GlobalsTexture.textGamefont = Content.Load<SpriteFont>("Font/gameText");

            GlobalsTexture.MainMenu2D = Content.Load<Texture2D>("Ui/MainMenu/MainMenu");
            GlobalsTexture.PauseBackground2D = Content.Load<Texture2D>("Ui/MainMenu/BackgroundPause");
            GlobalsTexture.Minus = Content.Load<Texture2D>("Ui/Button/minus");
            GlobalsTexture.Plus = Content.Load<Texture2D>("Ui/Button/plus");

            GlobalsTexture.Slime2D = Content.Load<Texture2D>("Monster/Slime/Slime01");

            GlobalsTexture.Apple = Content.Load<Texture2D>("Items/apple");

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

            musicManager = new MusicManager();
            musicManager.LoadMusic(Content);
            musicManager.LoadAllSoundEffect(Content);

            player.Texture = GlobalsTexture.listTexturesPlayer[0];
            _optionPause = new OptionPause();
            _mainMenu = new MainMenu();
            apple = new Items(800, 500, "Apple", "Soin", player);

        }


        protected override void Update(GameTime gameTime)
        {
            if (Globals.Exit) Exit();
            MouseState mouseState = Mouse.GetState();

            _mainMenu.UpdateMainMenu(gameTime, mouseState);
            _optionPause.OptionUpdate(gameTime, mouseState);
            musicManager.Update();

            if (!_optionPause.IsResume && Globals.LauchGame)
            {
                spawnManager.Update(gameTime, this);
                foreach (Shoot shoot in Globals.listShoots)
                {
                    shoot.Update(player);
                }
                Shoot.DeleteShootInBorder();
                Shoot.CollsionBulletWithEnnemies();
                foreach (Ennemies ennemies in Globals.listLittleSlime)
                {
                    ennemies.Update(player, gameTime);
                }
                foreach (BigSlime bigSLime in Globals.listBigSlime)
                {
                    bigSLime.Update(player, gameTime);
                }
                foreach (SlimeShooter shooterSLime in Globals.listShootSlime)
                {
                    shooterSLime.Update(player, gameTime);
                    shooterSLime.Shoot(gameTime, player);
                }
                player.Update(gameTime, this);

                foreach (var item in Globals.listItems)
                {
                    if (item.IsCollected)
                    {
                        Globals.listItems.Remove(item);
                        break;
                    }
                }

                Globals.listItems.ForEach(item => item.UpdateItems(player));

            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            GraphicsDevice.Clear(Color.LightGreen);
            _spriteBatch.Begin(default, null, SamplerState.PointClamp);
            if (Globals.LauchGame)
            {
                player.Draw();
                player.DrawLife();
                spawnManager.DrawLevel();

                foreach (Ennemies ennemies in Globals.listLittleSlime)
                {
                    ennemies.Draw();
                }
                foreach (BigSlime bigSLime in Globals.listBigSlime)
                {
                    bigSLime.Draw();
                }
                foreach (SlimeShooter shooterSLime in Globals.listShootSlime)
                {
                    shooterSLime.Draw();
                    shooterSLime.DrawBullet();
                }
                foreach (Shoot shoot in Globals.listShoots)
                {
                    shoot.Draw();
                }

            }
            Globals.listItems.ForEach(item => item.DrawItems());

            _mainMenu.DrawMainMenu();
            _optionPause.DrawOption();


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}