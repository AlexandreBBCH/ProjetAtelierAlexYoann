using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForestSurvivor.AllGlobals;

namespace ForestSurvivor
{
    internal class Shoot
    {
        private Texture2D _texture;
        private int _width;
        private int _height;
        private int _x;
        private int _y;
        private int _speed;
        private int _direction;
        private int _damage;
        private bool _destroy;

        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Speed { get => _speed; set => _speed = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public int Direction { get => _direction; set => _direction = value; }
        public bool Destroy { get => _destroy; set => _destroy = value; }
        public int Damage { get => _damage; set => _damage = value; }

        public Shoot(int x, int y, int direction)
        {
            _x = x;
            _y = y;
            _direction = direction;
            _width = 40;
            _height = 10;
            _speed = 20;
            _damage = 1;
            _destroy = false;
            Texture = GlobalsTexture.shootTexture;
        }

        public void Update()
        {
            if (Direction == 1) 
            {
                if (Y - Speed >= 0)
                {
                    Y -= Speed;
                }
                else
                {
                    Destroy = true;
                }
            }
            if (Direction == 2)
            {
                if ((Y - Speed) >= 0 && (X + Speed + Width) <= Globals.ScreenWidth)
                {
                    Y -= Speed;
                    X += Speed;
                }
                else
                {
                    Destroy = true;
                }
            }
            if (Direction == 3)
            {
                if (X + Speed <= Globals.ScreenWidth - Width)
                {
                    X += Speed;
                }
                else
                {
                    Destroy = true;
                }
            }
            if (Direction == 4)
            {
                if ((Y + Speed + Height) <= Globals.ScreenHeight && (X + Speed + Width) <= Globals.ScreenWidth)
                {
                    Y += Speed;
                    X += Speed;
                }
                else
                {
                    Destroy = true;
                }
            }
            if (Direction == 5)
            {
                if ((Y + Speed + Height) <= Globals.ScreenHeight)
                {
                    Y += Speed;
                }
                else
                {
                    Destroy = true;
                }
            }
            if (Direction == 6)
            {
                if ((Y + Speed + Height) <= Globals.ScreenHeight && (X - Speed) >= 0)
                {
                    Y += Speed;
                    X -= Speed;
                }
                else
                {
                    Destroy = true;
                }
            }
            if (Direction == 7)
            {
                if (X - Speed >= 0)
                {
                    X -= Speed;
                }
                else
                {
                    Destroy = true;
                }
            }
            if (Direction == 8)
            {
                if ((X - Speed) >= 0 && (Y - Speed) >= 0)
                {
                    X -= Speed;
                    Y -= Speed;
                }
                else
                {
                    Destroy = true;
                }
            }
        }

        public Rectangle GetShootRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, new Rectangle(X, Y, Width, Height), Color.White);
        }
    }
}
