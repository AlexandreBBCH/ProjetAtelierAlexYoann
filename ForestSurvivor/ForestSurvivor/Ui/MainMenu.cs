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
    public class MainMenu
    {
        OptionClickable _textLauchGame = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 3 + 250, 500, 200, 80, GlobalsTexture.titleFont, "Play", "Start","MainMenu");
        OptionClickable _textOption = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 3 + 250, 700, 300, 80, GlobalsTexture.titleFont, "Option", "Option", "/*MainMenu*/");
        OptionClickable _textQuitGame = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 3 + 250, 900, 200, 80, GlobalsTexture.titleFont, "Quit", "Exit", "MainMenu");



        public MainMenu()
        {

        }
        public void DrawMainMenu() 
        {

            if (!Globals.LauchGame)
            {

      
            Globals.SpriteBatch.Draw(GlobalsTexture.MainMenu2D, new Rectangle(0,0,Globals.graphics.PreferredBackBufferWidth, Globals.graphics.PreferredBackBufferHeight), Color.White);
            Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, "Survival Forest ", new Vector2(Globals.graphics.PreferredBackBufferWidth / 3, 100), Color.White);

                foreach (var textClick in Globals.optionClickables)
                {
                    if (textClick.Where == "MainMenu")
                    {
                        textClick.DrawTextClickable();

                    }
                }

            }
        }

        public void UpdateMainMenu(GameTime gameTime,MouseState mouseState)
        {

            //_message = mouseState.X.ToString() + " " + mouseState.Y;
            foreach (var button in Globals.optionClickables)
            {
                if (button.IsClicked(mouseState))
                {
                    button.UseIt();
                }
            }


        }
    }
}
