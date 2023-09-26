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


        public void GenerateItem(Player player,GameTime gameTime,float spawnTime)
        {
            timeSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Debug.Print(player.Life.ToString());
            Debug.Print(player.PvMax.ToString());


            if (timeSpawn >= spawnTime)
            {
                List<string> itemsNameList = new List<string>
                {
                    "Heal",
                    "HealMax",
                    "Speed",
                    "SpeedMax",
                    "Damage",
                    "DamageMax"
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

    }






}
