///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : MainMenu.cs
//Utilité : L'intéligence du main menu
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
using System.Diagnostics;

namespace ForestSurvivor.Ui
{
    public class MainMenu
    {
        OptionClickable _textLauchGame = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 2.3f, Globals.graphics.PreferredBackBufferHeight / 3f, 200, 80, "Play", "Start","MainMenu","Font" ,GlobalsTexture.titleFont,null);
        OptionClickable _textOption = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 2.3f, Globals.graphics.PreferredBackBufferHeight / 2.4f, 300, 80, "Option", "Option", "MainMenu","Font", GlobalsTexture.titleFont, null);
        OptionClickable _textQuitGame = new OptionClickable(Globals.graphics.PreferredBackBufferWidth / 2.3f, Globals.graphics.PreferredBackBufferHeight / 2f, 200, 80, "Quit", "Exit", "MainMenu","Font", GlobalsTexture.titleFont, null);
        private List<int> _dataBestGame;
        public List<int> DataBestGame { get => _dataBestGame; set => _dataBestGame = value; }

        public MainMenu()
        {
            // Récupère le niveau le plus haut atteint
            DataBestGame = Globals.Deserializer<List<int>>("bestscore.txt");
            if (DataBestGame == null)
            {
                DataBestGame = new List<int>() 
                {
                    0,0,0,0,0
                };
            }
        }


        public void DrawMainMenu() 
        {
            if (!Globals.LauchGame)
            {
                Globals.SpriteBatch.Draw(GlobalsTexture.Background2D, new Rectangle(0,0,Globals.graphics.PreferredBackBufferWidth, Globals.graphics.PreferredBackBufferHeight), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, "Survival Forest ", new Vector2(Globals.graphics.PreferredBackBufferWidth / 3,  Globals.graphics.PreferredBackBufferHeight / 5f), Color.White);
                // Draw best score
                Globals.SpriteBatch.DrawString(GlobalsTexture.textGamefont, "Best Score :", new Vector2(Globals.ScreenWidth / 1.45f, Globals.ScreenHeight / 3f + 20), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.textMenufont, $"Best level : {DataBestGame[0]}", new Vector2(Globals.ScreenWidth / 1.45f, Globals.ScreenHeight / 3f + 120), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.textMenufont, $"Slime killed : {DataBestGame[1]}", new Vector2(Globals.ScreenWidth / 1.45f, Globals.ScreenHeight / 3f + 170), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.textMenufont, $"Big slime killed : {DataBestGame[2]}", new Vector2(Globals.ScreenWidth / 1.45f, Globals.ScreenHeight / 3f + 220), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.textMenufont, $"Slime shooter killed : {DataBestGame[3]}", new Vector2(Globals.ScreenWidth / 1.45f, Globals.ScreenHeight / 3f + 270), Color.White);
                Globals.SpriteBatch.DrawString(GlobalsTexture.textMenufont, $"Shoot sucessfull rate : {DataBestGame[4]}%", new Vector2(Globals.ScreenWidth / 1.45f, Globals.ScreenHeight / 3f + 320), Color.White);
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

            foreach (var button in Globals.optionClickables)
            {
                if (button.IsClicked(mouseState))
                {
                    if (!Globals.LauchGame && button.Where == "MainMenu"  && Globals.ButtonEnabledMain)
                    {
                        button.UseIt();
                    }
                    else if ( button.Where == "Option" && Globals.ButtonEnabled)
                    {
                        button.UseIt();
                    }
     
                }
            }


        }
    }
}
