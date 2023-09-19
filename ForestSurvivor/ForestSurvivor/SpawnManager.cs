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
        private int nbSlimeShoot;
        private int nbBigSlime;

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

            nbSlime = 0;
            nbSlimeShoot = 1;
            nbBigSlime = 0;
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
                        Globals.listLittleSlime.Add(CreateSlime(game));
                        nbSlime--;
                    }

                    if (nbSlimeShoot > 0)
                    {
                        Globals.listShootSlime.Add(CreateShooterSlime(game));
                        nbSlimeShoot--;
                    }

                    if (nbBigSlime > 0)
                    {
                        Globals.listBigSlime.Add(CreateBigSlime(game));
                        nbBigSlime--;
                    }
                    timerBetweenSpawn = 0f;
                }

                // Si tous les ennemies on été tués
                if (Globals.listLittleSlime.Count == 0 && nbSlime == 0 && nbSlimeShoot == 0 && nbBigSlime == 0)
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
                    int tmpSlimeShoot = 0;
                    int tmpBigSlime = 0;

                    while (tmpdifficulty > 0)
                    {
                        tmpdifficulty -= tmpdifficulty;
                        tmpSlime = 0;

                        if (tmpdifficulty >= 2)
                        {
                            tmpSlimeShoot += 1;
                            tmpdifficulty -= 2;
                        }

                        if (tmpdifficulty >= 3)
                        {
                            tmpBigSlime += 1;
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
                    nbSlimeShoot = tmpSlimeShoot;
                    nbBigSlime = tmpBigSlime;

                    betweenLevel = true;
                }
            }
        }

        public static Ennemies CreateSlime(Game game)
        {
            Ennemies ennemies = new Ennemies(Globals.WIDTH_LITTLE_SLIME, Globals.HEIGHT_LITTLE_SLIME, Globals.LIFE_LITTLE_SLIME ,Globals.SPEED_LITTLE_SLIME, Globals.DAMAGE_LITTLE_SLIME, Globals.DAMAGE_SPEED_LITTLE_SLIME);
            ennemies.Texture =  ennemies.Texture = game.Content.Load<Texture2D>("Monster/Slime/Slime01");
            return ennemies;
        }

        public static BigSlime CreateBigSlime(Game game)
        {
            BigSlime ennemies = new BigSlime(115, 78, 5, 3, 2, 3);
            ennemies.Texture = game.Content.Load<Texture2D>("Monster/Slime/SlimeBig");
            return ennemies;
        }

        public static SlimeShooter CreateShooterSlime(Game game)
        {
            SlimeShooter ennemies = new SlimeShooter(Globals.WIDTH_LITTLE_SLIME, Globals.HEIGHT_LITTLE_SLIME, Globals.LIFE_LITTLE_SLIME ,Globals.SPEED_LITTLE_SLIME, Globals.DAMAGE_LITTLE_SLIME, 2);
            ennemies.Texture = game.Content.Load<Texture2D>("Monster/Slime/Slime01");
            ennemies.ShootTexture = game.Content.Load<Texture2D>("Monster/Slime/Slime01");
            return ennemies;
        }
    }
}
