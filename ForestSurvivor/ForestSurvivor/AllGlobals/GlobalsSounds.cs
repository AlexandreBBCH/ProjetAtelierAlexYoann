///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : GlobalsSound.cs
//Utilité : Stockage de global de audio  
///Projet : ForestSurvivor V1 (2023)
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestSurvivor.AllGlobals
{
    internal class GlobalsSounds
    {
        public static float Musique = 50f;

        public static float Sound = 50f;


        // Slime

        public static SoundEffect slimeMove;
        public static SoundEffect slimeDeath;
        public static SoundEffect bigSlimeExplosion;

        // Items

        public static SoundEffect appleEat;
        public static SoundEffect mushroomEat;
        public static SoundEffect steakEat;
        public static SoundEffect carotEat;

        // Dog

        public static SoundEffect dogHurt;
        public static SoundEffect dogDied;


        // Player

        public static List<SoundEffect> listPlayerHurt = new List<SoundEffect>();
        public static SoundEffect shootEffect;

    }
}
