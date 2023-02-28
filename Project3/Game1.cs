using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Project3.Core;
using Project3.Extensiens;
using Project3.GameObjects;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Project3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
        private Character _character;
        private readonly Container _container = new Container();

        // DI Container
        // Camera
        // IMGUI
        // Entities Container
        // Input
        // Time

        // TODO : Audio
        // Component system ? 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        protected override void Initialize()
        {
            base.Initialize();
            Entities.Initialize(_spriteBatch, GraphicsDevice, _container);
            Camera.Initialize(_spriteBatch.GraphicsDevice);

            //_graphics.PreferredBackBufferWidth = 1500;  // set this value to the desired width of your window
            //_graphics.PreferredBackBufferHeight = 900;   // set this value to the desired height of your window

            var height = _graphics.GraphicsDevice.DisplayMode.Height;
            var width = _graphics.GraphicsDevice.DisplayMode.Width;

            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;

            _graphics.ToggleFullScreen();
           // _graphics.ToggleFullScreen();

            _graphics.ApplyChanges();


            _container.Register<HighScoreManager>();
            _container.Register<Character>();
            _container.Register<EnemiesManager>();
            _container.Register<CameraController>();
            _container.Register<BackImage>();
            _container.Register<ForeImage>();
            _container.Register<Parallax>();
            _container.Register<AmmoManager>();
            _container.Register<MotherShip>();




            _container.RegisterInFactory<Enemy>();

            _container.Register<IMGUI>();


            _container.BuildInjection();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Resources.LoadResources(this.Content);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            Time.Update(gameTime);
            Entities.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);

            

            var transformationMatrix = Camera.GetTransformationMatrix();

            var character = _container.Resolve<Character>();
            var enemyManager = _container.Resolve<EnemiesManager>();
            var im = _container.Resolve<IMGUI>();


            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, transformationMatrix);



            Entities.Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // to do
        // Camera rendering 
        // dependency injection
        // entity/component system
    }
}