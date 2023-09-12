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
        private Texture2D _texture;
        private int _width;
        private int _height;
        private int _x;
        private int _y;
        private int _speed;
        private int _life;
        private int _score;
        private int _highscore;

        private int mouvementDirection;
        private bool canShoot;
        private float timerEnnemiHurt;
        bool hasShootTouchEnnemi = false;

        Ennemies ennemiesShoot = new Ennemies(0, 0, 0, 0, 0);

        public Texture2D Texture { get => _texture; set => _texture = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Speed { get => _speed; set => _speed = value; }
        public int Life { get => _life; set => _life = value; }
        public int Score { get => _score; set => _score = value; }
        public int Highscore { get => _highscore; set => _highscore = value; }

        public Player(int width, int height, int x, int y, int speed, int life)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;
            mouvementDirection = 1;
            Speed = speed;
            Life = life;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyPress = Keyboard.GetState();

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
            if (keyPress.IsKeyDown(Keys.Enter) && canShoot)
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

            if (keyPress.IsKeyUp(Keys.Enter))
            {
                canShoot = true;
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

            #region collision ennemies with player
            foreach (Ennemies ennemies in Globals.listEnnemies)
            {
                if (ennemies.GetEnnemieRectangle().Intersects(GetPlayerRectangle()))
                {
                    Life -= ennemies.Damage;
                    break;
                }
            }
            #endregion

            #region collision bullet with ennemies
            foreach (Shoot shoot in Globals.listShoots)
            {
                foreach (Ennemies ennemies in Globals.listEnnemies)
                {
                    if (shoot.GetShootRectangle().Intersects(ennemies.GetEnnemieRectangle()))
                    {
                        Globals.listShoots.Remove(shoot);
                        ennemies.Life -= shoot.Damage;
                        ennemiesShoot = ennemies;
                        hasShootTouchEnnemi = true;

                        if (ennemies.Life <= 0)
                        {
                            Globals.listEnnemies.Remove(ennemies);
                        }
                        break;
                    }
                }
                // Pour sortir du foreach sinon il y a une erreur car on supprime un élément de la liste que l'on parcourt
                if (hasShootTouchEnnemi)
                {
                    break;
                }
            }
            // Change la couleur de l'ennemi après avoir été touché
            if (hasShootTouchEnnemi)
            {
                timerEnnemiHurt += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timerEnnemiHurt <= 0.2f)
                {
                    ennemiesShoot.Color = Color.Red;
                }
                else if (timerEnnemiHurt >= 0.2f)
                {
                    ennemiesShoot.Color = Color.White;
                    hasShootTouchEnnemi = false;
                    timerEnnemiHurt = 0f;
                }
            }
            #endregion
        }
        
        public Rectangle GetPlayerRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, GetPlayerRectangle(), Color.White);
        }
    }
}
