///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : Card.cs
//Utilité : Le moule des cards
///Projet : ForestSurvivor V1 (2023)
using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllGlobals;
using ForestSurvivor.AllItems;
using ForestSurvivor.Animals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ForestSurvivor.CardManager
{
    internal class Card
    {


        private float _x;
        private float _textX;
        private float _y;
        private int _width;
        private int _height;
        private string _textInfos;
        private string _buffName;
        private bool wasLeftButtonPressedLastFrame = false;
        private bool CardEffectEnabled = false;

        public float X { get => _x; set => _x = value; }
        public float Y { get => _y; set => _y = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public string TextInfos { get => _textInfos; set => _textInfos = value; }
        public string BuffName { get => _buffName; set => _buffName = value; }
        public float TextX { get => _textX; set => _textX = value; }
        public bool CardEffectEnabled1 { get => CardEffectEnabled; set => CardEffectEnabled = value; }

        public Card(string textInfos, string buffName)
        {
            X = Globals.ScreenWidth / 5;
            TextX = Globals.ScreenWidth / 5 + 20;
            Y = -100;
            TextInfos = textInfos;
            BuffName = buffName;
            Globals.listCard.Add(this);
        }

        public void UpdateCard(MouseState mouseState, int x, int y, int width, int height, Player player)
        {
            IsClicked(mouseState, x, y, width, height);
            if (CardEffectEnabled) { ApplyCardEffect(player); };

        }

        public Rectangle GetRectangle(int X, int Y, int Width, int Height)
        {
            return new Rectangle(X, Y, Width, Height);
        }

        /// <summary>
        /// Verifie si cliquer 
        /// </summary>
        /// <param name="mouseState"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public bool IsClicked(MouseState mouseState, int x, int y, int width, int height)
        {
            if (GetRectangle(x, y, width, height).Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!wasLeftButtonPressedLastFrame)
                {
                    wasLeftButtonPressedLastFrame = true;
                    CardEffectEnabled = true;
                    Globals.LevelUpPause = false;

                    // Reset le Y de toutes les cartes
                    foreach (Card card in Globals.listCard)
                    {
                        card.Y = -100;
                    }

                    return true;
                }
            }
            else
            {
                wasLeftButtonPressedLastFrame = false;
            }
            return false;
        }

        /// <summary>
        /// Attribue les effet en fonction du nom de litem
        /// </summary>
        /// <param name="player"></param>
        public void ApplyCardEffect(Player player)
        {
            CardEffectEnabled = false;
            switch (BuffName)
            {
                case "MaxPv":
                    player.PvMax += 5;
                    player.Life += 5;
                    Globals.levelUpCard = null;
                    break;
                case "MaxSpeed":
                    player.SpeedMax += 0.5f;
                    player.Speed += 0.5f;
                    Globals.levelUpCard = null;
                    break;
                case "MaxDamage":
                    player.DamageMax += 1f;
                    player.ActualDamage += 1f;
                    Globals.levelUpCard = null;
                    break;
                case "DogMaxSpeed":
                    foreach (Dog dog in Globals.listDogs)
                    {
                        dog.Speed += 0.5f;
                    }
                    Globals.levelUpCard = null;
                    break;
                case "DogMaxDamage":
                    foreach (Dog dog in Globals.listDogs)
                    {
                        dog.Damage += 1f;
                    }
                    Globals.levelUpCard = null;
                    break;
                case "DogPv":
                    foreach (Dog dog in Globals.listDogs)
                    {
                        if (!dog.isDead)
                        {
                            dog.Life = 10;
                        }
                    }
                    Globals.levelUpCard = null;
                    break;
                case "DogRespawn":
                    foreach (Dog dog in Globals.listDogs)
                    {
                        if (dog.isDead)
                        {
                            dog.isDead = false;
                            dog.IsHurt = false;
                            dog.Life = 10;
                            dog.ennemiesTarget = null;
                            dog.bigSlimeTarget = null;
                            dog.shooterTarget = null;
                            dog.itemTarget = null;
                            dog.hasTarget = false;
                        }
                    }
                    Globals.levelUpCard = null;
                    break;
                case "DogMaxNumber":
                    SpawnManager.CreateDog(player);
                    Globals.levelUpCard = null;
                    break;
                case "DogShootingRate":
                    foreach (Dog dog in Globals.listDogs)
                    {
                        if (dog.DamageSpeed > 0.2f)
                        {
                            dog.DamageSpeed -= 0.1f;
                        }
                    }
                    Globals.levelUpCard = null;
                    break;
            }



        }
    }

}

