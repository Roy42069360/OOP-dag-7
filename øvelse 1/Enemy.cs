using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace øvelse_1
{
    class Enemy : GameObject
    {
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[4];
            sprites[0] = content.Load<Texture2D>("enemyBlue");
            sprites[1] = content.Load<Texture2D>("enemyRed");
            sprites[2] = content.Load<Texture2D>("enemyBlack");
            sprites[3] = content.Load<Texture2D>("enemyGreen");
            
            Respawn();
        }

        public override void Update(GameTime gameTime)
        {
          if (position.Y >= 500)
            {
                Respawn();
            }
            Move(gameTime);
           
        }
        private void Respawn()
        {
            position.Y = (-100);

            velocity = new Vector2(0, 1);
            Random rndSpeed = new Random();
            speed = rndSpeed.Next(50,150);

            Random rndEnemy = new Random();
            int i = rndEnemy.Next(0, 4);
            sprite = sprites[i];

            Random rndXPos = new Random();
            position.X = rndXPos.Next(0,575);
            
            


        }
        public override void OnCollision(GameObject other)
        {
            if (other is Laser)
            {
                GameWorld.Destroy(other);
                GameWorld.score++;
                Respawn();
            }
            if (other is Player)
            {
                Respawn();
            }
        }
    }
}
