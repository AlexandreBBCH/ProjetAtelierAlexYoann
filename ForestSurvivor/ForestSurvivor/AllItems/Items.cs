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
        private float _globalSpeed;
        private float _speed;
        private int _globalHeal;
        private int _heal;
        private float _damage;
        private float _globalDamage;
        private EffectItems _effectBonus;

        public bool IsCollected { get => _isCollected; set => _isCollected = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public bool IsCollected1 { get => _isCollected; set => _isCollected = value; }
        public string ItemName { get => _itemName; set => _itemName = value; }
        public string BonusName { get => _bonusName; set => _bonusName = value; }
        public Texture2D Texture2D { get => texture2D; set => texture2D = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public float GlobalSpeed { get => _globalSpeed; set => _globalSpeed = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public int GlobalHeal { get => _globalHeal; set => _globalHeal = value; }
        public int Heal { get => _heal; set => _heal = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public float GlobalDamage { get => _globalDamage; set => _globalDamage = value; }
        //public Effect EffectBonus { get => _effectBonus; set => _effectBonus = value; }

        public Items(int x,int y, string itemName,Player player)
        {
            _x = x;
            _y = y;


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
            player.PvMax += GlobalHeal;
            player.SpeedMax += GlobalSpeed;
            player.DamageMax += GlobalDamage;

            AddEffect(player);
            return _isCollected = true;
            }
            return false;
        }

        public void AddEffect(Player player)
        {
            if (Speed > 0) new EffectItems(player, ItemName);
            if (Heal > 0) new EffectItems(player, ItemName);
            if (Damage > 0) new EffectItems(player, ItemName);
            if (player.Life + Heal >= player.PvMax) player.Life = player.PvMax;
        }


        public Rectangle GetItemRectangle()
        {
            return new Rectangle(_x, _y, _width, _height);
        }

        //Fonction qui sert juste a init les items de base sans intelligence
        public void SetItem(string itemName,Player player)
        {

            switch (itemName)
            {
                case "HealMax":
                    texture2D = GlobalsTexture.GreenApple;
                    Width = 35;
                    Height = 35;
                    GlobalHeal = 5;
                    Heal = 5;
                    break;
                case "Heal":
                    texture2D = GlobalsTexture.Apple;
                    Width = 35;
                    Height = 35;
                    Heal = 5;
                    break;
                case "SpeedMax":
                    texture2D = GlobalsTexture.Mushroom;
                    Width = 45;
                    Height = 45;
                    GlobalSpeed = 0.5f;
                    Speed = 0.2f;
                    break;
                case "Speed":
                    texture2D = GlobalsTexture.BrownMushroom;
                    Width = 45;
                    Height = 45;
                    Speed = 3f;
                    break;
                case "Damage":
                    texture2D = GlobalsTexture.Carrot;
                    Width = 45;
                    Height = 45;
                    Damage = 2f;
                    break;
                case "DamageMax":
                    texture2D = GlobalsTexture.Steak;
                    Width = 45;
                    Height = 45;
                    GlobalDamage = 1f;
                    Damage =1f;
                    break;
            }

    

        }


    }
}
