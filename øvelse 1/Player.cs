using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace øvelse_1
{
    class Player : GameObject
{
        private Texture2D laser;
        private Vector2 spawnOffset;
        bool canFire = true;
        private SoundEffectInstance effect;
        private int fireDelay;
        public Player()
        {



            spawnOffset = new Vector2(-25, -105);
        }

        private void HandleInput()
        {
            velocity = Vector2.Zero;

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -1);
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, 1);
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(1, 0);
            }
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
            if (keyState.IsKeyDown(Keys.Space)&&canFire==true)
            {
                canFire = false;
                effect.Play();
                GameWorld.Instantiate(new Laser(laser, new Vector2(position.X + spawnOffset.X, position.Y + spawnOffset.Y)));
            }
            if (canFire==false && fireDelay < 52)
            {
                fireDelay++;
            }
            else
            {
                canFire = true;
                fireDelay = 0;
            }

        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[4];
            fps = 10;
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>(i + 1 + "fwd");
            }

            sprite = sprites[0];
            this.position = new Vector2(GameWorld.screenSize.X / 2, GameWorld.screenSize.Y - sprite.Height / 2);

            this.origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            offset = origin * -1;

            effect = content.Load<SoundEffect>("SFX_Powerup_01").CreateInstance();
            laser = content.Load<Texture2D>("laserGreen03");
        }



        public override void Update(GameTime gameTime)
        {
            HandleInput();
            Move(gameTime);
            Animate(gameTime);
            ScreenLimits();
            ScreenWarp();
        }

        private void ScreenWarp()
        {
            if (position.X > GameWorld.screenSize.X + sprite.Width)
            {
                position.X = -sprite.Width;
            }
            else if (position.X < -sprite.Width)
            {
                position.X = GameWorld.screenSize.X + sprite.Width;
            }
        }
        private void ScreenLimits()
        {
            if (position.Y - sprite.Height / 2 < 0)
            {
                position.Y = sprite.Height / 2;
            }
            else if (position.Y > GameWorld.screenSize.Y)
            {
                position.Y = GameWorld.screenSize.Y;
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                GameWorld.Lives--;
            }
        }
    }
}
