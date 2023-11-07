///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : LevelUpCard.cs
//Utilité : Gestionnaire de Card à l'augmentation d'un level
///Projet : ForestSurvivor V1 (2023)
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

namespace ForestSurvivor.CardManager
{
    internal class LevelUpCard
    {
        CardCreationBase CardGenerator = new CardCreationBase();
        private float timerClick;
        private const float TIME_BEFORE_CHOOSE_CARD = 2;

        public LevelUpCard()
        {
            Globals.actualCards = new List<Card>();
            Globals.actualCards = GetLvlCard(3);
            timerClick = 0;
        }
        /// <summary>
        /// Recupere des cartes aléatoire différentes
        /// </summary>
        /// <param name="nbCard"></param>
        /// <returns></returns>
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
                Globals.actualCards[i - 1].X = Globals.ScreenWidth / 4.8f * i;
                Globals.actualCards[i - 1].TextX = Globals.ScreenWidth / 4.8f * i;
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


}
