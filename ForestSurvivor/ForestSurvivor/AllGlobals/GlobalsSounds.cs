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

        public static SoundEffect shootEffect;
        public static SoundEffect slimeMove;
        public static SoundEffect slimeDeath;
        public static SoundEffect bigSlimeExplosion;
        public static SoundEffect appleEat;
        public static SoundEffect mushroomEat;
        public static SoundEffect steakEat;
        public static SoundEffect carotEat;

        public static List<SoundEffect> listPlayerHurt = new List<SoundEffect>();

    }
}
