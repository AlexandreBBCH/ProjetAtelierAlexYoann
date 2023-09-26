using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ForestSurvivor
{
    internal class Player
    {
        public const float TIME_COLOR_RED = 0.2f;

        private Texture2D _texture;
        private int _width;
        private int _height;
        private int _x;
        private int _y;
        private int _speed;
        private int _life;
        private int _score;
        private int _highscore;
        private Color _color;

        private int mouvementDirection;
        private bool canShoot;

        public Texture2D Texture { get => _texture; set => _texture = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Speed { get => _speed; set => _speed = value; }
        public int Life { get => _life; set => _life = value; }
        public int Score { get => _score; set => _score = value; }
        public int Highscore { get => _highscore; set => _highscore = value; }
        public Color PlayerColor { get => _color; set => _color = value; }

        public Player(int width, int height, int x, int y, int speed, int life, Color color)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;
            PlayerColor = color;
            mouvementDirection = 1;
            Speed = speed;
            Life = life;
        }

        public void Update(GameTime gameTime, Game game)
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
                int x = 0;
                int y = 0;
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

                Globals.listShoots.Add(new Shoot(x, y, mouvementDirection));
                canShoot = false;
            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                canShoot = true;
            }
            #endregion

            #region collision bullet with ennemies
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
            #endregion

            #region delete bullet in border
            foreach (Shoot shoot in Globals.listShoots)
            {
                if (shoot.Destroy)
                {
                    Globals.listShoots.Remove(shoot);
                    break;
                }
            }
            #endregion

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
                hasBigSlimeDied = bigSlime.CreateNewLittleSlime(game);
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
        }

        public Rectangle GetPlayerRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }

        /// <summary>
        /// Draw the player
        /// </summary>
        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, GetPlayerRectangle(), PlayerColor);
        }

        /// <summary>
        /// Draw the life of the player
        /// </summary>
        public void DrawLife()
        {
            Globals.SpriteBatch.DrawString(GlobalsTexture.textGamefont, $"Life : {Life}", new Vector2(0, 0), Color.White);
        }

    }
}
