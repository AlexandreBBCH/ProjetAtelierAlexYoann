using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ForestSurvivor.AllEnnemies
{
    internal class Ennemies
    {
        private Texture2D texture;
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private float _life;
        private int _speed;
        private int _damage;
        private float _damageSpeed;
        private bool _isDead;
        private Color _color;

        // Ennemi touch player
        bool isHurt = false;
        float timerHurt = 0;


        // Dog touch ennemi
        public bool isEnnemiHurtByDog = false;
        private bool canMakeDamage = true;
        private float timerDamage = 0;

        // Shoot touch ennemi
        private float timerEnnemiHurt;
        bool hasShootTouchEnnemi = false;

        private Random rnd;
        private float timerSound;

        public Texture2D Texture { get => texture; set => texture = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public float Life { get => _life; set => _life = value; }
        public int Speed { get => _speed; set => _speed = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public bool IsDead { get => _isDead; set => _isDead = value; }
        public Color Color { get => _color; set => _color = value; }
        public float DamageSpeed { get => _damageSpeed; set => _damageSpeed = value; }

        public Ennemies(int width, int height, int life, int speed, int damage, float damageSpeed)
        {
            Width = width;
            Height = height;
            Life = life;
            Speed = speed;
            Damage = damage;
            DamageSpeed = damageSpeed;
            Color = Color.White;
            timerSound = 0;
            IsDead = false;

            // Random spawn
            rnd = new Random();
            int border = rnd.Next(0, 4 + 1);
            switch (border)
            {
                case 1:
                    X = 0;
                    Y = rnd.Next(0,Globals.ScreenHeight / 2);
                    Y *= 2; 
                    break;
                case 2:
                    X = rnd.Next(0, Globals.ScreenWidth / 2);
                    X *=2;
                    Y = 0;
                    break;
                case 3:
                    X = Globals.ScreenWidth - Width;
                    Y = rnd.Next(0, Globals.ScreenHeight / 2);
                    Y *= 2;
                    break;
                case 4:
                    X = rnd.Next(0, Globals.ScreenWidth / 2);
                    X *= 2;
                    Y = Globals.ScreenHeight - Height;
                    break;
            }
        }

        public void Update(Player player, Dog dog, GameTime gameTime)
        {
            // Slime shooter don't follow player
            if (GetType() != typeof(SlimeShooter))
            {
                if (!isEnnemiHurtByDog)
                {
                    bool moreXcollided = false;
                    bool lessXcollided = false;
                    bool moreYcollided = false;
                    bool lessYcollided = false;

                    // Collision with others ennemies
                    foreach (Ennemies ennemies in Globals.listLittleSlime)
                    {
                        // If it's not us
                        if (GetEnnemieRectangle() != ennemies.GetEnnemieRectangle())
                        {
                            if (GetEnnemieRectangle().Intersects(ennemies.GetEnnemieRectangle()))
                            {
                                if (X >= ennemies.X)
                                {
                                    moreXcollided = true;
                                }
                                if (X <= ennemies.X)
                                {
                                    lessXcollided = true;
                                }
                                if (Y >= ennemies.Y)
                                {
                                    moreYcollided = true;
                                }
                                if (Y <= ennemies.Y)
                                {
                                    lessYcollided = true;
                                }
                            }
                        }
                    }

                    // Follow player
                    if (X + Width <= player.X && !lessXcollided)
                    {
                        X += Speed;

                    }
                    else if (X >= player.X + player.Width && !moreXcollided)
                    {
                        X -= Speed;
                    }

                    if (Y >= player.Y + player.Height && !moreYcollided)
                    {
                        Y -= Speed;
                    }
                    else if (Y + Height <= player.Y && !lessYcollided)
                    {
                        Y += Speed;
                    }
                }
                else
                {
                    if (!canMakeDamage)
                    {
                        timerDamage += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (timerDamage >= DamageSpeed)
                        {
                            canMakeDamage = true;
                            timerDamage = 0;
                        }
                    }
                    else
                    {
                        if (dog.GetRectangle().Intersects(GetEnnemieRectangle()))
                        {
                            canMakeDamage = false;
                            dog.IsHurt = true;
                            dog.Life -= Damage;
                            if (dog.Life <= 0)
                            {
                                dog.isDead = true;
                                MusicManager.PlaySoundEffect(GlobalsSounds.dogDied);
                                isEnnemiHurtByDog = false;
                            }
                        }
                    }
                }
            }

            // Slime sound
            timerSound += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timerSound >= 2)
            {
                MusicManager.PlaySoundEffect(GlobalsSounds.slimeMove);
                timerSound = 0;
            }


            // Color Ennemies when is hurt
            if (hasShootTouchEnnemi)
            {
                timerEnnemiHurt += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timerEnnemiHurt <= Player.TIME_COLOR_RED)
                {
                    Color = Color.Red;
                }
                else if (timerEnnemiHurt >= Player.TIME_COLOR_RED)
                {
                    Color = Color.White;
                    hasShootTouchEnnemi = false;
                    timerEnnemiHurt = 0f;
                }
            }
        }

        /// <summary>
        /// Collision with player : the player take damage and his color change to red for a few seconds
        /// </summary>
        /// <param name="player"></param>
        /// <param name="gameTime"></param>
        public void CollisionWithPlayer(Player player, GameTime gameTime)
        {
            if (isHurt)
            {
                timerHurt += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timerHurt >= DamageSpeed)
                {
                    isHurt = false;
                    timerHurt = 0;
                }
            }
            if (GetEnnemieRectangle().Intersects(player.GetPlayerRectangle()))
            {
                // Player take damage
                if (!isHurt)
                {
                    if (player.Life - Damage < 0)
                    {
                        player.Life = 0;
                    }
                    else
                    {
                        player.Life -= Damage;
                    }
                    isHurt = true;
                    player.IsPlayerHurt = true;
                }
            }
        }


        public bool CollisionWithBullet(Shoot shoot)
        {

            if (shoot.GetShootRectangle().Intersects(GetEnnemieRectangle()))
            {
                Globals.nbShootHasTouch++;
                Globals.listShoots.Remove(shoot);
                Life -= shoot.Damage;
                hasShootTouchEnnemi = true;
                if (Life <= 0)
                {
                    IsDead = true;
                    if (GetType() == typeof(Ennemies))
                    {
                        Globals.listLittleSlime.Remove(this);
                        Globals.nbSlimeKilled++;
                        MusicManager.PlaySoundEffect(GlobalsSounds.slimeDeath);
                    }
                    else if (GetType() == typeof(SlimeShooter))
                    {
                        Globals.listShootSlime.Remove((SlimeShooter)this);
                        Globals.nbShooterSlimeKilled++;
                        MusicManager.PlaySoundEffect(GlobalsSounds.slimeDeath);
                    }
                }
            }
            return hasShootTouchEnnemi;
        }

        public bool TakeDamageFromDog(Dog dog)
        {
            Life -= dog.Damage;
            hasShootTouchEnnemi = true;
            if (Life <= 0)
            {
                IsDead = true;
                if (GetType() == typeof(Ennemies))
                {
                    Globals.listLittleSlime.Remove(this);
                    Globals.nbSlimeKilled++;
                    MusicManager.PlaySoundEffect(GlobalsSounds.slimeDeath);
                }
                else if (GetType() == typeof(SlimeShooter))
                {
                    Globals.listShootSlime.Remove((SlimeShooter)this);
                    Globals.nbShooterSlimeKilled++;
                    MusicManager.PlaySoundEffect(GlobalsSounds.slimeDeath);
                }
                return true;
            }
            return false;
        }

        public Rectangle GetEnnemieRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, GetEnnemieRectangle(), Color);
        }

    }
}
