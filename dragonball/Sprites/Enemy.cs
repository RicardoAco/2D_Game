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
    public class Enemy : Sprite
    {
        /*DIT MAAKT EEN ENEMY AAN EN LAAT DEZE HEEN EN WEER WANDELEN IN HET SPEL/LEVEL*/
        bool check = false;
        int teller = 0;
        int lengte = 250;
        public Animation animation;

        new public Vector2 startPos;
        public Vector2 DiePos = new Vector2(2000, 2000);

        public Enemy(string imageSrc, Vector2 position, Color color, float speed, int lengteIn) : base(imageSrc, position, color, speed)
        {
            //AFSTAND OM AF TE LEGGEN
            lengte = lengteIn;
            startPos.X = position.X;
            startPos.Y = position.Y;
        }

        //VOOR COLLISION
        public override Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, animation.frameWidth, animation.frameHeight);
            }
        }

        public override void LoadContent(ContentManager Content)
        {
            _texture = Content.Load<Texture2D>(_imageSrc);
            animation = new Animation(Content, _imageSrc, 300, 4, true, Position);
        }

        public override void Update(ContentManager content, GameTime gameTime, List<Sprite> sprites)
        {
            _texture = content.Load<Texture2D>(_imageSrc);

            teller++;
           
            if(teller >= lengte)
            {
                teller = 0;
                check = !check;
                
            }

            if(check == false)
            {
                Velocity.X = Speed;
                _imageSrc = "buu_right";
            }
            

            if (check == true)
            {
                Velocity.X = -Speed;
                _imageSrc = "buu_left";
            }

            Position += Velocity;
            Velocity = Vector2.Zero;

            animation.PlayAnim(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, animation.sourceRect, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
        }
        public void DiePosition()
        {
            Position.X = DiePos.X;
            Position.Y = DiePos.Y;
        }

        public void RespawnPosition()
        {
            Position.X = startPos.X;
            Position.Y = startPos.Y;
            teller = 0;
            check = false;
        }

        
       
    }
}
