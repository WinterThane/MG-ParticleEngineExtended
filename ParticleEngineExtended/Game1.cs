using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers.Containers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.TextureAtlases;
using System.Collections.Generic;
using System;

namespace ParticleEngineExtended
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ParticleEffect _particleEffect;
        private Texture2D _particleTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _particleTexture = new Texture2D(GraphicsDevice, 1, 1);
            _particleTexture.SetData(new[] { Color.White });

            TextureRegion2D textureRegion = new(_particleTexture);
            _particleEffect = new ParticleEffect(autoTrigger: false)
            {
                Position = new Vector2(400, 240),
                Emitters = new List<ParticleEmitter>
                {
                    new ParticleEmitter(textureRegion, 500, TimeSpan.FromSeconds(2.5), Profile.BoxUniform(100,250))
                    {
                        Parameters = new ParticleReleaseParameters
                        {
                            Speed = new Range<float>(0f, 50f),
                            Quantity = 3,
                            Rotation = new Range<float>(-1f, 1f),
                            Scale = new Range<float>(3.0f, 4.0f)
                        },
                        Modifiers =
                        {
                            new AgeModifier
                            {
                                Interpolators =
                                {
                                    new ColorInterpolator
                                    {
                                        StartValue = new HslColor(0.33f, 0.5f, 0.5f),
                                        EndValue = new HslColor(0.5f, 0.9f, 1.0f)
                                    }
                                }
                            },
                            new RotationModifier {RotationRate = -2.1f},
                            new RectangleContainerModifier {Width = 800, Height = 480},
                            new LinearGravityModifier {Direction = -Vector2.UnitY, Strength = 30f},
                        }
                    }
                }
            };
        }

        protected override void UnloadContent()
        {
            _particleTexture.Dispose();
            _particleEffect.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _particleEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            _spriteBatch.Draw(_particleEffect);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
