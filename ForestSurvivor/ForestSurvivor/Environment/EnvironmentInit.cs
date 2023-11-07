///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : EnvironmentInit.cs
//Utilité : Sert à préparer la map 
///Projet : ForestSurvivor V1 (2023)
using ForestSurvivor.AllGlobals;
using ForestSurvivor.AllItems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ForestSurvivor.Environment
{

    /// <summary>
    /// sert au lancement de la partie a instancier des bush/arbre/caillou qui feront pendant les vagues apparaitre des monstres
    /// </summary>
    internal class EnvironmentInit
    {

        /// <summary>
        /// Nombre de spawner
        /// </summary>

        private int _quantity;
        public int Quantity { get => _quantity; set => _quantity = value; }
        public Random Random { get => _random; set => _random = value; }

        private Random _random;
        public EnvironmentInit(int quantity)
        {
            Quantity = quantity;
            Random = new Random();
        }



        /// <summary>
        /// Initialise les donnée de l'environnement nécéssair
        /// </summary>
        public void GenerateEnvironment()
        {

            List<string> spawners = new List<string>
            {
                "Bush",
                "Rock",
                "BushBerrie"
            };

            int minX = Globals.graphics.PreferredBackBufferWidth / 4; // Définir la limite gauche de la zone centrale
            int maxX = 3 * Globals.graphics.PreferredBackBufferWidth / 4; // Définir la limite droite de la zone centrale
            int minY = Globals.graphics.PreferredBackBufferHeight / 4; // Définir la limite supérieure de la zone centrale
            int maxY = 3 * Globals.graphics.PreferredBackBufferHeight / 4; // Définir la limite inférieure de la zone centrale

            for (int i = 0; i < Quantity; i++)
            {
                int randomSpawner = Random.Next(0, spawners.Count());
                string selectedSpawner = spawners[randomSpawner];

                int randomX, randomY;

                // Générer des coordonnées en dehors de la zone centrale
                do
                {
                    randomX = Random.Next(0, Globals.graphics.PreferredBackBufferWidth); // Ajusté à la largeur de l'objet
                    randomY = Random.Next(0, Globals.graphics.PreferredBackBufferHeight); // Ajusté à la hauteur de l'objet
                } while (randomX >= minX && randomX <= maxX && randomY >= minY && randomY <= maxY);

                new Spawner(randomX, randomY, selectedSpawner);
            }
        }

    }


}

