///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : CardCreatioBase.cs
//Utilité : C'est ici que l'ont rajoute des card dans la liste général
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
    internal class CardCreationBase
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