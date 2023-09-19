using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ForestSurvivor.AllItems
{
    internal class Items
    {
        private int _x;
        private int _y;
        private bool _isCollected;
        private string _itemName;
        private string _bonusName;
        private Texture2D texture2D;
        private int _width;
        private int _height;
        public bool IsCollected { get => _isCollected; set => _isCollected = value; }

        public Items(int x,int y, string itemName,string bonusName,Player player)
        {
            _x = x;
            _y = y;

     
            _bonusName = bonusName;
            _itemName = itemName;
            SetItem(_itemName, player);

            Globals.listItems.Add(this);
        }


        public void DrawItems()
        {
            Globals.SpriteBatch.Draw(texture2D, GetItemRectangle(), Color.White);

        }

        public void UpdateItems(Player player)
        {
            IsCollided(player);
        }


        public bool IsCollided(Player player)
        {

            if (GetItemRectangle().Intersects(player.GetPlayerRectangle()))
            {
             return _isCollected = true;
            }
            return false;
        }

        public Rectangle GetItemRectangle()
        {
            return new Rectangle(_x, _y, _width, _height);
        }

        //Fonction qui sert juste a init les items de base sans intelligence
        public void SetItem(string itemName,Player player)
        {
            if (itemName == "Apple")//pv
            {
                texture2D = GlobalsTexture.Apple;
                _width = 35;
                _height = 35;
                player.Life += 50;

            }
      
        }


    }
}
