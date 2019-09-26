using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace dragonball
{
    /*HIER WORDT DE CHECKPOINT GEMAAKT OM HET SPEL TE TRIGGEREN INDIEN HET EERSTE LEVEL UITGESPEELD IS, ER WORDT IN IEDER LEVEL EEN ORANJE OUDE MAN GEPLAATST ALS CHECKPOINT*/
    public class Rooshi : Sprite
    {
        public Rooshi(string imageSrc, Vector2 position, Color color, float speed) : base(imageSrc, position, color, speed)
        {
        }

        public override void LoadContent(ContentManager Content)
        {
            _texture = Content.Load<Texture2D>(_imageSrc);
        }

        public override void Update(ContentManager content, GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(content, gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }





}
