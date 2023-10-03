using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestSurvivor
{
    internal class HealthBar
    {

        protected readonly Texture2D background;
        protected readonly Texture2D foreground;
        protected readonly Vector2 position;
        protected readonly float maxValue;
        protected float currentValue;
        protected Rectangle part;
        private float timer; 

        private float _targetValue;
        private readonly float _animationSpeed = 20;
        private Rectangle _animationPart;
        private Vector2 _animationPosition;
        private Color _animationShade;

        public HealthBar(Texture2D bg, Texture2D fg, float max, Vector2 pos)
        {
            background = bg;
            foreground = fg;
            maxValue = max;
            currentValue = max;
            position = pos;
            part = new(0, 0, foreground.Width, foreground.Height);

            _targetValue = max;
            _animationPart = new(foreground.Width, 0, 0, foreground.Height);
            _animationPosition = pos;
            _animationShade = Color.DarkGray;

            timer = 0;
        }

        public void Update(float value, GameTime gameTime)
        {
            timer = 0;
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (value == currentValue) return;

            _targetValue = value;
            int x;

            if (_targetValue < currentValue)
            {
                currentValue -= _animationSpeed * timer;
                if (currentValue < _targetValue) currentValue = _targetValue;
                x = (int)(_targetValue / maxValue * foreground.Width);
                _animationShade = Color.Gray;
            }
            else
            {
                currentValue += _animationSpeed * timer;
                if (currentValue > _targetValue) currentValue = _targetValue;
                x = (int)(currentValue / maxValue * foreground.Width);
                _animationShade = Color.DarkGray * 0.5f;
            }

            part.Width = x;
            _animationPart.X = x;
            _animationPart.Width = (int)(Math.Abs(currentValue - _targetValue) / maxValue * foreground.Width);
            _animationPosition.X = position.X + x;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(background, position, Color.White);
            Globals.SpriteBatch.Draw(foreground, position, part, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            Globals.SpriteBatch.Draw(foreground, _animationPosition, _animationPart, _animationShade, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}
