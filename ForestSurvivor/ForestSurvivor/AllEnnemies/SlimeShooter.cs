using ForestSurvivor.AllGlobals;
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
            // Position initiale du tir
            projectilePosition = new Vector2(XBullet, YBullet);
        }

        public void Shoot(GameTime gameTime, Player player)
        {
            timerShoot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timerShoot >= DamageSpeed)
            {
                canShoot = true;
                XBullet = X;
                YBullet = Y;
                xPlayer = player.X + player.Width / 2;
                yPlayer = player.Y + player.Height / 2;
                timerShoot = 0;

                // Position initiale du tir
                projectilePosition = new Vector2(XBullet, YBullet);
                // Direction du tir
                direction = new Vector2(xPlayer - XBullet, yPlayer - YBullet);
                direction.Normalize();
            }

            if (canShoot)
            {
                projectilePosition += direction * shootSpeed;
            }

            if (GetRectangleShoot().Intersects(player.GetPlayerRectangle()) && canShoot)
            {
                projectilePosition.X = X;
                projectilePosition.Y = Y;
                direction = new Vector2(0, 0);
                canShoot = false;
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

        public Rectangle GetRectangleShoot()
        {
            return new Rectangle((int)projectilePosition.X, (int)projectilePosition.Y, 20, 10);
        }

        public void DrawBullet()
        {
            Globals.SpriteBatch.Draw(ShootTexture, GetRectangleShoot(), Color);
        }

        
    }
}
