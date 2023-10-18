///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : Shoot.cs
//Utilité : Moule du shoot et intélligence
///Projet : ForestSurvivor V1 (2023)
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using ForestSurvivor.AllEnnemies;

namespace ForestSurvivor
{
    internal class Shoot
    {
        private Texture2D _texture;
        private int _width;
        private int _height;
        private float _x;
        private float _y;
        private int _speed;
        private float _damage;
        private bool _destroy;
        private Vector2 directionTir;
        private Vector2 positionTir;
        private MouseState mouseState;

        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public float X { get => _x; set => _x = value; }
        public float Y { get => _y; set => _y = value; }
        public int Speed { get => _speed; set => _speed = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public bool Destroy { get => _destroy; set => _destroy = value; }
        public float Damage { get => _damage; set => _damage = value; }

        public Shoot(float x, float y, Player player)
        {
            _x = x;
            _y = y;
            _width = 20;
            _height = 20;
            _speed = 20;
            _damage = player.ActualDamage;
            _destroy = false;
            Texture = GlobalsTexture.bullet;

            mouseState = Mouse.GetState();
            directionTir = new Vector2(mouseState.X - X, mouseState.Y - Y);
            directionTir.Normalize();
            positionTir = new Vector2(x, y);
        }
        public void Update(Player player)
        {
            float thresholdX = 0.2f; // Ajustez ce seuil en fonction de la largeur du projectile
            float thresholdY = 0.3f; // Ajustez ce seuil en fonction de la hauteur du projectile

            // Mouvement du tir
            positionTir += directionTir * Speed;

            if (positionTir.X < 0 || positionTir.Y < 0 || positionTir.X > Globals.ScreenWidth || positionTir.Y > Globals.ScreenHeight)
            {
                Destroy = true;
            }

            // Tir en direction haut-gauche               
            if (directionTir.Y <= -thresholdY && directionTir.Y >= -1.0f && directionTir.X < -thresholdX && directionTir.X >= -1.0f)
            {
                player.Texture = GlobalsTexture.listTexturesPlayer[6];
            }
            // Tir en direction bas-gauche
            else if (directionTir.X <= 0 && directionTir.Y >= thresholdY)
            {
                player.Texture = GlobalsTexture.listTexturesPlayer[1];
            }
            // Tir vers la gauche
            else if (directionTir.X <= -thresholdX && directionTir.Y >= -thresholdY)
            {
                player.Texture = GlobalsTexture.listTexturesPlayer[3];
            }
            // Tir vers le bas
            else if (directionTir.Y >= thresholdY && Math.Abs(directionTir.X) < thresholdX)
            {
                player.Texture = GlobalsTexture.listTexturesPlayer[0];
            }
            // Tir en direction bas-droite
            else if (directionTir.Y >= thresholdY && directionTir.X >= thresholdX)
            {
                player.Texture = GlobalsTexture.listTexturesPlayer[2];
            }
            // Tir vers la droite
            else if (directionTir.X >= thresholdX && directionTir.Y >= -thresholdY)
            {
                player.Texture = GlobalsTexture.listTexturesPlayer[4];
            }
            // Tir en direction haut-droite
            else if (directionTir.Y <= -thresholdY && directionTir.X >= thresholdX)
            {
                player.Texture = GlobalsTexture.listTexturesPlayer[7];
            }
            // Tir vers le haut
            else if (directionTir.Y <= -thresholdY && Math.Abs(directionTir.X) < thresholdX)
            {
                player.Texture = GlobalsTexture.listTexturesPlayer[5];
            }
        }

        /// <summary>
        /// Supprime les balles en dehors de l'écran
        /// </summary>
        public static void DeleteShootInBorder()
        {
            foreach (Shoot shoot in Globals.listShoots)
            {
                if (shoot.Destroy)
                {
                    Globals.listShoots.Remove(shoot);
                    break;
                }
            }
        }

        /// <summary>
        /// Collision avec tous les ennemies
        /// </summary>
        public static void CollsionBulletWithEnnemies()
        {
            bool hasKilled = false;
            foreach (Shoot shoot in Globals.listShoots)
            {
                foreach (Ennemies ennemies in Globals.listLittleSlime)
                {
                    hasKilled = ennemies.CollisionWithBullet(shoot);
                    if (hasKilled)
                    {
                        break;
                    }
                }
                if (hasKilled)
                {
                    break;
                }
            }
            hasKilled = false;
            foreach (Shoot shoot in Globals.listShoots)
            {
                foreach (BigSlime ennemies in Globals.listBigSlime)
                {
                    hasKilled = ennemies.CollisionWithBullet(shoot);
                    if (hasKilled)
                    {
                        break;
                    }
                }
                if (hasKilled)
                {
                    break;
                }
            }
            hasKilled = false;
            foreach (Shoot shoot in Globals.listShoots)
            {
                foreach (SlimeShooter ennemies in Globals.listShootSlime)
                {
                    hasKilled = ennemies.CollisionWithBullet(shoot);
                    if (hasKilled)
                    {
                        break;
                    }
                }
                if (hasKilled)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Retourne le rectangle de la balle
        /// </summary>
        /// <returns></returns>
        public Rectangle GetShootRectangle()
        {
            return new Rectangle((int)positionTir.X, (int)positionTir.Y, Width, Height);
        }

        /// <summary>
        /// Affiche la balle
        /// </summary>
        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, new Rectangle((int)positionTir.X, (int)positionTir.Y, Width, Height), Color.White);
        }
    }
}
