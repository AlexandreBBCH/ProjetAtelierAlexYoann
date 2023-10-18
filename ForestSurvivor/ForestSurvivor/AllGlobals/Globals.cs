///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : Globals.cs
//Utilité : Stockage de global de logique  
///Projet : ForestSurvivor V1 (2023)

using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllItems;
using ForestSurvivor.Environment;
using ForestSurvivor.CardManager;
using ForestSurvivor.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using ForestSurvivor.Animals;

namespace ForestSurvivor.AllGlobals
{
    internal class Globals
    {
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDeviceManager graphics;

        public static bool LauchGame = false;
        public static bool IsResume = false;
        public static bool Back = false;
        public static bool Exit = false;
        public static bool ButtonEnabled = true;
        public static bool ButtonEnabledMain = true;
        public static List<OptionClickable> optionClickables = new List<OptionClickable>();
        public static List<Slider> allSliders = new List<Slider>();
        public static bool GameOver = false;
        public static bool Restart = false;
        public static bool LevelTime = false;
        public static bool Pause = false;
        public static bool LevelUpPause = false;



        public static List<Shoot> listShoots = new List<Shoot>();
        public static List<Ennemies> listLittleSlime = new List<Ennemies>();
        public static List<SlimeShooter> listShootSlime = new List<SlimeShooter>();
        public static List<BigSlime> listBigSlime = new List<BigSlime>();
        public static List<Items> listItems = new List<Items>();
        public static List<EffectItems> listEffect = new List<EffectItems>();
        public static List<Dog> listDogs = new List<Dog>();


        public static List<Spawner> listEnvironment = new List<Spawner>();
        public static List<Card> listCard = new List<Card>();
        public static List<Card> actualCards = new List<Card>();

        public static LevelUpCard levelUpCard;


        public static int WIDTH_LITTLE_SLIME = 69;
        public static int HEIGHT_LITTLE_SLIME = 47;
        public static int SPEED_LITTLE_SLIME = 5;
        public static int DAMAGE_LITTLE_SLIME = 1;
        public static float DAMAGE_SPEED_LITTLE_SLIME = 1;
        public static int LIFE_LITTLE_SLIME = 3;

        public static int nbSlimeKilled = 0;
        public static int nbBigSlimeKilled = 0;
        public static int nbShooterSlimeKilled = 0;
        public static int nbShoot = 0;
        public static int nbShootHasTouch = 0;


        public static int ScreenWidth;
        public static int ScreenHeight;


        /// <summary>
        /// Reset toutes les globals utiles pour relancer la partie
        /// </summary>
        public static void ResetGlobals()
        {
            LauchGame = true;
            IsResume = false;
            Back = false;
            Exit = false;
            ButtonEnabled = true;
            ButtonEnabledMain = true;
            optionClickables = new List<OptionClickable>();
            allSliders = new List<Slider>();
            GameOver = false;
            Restart = false;
            LevelTime = false;
            Pause = false;
            LevelUpPause = false;

            nbSlimeKilled = 0;
            nbBigSlimeKilled = 0;
            nbShooterSlimeKilled = 0;
            nbShoot = 0;
            nbShootHasTouch = 0;

            listShoots = new List<Shoot>();
            listLittleSlime = new List<Ennemies>();
            listShootSlime = new List<SlimeShooter>();
            listBigSlime = new List<BigSlime>();
            listItems = new List<Items>();
            listEffect = new List<EffectItems>();
            listDogs = new List<Dog>();


            listEnvironment = new List<Spawner>();
            listCard = new List<Card>();
            actualCards = new List<Card>();
        }


        /// <summary>
        /// Serialize n'importe quel type de donnée
        /// </summary>
        /// <typeparam name="T">Type des données</typeparam>
        /// <param name="nameFile">nom du fichier où stocker les données</param>
        /// <param name="data">données à sauvegarder</param>
        public static void Serializer<T>(string nameFile, T data)
        {
            XmlSerializer serializer = new XmlSerializer(data.GetType());
            StreamWriter writer = new StreamWriter(nameFile);
            serializer.Serialize(writer, data);
            writer.Close();
        }

        /// <summary>
        /// Récupère des données avec le type spécifiée
        /// </summary>
        /// <typeparam name="T">Type de donnée à récupérée</typeparam>
        /// <param name="nameFile">nom du fichier où sont stocké les données</param>
        /// <returns>Les données récupérés</returns>
        public static T Deserializer<T>(string nameFile)
        {
            if (File.Exists(nameFile))
            {
                StreamReader reader = new StreamReader(nameFile);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T data = (T)serializer.Deserialize(reader);
                return data;
            }

            // Si le fichier n'existe pas, retournez la valeur par défaut du type T
            return default;
        }
    }
}
