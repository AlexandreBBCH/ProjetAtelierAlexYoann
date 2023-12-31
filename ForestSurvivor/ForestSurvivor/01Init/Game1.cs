﻿///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : Game1.cs
//Utilité : Le manageur du jeu 
///Projet : ForestSurvivor V1 (2023)
using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using ForestSurvivor.AllItems;
using ForestSurvivor.Animals;
using ForestSurvivor.CardManager;
using ForestSurvivor.Environment;
using ForestSurvivor.SongManager;
using ForestSurvivor.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ForestSurvivor
{
    public class Game1 : Game
    {
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
        CardCreationBase cardManager;
        
        public Game1()
        {
            Globals.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Globals.graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;// Largeur
            Globals.graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; // Hauteur

            Globals.ScreenHeight = Globals.graphics.PreferredBackBufferHeight;
            Globals.ScreenWidth = Globals.graphics.PreferredBackBufferWidth;
            Globals.graphics.IsFullScreen = true;
            Globals.graphics.ApplyChanges();
        }
        protected override void Initialize()
        {
            player = new Player(120, 120, Globals.ScreenWidth / 2, Globals.ScreenHeight/2, 8f, 10, 1f, Color.White);
            spawnManager = new SpawnManager();
            cardManager = new CardCreationBase();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalsTexture.titleFont = Content.Load<SpriteFont>("Font/title");
            GlobalsTexture.textGamefont = Content.Load<SpriteFont>("Font/gameText");
            GlobalsTexture.textMenufont = Content.Load<SpriteFont>("Font/textMenu");
            GlobalsTexture.lvlInfoFont = Content.Load<SpriteFont>("Font/lvlInfosFont");

            GlobalsTexture.MainMenu2D = Content.Load<Texture2D>("Ui/MainMenu/MainMenu");
            GlobalsTexture.PauseBackground2D = Content.Load<Texture2D>("Ui/MainMenu/BackgroundPause");
            GlobalsTexture.Background2D = Content.Load<Texture2D>("Environment/grassV4");
            GlobalsTexture.Minus = Content.Load<Texture2D>("Ui/Button/minus");
            GlobalsTexture.Plus = Content.Load<Texture2D>("Ui/Button/plus");
            GlobalsTexture.cardInfos = Content.Load<Texture2D>("Ui/UpgradePlayerMenu/cardInfo");
            GlobalsTexture.cardView = Content.Load<Texture2D>("Ui/UpgradePlayerMenu/cardVisuel");

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
            GlobalsTexture.BushSpriteSheet = Content.Load<Texture2D>("Environment/BushSpriteSheet");
            GlobalsTexture.BushBerriesSpriteSheet = Content.Load<Texture2D>("Environment/BushBerriesSpriteSheet");

            GlobalsTexture.back = Content.Load<Texture2D>("Player/back");
            GlobalsTexture.front = Content.Load<Texture2D>("Player/front");
            GlobalsTexture.backHealDog = Content.Load<Texture2D>("Dog/HealthDog");
            GlobalsTexture.frontHealDog = Content.Load<Texture2D>("Dog/frontHealthDog");

            GlobalsTexture.effectHeal = Content.Load<Texture2D>("Items/EffectItems/healEffect");
            GlobalsTexture.effectDamage = Content.Load<Texture2D>("Items/EffectItems/DamageEffects");
            GlobalsTexture.effectSpeed = Content.Load<Texture2D>("Items/EffectItems/SpeedEffects");
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
            GlobalsTexture.DogSheets = Content.Load<Texture2D>("Dog/DogSpriteSheet");
            musicManager = new MusicManager();
            musicManager.LoadMusic(Content);
            musicManager.LoadAllSoundEffect(Content);

            player.Texture = GlobalsTexture.listTexturesPlayer[0];
            _optionPause = new OptionPause();
            _mainMenu = new MainMenu();
            _gameOver = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 2.5f, Globals.graphics.PreferredBackBufferHeight / 5f, 200, 80, "GAME OVER", "Start", "", "Font", GlobalsTexture.titleFont, null);
            _restart = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 3f, Globals.graphics.PreferredBackBufferHeight / 2f + 400, 200, 80, "PRESS R TO RESTART", "Start", "", "Font", GlobalsTexture.titleFont, null);
            cardManager.CreateCard();

            GlobalsTexture.back = Content.Load<Texture2D>("Player/back");
            GlobalsTexture.front = Content.Load<Texture2D>("Player/front");

            _healthBarAnimated = new HealthBar(GlobalsTexture.back, GlobalsTexture.front, player.PvMax, new Vector2(Globals.ScreenWidth / 2.2f, 30));

            itemGenerator = new ItemsGenerator();
            environmentInitialisation = new EnvironmentInit(20);
            environmentInitialisation.GenerateEnvironment();
            // Ajoute un premier chien
            SpawnManager.CreateDog(player);
        }


        protected override void Update(GameTime gameTime)
        {
            if (Globals.Exit) Exit();

            MouseState mouseState = Mouse.GetState();
            _mainMenu.UpdateMainMenu(gameTime, mouseState);
            _optionPause.OptionUpdate(gameTime, mouseState);
     
            musicManager.Update();
            ResetAll();

            // Affich les cartes tous les 2 niveaux
            if (spawnManager.Level % 2 == 0 && !Globals.LevelTime)
            {
                Globals.LevelTime = true;
                Globals.levelUpCard = new LevelUpCard();
                Globals.LevelUpPause = true;
            }
            else if (spawnManager.Level % 2 == 1)
            {
                Globals.LevelTime = false;
            }
            if (Globals.levelUpCard != null)
            {
                Globals.levelUpCard.UpdateCard(player, gameTime);
            }

            itemGenerator.GenerateItem(player, gameTime, 8f);

            // Pendant le jeu
            if (!_optionPause.IsResume && Globals.LauchGame && !player.IsDead() && !Globals.LevelUpPause)
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
                    ennemies.Update(player ,gameTime);
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
                foreach (Dog dog in Globals.listDogs)
                {
                    dog.Update(player, gameTime);
                }
                _healthBarAnimated.Update(player.Life, gameTime);

                // Supprime les items déjà collecté
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

                // Enlève les effets temporaire qui sont arrivée à la fin du temps
                foreach (var effect in Globals.listEffect)
                {
                    if (effect.IsEffectEnd)
                    {
                        Globals.listEffect.Remove(effect);
                        break;
                    }
                }
                Globals.listEnvironment.ForEach(spawner => spawner.UpdateSpawner(gameTime, player));
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
                Globals.listEffect.ForEach(effect => effect.DisplayEffectItem());

                Globals.listItems.ForEach(item => item.DrawItems());

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
                foreach (Dog dog in Globals.listDogs)
                {
                    dog.Draw();
                }
                player.Draw();
                
                _healthBarAnimated.Draw();
                spawnManager.DrawLevel();
                if (Globals.levelUpCard != null) 
                {
                    Globals.levelUpCard.DrawCards();
                } 


            }

            // Si la partie est finie
            if (player.IsDead() && Globals.LauchGame) {
                // Calcul le pourcentage de tir réussi
                int pourcentageShootSucessfull;
                if (Globals.nbShoot != 0) 
                {
                    pourcentageShootSucessfull = (int)Math.Round((double)Globals.nbShootHasTouch / Globals.nbShoot * 100, 0);
                }
                else
                {
                    pourcentageShootSucessfull = 100;
                }
                // Si c'est un nouveau meilleur score, remplace dans le fichier texte en Xml
                if (_mainMenu.DataBestGame[0] < spawnManager.Level || _mainMenu.DataBestGame[1] < Globals.nbSlimeKilled || _mainMenu.DataBestGame[2] < Globals.nbBigSlimeKilled || _mainMenu.DataBestGame[3] < Globals.nbShooterSlimeKilled)
                {
                    List<int> newData = new List<int>
                    {
                        spawnManager.Level,
                        Globals.nbSlimeKilled,
                        Globals.nbBigSlimeKilled,
                        Globals.nbShooterSlimeKilled,
                        pourcentageShootSucessfull
                    };
                    if (Globals.canSerialize)
                    {
                        Globals.Serializer("bestscore.txt", newData);
                        Globals.canSerialize = false;
                    }
                }

                // Affiche le menu de fin de partie
                _restart.DrawTextClickable();
                Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, "Number of slime killed :", new Vector2(Globals.ScreenWidth / 4, Globals.ScreenHeight / 2.5f - 100), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.textGamefont, $"Blue slime : {Globals.nbSlimeKilled}", new Vector2(Globals.ScreenWidth / 4, Globals.ScreenHeight / 2.5f), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.textGamefont, $"Red slime : {Globals.nbShooterSlimeKilled}", new Vector2(Globals.ScreenWidth / 4, Globals.ScreenHeight / 2.5f + 100), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.textGamefont, $"Black slime : {Globals.nbBigSlimeKilled}", new Vector2(Globals.ScreenWidth / 4, Globals.ScreenHeight / 2.5f + 200), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.textGamefont, $"Shoot successfull : {pourcentageShootSucessfull}%", new Vector2(Globals.ScreenWidth / 4, Globals.ScreenHeight / 2.5f + 300), Color.White);
                _gameOver.DrawTextClickable();
            }
            
            _mainMenu.DrawMainMenu();
            _optionPause.DrawOption();

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Remet à 0 le jeu : remet à 0 les variables globals et instancie de nouveau les classes
        /// </summary>
        public void ResetAll()
        {
            KeyboardState keyPress = Keyboard.GetState();
            if ((player.IsDead() && keyPress.IsKeyDown(Keys.R)) || Globals.Restart)
            {
                Globals.ResetGlobals();

                spawnManager = new SpawnManager();
                player = new Player(120, 120, Globals.ScreenWidth / 2, Globals.ScreenHeight / 2, 8f, 10, 1f, Color.White);
                player.Texture = GlobalsTexture.listTexturesPlayer[0];

                itemGenerator = new ItemsGenerator();

                environmentInitialisation = new EnvironmentInit(20);
                environmentInitialisation.GenerateEnvironment();

                _healthBarAnimated = new HealthBar(GlobalsTexture.back, GlobalsTexture.front, player.PvMax, new Vector2(Globals.ScreenWidth / 2.2f, 30));

                _mainMenu = new MainMenu();
                _optionPause = new OptionPause();

                cardManager = new CardCreationBase();
                cardManager.CreateCard();

                SpawnManager.CreateDog(player);
            }
        }
    }
}