using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using ForestSurvivor.AllGlobals;

namespace ForestSurvivor.Ui
{
    public class OptionPause
    {


        bool isOpen = false;
        float resumeStateTimer;
        bool activeChrono = false;
        int compteur = 0;
        int _resumeState;
        bool _isResume;
        //OptionClickable _textLauchGame = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 3 + 250, 400, 200, 80, GlobalsTexture.titleFont, "Play", "Start","Option");
        OptionClickable _textQuitGame = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 3 + 250, 800, 200, 80, GlobalsTexture.titleFont, "Quit", "Exit", "Option");
        Slider _sldMusique = new Slider(Globals.graphics.PreferredBackBufferWidth / 4 + 400, 450, 500,70);
        Slider _sldSound = new Slider(Globals.graphics.PreferredBackBufferWidth / 4 + 400, 550, 500, 70);
        public OptionPause()
        {
            _resumeState = 4;
            _isResume = false;

        }

        public bool IsResume { get => _isResume; set => _isResume = value; }

        public void OptionUpdate(GameTime gameTime, MouseState mouseState)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !isOpen)

            {
                if (compteur == 0)
                {
                    compteur++;
                    isOpen = true;

                }
                else if (compteur == 1)
                {
                    compteur++;
                }


            }
            if (Keyboard.GetState().IsKeyUp(Keys.Escape) && isOpen)
            {
                isOpen = false;
            }

            if (compteur == 1)
            {
                _resumeState = 4;
                _isResume = true;
            }
            if (compteur == 2)
            {
                activeChrono = true;
                compteur = 3;
                _resumeState = 4;

            }

            if (activeChrono)
            {
                resumeStateTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (resumeStateTimer > 1)
                {
                    resumeStateTimer = 0f;
                    if (_resumeState <= 1)
                    {
                        _isResume = false;
                        activeChrono = false;

                        resumeStateTimer = 0;
                        compteur = 0;
                    }
                    _resumeState--;

                }


            }
            //if (_textLauchGame.IsClicked(mouseState)) _textLauchGame.UseIt();
            foreach (var button in Globals.optionClickables)
            {
                if (button.IsClicked(mouseState))
                {
                    button.UseIt();
                }
            }

            if (_sldMusique.IsClicked(mouseState))
            {
                if (_sldMusique.XButton<_sldMusique.X + 450)
                {
                    _sldMusique.XButton = mouseState.X;
                }
             
                GlobalsSounds.Musique++;
            }
            if (_sldSound.IsClicked(mouseState))
            {
                if (_sldSound.XButton < _sldSound.X + 450)
                {
                    _sldSound.XButton = mouseState.X;
                }

                GlobalsSounds.Sound++;
            }
        }

        public void DrawOption()
        {
            if (_isResume)
            {
                if (_resumeState >= 4)
                {
                 Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, "Pause ", new Vector2(Globals.graphics.PreferredBackBufferWidth / 3 + 250,100), Color.White);
                    Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, "Musique ", new Vector2(Globals.graphics.PreferredBackBufferWidth / 4, 450), Color.White);
                    Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, "Son ", new Vector2(Globals.graphics.PreferredBackBufferWidth / 4, 550), Color.White);
                    _sldMusique.DrawSlider();
                    _sldSound.DrawSlider();
                    foreach (var textClick in Globals.optionClickables)
                    {
                        if (textClick.Where == "Option")
                        {
                            textClick.DrawTextClickable();

                        }
                    }
                    //Globals.optionClickables.ForEach(textClick => textClick.DrawTextClickable());
                }
                if (_resumeState != 4)
                {
                 Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, " " + _resumeState.ToString(), new Vector2(Globals.graphics.PreferredBackBufferWidth / 2,Globals.graphics.PreferredBackBufferHeight / 2), Color.White);
                }
                
            }


        }
    }
} 
    
        
    
