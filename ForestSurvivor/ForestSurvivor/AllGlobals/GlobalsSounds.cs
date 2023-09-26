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
        public static float Musique = 20f;

        public static float Sound = 20f;

        public static SoundEffect shootEffect;
        public static SoundEffect slimeMove;
        public static SoundEffect slimeDeath;
        public static SoundEffect appleEat;
        public static List<SoundEffect> listPlayerHurt = new List<SoundEffect>();

    }
}
