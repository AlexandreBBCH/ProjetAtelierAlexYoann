﻿using ForestSurvivor.AllGlobals;
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
        private float _x;
        private float _y;
        private int _width;
        private int _height;
        private string _text;
        private Rectangle _collider;
        private string _where;
        private Texture2D _texture2D;
        private string _typeTexture;
        public string ButtonName { get => _buttonName; set => _buttonName = value; }
        public string Where { get => _where; set => _where = value; }

        public OptionClickable(float x, float y, int width, int height,  string text, string buttonName,string where,string typeTexture, SpriteFont font, Texture2D texture2D)
        {
            _font = font;
            _texture2D = texture2D;
            _buttonName = buttonName;
            _text = text;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _where = where;
            _typeTexture = typeTexture;
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
            if (_typeTexture == "Font")
            {
                Globals.SpriteBatch.DrawString(_font, _text, new Vector2(_x, _y), Color.White);
            }
            else if(_typeTexture == "Image")
            {
                Globals.SpriteBatch.Draw(_texture2D,new Rectangle((int)_x, (int)_y,_width,_height), Color.White);
            }
     
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)_x, (int)_y, _width, _height);
        }

        public void UseIt()
        {

            switch (_buttonName)
            {

                case "Start":
                    Globals.LauchGame = true;
                    break;
                case "Option":
                    Globals.IsResume = true;
                    break;
                case "BackMenu":
                        Globals.Back = true;
                        Globals.LauchGame = false;
                    break;
                case "Resume":
                    Globals.Back = true;
                    break;
                case "Exit":
                    Globals.Exit = true;
                    break;
                case "AddMusique":
                    if (GlobalsSounds.Musique <= 95) GlobalsSounds.Musique += 5;
                    break;
                case "RetireMusique":
                    if (GlobalsSounds.Musique >= 5) GlobalsSounds.Musique -= 5;
                    break;
                case "AddSound":
                    if (GlobalsSounds.Sound <= 95) GlobalsSounds.Sound += 5;
                    break;
                case "RetireSound":
                    if (GlobalsSounds.Sound >= 5) GlobalsSounds.Sound -= 5;
                    break;
                default:
                    break;
            }
        }

        public bool IsClicked(MouseState mouseState)
        {
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
