using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace dragonball.Sprites
{
    
    public class Player : Sprite
    {
        /*DEZE KLASSE DIENT OM EEN NIEUWE SPELER TOE TE VOEGEN IN HET SPEL*/



        //SHOOT
        public Bullet bullet = new Bullet("energy", new Vector2(2000, 2000), Color.White, 9f);

        //JUMP VARIABELEN
        bool jump = false;
        float jumpVelocity = 10f;
        int i = 0;
        

        //ANIMATIE

        public Animation animation;
        float gravity = 1f;

        //BOOL JUMP ANIMATION
        bool jumpAnim = false;

        //DIENT VOOR COLLISION TERUG TE GEVEN
        public override Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, animation.frameWidth, animation.frameHeight);
            }
        }

        public Player(string imageSrc, Vector2 position, Color color, float speed) : base(imageSrc, position, color, speed)
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            
            _texture = Content.Load<Texture2D>(_imageSrc);
            animation = new Animation(Content, _imageSrc, 80, 8, true, Position);

            //SHOOT
            bullet.LoadContent(Content);
        }

        public override void Update(ContentManager content,GameTime gameTime, List<Sprite> sprites)
        {
            

            Input.Update();
            Move(content, gameTime);
            _texture = content.Load<Texture2D>(_imageSrc);

            //COLLISION
            collisionDetection(sprites);

            Position += Velocity;
            Velocity = Vector2.Zero;
            animation.PlayAnim(gameTime);

            //SHOOT
            bullet.FlyingBullet();


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, animation.sourceRect, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

            //SHOOT
            bullet.Draw(spriteBatch);
        }
        
        //DEZE METHODE DIENT OM PLAYER TE BESTUREN EN ZWAARTEKRACHT TE GEVEN
        private void Move(ContentManager content,GameTime gameTime)
        {
            keyInput();
            grav();
        }

        //GRAVITY METHOD
        public void grav()
        {
            //Gravity
            if (_touch_ground == false)
            {
                Velocity.Y += 1.7f * gravity;
                Speed = 4f;
                gravity *= 1.07f;
            }

            if (_touch_ground == true)
            {
                gravity = 1f;
                Velocity.Y += 2f * gravity;
                Speed = 3f;
            }
        }
        //INPUT FOR KEYS METHOD
        public void keyInput()
        {
            //WALK LEFT
            if (Input.left)
            {
                _imageSrc = "walking_left";
                Velocity.X = -Speed;
                Input.state = true;

            }
            //WALK RIGHT
            if (Input.right)
            {
                _imageSrc = "walking_right";

                Velocity.X = Speed;
                Input.state = false;
            }
            //IDLE RIGHT
            if (!Input.left && !Input.right && !Input.state)
            {
                _imageSrc = "idle_right";
            }
            //IDLE LEFT
            if (!Input.left && !Input.right && Input.state)
            {
                _imageSrc = "idle_left";

            }

            //SHOOTING RIGHT WHILE IDLE
            if (_imageSrc == "idle_right" && Input.shoot)
            {
                _imageSrc = "shoot_right";
                bullet.Position.X = this.Position.X + 35;
                bullet.Position.Y = this.Position.Y + 17;
                bullet.boolFlyLeft = false;
                bullet.boolFlyRight = true;
            }

            if (_imageSrc == "shoot_right" && !Input.shoot)
            {
                _imageSrc = "idle_right";
            }

            //SHOOTING RIGHT WHILE IDLE
            if (_imageSrc == "idle_left" && Input.shoot)
            {
                _imageSrc = "shoot_left";
                bullet.Position.X = Position.X - 15;
                bullet.Position.Y = Position.Y + 17;
                bullet.boolFlyRight = false;
                bullet.boolFlyLeft = true;
            }

            if (_imageSrc == "shoot_left" && !Input.shoot)
            {
                _imageSrc = "idle_left";
            }
            //ACTIVATE JUMP

            if (Input.space)
            {
                
                if (_touch_ground == true && jump == false)
                {

                    jump = true;
                    _touch_ground = false;
                }
                
            }
            //JUMP AFTER ACTIVATION
            if (jump == true && _touch_ground == false)
            {

                jumpAnim = true;

                gravity = 0;

                if (i < 12)
                {
                    i++;
                    Velocity.Y = -jumpVelocity;
                    jumpVelocity -= 0.05f;
                    
                }

                if (i >= 12)
                {
                    gravity = 1f;
                    i = 0;
                    jumpVelocity = 10f;
                    jump = false;
                }
                

            }

            //JUMP ANIMATION AFTER JUMP BOOL ACTIVATION
            if(jumpAnim == true)
            {
                JumpAnimSprite();
            }

            //FALLING DOWN
            if (_touch_ground == false)
            {
                JumpAnimSprite();
            }
        }
        //COLLISION METHOD
        public void collisionDetection(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                    (this.Velocity.X < 0 & this.IsTouchingRight(sprite)))
                {
                    this.Velocity.X = 0;
                }


                if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)))
                {
                    this.Velocity.Y = 0;

                    gravity = 1f;
                    i = 0;
                    jumpVelocity = 10f;
                    jump = false;
                }
                if (this.Velocity.Y < 0 & this.IsTouchingBottom(sprite))
                {
                    this.Velocity.Y = 0;
                }





                    if (this.Velocity.Y == 0)
                {
                    _touch_ground = true;
                    jumpAnim = false;
                    //TERUG instellen op 8 FRAMES EN JUISTE FRAME SNELHEID
                    animation.frames = 8;
                    animation.frameTime = 80;
                    
                }
                
                if(this.Velocity.Y > 0)
                {
                    _touch_ground = false;
                    jump = false;
                    
                }

                if(this.Velocity.Y == 0 && this.IsTouchingBottom(sprite))
                {
                    _touch_ground = true;
                    jump = false;
                    
                }
                
                
            }

            
        }
        
        //JUMP ANIMATIE METHODE
        public void JumpAnimSprite()
        {
            //SNEL OVERSCHAKELEN NAAR JUMP IMG
            animation.frames = 1;
            animation.frameTime = 1;

            //JUMP LEFT + ARROW LEFT
            if (Input.left)
            {
                _imageSrc = "jump_left";
                Velocity.X = -Speed;
                Input.state = true;

            }
            //JUMP RIGHT + ARROW RIGHT
            if (Input.right)
            {
                _imageSrc = "jump_right";

                Velocity.X = Speed;
                Input.state = false;
            }
            //JUMP RIGHT
            if (!Input.left && !Input.right && !Input.state)
            {
                _imageSrc = "jump_right";
            }
            //JUMP LEFT
            if (!Input.left && !Input.right && Input.state)
            {
                _imageSrc = "jump_left";

            }

            
        }

        public void resetPosPlayer(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
        
    }

}
