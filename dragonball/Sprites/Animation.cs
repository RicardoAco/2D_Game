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
    public class Animation
    {

        /*DEZE KLASSE DIENT OM EEN ANIMATIE TE GEVEN VOOR EEN SPRITESHEET*/

        public Texture2D animation { get; set; }
        public Rectangle sourceRect;
        public Vector2 position { get; set; }
        public string _image;
        public float positionX { get; set; }
        public float positionY { get; set; }

        float elapsed;
        public float frameTime; //speed of the animation

        public int frames; // numofframes
        int currentFrame;



        public int frameHeight;
        public int frameWidth;
        bool looping;

        public Animation(ContentManager Content, string image, float frameSpeed, int frames, bool looping, Vector2 positie)
        {
            this.frameTime = frameSpeed;
            this.frames = frames;
            this.looping = looping;

            _image = image;
            this.animation = Content.Load<Texture2D>(_image);

            frameWidth = (animation.Width / frames);
            frameHeight = (animation.Height);

            positionX = positie.X;
            positionY = positie.Y;

            position = positie;
        }

        //UPDATE
        /*DEZE METHODE LOOPED DOORHEEN DE DE SPRITESHEET STUKJE PER STUKJE ZOALS EEN FILMROL, DE HOOGTE EN BREEDTE WORDT BEPAALD VAN DE VOLLEDIGE AFBEELDING.*/
        public void PlayAnim(GameTime gameTime)
        {
            Vector2 temp = new Vector2(positionX, positionY);
            position = temp;

            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            sourceRect = new Rectangle(currentFrame * frameWidth + 1, 0, frameWidth, frameHeight);

            if (elapsed >= frameTime)
            {
                if (currentFrame >= frames - 1)
                {
                    if (looping)
                    {
                        currentFrame = 0;
                    }
                }
                else
                {
                    currentFrame++;
                }
                elapsed = 0;
            }

        }


        //DRAW
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animation, position, sourceRect, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
        }

    }
}
