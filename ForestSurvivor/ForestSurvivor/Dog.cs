using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using ForestSurvivor.AllItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ForestSurvivor
{
    internal class Dog
    {
        private Texture2D _texture;
        private float _width;
        private float _height;
        private float _x;
        private float _y;
        private float _speed;
        private int _life;
        private int _pvMax;
        private float _speedMax;
        private float _damage;
        private float _damageSpeed;
        private Color _color;
        private Vector2 direction;
        private Vector2 position;

        private const int TIME_STOP_WHEN_COME_TO_PLAYER = 1;

        public bool isDead;
        private float timerDamageEnnemi;
        private float timerBetweenTarget;
        private bool hasTarget;
        private bool isItemCollected;
        private bool isPlayerTouch;
        private float distanceBetweenTarget;
        private Ennemies ennemiesTarget;
        private SlimeShooter shooterTarget;
        private BigSlime bigSlimeTarget;
        private Items itemTarget;

        public Texture2D Texture { get => _texture; set => _texture = value; }
        public float Width { get => _width; set => _width = value; }
        public float Height { get => _height; set => _height = value; }
        public float X { get => _x; set => _x = value; }
        public float Y { get => _y; set => _y = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public int Life { get => _life; set => _life = value; }
        public int PvMax { get => _pvMax; set => _pvMax = value; }
        public float SpeedMax { get => _speedMax; set => _speedMax = value; }
        public Color Color { get => _color; set => _color = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public float DamageSpeed { get => _damageSpeed; set => _damageSpeed = value; }

        public Dog(float width, float height, float x, float y, float speed, int life, float damage, float damageSpeed)
        {
            _width = width;
            _height = height;
            _x = x;
            _y = y;
            _speed = speed;
            _life = life;
            _damage = damage;
            _damageSpeed = damageSpeed;

            Color = Color.Green;
            Texture = GlobalsTexture.Slime2D;
            position = new Vector2(_x, _y);
            direction = new Vector2(0, 0);
            distanceBetweenTarget = Globals.ScreenWidth;
            ennemiesTarget = null;
            shooterTarget = null;
            bigSlimeTarget = null;
            hasTarget = false;
            isItemCollected = false;
            isPlayerTouch = false;
            isDead = false;
            timerBetweenTarget = 0;
            timerDamageEnnemi = 0;
        }

        public void Update(Player player, GameTime gameTime)
        {
            if (!isDead)
            {
                if (ennemiesTarget == null && bigSlimeTarget == null && shooterTarget == null && itemTarget == null && !hasTarget)
                {
                    foreach (Ennemies ennemies in Globals.listLittleSlime)
                    {
                        float tmpDistanceBetweenTarget = GetDistanceBetween(ennemies.X, ennemies.Y);
                        if (tmpDistanceBetweenTarget < distanceBetweenTarget)
                        {
                            distanceBetweenTarget = tmpDistanceBetweenTarget;
                            ennemiesTarget = ennemies;
                            hasTarget = true;
                        }
                    }
                    foreach (BigSlime bigSlime in Globals.listBigSlime)
                    {
                        float tmpDistanceBetweenTarget = GetDistanceBetween(bigSlime.X, bigSlime.Y);
                        if (tmpDistanceBetweenTarget < distanceBetweenTarget)
                        {
                            distanceBetweenTarget = tmpDistanceBetweenTarget;
                            bigSlimeTarget = bigSlime;
                            hasTarget = true;
                        }
                    }
                    foreach (SlimeShooter slimeShooter in Globals.listShootSlime)
                    {
                        float tmpDistanceBetweenTarget = GetDistanceBetween(slimeShooter.X, slimeShooter.Y);
                        if (tmpDistanceBetweenTarget < distanceBetweenTarget)
                        {
                            distanceBetweenTarget = tmpDistanceBetweenTarget;
                            shooterTarget = slimeShooter;
                            hasTarget = true;
                        }
                    }
                    foreach (Items item in Globals.listItems)
                    {
                        float tmpDistanceBetweenTarget = GetDistanceBetween(item.X, item.Y);
                        if (tmpDistanceBetweenTarget < distanceBetweenTarget)
                        {
                            distanceBetweenTarget = tmpDistanceBetweenTarget;
                            itemTarget = item;
                            hasTarget = true;
                        }
                    }
                }
                else if (ennemiesTarget != null)
                {
                    if (!ennemiesTarget.isEnnemiHurtByDog)
                    {
                        direction = new Vector2(ennemiesTarget.X - position.X, ennemiesTarget.Y - position.Y);
                        direction.Normalize();
                        position += direction * Speed;
                        SetTextureWithDirection(direction);
                    }

                    if (GetRectangle().Intersects(ennemiesTarget.GetEnnemieRectangle()))
                    {
                        ennemiesTarget.isEnnemiHurtByDog = true;
                        timerDamageEnnemi += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (timerDamageEnnemi >= DamageSpeed)
                        {
                            bool isEnnemieDead = ennemiesTarget.TakeDamageFromDog(this);
                            if (isEnnemieDead)
                            {
                                ennemiesTarget = null;
                                hasTarget = false;
                                distanceBetweenTarget = Globals.ScreenWidth;
                            }
                            timerDamageEnnemi = 0;
                        }
                    }

                    if (!Globals.listLittleSlime.Contains(ennemiesTarget))
                    {
                        ennemiesTarget = null;
                        hasTarget = false;
                        distanceBetweenTarget = Globals.ScreenWidth;
                        timerDamageEnnemi = 0;
                    }
                }
                else if (bigSlimeTarget != null)
                {
                    if (!bigSlimeTarget.isEnnemiHurtByDog)
                    {
                        direction = new Vector2(bigSlimeTarget.X - position.X, bigSlimeTarget.Y - position.Y);
                        direction.Normalize();
                        position += direction * Speed;
                        SetTextureWithDirection(direction);
                    }

                    if (GetRectangle().Intersects(bigSlimeTarget.GetEnnemieRectangle()))
                    {
                        bigSlimeTarget.isEnnemiHurtByDog = true;
                        timerDamageEnnemi += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (timerDamageEnnemi >= DamageSpeed)
                        {
                            bool isEnnemieDead = bigSlimeTarget.TakeDamageFromDog(this);
                            if (isEnnemieDead)
                            {
                                bigSlimeTarget = null;
                                hasTarget = false;
                                distanceBetweenTarget = Globals.ScreenWidth;
                            }
                            timerDamageEnnemi = 0;
                        }
                    }

                    if (!Globals.listBigSlime.Contains(bigSlimeTarget))
                    {
                        bigSlimeTarget = null;
                        hasTarget = false;
                        distanceBetweenTarget = Globals.ScreenWidth;
                        timerDamageEnnemi = 0;
                    }
                }
                else if (shooterTarget != null)
                {
                    if (!shooterTarget.isEnnemiHurtByDog)
                    {
                        direction = new Vector2(shooterTarget.X - position.X, shooterTarget.Y - position.Y);
                        direction.Normalize();
                        position += direction * Speed;
                        SetTextureWithDirection(direction);
                    }

                    if (GetRectangle().Intersects(shooterTarget.GetEnnemieRectangle()))
                    {
                        shooterTarget.isEnnemiHurtByDog = true;
                        timerDamageEnnemi += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (timerDamageEnnemi >= DamageSpeed)
                        {
                            bool isEnnemieDead = shooterTarget.TakeDamageFromDog(this);
                            if (isEnnemieDead)
                            {
                                shooterTarget = null;
                                hasTarget = false;
                                distanceBetweenTarget = Globals.ScreenWidth;
                            }
                            timerDamageEnnemi = 0;
                        }
                    }

                    if (!Globals.listShootSlime.Contains(shooterTarget))
                    {
                        shooterTarget = null;
                        hasTarget = false;
                        distanceBetweenTarget = Globals.ScreenWidth;
                        timerDamageEnnemi = 0;
                    }
                }
                else if (itemTarget != null)
                {

                    if (GetRectangle().Intersects(itemTarget.GetItemRectangle()))
                    {
                        isItemCollected = true;
                        itemTarget.IsCollected = true;
                    }

                    if (!isItemCollected)
                    {
                        direction = new Vector2(itemTarget.X - position.X, itemTarget.Y - position.Y);
                        direction.Normalize();
                        position += direction * Speed;
                        SetTextureWithDirection(direction);
                    }
                    else
                    {
                        if (timerBetweenTarget == 0)
                        {
                            direction = new Vector2(player.X - position.X, player.Y - position.Y);
                            direction.Normalize();
                            position += direction * Speed;
                            SetTextureWithDirection(direction);
                        }

                        if (GetRectangle().Intersects(player.GetPlayerRectangle()))
                        {
                            isPlayerTouch = true;
                        }
                        if (isPlayerTouch)
                        {
                            if (timerBetweenTarget == 0)
                            {
                                itemTarget.AddEffect(player);
                            }
                            timerBetweenTarget += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (timerBetweenTarget >= TIME_STOP_WHEN_COME_TO_PLAYER)
                            {
                                itemTarget = null;
                                distanceBetweenTarget = Globals.ScreenWidth;
                                hasTarget = false;
                                isItemCollected = false;
                                timerBetweenTarget = 0;
                                isPlayerTouch = false;
                            }
                        }

                    }

                    if (!Globals.listItems.Contains(itemTarget) && !isItemCollected)
                    {
                        itemTarget = null;
                        distanceBetweenTarget = Globals.ScreenWidth;
                        hasTarget = false;
                        isItemCollected = false;
                        timerBetweenTarget = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Change la texture en fonction de la direction
        /// </summary>
        /// <param name="direction">direction ou se déplace le chien</param>
        public void SetTextureWithDirection(Vector2 direction)
        {
            // Left
            if (direction.X < -0.2f && direction.Y <= 1 && direction.Y >= 0)
            {
                
            }
            // Right
            else if (direction.X > 0.2f && direction.Y >= 0 && direction.Y <= 1)
            {
                
            }
            // Top
            else if (direction.X >= -0.2f && direction.X <= 0.2f && direction.Y > 0 && direction.Y <= 1)
            {
                
            }
            // Bottom
            else if (direction.X >= -0.2f && direction.X <= 0.2f && direction.Y < 0 && direction.Y >= -1)
            {
                
            }
        }


        public float GetDistanceBetween(int Xtarget, int Ytarget)
        {
            float xbetween = (X - Xtarget);
            if (xbetween < 0)
            {
                xbetween = -xbetween;
            }

            float ybetween = (Y - Ytarget);
            if (ybetween < 0)
            {
                ybetween = -ybetween;
            }
            return xbetween + ybetween;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)Width, (int)Height);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, GetRectangle(), Color);
        }
    }
}
