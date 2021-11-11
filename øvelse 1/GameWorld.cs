using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace øvelse_1
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;
        public static Vector2 screenSize;
        private Texture2D CollisionTexture;
        private SpriteFont text;
        private List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> newObjects = new List<GameObject>();
        private static List<GameObject> deleteObjects = new List<GameObject>();
        public static int score;
        public static int Lives = 3;
        Player player;
        Enemy enemy;
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            enemy = new Enemy();

            gameObjects.Add(player);
            gameObjects.Add(enemy);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            player.LoadContent(Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            enemy.LoadContent(Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            CollisionTexture = Content.Load<Texture2D>("CollisionTexture");
            text = Content.Load<SpriteFont>("File");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            gameObjects.AddRange(newObjects);
            newObjects.Clear();

            foreach (var gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
                foreach (var other in gameObjects)
                {
                    gameObject.CheckCollision(other);
                }
            }

            foreach (GameObject gameObject in deleteObjects)
            {
                gameObjects.Remove(gameObject);
            }
            deleteObjects.Clear();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            foreach (var gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
#if DEBUG
                DrawCollisionBox(gameObject);
#endif
            }
            _spriteBatch.DrawString(text, $"Score: {score}\nLives: {Lives}", new Vector2(0,0),Color.White);
            _spriteBatch.End();
            

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        
        private void DrawCollisionBox(GameObject gameObject)
        {
            Rectangle collisionBox = gameObject.GetCollisionBox();
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            _spriteBatch.Draw(CollisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(CollisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(CollisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(CollisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public static void Instantiate(GameObject gameObject)
        {
            newObjects.Add(gameObject);
        }

        public static void Destroy(GameObject gameObject)
        {
            deleteObjects.Add(gameObject);
        }



    }
}
