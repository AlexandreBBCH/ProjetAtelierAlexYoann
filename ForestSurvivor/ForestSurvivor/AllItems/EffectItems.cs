using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestSurvivor.AllItems
{
    internal class EffectItems
    {
        Player _player;
        string _effectName;
        float _effectTimer;
        bool isRunning = false;
        bool isEffectEnd = false;
        Texture2D effectDisplay;
        public string EffectName { get => _effectName; set => _effectName = value; }
        internal Player Player { get => _player; set => _player = value; }
        public float EffectTimer { get => _effectTimer; set => _effectTimer = value; }
        public bool IsEffectEnd { get => isEffectEnd; set => isEffectEnd = value; }
        public Texture2D EffectDisplay { get => effectDisplay; set => effectDisplay = value; }

        public EffectItems(Player player, string effectName)
        {

            Player = player;
            _effectName = effectName;

            Globals.listEffect.Add(this);

        }


        
        public void UpdateEffect(GameTime gameTime)
        {

            switch (EffectName)
            {
                case "Heal":
                    PvEffect(gameTime);
                    effectDisplay = GlobalsTexture.effectHeal;
                    break;
                case "HealMax":
                    PvEffect(gameTime);
                    break;
                case "Speed":
                    SpeedEffect(gameTime);
                    effectDisplay = GlobalsTexture.effectSpeed;
                    break;
                case "SpeedMax":
                    SpeedEffect(gameTime);
                    break;
                case "Damage":
                    DamageEffect(gameTime);
                    effectDisplay = GlobalsTexture.effectDamage;
                    break;
                case "DamageMax":
                    DamageEffect(gameTime);
                    break;
            }
        }


        public void DisplayEffectItem()
        {
          Globals.SpriteBatch.Draw(EffectDisplay, new Rectangle((int)Player.X + 100, (int)Player.Y - 20, 44, 44), Color.White);
        }
        public void PvEffect(GameTime gameTime)
        {
            EffectTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!isRunning)
            {
                isRunning = true;
                if (Player.Life + 10 <= Player.PvMax)
                {
                    Player.Life += 10;
                    MusicManager.PlaySoundEffect(GlobalsSounds.appleEat);
                }
                else
                {
                    Player.Life = Player.PvMax;
                    MusicManager.PlaySoundEffect(GlobalsSounds.appleEat);

                }
            }
            if (EffectTimer >= 3)
            {
                IsEffectEnd = true;

            }
            //Globals.listEffect.Remove(this);

        }

        public void SpeedEffect(GameTime gameTime)
        {
            EffectTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!isRunning)
            {
                isRunning = true;
                Player.Speed += 3;
                MusicManager.PlaySoundEffect(GlobalsSounds.mushroomEat);
            }
            if (EffectTimer >= 3)
            {
                if (Player.Speed - 3 < Player.SpeedMax)
                {
                    Player.Speed = Player.SpeedMax;
                }
                else
                {
                    Player.Speed -= 3;
                }
                IsEffectEnd = true;

            }



        }
        public void DamageEffect(GameTime gameTime)
        {
            EffectTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!isRunning)
            {
                isRunning = true;
                Player.ActualDamage += 2;
                MusicManager.PlaySoundEffect(GlobalsSounds.steakEat);

            }
            if (EffectTimer >= 3)
            {
                if (Player.ActualDamage - 2 < Player.DamageMax)
                {
                    Player.ActualDamage = Player.DamageMax;
                }
                else
                {
                    Player.ActualDamage -= 2;
                }

                IsEffectEnd = true;

                //Globals.listEffect.Remove(this);
            }
        }



    }
}
