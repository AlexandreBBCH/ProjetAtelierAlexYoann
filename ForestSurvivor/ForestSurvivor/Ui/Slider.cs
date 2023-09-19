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
    internal class Slider
    {
        private int _x;
        private int _y;
        private int _xButton;
        private int _yButton;
        private int _width;
        private int _height;
        private bool wasLeftButtonPressedLastFrame;
        private string _buttonName;
        private string _name;
        bool isPress;
        public Slider(int x, int y, int width, int height,string name) 
        {
            _x = x;
            _xButton = x;
            _y = y;
            _yButton = y;
            _width = width;
            _height = height;
            _name = name;
            Globals.allSliders.Add(this);

        }

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int XButton { get => _xButton; set => _xButton = value; }
        public int YButton { get => _yButton; set => _yButton = value; }
        public string Name { get => _name; set => _name = value; }

        public void DrawSlider()
        {
            Globals.SpriteBatch.Draw(GlobalsTexture.MainMenu2D, new Rectangle(_x,_y,_width,_height), Color.Wheat);
            Globals.SpriteBatch.Draw(GlobalsTexture.Slime2D, GetRectangle(), Color.Wheat);

            Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, Math.Round(GlobalsSounds.Musique).ToString(),new Vector2( Globals.graphics.PreferredBackBufferWidth / 4 + 950, 440), Color.White);
            Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, Math.Round(GlobalsSounds.Sound).ToString(), new Vector2(Globals.graphics.PreferredBackBufferWidth / 4 + 950, 540), Color.White);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(_xButton-20, _yButton, 50, 70);
        }
      
        public void ManageSound(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed && !isPress)
            {
                if (Name == "MusiqueAdd")
                {
                    GlobalsSounds.Musique += 20;
                }
                if (Name == "SoundAdd")
                {
                    GlobalsSounds.Sound += 20;
                }
                if (Name == "Musique")
                {
                    GlobalsSounds.Musique -= 20;
                }
                if (Name == "Sound")
                {
                    GlobalsSounds.Sound -= 20;
                }
                isPress = true;
            }
            if (mouseState.LeftButton == ButtonState.Released)
            {
                isPress = false;
            }
        }
       
     
        public bool IsClicked(MouseState mouseState)
        {
            // Verify the position of the mouse cursor and its state (pressed or not)
            if (GetRectangle().Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
   
             return true;
                
            }
            return false;
        }
    }
}
