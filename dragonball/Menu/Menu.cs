using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragonball
{
    public class Menu
    {
        //BEDIENING TOEVOEGEN
        public BedieningPijltjes menuInput = new BedieningPijltjes();

        //DEZE BOOL DIENT OM HET SPEL TE STARTEN WANNEER DEZE TRUE IS
        public bool play = false;

        //MENU TEXTURE
        public Texture2D texture;
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("menu");
        }

        public void Update(GameTime gameTime)
        {
            menuInput.Update();

            //ALS DE GEBRUIKER OP "S" DRUKT DAN START HET SPEL
            if (menuInput.start)
            {
                play = true;

            }

           
        }

        public  void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,new Vector2(0,0),Color.White);
        }
    }
}
