using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace øvelse_1
{
    public abstract class GameObject
    {
        protected Texture2D sprite;
        protected Vector2 position;
        protected Texture2D[] sprites;
        protected float fps = 1;
        private float timeElaplsed;
        private int currentIndex;
        protected float speed = 200f;
        protected Vector2 velocity;
        protected Vector2 origin;
        protected Vector2 offset;
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                       (int)(position.X + offset.X),
                       (int)(position.Y + offset.Y),
                       sprite.Width,
                       sprite.Height
                   );
            }
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 0);

        }
            protected void Animate(GameTime gameTime)
            {
                timeElaplsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                currentIndex = (int)(timeElaplsed * fps);


                if (currentIndex >= sprites.Length)
                {
                    timeElaplsed = 0;
                    currentIndex = 0;
                }
                sprite = sprites[currentIndex];
            }
            protected void Move(GameTime gameTime)
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                position += ((velocity * speed) * deltaTime);
            }
        public virtual Rectangle GetCollisionBox()
        {
            return new Rectangle((int)position.X + (int)offset.X, (int)position.Y + (int)offset.Y, sprite.Width, sprite.Height);
        }

        public abstract void OnCollision(GameObject other);

        public void CheckCollision(GameObject other)
        {
            if (GetCollisionBox().Intersects(other.GetCollisionBox()))
            {

                OnCollision(other);

            }
        }
        
    }
}

