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
using static System.Net.Mime.MediaTypeNames;

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
        OptionClickable _addMusique = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 2.2f, Globals.graphics.PreferredBackBufferHeight / 2.5f, 60, 60, "-", "RetireMusique", "Option", "Image", null, GlobalsTexture.Minus);
        OptionClickable _retireMusique = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 1.75f, Globals.graphics.PreferredBackBufferHeight / 2.5f, 60, 60, "+", "AddMusique", "Option", "Image", null, GlobalsTexture.Plus);
        OptionClickable _addSound = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 2.2f, Globals.graphics.PreferredBackBufferHeight / 2f, 60, 60, "-", "RetireSound", "Option", "Image", null, GlobalsTexture.Minus);
        OptionClickable _retireSound = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 1.75f, Globals.graphics.PreferredBackBufferHeight / 2f, 60, 60, "+", "AddSound", "Option", "Image", null, GlobalsTexture.Plus);


        OptionClickable _textResume = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 2.3f, Globals.graphics.PreferredBackBufferHeight / 1.5f, 200, 80, "Retour", "Resume", "Option", "Font", GlobalsTexture.titleFont, null);
        OptionClickable _textMainMenu = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 3f, Globals.graphics.PreferredBackBufferHeight / 1.2f, 650, 80, "Menu Principal", "BackMenu", "Option", "Font", GlobalsTexture.titleFont, null);
        public OptionPause()
        {
            _resumeState = 4;
            _isResume = false;

        }

        public bool IsResume { get => _isResume; set => _isResume = value; }

        public void OptionUpdate(GameTime gameTime, MouseState mouseState)
        {
            if (!_isResume && Globals.LauchGame)
            {
                Globals.ButtonEnabled = false;

            }
            else
            {
                Globals.ButtonEnabled = true;
            }

      

                if ((Keyboard.GetState().IsKeyDown(Keys.Escape) && !isOpen) || Globals.IsResume)  
            {
                Globals.ButtonEnabled = false;
                Globals.ButtonEnabledMain = false;
                Globals.IsResume = false;
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
            if ((Keyboard.GetState().IsKeyUp(Keys.Escape) && isOpen) ||Globals.Back)
            {
                isOpen = false;
            }


            if (compteur == 1 )
            {
                _resumeState = 4;
                _isResume = true;
            }
            if (compteur == 2 )
            {
                activeChrono = true;
                compteur = 3;
                _resumeState = 4;

            }
            if (Globals.Back)
            {
                activeChrono = true;
                compteur = 3;
                _resumeState = 4;
                _isResume = true;
                Globals.Back = false;
             
            }

            if (activeChrono )
            {
                Globals.ButtonEnabled = false;
                resumeStateTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (resumeStateTimer > 0.7)
                {
                    resumeStateTimer = 0f;
                    if (_resumeState <= 1f)
                    {
                        _isResume = false;
                        activeChrono = false;

                        resumeStateTimer = 0;
                        compteur = 0;
                        Globals.ButtonEnabledMain = true;
                        Globals.IsResume = false;
                    }
                    _resumeState--;

                }


            }
      
            foreach (var button in Globals.optionClickables)
            {
                if (button.IsClicked(mouseState) && Globals.ButtonEnabled)
                {
                
                        button.UseIt();
                 
                }
            }

       

        }

        public void DrawOption()
        {

            if (_isResume)
            {
                if (_resumeState >= 4 )
                {
                    Globals.SpriteBatch.Draw(GlobalsTexture.PauseBackground2D, new Rectangle(0, 0, Globals.graphics.PreferredBackBufferWidth, Globals.graphics.PreferredBackBufferHeight), Color.White);
                    Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, "Pause ", new Vector2(Globals.graphics.PreferredBackBufferWidth / 2.3f, Globals.graphics.PreferredBackBufferHeight / 5f), Color.White);
                    Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, "Musique ", new Vector2(Globals.graphics.PreferredBackBufferWidth / 4, Globals.graphics.PreferredBackBufferHeight/2.56f), Color.White);
                    Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, "Son ", new Vector2(Globals.graphics.PreferredBackBufferWidth / 4, Globals.graphics.PreferredBackBufferHeight / 2.06f), Color.White);
                    Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, Math.Round(GlobalsSounds.Musique).ToString(), new Vector2(Globals.graphics.PreferredBackBufferWidth / 2f , Globals.graphics.PreferredBackBufferHeight / 2.56f), Color.White);
                    Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, Math.Round(GlobalsSounds.Sound).ToString(), new Vector2(Globals.graphics.PreferredBackBufferWidth /2f, Globals.graphics.PreferredBackBufferHeight / 2.06f), Color.White);

                    foreach (var textClick in Globals.optionClickables)
                    {
                        if (textClick.Where == "Option")
                        {
                            if (!Globals.LauchGame && textClick.ButtonName != "BackMenu" )
                            {
                                textClick.DrawTextClickable();
                            }
                            else if(Globals.LauchGame )
                            {
                                textClick.DrawTextClickable();
                            }

                        }
                    }
                    //Globals.optionClickables.ForEach(textClick => textClick.DrawTextClickable());
                }
                if (_resumeState <= 1)
                {
                    Globals.ButtonEnabled = false;

                }


                if (_resumeState != 4 && Globals.LauchGame)
                {
                 Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, " " + _resumeState.ToString(), new Vector2(Globals.graphics.PreferredBackBufferWidth / 2,Globals.graphics.PreferredBackBufferHeight / 2), Color.White);
                }

            }


        }
    }
} 
    
        
    
