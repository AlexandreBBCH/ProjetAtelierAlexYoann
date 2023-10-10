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
       

        public LevelUpCard()
        {
            Globals.actualCards = new List<Card>();
            CardGenerator.CreateCard();
            Globals.actualCards = GetLvlCard(3);
        }

        public List<Card> GetLvlCard(int nbCard)
        {
            List<Card> lvlCards = new List<Card>();
            Random rdm = new Random();

            List<Card> takenCards = new List<Card>();

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
                Globals.SpriteBatch.Draw(GlobalsTexture.cardView, Globals.actualCards[i - 1].GetRectangle((int)Globals.actualCards[i - 1].X, (int)(Globals.actualCards[i - 1].Y + Globals.ScreenHeight / 9.5f),(int) GlobalsTexture.cardView.Width * 2, (int)GlobalsTexture.cardView.Height * 2), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.lvlInfoFont, Globals.actualCards[i - 1].TextInfos, new Vector2(Globals.actualCards[i - 1].TextX, Globals.actualCards[i - 1].Y + Globals.actualCards[i - 1].Y / 3), Color.White);
            }
        }

        public void UpdateCard(Player player)
        {
            MouseState mouse = Mouse.GetState();
            foreach (var card in Globals.actualCards)
            {
                card.UpdateCard(mouse,(int)card.X, (int)card.Y, GlobalsTexture.cardView.Width * 2, GlobalsTexture.cardView.Height * 2,player);    
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
            TextX = Globals.ScreenWidth / 5;
            Y = Globals.ScreenHeight / 4.5f;
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
            Debug.Print(player.PvMax.ToString());
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
                    player.DamageMax += 1f;
                    player.ActualDamage += 1f;
                    Globals.levelUpCard = null;
                    break;
                case "DogMaxDamage":
                    player.DamageMax += 1f;
                    player.ActualDamage += 1f;
                    Globals.levelUpCard = null;
                    break;
                case "DogMaxNumber":
                    player.DamageMax += 1f;
                    player.ActualDamage += 1f;
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
            Card PvMaxDogCard = new Card("+DOG PV MAX", "DogMaxPv");
            Card SpeedMaxDogCard = new Card("+DOG SPEED MAX", "DogMaxSpeed");
            Card DamageMaxDogCard = new Card("+DOG DAMAGE MAX", "DogMaxDamage");
            Card NumberMaxDogCard = new Card("+1 DOG", "DogMaxNumber");
        }
    }
}
