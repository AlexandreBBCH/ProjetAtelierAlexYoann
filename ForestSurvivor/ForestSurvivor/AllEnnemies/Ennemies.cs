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
        private int _life;
        private int _speed;
        private int _damage;
        private bool _isDead;

        private Color _color;

        private Random rnd;

        public Texture2D Texture { get => texture; set => texture = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public int Life { get => _life; set => _life = value; }
        public int Speed { get => _speed; set => _speed = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public bool IsDead { get => _isDead; set => _isDead = value; }
        public Color Color { get => _color; set => _color = value; }

        public Ennemies(int width, int height, int life, int speed, int damage)
        {
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
                    X = Globals.ScreenWidth;
                    Y = rnd.Next(0, Globals.ScreenHeight / 2);
                    Y *= 2;
                    break;
                case 4:
                    X = rnd.Next(0, Globals.ScreenWidth / 2);
                    X *= 2;
                    Y = Globals.ScreenHeight;
                    break;
            }

            Width = width;
            Height = height;
            Life = life;
            Speed = speed;
            Damage = damage;
            Color = Color.White;
            IsDead = false;
        }

        public void Update(Player player)
        {
            bool moreXcollided = false;
            bool lessXcollided = false;
            bool moreYcollided = false;
            bool lessYcollided = false;

            foreach (Ennemies ennemies in Globals.listEnnemies)
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
