using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestSurvivor.Ui
{
    internal class OptionClickable
    {
        private SpriteFont _font;
        private Rectangle _rectangle;
        private string _tooltipText;
        private bool wasLeftButtonPressedLastFrame;
        private string _buttonName;
        private float brightness = 1f;
        private float elapsedClickTime = float.MaxValue;
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private string _text;
        private Rectangle _collider;
        private string _where;

        public string ButtonName { get => _buttonName; set => _buttonName = value; }
        public string Where { get => _where; set => _where = value; }

        public OptionClickable(int x, int y, int width, int height, SpriteFont font, string text, string buttonName,string where)
        {
            _font = font;
            _buttonName = buttonName;
            _text = text;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _where = where;
            Globals.optionClickables.Add(this);
        }

        public void OnClickChangeBrightness()
        {
            brightness *= 0.5f;

            elapsedClickTime = 0f;
        }

        public void DrawTextClickable()
        {
            //Globals.SpriteBatch.Draw(GlobalsTexture.MainMenu2D, GetRectangle(), Color.White);
            Globals.SpriteBatch.DrawString(GlobalsTexture.titleFont, _text, new Vector2(_x, _y), Color.White);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(_x, _y, _width, _height);
        }

        public void UseIt()
        {
            if (_buttonName == "Start")
            {
                Globals.LauchGame = true;
            }
            if (_buttonName == "Exit")
            {
                Globals.Exit = true;

            }
        }

        public bool IsClicked(MouseState mouseState)
        {
            // Verify the position of the mouse cursor and its state (pressed or not)
            if (GetRectangle().Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!wasLeftButtonPressedLastFrame)
                {
                    wasLeftButtonPressedLastFrame = true;
                    return true;
                }
            }
            else
            {
                wasLeftButtonPressedLastFrame = false;
                brightness = 0.5f;
            }
            return false;
        }
    }
}
