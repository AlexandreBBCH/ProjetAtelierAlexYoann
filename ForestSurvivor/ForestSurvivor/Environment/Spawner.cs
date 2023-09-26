using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace ForestSurvivor.Environment
{
    internal class Spawner
    {


        private int _x;
        private int _y;
        private string _spawnerName;
        private Texture2D texture2D;
        private int _width;
        private int _height;
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public string SpawnerName { get => _spawnerName; set => _spawnerName = value; }
        public Texture2D Texture2D { get => texture2D; set => texture2D = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public Spawner(int x, int y, string spawnerName)
        {
            _x = x;
            _y = y;
            _spawnerName = spawnerName;
            SetSpawner(spawnerName);
            Globals.listEnvironment.Add(this);
        }

        public Rectangle GetSpawnerRectangle()
        {
            return new Rectangle(_x, _y, _width, _height);
        }
        public void UpdateSpawner(Player player)
        {
            IsCollided(player);
        }
        public bool IsCollided(Player player)
        {

            if (GetSpawnerRectangle().Intersects(player.GetPlayerRectangle()))
            {
                return true;
            }
            return false;
        }
        public void DrawEnvironment()
        {
            Globals.SpriteBatch.Draw(texture2D, GetSpawnerRectangle(), Color.White);

        }



        public void SetSpawner(string spawnerName)
        {

            switch (spawnerName)
            {
                case "Bush":
                    texture2D = GlobalsTexture.Bush;
                    Width = 100;
                    Height = 50;
                break;
                case "Rock":
                    texture2D = GlobalsTexture.Rock;
                    Width = 384/5;
                    Height = 240/5;
                    break;
                case "Tree":
                    texture2D = GlobalsTexture.Bush;
                    Width = 80;
                    Height = 50;
                    break;
            }


        }



        public void spawnEnemy(string level)
        {

        }
    }
}
