///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : Ennemies.cs
//Utilité : Gestionnaire du petit slime bleu  
///Projet : ForestSurvivor V1 (2023)
using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using ForestSurvivor.Animals;
using ForestSurvivor.SongManager;
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
        bool isHurt = false;
        float timerHurt = 0;
        public bool isEnnemiHurtByDog = false;
        private bool canMakeDamage = true;
        private float timerDamage = 0;
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

            // Apparait dans une bordure aléatoire de l'écran
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

        public void Update(Player player, GameTime gameTime)
        {
            // Si ce n'est pas le slime shooter (il hérite de cette classe) car il ne suit pas le joueur
            if (GetType() != typeof(SlimeShooter))
            {
                if (!isEnnemiHurtByDog)
                {
                    bool moreXcollided = false;
                    bool lessXcollided = false;
                    bool moreYcollided = false;
                    bool lessYcollided = false;

                    // Collision avec les autres ennemies pour qu'ils ne soient pas les uns dans les autres
                    foreach (Ennemies ennemies in Globals.listLittleSlime)
                    {
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

                    // Suit le joueur
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
                    // temps entre les attaques
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
                        // Inflige des dégâts au chien qui l'attaque
                        foreach (Dog dog in Globals.listDogs)
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
            }

            // Son de mouvement du slime toute les 2sec
            timerSound += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timerSound >= 2)
            {
                MusicManager.PlaySoundEffect(GlobalsSounds.slimeMove);
                timerSound = 0;
            }


            // Change la couleur pour rouge lors d'une blessure
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
        /// Collision avec le joueur : le joueur prend des dégâts et sa couleur change pour du rouge pendant un peit instant
        /// </summary>
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
                // Joueur prend des dégâts
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

        /// <summary>
        /// Collision entre l'ennemi et un shoot
        /// </summary>
        /// <param name="shoot"></param>
        public bool CollisionWithBullet(Shoot shoot)
        {
            // Si il y a une collision, supprime la balle et inflige des dégâts à l'ennemi
            if (shoot.GetShootRectangle().Intersects(GetEnnemieRectangle()))
            {
                Globals.nbShootHasTouch++;
                Globals.listShoots.Remove(shoot);
                Life -= shoot.Damage;
                hasShootTouchEnnemi = true;
                // Supprime l'ennemi mort de la liste si il n'est pas gros car le gros à une fonction différente pour sa mort
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

        /// <summary>
        /// L'ennmi prend des dégâts du chien
        /// </summary>
        /// <param name="dog"></param>
        /// <returns></returns>
        public bool TakeDamageFromDog(Dog dog)
        {
            Life -= dog.Damage;
            hasShootTouchEnnemi = true;
            if (Life <= 0)
            {
                // Supprime l'ennemi mort de la liste si il n'est pas gros car le gros à une fonction différente pour sa mort
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

        /// <summary>
        /// Retourne le rectangle de l'ennemi
        /// </summary>
        public Rectangle GetEnnemieRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }

        /// <summary>
        /// Affiche l'ennemi
        /// </summary>
        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, GetEnnemieRectangle(), Color);
        }

    }
}
