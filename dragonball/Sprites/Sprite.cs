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
    public class Sprite
    {

        public Texture2D _texture;
        public string _imageSrc;

        public Vector2 Position;
        public Vector2 Velocity;
        public Color Colour = Color.White;
        public float Speed;
        public BedieningPijltjes Input = new BedieningPijltjes();



        //TOUCH GROUND
        public bool _touch_ground = false;

        //ENEMY
        public bool IsEnemy = false;

        public Sprite(string imageSrc, Vector2 position, Color color, float speed)
        {

            _imageSrc = imageSrc;
            Position = position;
            Colour = color;
            Speed = speed;


        }

        public virtual void LoadContent(ContentManager Content)
        {
            _texture = Content.Load<Texture2D>(_imageSrc);

        }

        public virtual Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public virtual void Update(ContentManager content, GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Colour);

        }

        //COLLISION
        #region collision blocks

        protected bool IsTouchingLeft(Sprite sprite)
        {

            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
             this.Rectangle.Left < sprite.Rectangle.Left &&
             this.Rectangle.Bottom > sprite.Rectangle.Top &&
             this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        #endregion



    }
}
