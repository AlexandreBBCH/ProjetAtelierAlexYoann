///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : MusicManager.cs
//Utilité : Le manager de musique
///Projet : ForestSurvivor V1 (2023)
using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestSurvivor.SongManager
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

        /// <summary>
        /// Charge toutes les musiques du jeu
        /// </summary>
        /// <param name="contentManager"></param>
        public void LoadMusic(ContentManager contentManager)
        {
            for (int i = 1; i <= NB_MUSIC; i++)
            {
                Song song;
                song = contentManager.Load<Song>($"Music/music{i}");
                AllMusic.Add(song);
            }
            // Ajoute l'évenement pour changer de musique
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            // Lance la première musique
            MediaPlayer.Play(AllMusic[IdMusic]);
        }

        /// <summary>
        /// Charge tous les effets sonores
        /// </summary>
        /// <param name="contentManager"></param>
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

        /// <summary>
        /// Lance un effet sonore
        /// </summary>
        /// <param name="soundEffect">l'effet sonore</param>
        public static void PlaySoundEffect(SoundEffect soundEffect)
        {
            soundEffect.Play(volume: GlobalsSounds.Sound / 100, pitch: 0, pan: 0);
        }

        /// <summary>
        /// Lance un son aléatoire de dégât du joueur
        /// </summary>
        public static void PlayRandomHurtEffect()
        {
            Random rnd = new Random();
            int rndSound = rnd.Next(0, GlobalsSounds.listPlayerHurt.Count);
            PlaySoundEffect(GlobalsSounds.listPlayerHurt[rndSound]);
        }

        /// <summary>
        /// Evenement pour passer à la musique suivant lorsqu'une musique se finit
        /// </summary>
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

        /// <summary>
        /// Met à jour le son qui a peut être été modifié
        /// </summary>
        public void Update()
        {
            MediaPlayer.Volume = GlobalsSounds.Musique / 100;
        }
    }
}
