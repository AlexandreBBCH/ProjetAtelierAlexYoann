using ForestSurvivor.AllGlobals;
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

        public BigSlime(int width, int height, int life, int speed, int damage, float damageSpeed) : base(width, height, life, speed, damage, damageSpeed) { }

        /// <summary>
        /// Create 4 new slime when it died
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
                GlobalsSounds.slimeDeath.Play(volume: GlobalsSounds.Sound / 100, pitch: 0, pan: 0);
                Globals.listBigSlime.Remove(this);
                return true;
            }
            return false;
        }
    }
}
