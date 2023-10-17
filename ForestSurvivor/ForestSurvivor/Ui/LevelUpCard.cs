using ForestSurvivor.AllGlobals;
using ForestSurvivor.AllItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ForestSurvivor.Ui
{
    internal class LevelUpCard
    {
        CardCreation CardGenerator = new CardCreation();
        private float timerClick;
        private const float TIME_BEFORE_CHOOSE_CARD = 2;

        public LevelUpCard()
        {
            Globals.actualCards = new List<Card>();
            Globals.actualCards = GetLvlCard(3);
            timerClick = 0;
        }

        public List<Card> GetLvlCard(int nbCard)
        {
            List<Card> lvlCards = new List<Card>();
            Random rdm = new Random();

            HashSet<Card> takenCards = new HashSet<Card>();

            for (int i = 0; i < nbCard; i++)
            {
                List<Card> availableCards = Globals.listCard.Except(takenCards).ToList();

                if (availableCards.Count == 0)
                {
                    break;
                }

                int randomIndex = rdm.Next(0, availableCards.Count);
                Card selectedCard = availableCards[randomIndex];
                lvlCards.Add(selectedCard);
                takenCards.Add(selectedCard);
            }

            return lvlCards;
        }


        public void DrawCards()
        {
            for (int i = 1; i <= Globals.actualCards.Count; i++)
            {
                Globals.actualCards[i-1].X = (Globals.ScreenWidth / 4.8f) * i;
                Globals.actualCards[i-1].TextX = ((Globals.ScreenWidth / 4.8f) * i);
                Globals.SpriteBatch.Draw(GlobalsTexture.cardInfos, new Rectangle((int)Globals.actualCards[i - 1].X, (int)Globals.actualCards[i - 1].Y, GlobalsTexture.cardView.Width * 2, GlobalsTexture.cardView.Height), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.lvlInfoFont, Globals.actualCards[i - 1].TextInfos, new Vector2(Globals.actualCards[i - 1].TextX, Globals.actualCards[i - 1].Y + 80), Color.White);
            }
        }

        public void UpdateCard(Player player, GameTime gameTime)
        {
            timerClick += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timerClick >= TIME_BEFORE_CHOOSE_CARD)
            {
                MouseState mouse = Mouse.GetState();
                foreach (var card in Globals.actualCards)
                {
                    card.UpdateCard(mouse, (int)card.X, (int)card.Y, GlobalsTexture.cardView.Width * 2, GlobalsTexture.cardView.Height * 2, player);
                }
            }
            else
            {
                for (int i = 0; i < Globals.actualCards.Count; i++)
                {
                    Globals.actualCards[i].Y += 4;
                }
            }
            
        }
    }

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
        public float X { get => _x; set => _x = value; }
        public float Y { get => _y; set => _y = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public string TextInfos { get => _textInfos; set => _textInfos = value; }
        public string BuffName { get => _buffName; set => _buffName = value; }
        public float TextX { get => _textX; set => _textX = value; }

        public Card(string textInfos, string buffName)
        {
            X = Globals.ScreenWidth / 5;
            TextX = Globals.ScreenWidth / 5 + 20;
            Y = -100;
            TextInfos = textInfos;
            BuffName = buffName;
            Globals.listCard.Add(this);
        }

        public void UpdateCard(MouseState mouseState, int x, int y, int width, int height,Player player)
        {
            IsClicked(mouseState, x, y, width, height);
            if (CardEffectEnabled) { ApplyCardEffect(player); } ;

        }

        public Rectangle GetRectangle(int X,int Y,int Width,int Height)
        {
            return new Rectangle(X,Y,Width,Height);
        }
        bool CardEffectEnabled = false;

        public bool IsClicked(MouseState mouseState,int x,int y,int width,int height)
        {
            if (GetRectangle(x,y,width,height).Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
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

    internal class CardCreation
    {
        public void CreateCard()
        {
            Card PvMaxCard = new Card("+PV MAX", "MaxPv");
            Card SpeedMaxCard = new Card("+SPEED MAX", "MaxSpeed");
            Card DamageMaxCard = new Card("+DAMAGE MAX", "MaxDamage");
            Card PvMaxDogCard = new Card("+DOG HEAL", "DogPv");
            Card SpeedMaxDogCard = new Card("+DOG SPEED MAX", "DogMaxSpeed");
            Card DamageMaxDogCard = new Card("+DOG DAMAGE MAX", "DogMaxDamage");
            Card NumberMaxDogCard = new Card("+1 DOG", "DogMaxNumber");
            Card RespawnDogCard = new Card("RESPAWN ALL DOG", "DogRespawn");
            Card ShootSpeedDogCard = new Card("+Dog Shoot speed", "DogShootingRate");

        }
    }
}
