using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RasterBarDX
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class RasterbarGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tRaster;
        Color[] rasterColors = null;
        Color lineColor = Color.Black;
        Color screenLines = Color.Black;
        Vector2 s = new Vector2(0, 0);

        Texture2D spriteAtari1, spriteAtari2, spriteAtari3;

        public RasterbarGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.PreferredBackBufferWidth = 800;
            //graphics.PreferredBackBufferHeight = 600;

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;

            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //IsFixedTimeStep = true;
            //TargetElapsedTime = TimeSpan.FromMilliseconds(20); // 20 milliseconds, or 50 FPS.

            //font1 = Content.Load<SpriteFont>("default");

            tRaster = new Texture2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            rasterColors = new Color[tRaster.Width * tRaster.Height];

            spriteAtari1 = Content.Load<Texture2D>("atari1");
            spriteAtari2 = Content.Load<Texture2D>("atari2");
            spriteAtari3 = Content.Load<Texture2D>("atari3");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        Color nextRainbow(Color c)
        {
            Color r = c;
            r.R += 20;
            r.G += 10;
            r.B += 5;

            return (r);
        }

        // Red(web color) (Hex: #FF0000) (RGB: 255, 0, 0)
        // Orange(color wheel Orange) (Hex: #FF7F00) (RGB: 255, 127, 0)
        // Yellow(web color) (Hex: #FFFF00) (RGB: 255, 255, 0)
        // Green(X11) (Electric Green) (HTML/CSS “Lime”) (Color wheel green) (Hex: #00FF00) (RGB: 0, 255, 0)
        // Blue(web color) (Hex: #0000FF) (RGB: 0, 0, 255)
        // Violet(Electric Violet) (Hex: #8B00FF) (RGB: 139, 0, 255)

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            screenLines = nextRainbow(screenLines);
            lineColor = screenLines;

            for (int j = 0; j < tRaster.Height; j++)
            {
                lineColor = nextRainbow(lineColor);
 
                for (int k = 0; k < tRaster.Width; k++)
                {
                    rasterColors[(j * tRaster.Width) + k] = lineColor;
                }
            }

             base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred);

            tRaster.SetData<Color>(rasterColors);

            s.X = 0;
            s.Y = 0;
            spriteBatch.Draw(tRaster, s, Color.White);

            s.X = tRaster.Width / 2 - (spriteAtari3.Width / 2);
            s.Y = tRaster.Height / 2 - (spriteAtari3.Height / 2); ;
            spriteBatch.Draw(spriteAtari3, s, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
