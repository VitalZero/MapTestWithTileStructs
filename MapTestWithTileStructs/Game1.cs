using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MapTestWithTileStructs
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private float globalScale = 4;
        private int width = 320;
        private int height = 180;
        Matrix screenScale;
        RenderTarget2D renderTarget;
        Map map;
        private KeyboardState kbd;
        private Vector2 target;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Map("tilesetHD");
            graphics.PreferredBackBufferWidth = width * (int)globalScale;
            graphics.PreferredBackBufferHeight = height * (int)globalScale;
            graphics.ApplyChanges();
            screenScale = Matrix.CreateScale(globalScale);
            Camera2D.Viewport = new Rectangle(0, 0, width, height);// GraphicsDevice.Viewport.Bounds;
            Camera2D.Init();
            target = new Vector2(width / 2, height / 2);

            renderTarget = new RenderTarget2D(GraphicsDevice, width, height);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            map.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            kbd = Keyboard.GetState();

            if (kbd.IsKeyDown(Keys.Left))
            {
                target += new Vector2(-5f, 0);
            }
            if (kbd.IsKeyDown(Keys.Right))
            {
                target += new Vector2(5f, 0);
            }
            if (kbd.IsKeyDown(Keys.Up))
            {
                target += new Vector2(0, -5f);
            }
            if (kbd.IsKeyDown(Keys.Down))
            {
                target += new Vector2(0, 5f);
            }
            if (kbd.IsKeyDown(Keys.W))
                Camera2D.ZoomBy(0.03f);
            if (kbd.IsKeyDown(Keys.S))
                Camera2D.ZoomBy(-0.03f);
            if (kbd.IsKeyDown(Keys.A))
                Camera2D.Rotate(-1f);
            if (kbd.IsKeyDown(Keys.D))
                Camera2D.Rotate(1f);

            Camera2D.Follow(target);
            map.Update(dt);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.Clear(new Color(0, 0, 0, 0));

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            map.Draw(spriteBatch);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: screenScale * Camera2D.TransformMatrix());

            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
