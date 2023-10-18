///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : SlimeShooter.cs
//Utilité : Gestionnaire du tireur slime rouge  
///Projet : ForestSurvivor V1 (2023)
using ForestSurvivor.AllGlobals;
using ForestSurvivor.Animals;
using ForestSurvivor.SongManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestSurvivor.AllEnnemies
{
    internal class SlimeShooter : Ennemies
    {
        private float timerShoot;
        private bool canShoot;
        private float xPlayer;
        private float yPlayer;
        private int shootSpeed;
        private Texture2D _shootTexture;
        private int _xBullet;
        private int _yBullet;
        private Vector2 projectilePosition;
        private Vector2 direction;
        public Dog dogShoot;


        public Texture2D ShootTexture { get => _shootTexture; set => _shootTexture = value; }
        public int XBullet { get => _xBullet; set => _xBullet = value; }
        public int YBullet { get => _yBullet; set => _yBullet = value; }

        public SlimeShooter(int width, int height, int life, int speed, int damage, float damageSpeed) : base(width, height, life, speed, damage, damageSpeed)
        {
            timerShoot = 0;
            canShoot = false;
            shootSpeed = 15;
            XBullet = X;
            YBullet = Y;
        }

        public void Shoot(GameTime gameTime, Player player)
        {
            // Si il ne peut pas encore tiré, récupère la position de la cible 
            if (!canShoot)
            {
                timerShoot += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timerShoot >= DamageSpeed)
                {
                    canShoot = true;
                    XBullet = X;
                    YBullet = Y;

                    if (!isEnnemiHurtByDog)
                    {
                        xPlayer = player.X + player.Width / 2;
                        yPlayer = player.Y + player.Height / 2;
                    }
                    else
                    {
                        xPlayer = dogShoot.X + dogShoot.Width / 2;
                        yPlayer = dogShoot.Y + dogShoot.Height / 2;
                    }

                    timerShoot = 0;

                    // Position initiale du tir
                    projectilePosition = new Vector2(XBullet, YBullet);
                    // Direction du tir
                    direction = new Vector2(xPlayer - XBullet, yPlayer - YBullet);
                    direction.Normalize();
                }
            }
            else
            {
                // Le projectile avance
                projectilePosition += direction * shootSpeed;
                
                if (!isEnnemiHurtByDog)
                {
                    // Si la balle touche le joueur, inflige des dégâts et lance un son de dégât du joueur
                    if (GetRectangleShoot().Intersects(player.GetPlayerRectangle()))
                    {
                        canShoot = false;
                        MusicManager.PlayRandomHurtEffect();
                        if (player.Life - Damage < 0)
                        {
                            player.Life = 0;
                        }
                        else
                        {
                            player.Life -= Damage;
                        }
                    }
                }
                else
                {
                    // Si la balle touche le chien, inflige des dégâts et lance le son de douleur ou de mort
                    if (GetRectangleShoot().Intersects(dogShoot.GetRectangle()))
                    {
                        canShoot = false;
                        dogShoot.IsHurt = true;
                        dogShoot.Life -= Damage;
                        if (dogShoot.Life <= 0)
                        {
                            dogShoot.isDead = true;
                            MusicManager.PlaySoundEffect(GlobalsSounds.dogDied);
                            isEnnemiHurtByDog = false;
                        }
                    }
                }

                // Supprime le tir si il est en dehors de la map
                if (projectilePosition.X < 0 || projectilePosition.Y < 0 || projectilePosition.X > Globals.ScreenWidth || projectilePosition.Y > Globals.ScreenHeight)
                {
                    canShoot = false;
                }
            }
        }

        /// <summary>
        /// Retourne le rectangle du projectile du slime
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangleShoot()
        {
            return new Rectangle((int)projectilePosition.X, (int)projectilePosition.Y, 20, 10);
        }

        /// <summary>
        /// Affiche le tire du slime
        /// </summary>
        public void DrawBullet()
        {
            if (canShoot)
            {
                Globals.SpriteBatch.Draw(ShootTexture, GetRectangleShoot(), Color);
            }
        }
        
    }
}
