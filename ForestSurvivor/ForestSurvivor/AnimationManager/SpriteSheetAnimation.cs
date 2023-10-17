///Auteur : Alexandre Babich , Yoann Meier
//Date : 17.10.2023
//Page : SpriteSheetAnimation.cs
//Utilité : Gestionnaire d'animations  
///Projet : ForestSurvivor V1 (2023)
using ForestSurvivor.AllGlobals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace ForestSurvivor.AnimationManager
{
    internal class SpriteSheetAnimation
    {
        private Texture2D _spriteSheet;
        private List<Rectangle> _frames;
        private int _currentFrame;
        private float _frameDuration;
        private float _frameTimer;
        private bool _loop;
        private bool _isPlaying = true;
        private float _scaleMultiplayer;
        private int _row;
        private int _column;
        private int _frameWidth;
        private int _frameHeight;
        private float _positionX = 0;
        private float _positionY = 0;

        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; } = Vector2.One;
        public bool Loop { get => _loop; set => _loop = value; }
        public bool IsPlaying { get => _isPlaying; set => _isPlaying = value; }
        public float ScaleMultiplayer { get => _scaleMultiplayer; set => _scaleMultiplayer = value; }
        public int Row { get => _row; set => _row = value; }
        public int Column { get => _column; set => _column = value; }
        public int FrameWidth { get => _frameWidth; set => _frameWidth = value; }
        public int FrameHeight { get => _frameHeight; set => _frameHeight = value; }
        public Texture2D SpriteSheet { get => _spriteSheet; set => _spriteSheet = value; }
        public List<Rectangle> Frames { get => _frames; set => _frames = value; }
        public int CurrentFrame { get => _currentFrame; set => _currentFrame = value; }
        public float FrameDuration { get => _frameDuration; set => _frameDuration = value; }
        public float FrameTimer { get => _frameTimer; set => _frameTimer = value; }
        public float PositionX { get => _positionX; set => _positionX = value; }
        public float PositionY { get => _positionY; set => _positionY = value; }

        public SpriteSheetAnimation(Texture2D spriteSheet, int row, int column, float frameDuration, bool loop = true, float scaleMultiplayer = 1)
        {
            SpriteSheet = spriteSheet;
            FrameDuration = frameDuration;
            Frames = new List<Rectangle>();
            CurrentFrame = 0;
            Column = column;
            Row = row;
            FrameWidth = spriteSheet.Width / column;
            FrameHeight = spriteSheet.Height / row;
            Loop = loop;
            ScaleMultiplayer = scaleMultiplayer;
            CutSpriteSheet();
        }



        public void UpdateAnimation(GameTime gameTime)
        {
            AnimateSpriteSheetStatic(gameTime);
        }


        public void DrawAnimation(Rectangle rectangle, Color color)
        {
            if (CurrentFrame >= 0 && CurrentFrame < Frames.Count)
            {

                Rectangle destinationRect = new Rectangle((int)PositionX, (int)PositionY, (int)(Frames[CurrentFrame].Width * ScaleMultiplayer), (int)(Frames[CurrentFrame].Height * ScaleMultiplayer));
                destinationRect = rectangle;
                Globals.SpriteBatch.Draw(SpriteSheet, destinationRect, Frames[CurrentFrame], color);
            }
        }


        public void CutSpriteSheet()
        {
            for (int y = 0; y < Row; y++)
            {
                for (int x = 0; x < Column; x++)
                {
                    int frameX = x * FrameWidth;
                    int frameY = y * FrameHeight;
                    Frames.Add(new Rectangle(frameX, frameY, FrameWidth, FrameHeight));
                }
            }
        }

        int firstFrame = 0;
        int lastFrame = 3;
        float frameDuration = 0.3f;
        /// <summary>
        /// Sert a definir l'animation de depart, de fin et la durée d'animation
        /// </summary>
        /// <param name="animationStartFrameNew"></param>
        /// <param name="animationLastFrameNew"></param>
        /// <param name="animationDurationNew"></param>
        public void setAnimation(int animationStartFrameNew, int animationLastFrameNew, float animationDurationNew)
        {
            firstFrame = animationStartFrameNew;
            lastFrame = animationLastFrameNew;
            frameDuration = animationDurationNew;
        }


        /// <summary>
        /// Joue l'entiereté de l'animation en boucle
        /// </summary>
        /// <param name="gameTime"></param>
        public void AnimateSpriteSheetStatic(GameTime gameTime)
        {
            if (IsPlaying)
            {
                FrameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (FrameTimer >= FrameDuration)
                {

                    CurrentFrame++;
                    if (CurrentFrame >= Frames.Count)
                    {
                        CurrentFrame = 0;
                        if (!Loop)
                        {
                            IsPlaying = false;
                        }
                    }

                    FrameTimer = 0f;
                }
            }
        }

        public void AnimateLooped(GameTime gameTime)
        {
            if (IsPlaying)
            {
                FrameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                while (FrameTimer >= frameDuration)
                {
                    if (CurrentFrame < firstFrame || CurrentFrame > lastFrame)
                    {
                        CurrentFrame = firstFrame;
                    }
                    else
                    {
                        CurrentFrame++;
                        if (CurrentFrame > lastFrame)
                        {
                            CurrentFrame = firstFrame;
                        }
                    }

                    FrameTimer -= frameDuration;
                }
            }
            else
            {
                CurrentFrame = firstFrame;
            }
        }
    }
}
