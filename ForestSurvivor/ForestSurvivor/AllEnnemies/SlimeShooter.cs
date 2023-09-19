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
        private int xPlayer;
        private int yPlayer;
        private int xBetween;
        private int yBetween;
        private int shootSpeed;
        private Texture2D _shootTexture;
        private int _xBullet;
        private int _yBullet;

        public Texture2D ShootTexture { get => _shootTexture; set => _shootTexture = value; }
        public int XBullet { get => _xBullet; set => _xBullet = value; }
        public int YBullet { get => _yBullet; set => _yBullet = value; }

        public SlimeShooter(int width, int height, int life, int speed, int damage, float damageSpeed) : base(width, height, life, speed, damage, damageSpeed)
        {
            timerShoot = 0;
            canShoot = false;
            xPlayer = 0;
            yPlayer = 0;
            xBetween = 0;
            yBetween = 0;
            shootSpeed = 10;
            XBullet = 0;
            YBullet = 0;
        }

        public void Shoot(GameTime gameTime, Player player)
        {
            timerShoot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timerShoot >= DamageSpeed)
            {
                canShoot = true;
                XBullet = X;
                YBullet = Y;
                xPlayer = player.X;
                yPlayer = player.Y;
                timerShoot = 0;
            }

            if (canShoot)
            {
                xBetween = XBullet - xPlayer;
                yBetween = YBullet - yPlayer;

                if (xBetween != 0)
                {
                    if (xBetween < 0)
                    {
                        XBullet += shootSpeed;
                        xBetween += shootSpeed;
                    }
                    else
                    {
                        XBullet -= shootSpeed;
                        xBetween -= shootSpeed;
                    }
                    
                }
                if (yBetween != 0)
                {
                    if (yBetween < 0)
                    {
                        YBullet += shootSpeed;
                        yBetween += shootSpeed;
                    }
                    else
                    {
                        YBullet -= shootSpeed;
                        yBetween -= shootSpeed;
                    }
                }
                if (xBetween == 0 && yBetween == 0)
                {
                    XBullet = -1;
                    YBullet = -1;
                }
            }
        }

        public void DrawBullet()
        {
            Globals.SpriteBatch.Draw(ShootTexture, new Rectangle(XBullet, YBullet, 20, 10), Color);
        }

    }
}
