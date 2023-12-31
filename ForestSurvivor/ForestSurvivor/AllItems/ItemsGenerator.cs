﻿///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : ItemGenerator.cs
//Utilité : Le spawner à Item  
///Projet : ForestSurvivor V1 (2023)
using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestSurvivor.AllItems
{
    internal class ItemsGenerator
    {
        private float timeSpawn;
        private Random random;
        public ItemsGenerator() 
        {
            timeSpawn = 0f;
             random= new Random();
        }

        /// <summary>
        /// Genere des items de maniere aleatoire
        /// </summary>
        /// <param name="player"></param>
        /// <param name="gameTime"></param>
        /// <param name="spawnTime"></param>
        public void GenerateItem(Player player,GameTime gameTime,float spawnTime)
        {
            timeSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSpawn >= spawnTime && !Globals.LevelUpPause)
            {
                List<string> itemsNameList = new List<string>
                {
                    //"Heal",
                    "Speed",
                    "Damage",
                };
                int minY = 0; // Position Y minimale (haut de la fenêtre)
                int maxY = Globals.graphics.PreferredBackBufferHeight - 150; // Position Y maximale (bas de la fenêtre) - ajustée à la hauteur de l'objet
                int randomItem = random.Next(0, itemsNameList.Count());
                int randomX = random.Next(0, Globals.graphics.PreferredBackBufferWidth - 150); // Ajusté à la largeur de l'objet
                int randomY = random.Next(minY, maxY + 1);
                //int randomSpeed = random.Next(0, (int)speed);
                new Items(randomX, randomY, itemsNameList[randomItem], player);
                timeSpawn = 0;

            }


        }
        /// <summary>
        /// Genere un item a la mort d'un enemie
        /// </summary>
        /// <param name="bigSlime"></param>
        /// <param name="player"></param>
        public void GenerateItemDeath(BigSlime bigSlime,Player player)
        {

                List<string> itemsNameList = new List<string>
                {
                    "Heal",
                };
                int randomItem = random.Next(0, itemsNameList.Count());
                new Items(bigSlime.X, bigSlime.Y, itemsNameList[randomItem], player);
        }
    }






}
