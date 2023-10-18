///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : BigSlime.cs
//Utilité : Gestionnaire du big slime noir 
///Projet : ForestSurvivor V1 (2023)
using ForestSurvivor.AllGlobals;
using ForestSurvivor.AllItems;
using ForestSurvivor.SongManager;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestSurvivor.AllEnnemies
{
    internal class BigSlime : Ennemies
    {
        private const int NB_SLIME_CREATE_WHEN_DIED = 4;
        ItemsGenerator items;
        Player fakePlayer;
        public BigSlime(int width, int height, int life, int speed, int damage, float damageSpeed) : base(width, height, life, speed, damage, damageSpeed) { 
            fakePlayer = new Player(0, 0, 0, 0, 0, 0, 0, Color.Wheat);
            items = new ItemsGenerator();
        }

        /// <summary>
        /// Créer 4 slime autour de lui lors de sa mort
        /// </summary>
        public bool CreateNewLittleSlime()
        {
            if (Life <= 0)
            {
                for (int i = 1; i <= NB_SLIME_CREATE_WHEN_DIED; i++) 
                {
                    Ennemies ennemies = SpawnManager.CreateSlime();
                    if (i == 1)
                    {
                        ennemies.X = X - 100;
                        ennemies.Y = Y - 100;
                    }
                    else if (i == 2)
                    {
                        ennemies.X = X + 100;
                        ennemies.Y = Y - 100;
                    }
                    else if (i == 3)
                    {
                        ennemies.X = X - 100;
                        ennemies.Y = Y + 100;
                    }
                    else if (i == 4)
                    {
                        ennemies.X = X + 100;
                        ennemies.Y = Y + 100;
                    }
                    Globals.listLittleSlime.Add(ennemies);
                }
                // Lance le son d'explosion du slime, le supprime de la liste et génère une pomme
                MusicManager.PlaySoundEffect(GlobalsSounds.bigSlimeExplosion);
                Globals.listBigSlime.Remove(this);
                Globals.nbBigSlimeKilled++;
                items.GenerateItemDeath(this, fakePlayer);
                return true;
            }
            return false;
        }
    }
}
