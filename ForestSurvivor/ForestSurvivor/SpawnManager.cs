using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ForestSurvivor
{
    internal class SpawnManager
    {
        private int _level;
        private Random random;
        private float timerBetweenSpawn;
        private float timerBetweenLevel;
        private int _nbTotalMonster;
        private int _difficultyLevel;
        private const int TIME_BETWEEN_LEVEL = 5;
        private float _timeBetweenMonsterSpawn;
        private bool betweenLevel;
        private int nbSlime;
        private int nbMonster2;
        private int nbMonster3;

        public int Level { get => _level; set => _level = value; }
        public int NbTotalMonster { get => _nbTotalMonster; set => _nbTotalMonster = value; }
        public int DifficultyLevel { get => _difficultyLevel; set => _difficultyLevel = value; }
        public float TimeBetweenMonsterSpawn { get => _timeBetweenMonsterSpawn; set => _timeBetweenMonsterSpawn = value; }

        public SpawnManager() 
        {
            random = new Random();
            _level = 1;
            _timeBetweenMonsterSpawn = 3f;
            timerBetweenSpawn = 0f;
            timerBetweenLevel = 0f;
            _nbTotalMonster = 5;
            _difficultyLevel = 5;
            betweenLevel = false;

            nbSlime = DifficultyLevel;
            nbMonster2 = 0;
            nbMonster3 = 0;
        }

        public void Update(GameTime gameTime, Game game)
        {
            // Entre les niveaux, pause de 10sec
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
                        Globals.listEnnemies.Add(CreateSlime(game));
                        nbSlime--;
                    }

                    if (nbMonster2 > 0)
                    {
                        // Create monster
                        nbMonster2--;
                    }

                    if (nbMonster3 > 0)
                    {
                        // Create monster
                        nbMonster3--;
                    }
                    timerBetweenSpawn = 0f;
                }

                // Si tous les ennemies on été tués
                if (Globals.listEnnemies.Count == 0 && nbSlime == 0 && nbMonster2 == 0 && nbMonster3 == 0)
                {
                    // monte la dificulté
                    Level++;
                    if (TimeBetweenMonsterSpawn >= 0.5f)
                    {
                        TimeBetweenMonsterSpawn -= 0.2f;
                    }
                    DifficultyLevel += Level * 2;
                    NbTotalMonster -= DifficultyLevel / 2;

                    nbSlime = DifficultyLevel - Level;
                    // Pour qu'il y est toujours 2 slime
                    if (nbSlime <= 2)
                    {
                        nbSlime = 2;
                    }

                    int tmpdifficulty = DifficultyLevel;
                    int tmpSlime = nbSlime;
                    int tmpMonster2 = 0;
                    int tmpMonster3 = 0;

                    while (tmpdifficulty > 0)
                    {
                        tmpdifficulty -= tmpdifficulty;
                        tmpSlime = 0;

                        if (tmpdifficulty >= 2)
                        {
                            tmpMonster2 += 1;
                            tmpdifficulty -= 2;
                        }

                        if (tmpdifficulty >= 3)
                        {
                            tmpMonster3 += 1;
                            tmpdifficulty -= 3;
                        }

                        if (tmpdifficulty == 1)
                        {
                            tmpSlime += 1;
                            tmpdifficulty -= 1;
                        }
                    }
                    // New monster number
                    nbSlime += tmpSlime;
                    nbMonster2 = tmpMonster2;
                    nbMonster3 = tmpMonster3;

                    betweenLevel = true;
                }
            }
        }

        public Ennemies CreateSlime(Game game)
        {
            Ennemies ennemies = new Ennemies(69, 47, 2, 5, 1);
            ennemies.Texture =  ennemies.Texture = game.Content.Load<Texture2D>("Monster/Slime/Slime01");
            return ennemies;
        }
    }
}
