using ForestSurvivor.AllEnnemies;
using ForestSurvivor.AllItems;
using ForestSurvivor.Ui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ForestSurvivor.AllGlobals
{
    internal class Globals
    {
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDeviceManager graphics;

        public static StreamReader reader;
        public static StreamWriter writer;
        public static XmlSerializer serializer;
        public static bool LauchGame = false;
        public static bool IsResume = false;
        public static bool Back = false;
        public static bool Exit = false;
        public static bool ButtonEnabled = true;
        public static bool ButtonEnabledMain = true;
        public static List<OptionClickable> optionClickables = new List<OptionClickable>();
        public static List<Slider> allSliders = new List<Slider>();



        public static List<Shoot> listShoots = new List<Shoot>();
        public static List<Ennemies> listEnnemies = new List<Ennemies>();
        public static List<Items> listItems = new List<Items>();



        public static int ScreenWidth;
        public static int ScreenHeight;

    }
}
