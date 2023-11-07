///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : LevelMenu.cs
//Utilité : Le visuel du Menu principal
///Projet : ForestSurvivor V1 (2023)
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
    internal class LevelMenu
    {
        OptionClickable _CardView = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 2.3f, Globals.graphics.PreferredBackBufferHeight / 3f, 200, 80, "Play", "Start", "Level", "Font", GlobalsTexture.titleFont, null);
        OptionClickable _textOption = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 2.3f, Globals.graphics.PreferredBackBufferHeight / 2.4f, 300, 80, "Option", "Option", "Level", "Font", GlobalsTexture.titleFont, null);
        OptionClickable _textQuitGame = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 2.3f, Globals.graphics.PreferredBackBufferHeight / 2f, 200, 80, "Quit", "Exit", "Level", "Font", GlobalsTexture.titleFont, null);
        public void DrawMainMenu()
        {

            if (!Globals.LauchGame)
            {
                Globals.SpriteBatch.Draw(GlobalsTexture.Background2D, new Rectangle(0, 0, Globals.graphics.PreferredBackBufferWidth, Globals.graphics.PreferredBackBufferHeight), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, "Survival Forest ", new Vector2(Globals.graphics.PreferredBackBufferWidth / 3, Globals.graphics.PreferredBackBufferHeight / 5f), Color.White);

                foreach (var textClick in Globals.optionClickables)
                {
                    if (textClick.Where == "MainMenu")
                    {
                        textClick.DrawTextClickable();

                    }
                }

            }
        }

        public void UpdateMainMenu(GameTime gameTime, MouseState mouseState)
        {

            foreach (var button in Globals.optionClickables)
            {
                if (button.IsClicked(mouseState))
                {
                    if (!Globals.LauchGame && button.Where == "MainMenu" && Globals.ButtonEnabledMain)
                    {
                        button.UseIt();
                    }
                    else if (button.Where == "Option" && Globals.ButtonEnabled)
                    {
                        button.UseIt();
                    }

                }
            }


        }
    }
}
