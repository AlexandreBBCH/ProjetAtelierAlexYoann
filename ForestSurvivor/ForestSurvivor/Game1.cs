using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using ForestSurvivor.AllItems;
using ForestSurvivor.Environment;
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
        ItemsGenerator itemGenerator;
        EnvironmentInit environmentInitialisation;
        OptionClickable _gameOver;
        OptionClickable _restart;
        HealthBar _healthBarAnimated;

        public Game1()
        {
            Globals.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Globals.graphics.PreferredBackBufferWidth = 2560; // Largeur
            Globals.graphics.PreferredBackBufferHeight = 1440; // Hauteur

            Globals.ScreenHeight = Globals.graphics.PreferredBackBufferHeight;
            Globals.ScreenWidth = Globals.graphics.PreferredBackBufferWidth;
            //Globals.graphics.IsFullScreen = true;
            Globals.graphics.ApplyChanges();
        }
        protected override void Initialize()
        {
            player = new Player(120, 120, Globals.ScreenWidth / 2, Globals.ScreenHeight/2, 8f, 10, 1f, Color.White);
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
            GlobalsTexture.Background2D = Content.Load<Texture2D>("Environment/grassV3");
            GlobalsTexture.Minus = Content.Load<Texture2D>("Ui/Button/minus");
            GlobalsTexture.Plus = Content.Load<Texture2D>("Ui/Button/plus");

            GlobalsTexture.Slime2D = Content.Load<Texture2D>("Monster/Slime/Slime01");
            GlobalsTexture.SlimeShooter2D = Content.Load<Texture2D>("Monster/Slime/SlimeShooter");
            GlobalsTexture.SlimeShooterAmmo = Content.Load<Texture2D>("Monster/Slime/SlimeShooterShot");
            GlobalsTexture.BigSlime2D = Content.Load<Texture2D>("Monster/Slime/SlimeBig");

            GlobalsTexture.Apple = Content.Load<Texture2D>("Items/apple");
            GlobalsTexture.Mushroom = Content.Load<Texture2D>("Items/mushroom");
            GlobalsTexture.BrownMushroom = Content.Load<Texture2D>("Items/BrownMushroom");
            GlobalsTexture.GreenApple = Content.Load<Texture2D>("Items/greenApple");
            GlobalsTexture.Steak = Content.Load<Texture2D>("Items/steak");
            GlobalsTexture.Carrot = Content.Load<Texture2D>("Items/carrot");

            GlobalsTexture.Bush = Content.Load<Texture2D>("Environment/bushV2");
            GlobalsTexture.Rock = Content.Load<Texture2D>("Environment/RockV2");

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
            GlobalsTexture.bullet = Content.Load<Texture2D>("Player/bullet");

            musicManager = new MusicManager();
            musicManager.LoadMusic(Content);
            musicManager.LoadAllSoundEffect(Content);

            player.Texture = GlobalsTexture.listTexturesPlayer[0];
            _optionPause = new OptionPause();
            _mainMenu = new MainMenu();
            _gameOver = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 2.5f, Globals.graphics.PreferredBackBufferHeight / 3f, 200, 80, "GAME OVER", "Start", "", "Font", GlobalsTexture.titleFont, null);
            _restart = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 3f, Globals.graphics.PreferredBackBufferHeight / 2f, 200, 80, "PRESS R TO RESTART", "Start","","Font" ,GlobalsTexture.titleFont,null);

            GlobalsTexture.back = Content.Load<Texture2D>("Player/back");
            GlobalsTexture.front = Content.Load<Texture2D>("Player/front");

            _healthBarAnimated = new HealthBar(GlobalsTexture.back, GlobalsTexture.front, player.PvMax, new Vector2(10, 10));


            itemGenerator = new ItemsGenerator();
            environmentInitialisation = new EnvironmentInit(20);
            environmentInitialisation.GenerateEnvironment();

        }


        protected override void Update(GameTime gameTime)
        {
            if (Globals.Exit) Exit();
            MouseState mouseState = Mouse.GetState();
            _mainMenu.UpdateMainMenu(gameTime, mouseState);
            _optionPause.OptionUpdate(gameTime, mouseState);
     
            musicManager.Update();
            ResetAll();

            if (!_optionPause.IsResume && Globals.LauchGame && !player.IsDead())
            {
                spawnManager.Update(gameTime);
                itemGenerator.GenerateItem(player, gameTime, 8f);
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
                player.Update(gameTime);
                _healthBarAnimated.Update(player.Life, gameTime);

                foreach (var item in Globals.listItems)
                {
                    if (item.IsCollected)
                    {
                        Globals.listItems.Remove(item);
                        break;
                    }
                }

                Globals.listItems.ForEach(item => item.UpdateItems(player));
                Globals.listEffect.ForEach(effect => effect.UpdateEffect(gameTime));
                foreach (var effect in Globals.listEffect)
                {
                    if (effect.IsEffectEnd)
                    {
                        Globals.listEffect.Remove(effect);
                        break;
                    }
                }
                Globals.listEnvironment.ForEach(spawner => spawner.UpdateSpawner(player));

            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            GraphicsDevice.Clear(Color.LightGreen);
            _spriteBatch.Begin(default, null, SamplerState.PointClamp);
            Globals.SpriteBatch.Draw(GlobalsTexture.Background2D, new Rectangle(0,0,Globals.ScreenWidth,Globals.ScreenHeight),Color.White);

            if (Globals.LauchGame)
            {


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
                Globals.listEnvironment.ForEach(Spawner => Spawner.DrawEnvironment());

                player.Draw();
                _healthBarAnimated.Draw();
                spawnManager.DrawLevel();

               

            }
            Globals.listItems.ForEach(item => item.DrawItems());

            if (player.IsDead() && Globals.LauchGame) {
                _restart.DrawTextClickable(); 
                _gameOver.DrawTextClickable();
            }
  
            _mainMenu.DrawMainMenu();
            _optionPause.DrawOption();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ResetAll()
        {
            KeyboardState keyPress = Keyboard.GetState();
            if ((player.IsDead() && keyPress.IsKeyDown(Keys.R)) || Globals.Restart)
            {
                Globals.listBigSlime.Clear();
                Globals.listShootSlime.Clear();
                Globals.listItems.Clear();
                Globals.listEffect.Clear();
                Globals.listEnvironment.Clear();
                Globals.listLittleSlime.Clear();
                spawnManager = new SpawnManager();
                player = new Player(120, 120, Globals.ScreenWidth / 2, Globals.ScreenHeight / 2, 8f, 10, 1f, Color.White);
                player.Texture = GlobalsTexture.listTexturesPlayer[0];
                environmentInitialisation.GenerateEnvironment();
                Globals.Restart = false;
            }
     



        }
    }
}