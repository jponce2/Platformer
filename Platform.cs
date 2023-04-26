using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DMIT1514_Platformer_Final_MG2022
{
    public class Platform
    {
        protected Texture2D texture;
        protected string textureName;
        protected Vector2 position;
        protected Vector2 dimensions;

        protected Collider colliderTop;
        protected Collider colliderRight;
        protected Collider colliderBottom;
        protected Collider colliderLeft;

        public Platform(Vector2 position, Vector2 dimensions, string textureName)
        {
            this.textureName = textureName;
            colliderTop = new Collider(
                                        new Vector2(position.X + 3, position.Y),
                                        new Vector2(dimensions.X - 6, 1),
                                        Collider.ColliderType.Top);
            colliderRight = new Collider(
                                        new Vector2(position.X + dimensions.X - 1, position.Y + 1),
                                        new Vector2(1, dimensions.Y - 2),
                                        Collider.ColliderType.Right);
            colliderBottom = new Collider(
                                        new Vector2(position.X + 3, position.Y + dimensions.Y),
                                        new Vector2(dimensions.X - 6, 1),
                                        Collider.ColliderType.Bottom);
            colliderLeft = new Collider(
                                        new Vector2(position.X, position.Y + 1),
                                        new Vector2(1, dimensions.Y - 2),
                                        Collider.ColliderType.Left);
        }
        internal void LoadContent(ContentManager Content)
        {
            colliderTop.LoadContent(Content);
            colliderRight.LoadContent(Content);
            colliderBottom.LoadContent(Content);
            colliderLeft.LoadContent(Content);
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            colliderTop.Draw(spriteBatch);
            colliderRight.Draw(spriteBatch);
            colliderBottom.Draw(spriteBatch);
            colliderLeft.Draw(spriteBatch);
        }

        internal void ProcessCollisions(Player player)
        {
            colliderTop.ProcessCollisions(player);
            colliderRight.ProcessCollisions(player);
            colliderBottom.ProcessCollisions(player);
            colliderLeft.ProcessCollisions(player);
        }
    }
}
