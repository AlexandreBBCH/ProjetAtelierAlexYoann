using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestSurvivor.AllGlobals
{
    internal class GlobalsTexture
    {
        /// <summary>
        /// Ui
        /// </summary>
        public static SpriteFont font;
        public static SpriteFont titleFont;
        public static SpriteFont textGamefont;
        public static Texture2D MainMenu2D;
        public static Texture2D PauseBackground2D;
        public static Texture2D Minus;
        public static Texture2D Plus;
        /// <summary>
        /// Monsters
        /// </summary>
        public static Texture2D Slime2D;
        public static Texture2D SlimeShooter2D;
        public static Texture2D SlimeShooterAmmo;
        public static Texture2D BigSlime2D;


        /// <summary>
        /// Items
        /// </summary>
        public static Texture2D Apple;
        public static Texture2D GreenApple;
        public static Texture2D Mushroom;
        public static Texture2D BrownMushroom;
        public static Texture2D Carrot;
        public static Texture2D Steak;
        /// <summary>
        /// Environment/Spawner
        /// </summary>
        public static Texture2D Bush;
        public static Texture2D Rock;
        public static Texture2D Background2D;
        /// <summary>
        /// Player
        /// </summary>
        public static List<Texture2D> listTexturesPlayer;
        public static Texture2D shootTexture;
    }
}
