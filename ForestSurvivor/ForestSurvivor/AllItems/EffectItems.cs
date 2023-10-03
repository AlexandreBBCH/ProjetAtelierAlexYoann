using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
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
        public string EffectName { get => _effectName; set => _effectName = value; }
        internal Player Player { get => _player; set => _player = value; }
        public float EffectTimer { get => _effectTimer; set => _effectTimer = value; }
        public bool IsEffectEnd { get => isEffectEnd; set => isEffectEnd = value; }

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
                    PvEffect();
                    break;
                case "HealMax":
                    PvEffect();
                    break;
                case "Speed":
                    SpeedEffect(gameTime);
                    break;
                case "SpeedMax":
                    SpeedEffect(gameTime);
                    break;
                case "Damage":
                    DamageEffect(gameTime);
                    break;
                case "DamageMax":
                    DamageEffect(gameTime);
                    break;
            }
        }


        public void PvEffect()
        {

            if (!isRunning)
            {
                isRunning = true;
                if (Player.Life + 3 <= Player.PvMax)
                {
                    Player.Life += 3;

                }
                else
                {
                    Player.Life = Player.PvMax;
                }
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
