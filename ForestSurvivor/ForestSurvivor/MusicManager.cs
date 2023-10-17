using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestSurvivor
{
    internal class MusicManager
    {
        private List<Song> _allMusic;
        private int _idMusic;
        private const int NB_MUSIC = 5;
        private const int NB_DEATH_SOUND = 7;

        public List<Song> AllMusic { get => _allMusic; set => _allMusic = value; }
        public int IdMusic { get => _idMusic; set => _idMusic = value; }

        public MusicManager()
        {
            _allMusic = new List<Song>();
            _idMusic = 0;
        }

        public void LoadMusic(ContentManager contentManager)
        {
            for (int i = 1; i <= NB_MUSIC; i++)
            {
                Song song;
                song = contentManager.Load<Song>($"Music/music{i}");
                AllMusic.Add(song);
            }
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            MediaPlayer.Play(AllMusic[IdMusic]);
        }

        public void LoadAllSoundEffect(ContentManager contentManager)
        {
            GlobalsSounds.shootEffect = contentManager.Load<SoundEffect>("Music/shoot");

            GlobalsSounds.slimeMove = contentManager.Load<SoundEffect>("Music/slimeMove");
            GlobalsSounds.slimeDeath = contentManager.Load<SoundEffect>("Music/slimeDeath");
            GlobalsSounds.bigSlimeExplosion = contentManager.Load<SoundEffect>("Music/bigSlimeExplosion");

            GlobalsSounds.appleEat = contentManager.Load<SoundEffect>("Music/apple");
            GlobalsSounds.mushroomEat = contentManager.Load<SoundEffect>("Music/mushroomSound");
            GlobalsSounds.carotEat = contentManager.Load<SoundEffect>("Music/carotSound");
            GlobalsSounds.steakEat = contentManager.Load<SoundEffect>("Music/steakSound");

            for (int i = 1; i <= NB_DEATH_SOUND; i++)
            {
                SoundEffect soundDeath = contentManager.Load<SoundEffect>($"Music/soundHurt{i}");
                GlobalsSounds.listPlayerHurt.Add(soundDeath);
            }

            GlobalsSounds.dogHurt = contentManager.Load<SoundEffect>("Music/dogHurt");
            GlobalsSounds.dogDied = contentManager.Load<SoundEffect>("Music/dogDied");
        }

        public static void PlaySoundEffect(SoundEffect soundEffect)
        {
            soundEffect.Play(volume: GlobalsSounds.Sound / 100, pitch: 0, pan: 0);
        }

        public static void PlayRandomHurtEffect()
        {
            Random rnd = new Random();
            int rndSound = rnd.Next(0, GlobalsSounds.listPlayerHurt.Count);
            PlaySoundEffect(GlobalsSounds.listPlayerHurt[rndSound]);
        }

        private void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                if (AllMusic.ElementAtOrDefault(IdMusic + 1) != null)
                {
                    IdMusic++;
                }
                else
                {
                    IdMusic = 0;
                }
                MediaPlayer.Play(AllMusic[IdMusic]);
            }
        }

        public void Update()
        {
            MediaPlayer.Volume = GlobalsSounds.Musique / 100;
        }
    }
}
