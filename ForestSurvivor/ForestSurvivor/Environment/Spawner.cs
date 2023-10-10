using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Numerics;
using TutoYoutube;
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
        private bool _isAnimated = false;
        private SpriteSheetAnimation _animationSheet;
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public string SpawnerName { get => _spawnerName; set => _spawnerName = value; }
        public Texture2D Texture2D { get => texture2D; set => texture2D = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public bool IsAnimated { get => _isAnimated; set => _isAnimated = value; }
        internal SpriteSheetAnimation AnimationSheet { get => _animationSheet; set => _animationSheet = value; }

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
        public void UpdateSpawner(GameTime gameTime,Player player)
        {
            IsCollided(player);
            if (IsAnimated)
            {
                AnimationSheet.UpdateAnimation(gameTime);
            }
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
            if (IsAnimated)
            {
                AnimationSheet.PositionX = X;
                AnimationSheet.PositionY = Y;
                AnimationSheet.DrawAnimation();

            }
            else
            {
                Globals.SpriteBatch.Draw(texture2D, GetSpawnerRectangle(), Color.White);

            }

        }



        public void SetSpawner(string spawnerName)
        {

            switch (spawnerName)
            {
                case "Bush":
                    IsAnimated = true;
                    texture2D = GlobalsTexture.BushSpriteSheet;
                    Width = 100;
                    Height = 50;
                    AnimationSheet = new SpriteSheetAnimation(texture2D, 1, 3, 0.5f, true, 0.1f,21);
                    break;
                case "Rock":
                    texture2D = GlobalsTexture.Rock;
                    Width = 384/5;
                    Height = 240/5;
                    break;
                case "BushBerrie":
                    IsAnimated = true;
                    texture2D = GlobalsTexture.BushBerriesSpriteSheet;
                    Width = 120;
                    Height = 50;
                    AnimationSheet = new SpriteSheetAnimation(texture2D, 1, 3, 0.5f, true, 0.1f, 21);
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
