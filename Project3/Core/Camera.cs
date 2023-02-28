using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Project3.Extensiens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Core
{
    public static class Camera
    {
        public static Vector2 Position = new Vector2(1, 1);
        public static float Size = 1;

        private static GraphicsDevice _device;
        private static float _width => _device.PresentationParameters.BackBufferWidth;
        private static float _height => _device.PresentationParameters.BackBufferHeight;

        public static void Initialize(GraphicsDevice device)
        {
            _device = device;
        }

        public static Matrix GetTransformationMatrix()
        {
            Matrix zoomMatrix = Matrix.CreateScale(1f);
            Matrix cameraMatrix = Matrix.CreateTranslation(-(Position.X + 100), -(Position.Y + 100), 0f);
            Matrix transformMatrix = zoomMatrix * cameraMatrix;

            return transformMatrix;
        }

        public static Vector2 WorldToScreen(Vector2 worldPosition)
        {
            var transformationMatrix = GetTransformationMatrix();
            Vector2 screen = Vector2.Transform(worldPosition, transformationMatrix);
            return screen;
        }

        public static Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            var transformationMatrix = GetTransformationMatrix();
            Matrix inverseMatrix = Matrix.Invert(transformationMatrix);
            return Vector2.Transform(screenPosition, inverseMatrix);
        }

        public static RectangleF GetCameraBoundsInWorldPosition()
        {
            return RectangleF.Empty;
        }


        public static Vector2 ScreenToViewPort(Vector2 screenPosition)
        {
            // checks if values are shit
            float viewx = screenPosition.X / _width;
            float viewy = screenPosition.Y / _height;
            return new Vector2(viewx, viewy);
        }

        public static Vector2 ViewPortToWorld(Vector2 viewPortPosition)
        {
            var screen = Camera.ViewPortToScreen(viewPortPosition);
            var world = Camera.ScreenToWorld(screen);
            return world;
        }
        public static Vector2 ViewPortToWorld(float x, float y)
        {
            var screen = Camera.ViewPortToScreen(new Vector2(x,y));
            var world = Camera.ScreenToWorld(screen);
            return world;
        }

        public static Vector2 ViewPortToScreen(Vector2 viewPortPosition)
        {
            float screenx = _width * viewPortPosition.X;
            float screeny = _height * viewPortPosition.Y;
            var screen = new Vector2(screenx, screeny);
            return screen;
        }

        public static Vector2 ViewPortToScreen(float x, float y)
        {
            float screenx = _width * x;
            float screeny = _height * y;
            var screen = new Vector2(screenx, screeny);
            return screen;
        }
    }
}
