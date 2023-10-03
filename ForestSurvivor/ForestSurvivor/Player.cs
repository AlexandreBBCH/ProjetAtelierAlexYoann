using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ForestSurvivor
{
    internal class Player
    {
        public const float TIME_COLOR_RED = 0.2f;

        private Texture2D _texture;
        private float _width;
        private float _height;
        private float _x;
        private float _y;
        private float _speed;
        private int _life;
        private int _score;
        private int _pvMax; 
        private float _speedMax;
        private int _highscore;
        private Color _color;
        private bool _isPlayerHurt;

        private int mouvementDirection;
        private bool canShoot;
        private float timerPlayerHurt;
        private Random rnd;
        private bool canMakeSoundHurt;
        private bool _isSpeedItemAcivated;
        private float _tmpSpeedTimer;
        private bool _isDamageItemAcivated;
        private float _tmpDamageTimer;
        private float _actualDamage;
        private float _damageMax;
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public float Width { get => _width; set => _width = value; }
        public float Height { get => _height; set => _height = value; }
        public float X { get => _x; set => _x = value; }
        public float Y { get => _y; set => _y = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public int Life { get => _life; set => _life = value; }
        public int Score { get => _score; set => _score = value; }
        public int Highscore { get => _highscore; set => _highscore = value; }
        public Color PlayerColor { get => _color; set => _color = value; }
        public int PvMax { get => _pvMax; set => _pvMax = value; }
        public float SpeedMax { get => _speedMax; set => _speedMax = value; }
        public bool IsPlayerHurt { get => _isPlayerHurt; set => _isPlayerHurt = value; }

        public float ActualDamage { get => _actualDamage; set => _actualDamage = value; }
        public float DamageMax { get => _damageMax; set => _damageMax = value; }
        public float TmpSpeedTimer { get => _tmpSpeedTimer; set => _tmpSpeedTimer = value; }
        public bool IsSpeedItemAcivated { get => _isSpeedItemAcivated; set => _isSpeedItemAcivated = value; }
        public bool IsDamageItemAcivated { get => _isDamageItemAcivated; set => _isDamageItemAcivated = value; }
        public float TmpDamageTimer { get => _tmpDamageTimer; set => _tmpDamageTimer = value; }

        public Player(int width, int height, int x, int y, float speedMax, int lifeMax, float damageMax, Color color)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;
            PlayerColor = color;
            mouvementDirection = 1;
            Speed = speedMax;
            SpeedMax = speedMax;
            Life = lifeMax;
            PvMax = lifeMax;
            DamageMax = damageMax;
            ActualDamage = damageMax;
            IsSpeedItemAcivated = false;
            IsPlayerHurt = false;
            timerPlayerHurt = 0;
            rnd = new Random();
            canMakeSoundHurt = false;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyPress = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            #region Top Left Mouvement
            if (keyPress.IsKeyDown(Keys.W) && keyPress.IsKeyDown(Keys.A))
            {
                if (Y - Speed >= 0)
                {
                    Y -= Speed;
                }
                if (X - Speed >= 0)
                {
                    X -= Speed;
                }
                Texture = GlobalsTexture.listTexturesPlayer[6];
                mouvementDirection = 8;
            }
            #endregion

            #region Top Right Mouvement
            else if (keyPress.IsKeyDown(Keys.W) && keyPress.IsKeyDown(Keys.D))
            {
                if (Y - Speed >= 0)
                {
                    Y -= Speed;
                }
                if (X + Speed + Width <= Globals.ScreenWidth)
                {
                    X += Speed;
                }
                Texture = GlobalsTexture.listTexturesPlayer[7];
                mouvementDirection = 2;
            }
            #endregion

            #region Bottom Left Mouvement
            else if (keyPress.IsKeyDown(Keys.S) && keyPress.IsKeyDown(Keys.A))
            {
                if (Y + Speed + Height <= Globals.ScreenHeight)
                {
                    Y += Speed;
                }
                if (X - Speed >= 0)
                {
                    X -= Speed;
                }
                Texture = GlobalsTexture.listTexturesPlayer[1];
                mouvementDirection = 6;
            }
            #endregion

            #region Bottom Right Mouvement
            else if (keyPress.IsKeyDown(Keys.S) && keyPress.IsKeyDown(Keys.D))
            {
                if (Y + Speed + Height <= Globals.ScreenHeight)
                {
                    Y += Speed;
                }
                if (X + Speed + Width <= Globals.ScreenWidth)
                {
                    X += Speed;
                }
                Texture = GlobalsTexture.listTexturesPlayer[2];
                mouvementDirection = 4;
            }
            #endregion

            #region Top Mouvement
            else if (keyPress.IsKeyDown(Keys.W))
            {
                if (Y - Speed >= 0)
                {
                    Y -= Speed;
                }
                Texture = GlobalsTexture.listTexturesPlayer[5];
                mouvementDirection = 1;
            }
            #endregion

            #region Left Mouvement
            else if (keyPress.IsKeyDown(Keys.A))
            {
                if (X - Speed >= 0)
                {
                    X -= Speed;
                }
                Texture = GlobalsTexture.listTexturesPlayer[3];
                mouvementDirection = 7;
            }
            #endregion

            #region Right Mouvement
            else if (keyPress.IsKeyDown(Keys.D))
            {
                if (X + Speed + Width <= Globals.ScreenWidth)
                {
                    X += Speed;
                }
                Texture = GlobalsTexture.listTexturesPlayer[4];
                mouvementDirection = 3;
            }
            #endregion

            #region Bottom Mouvement
            else if (keyPress.IsKeyDown(Keys.S))
            {
                if (Y + Speed + Height <= Globals.ScreenHeight)
                {
                    Y += Speed;
                }
                Texture = GlobalsTexture.listTexturesPlayer[0];
                mouvementDirection = 5;
            }
            #endregion

            #region add Shoot
            if (mouseState.LeftButton == ButtonState.Pressed && canShoot)
            {
                float x = 0;
                float y = 0;
                if (mouvementDirection == 1)
                {
                    x = X + Width / 2;
                    y = Y;
                }
                else if (mouvementDirection == 2)
                {
                    x = X + Width / 2;
                    y = Y + Height / 4;
                }
                else if (mouvementDirection == 3)
                {
                    x = X + Width / 2;
                    y = Y + Height / 3;
                }
                else if (mouvementDirection == 4)
                {
                    x = X + Width / 2;
                    y = Y + Height / 2;
                }
                else if (mouvementDirection == 5)
                {
                    x = X + Width / 3;
                    y = Y + Height / 2;
                }
                else if (mouvementDirection == 6)
                {
                    x = X + Width / 4 - 10;
                    y = Y + Height / 3 + 9;
                }
                else if (mouvementDirection == 7)
                {
                    x = X;
                    y = Y + Height / 3;
                }
                else if (mouvementDirection == 8)
                {
                    x = X + Width / 4;
                    y = Y + Height / 4;
                }

                Globals.listShoots.Add(new Shoot(x, y, this));
                MusicManager.PlaySoundEffect(GlobalsSounds.shootEffect);
                canShoot = false;
            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                canShoot = true;
            }
            #endregion

            // Change la couleur du joueur après avoir été touché
            if (IsPlayerHurt)
            {
                if (!canMakeSoundHurt)
                {
                    MusicManager.PlayRandomHurtEffect();
                    canMakeSoundHurt = true;
                }
                timerPlayerHurt += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timerPlayerHurt <= TIME_COLOR_RED)
                {
                    PlayerColor = Color.Red;
                }
                else if (timerPlayerHurt >= TIME_COLOR_RED)
                {
                    PlayerColor = Color.White;
                    IsPlayerHurt = false;
                    timerPlayerHurt = 0f;
                    canMakeSoundHurt = false;
                }
            }

            foreach (Ennemies ennemies in Globals.listLittleSlime)
            {
                ennemies.CollisionWithPlayer(this, gameTime);
            }

            foreach (BigSlime bigSlime in Globals.listBigSlime)
            {
                bigSlime.CollisionWithPlayer(this, gameTime);
            }

            foreach (SlimeShooter slimeShooter in Globals.listShootSlime)
            {
                slimeShooter.CollisionWithPlayer(this, gameTime);
            }

            foreach (BigSlime bigSlime in Globals.listBigSlime)
            {
                bool hasBigSlimeDied = false;
                hasBigSlimeDied = bigSlime.CreateNewLittleSlime();
                if (hasBigSlimeDied)
                {
                    break;
                }
            }

            #region player is dead
            if (Life == 0)
            {

            }

            #endregion
            if (IsSpeedItemAcivated) ResetSpeed(gameTime);
            if (IsDamageItemAcivated) ResetDamage(gameTime);
        }

        public Rectangle GetPlayerRectangle()
        {
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }

        /// <summary>
        /// Draw the player
        /// </summary>
        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, GetPlayerRectangle(), PlayerColor);
        }

        public void ResetSpeed(GameTime gameTime)
        {
            TmpSpeedTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (TmpSpeedTimer >= 5f)
            {
                Speed = SpeedMax;
                IsSpeedItemAcivated = false;
                TmpSpeedTimer = 0;
            }

        }

        public void AddItemSpeed(float speed)
        {
            if (!IsSpeedItemAcivated && speed > 0)
                Speed += speed;
            IsSpeedItemAcivated = true;
        }

        public void ResetDamage(GameTime gameTime)
        {
            TmpDamageTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (TmpDamageTimer >= 5f)
            {
                ActualDamage = DamageMax;
                IsDamageItemAcivated = false;
                TmpSpeedTimer = 0;
            }

        }

        public void AddItemDamage(float damage)
        {
            if (!IsDamageItemAcivated && damage > 0)
                ActualDamage += damage;
            IsDamageItemAcivated = true;
        }
        public bool IsDead()
        {
            if (Life <= 0)
            {
                Globals.GameOver = true;
                return true;
            }
            return false;
        }

    }
}
