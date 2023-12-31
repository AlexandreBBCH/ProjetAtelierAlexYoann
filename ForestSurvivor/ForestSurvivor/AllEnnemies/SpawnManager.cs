﻿///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : SpawnManager.cs
//Utilité : Manager des vague / des slimes
///Projet : ForestSurvivor V1 (2023)
using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ForestSurvivor.Animals;

namespace ForestSurvivor.AllEnnemies
{
    internal class SpawnManager
    {
        private int _level;
        private float timerBetweenSpawn;
        private float timerBetweenLevel;
        private int _difficultyLevel;
        private const int TIME_BETWEEN_LEVEL = 5;
        private float _timeBetweenMonsterSpawn;
        private bool betweenLevel;
        private int nbSlime;
        private int nbSlimeShoot;
        private int nbBigSlime;

        public int Level { get => _level; set => _level = value; }
        public int DifficultyLevel { get => _difficultyLevel; set => _difficultyLevel = value; }
        public float TimeBetweenMonsterSpawn { get => _timeBetweenMonsterSpawn; set => _timeBetweenMonsterSpawn = value; }

        public SpawnManager()
        {
            _level = 1;
            _timeBetweenMonsterSpawn = 3f;
            timerBetweenSpawn = 0f;
            timerBetweenLevel = 0f;
            _difficultyLevel = 5;
            betweenLevel = false;

            nbSlime = DifficultyLevel;
            nbSlimeShoot = 0;
            nbBigSlime = 0;
        }

        public void Update(GameTime gameTime)
        {
            // Entre les niveaux, pause de 5sec
            if (betweenLevel)
            {
                timerBetweenLevel += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timerBetweenLevel >= TIME_BETWEEN_LEVEL)
                {
                    betweenLevel = false;
                }

            }
            else
            {
                // Temps entre le spawn
                timerBetweenSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timerBetweenSpawn >= TimeBetweenMonsterSpawn)
                {
                    // Spawn les ennemies
                    if (nbSlime > 0)
                    {
                        Globals.listLittleSlime.Add(CreateSlime());
                        nbSlime--;
                    }

                    if (nbSlimeShoot > 0)
                    {
                        Globals.listShootSlime.Add(CreateShooterSlime());
                        nbSlimeShoot--;
                    }

                    if (nbBigSlime > 0)
                    {
                        Globals.listBigSlime.Add(CreateBigSlime());
                        nbBigSlime--;
                    }
                    timerBetweenSpawn = 0f;
                }

                // Si tous les ennemies on été tués
                if (Globals.listLittleSlime.Count == 0 && Globals.listShootSlime.Count == 0 && Globals.listBigSlime.Count == 0 && nbSlime == 0 && nbSlimeShoot == 0 && nbBigSlime == 0)
                {
                    // monte la dificulté
                    Level++;
                    if (TimeBetweenMonsterSpawn >= 0.5f)
                    {
                        TimeBetweenMonsterSpawn -= 0.2f;
                    }
                    DifficultyLevel += 3;

                    // Génère les monstres du prochain niveau
                    int tmpdifficulty = DifficultyLevel;
                    int tmpSlime = 0;
                    int tmpSlimeShoot = 0;
                    int tmpBigSlime = 0;

                    while (tmpdifficulty > 0)
                    {
                        if (tmpdifficulty >= 1)
                        {
                            tmpSlime += 1;
                            tmpdifficulty -= 1;
                        }

                        if (tmpdifficulty >= 4)
                        {
                            tmpSlimeShoot += 1;
                            tmpdifficulty -= 4;
                        }

                        if (tmpdifficulty >= 5)
                        {
                            tmpBigSlime += 1;
                            tmpdifficulty -= 5;
                        }
                    }
                    nbSlime += tmpSlime;
                    nbSlimeShoot = tmpSlimeShoot;
                    nbBigSlime = tmpBigSlime;
                    betweenLevel = true;
                    timerBetweenLevel = 0;
                }
            }
        }

        /// <summary>
        /// Créer un slime
        /// </summary>
        public static Ennemies CreateSlime()
        {
            Ennemies ennemies = new Ennemies(Globals.WIDTH_LITTLE_SLIME, Globals.HEIGHT_LITTLE_SLIME, Globals.LIFE_LITTLE_SLIME, Globals.SPEED_LITTLE_SLIME, Globals.DAMAGE_LITTLE_SLIME, Globals.DAMAGE_SPEED_LITTLE_SLIME);
            ennemies.Texture = GlobalsTexture.Slime2D;
            return ennemies;
        }

        /// <summary>
        /// Créer un gros Slime
        /// </summary>
        public static BigSlime CreateBigSlime()
        {
            BigSlime ennemies = new BigSlime(115, 78, 5, 3, 2, 3);
            ennemies.Texture = GlobalsTexture.BigSlime2D;
            return ennemies;
        }

        /// <summary>
        /// Créer un chien et l'ajoute dans la liste
        /// </summary>
        /// <param name="player">Le joueur dont le chien va apparaître à coté</param>
        public static void CreateDog(Player player)
        {
            Dog dog = new Dog(96, 96, player.X - 100, player.Y - 100, 5, 10, 1, 0.5f);
            dog.Texture = GlobalsTexture.DogSheets;
            Globals.listDogs.Add(dog);
        }

        /// <summary>
        /// Créer un slime qui tire
        /// </summary>
        /// <returns></returns>
        public static SlimeShooter CreateShooterSlime()
        {
            SlimeShooter ennemies = new SlimeShooter(Globals.WIDTH_LITTLE_SLIME, Globals.HEIGHT_LITTLE_SLIME, Globals.LIFE_LITTLE_SLIME, Globals.SPEED_LITTLE_SLIME, Globals.DAMAGE_LITTLE_SLIME, 2);
            ennemies.Texture = GlobalsTexture.SlimeShooter2D;
            ennemies.ShootTexture = GlobalsTexture.SlimeShooterAmmo;
            return ennemies;
        }

        /// <summary>
        /// Affiche le niveau actuel
        /// </summary>
        public void DrawLevel()
        {
            Globals.SpriteBatch.DrawString(GlobalsTexture.textGamefont, $"Level: {Level}", new Vector2(Globals.ScreenWidth - 300, 0), Color.White);
        }
    }
}
