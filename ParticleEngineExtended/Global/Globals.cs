using Microsoft.Xna.Framework.Content;

namespace ParticleEngineExtended.Global
{
    public static class Globals
    {
        public static float TotalSeconds { get; set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }

        public static void Update(GameTime gameTime)
        {
            TotalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
