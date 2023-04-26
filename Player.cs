using DMIT1514_Platformer_Final_MG2022;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DMIT1514_Platformer_Final_MG2022
{
    public class Player
    {
        protected const int JumpForce = -300;
        protected const int Speed = 150;
        protected enum State
        {
            Idle,
            Walking,
            Jumping
        }
        protected State state;
        protected CelAnimationSequence idleSequence;
        protected CelAnimationSequence jumpSequence;
        protected CelAnimationSequence walkSequence;

        protected CelAnimationPlayer animationPlayer;

        protected Vector2 position;
        protected Vector2 velocity;
        internal Vector2 Velocity
        {
            get { return velocity; }
        }

        protected bool facingRight = true;
        protected Rectangle gameBoundingBox;
        protected Vector2 dimensions;

        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
            }
        }
        public Player(Vector2 position, Rectangle gameBoundingBox)
        {
            this.position = position;
            this.gameBoundingBox = gameBoundingBox;
            dimensions = new Vector2(46, 40);
            animationPlayer = new CelAnimationPlayer();
        }
        internal void Initialize()
        {
            state = State.Idle;
            animationPlayer.Play(idleSequence);
        }
        internal void LoadContent(ContentManager Content)
        {
            idleSequence = new CelAnimationSequence(Content.Load<Texture2D>("Idle"), 30, 1 / 8f);
            walkSequence = new CelAnimationSequence(Content.Load<Texture2D>("Walk"), 35, 1 / 8f);
            jumpSequence = new CelAnimationSequence(Content.Load<Texture2D>("JumpOne"), 30, 1 / 8f);
        }
        internal void Update(GameTime gameTime)
        {
            animationPlayer.Update(gameTime);

            velocity.Y += PlatformerGame.Gravity;

            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Math.Abs(velocity.Y) > PlatformerGame.Gravity)
            {
                state = State.Jumping;
                animationPlayer.Play(jumpSequence);
            }

            switch (state)
            {
                case State.Jumping:
                    break;
                case State.Idle:
                    break;
                case State.Walking:
                    break;
            }
        }

        internal void Draw(SpriteBatch SpriteBatch)
        {
            switch (state)
            {
                case State.Jumping:
                case State.Idle:
                case State.Walking:
                    SpriteEffects effects = SpriteEffects.None;
                    if (!facingRight)
                    {
                        effects = SpriteEffects.FlipHorizontally;
                    }
                    animationPlayer.Draw(SpriteBatch, position, effects);
                    break;
            }
        }
        internal void MoveHorizontally(float direction)
        {
            float oldXDirection = velocity.X;
            velocity.X = direction * Speed;
            if (velocity.X > 0)
            {
                facingRight = true;
            }
            else if (velocity.X < 0)
            {
                facingRight = false;
            }
            if (state != State.Jumping)
            {
                animationPlayer.Play(walkSequence);
                state = State.Walking;
            }
        }
        internal void MoveVertically(float direction)
        {
            velocity.Y = direction * Speed;
        }
        internal void Land(Rectangle whatILandedOn)
        {
            if (state == State.Jumping)
            {
                position.Y = whatILandedOn.Top - dimensions.Y + 1;
                velocity.Y = 0;
                state = State.Walking;
            }
        }
        internal void StandOn(Rectangle whatImStandingOn)
        {
            velocity.Y -= PlatformerGame.Gravity;
        }
        internal void Stop()
        {
            if (state == State.Walking)
            {
                velocity = Vector2.Zero;
                state = State.Idle;
                animationPlayer.Play(idleSequence);
            }
        }
        internal void Jump()
        {
            if (state != State.Jumping)
            {
                velocity.Y = JumpForce;
            }
        }
    }
}
