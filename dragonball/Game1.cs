using dragonball.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace dragonball
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CheckGameStatus DrawGameStatus = new CheckGameStatus();

        //MUSIC
        Song music;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 640;
        }

        protected override void Initialize()
        {
            DrawGameStatus.InitInitialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //MUSIC
            music = Content.Load<Song>("game_music");
            //MUSIC
            MediaPlayer.Play(music);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            DrawGameStatus.LoadContent(Content);
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {


            DrawGameStatus.Update(gameTime, Content);
            if (DrawGameStatus.gameMenu.menuInput.exit)
            {
                this.Exit();
            }

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            DrawGameStatus.Draw(gameTime, spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }



    }
}
