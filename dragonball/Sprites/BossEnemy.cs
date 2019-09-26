using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace dragonball.Sprites
{
    class BossEnemy : Sprite
    {

        public Animation animation;

        //HEALTH
        public int health = 10;
        Healthbar VegetaHealthBar = new Healthbar("health_bar_10", new Vector2(780, 25), Color.White, 5f);

        //TIMER
        float timer;
        double timecounter;
        //TEXT
        bool textActive;
        Texture2D _text;

        //POSITIONS
        new public Vector2 startPos;
        public Vector2 DiePos = new Vector2(2000, 2000);

        //BULLETS
        public Vector2 bulletStart = new Vector2(2000, 2000); //UIT MAP
        double distanceBetweenBullets = 100;
        public List<Bullet> bullets = new List<Bullet>();

        public BossEnemy(string imageSrc, Vector2 position, Color color, float speed) : base(imageSrc, position, color, speed)
        {
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
            _text = Content.Load<Texture2D>("weakling");
            animation = new Animation(Content, _imageSrc, 300, 4, true, Position);

            //HEALTH
            VegetaHealthBar.LoadContent(Content);

            //BULLET
            bullets.Add(new Bullet("energy_blue", bulletStart, Color.White, 3.5f));
            bullets.Add(new Bullet("energy_blue", bulletStart, Color.White, 3.5f));
            bullets.Add(new Bullet("energy_blue", bulletStart, Color.White, 3.5f));

            bullets.Add(new Bullet("energy_blue", bulletStart, Color.White, 3.5f));
            bullets.Add(new Bullet("energy_blue", bulletStart, Color.White, 3.5f));
            bullets.Add(new Bullet("energy_blue", bulletStart, Color.White, 3.5f));

            foreach (Bullet bullet in bullets)
            {
                bullet.LoadContent(Content);
            }


        }

        public override void Update(ContentManager content, GameTime gameTime, List<Sprite> sprites)
        {
            //TIMER
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timecounter += (int)timer;
            if (timer >= 1.0F) timer = 0F;

            //HEALTH
            if (health < 0)
            {
                health = 0;
            }

            Script(content);

            VegetaHealthBar._texture = content.Load<Texture2D>("health_bar_" + Convert.ToString(health));
            animation.PlayAnim(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, animation.sourceRect, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
            VegetaHealthBar.Draw(spriteBatch);

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }

            if (textActive)
            {
                spriteBatch.Draw(_text, new Vector2(375, 360), Color.White);
            }

        }

        //METHODES
        public void DiePosition()
        {
            Position.X = DiePos.X;
            Position.Y = DiePos.Y;
        }

        public void RespawnPosition()
        {
            Position.X = startPos.X;
            Position.Y = startPos.Y;
        }

        public void TeleportMidden()
        {
            Position.X = 360;
            Position.Y = 360;
        }

        public void TeleportRechts(int t1)
        {
            if (timecounter == t1)
            {
                Position.X = 890;
                Position.Y = 358;
            }
        }
        //SCHIET NAAR LINKS NA EEN INGEGEVEN TIJDS INTERVAL. WORDT GEBRUIKT OM DE VIJAND EEN SCRIPT TE GEVEN OP HET LAATSTE LEVEL
        public void ShootLeft(int t1, int t2, int t3)
        {

            for (int i = 0; i < 3; i++)
            {
                if (!bullets[i].boolFlyLeft)
                {
                    bullets[i].Position.X = Position.X - 20;
                    bullets[i].Position.Y = Position.Y + 50;
                    bullets[i].boolFlyRight = false;
                    bullets[i].boolFlyLeft = true;
                }

            }

            if (timecounter >= t1)
            {

                bullets[0].FlyingBullet();
            }

            if (timecounter >= t2)
            {
                bullets[1].FlyingBullet();
            }

            if (timecounter >= t3)
            {
                bullets[2].FlyingBullet();
            }
        }
        //SCHIET NAAR RECHTS NA EEN INGEGEVEN TIJDS INTERVAL. WORDT GEBRUIKT OM DE VIJAND EEN SCRIPT TE GEVEN OP HET LAATSTE LEVEL
        public void ShootRight(int t1, int t2, int t3)
        {
            for (int i = 3; i < 6; i++)
            {
                if (!bullets[i].boolFlyRight)
                {
                    bullets[i].Position.X = Position.X + 75;
                    bullets[i].Position.Y = Position.Y + 50;
                    bullets[i].boolFlyLeft = false;
                    bullets[i].boolFlyRight = true;
                }


            }

            if (timecounter >= t1)
            {

                bullets[3].FlyingBullet();
            }

            if (timecounter >= t2)
            {
                bullets[4].FlyingBullet();
            }

            if (timecounter >= t3)
            {
                bullets[5].FlyingBullet();
            }
        }
        //DE SCRIPT DIE DE EINDBAAS IN HET LAASTE LEVEL AFSPEELT IN EEN LOOP. DE SCRIPT DUURT TELKENS 25 SECONDEN
        public void Script(ContentManager Content)
        {
            if (timecounter < 11)
            {
                ShootLeft(1, 3, 5);
                ShootRight(2, 4, 6);
                TeleportRechts(9);
            }

            if (timecounter == 10)
            {
                for (int i = 0; i < 3; i++)
                {
                    bullets[i].boolFlyLeft = false;
                }
            }

            ShootLeft(12, 13, 14);

            if (timecounter == 17)
            {
                TeleportMidden();
            }

            if (timecounter == 18)
            {
                _texture = Content.Load<Texture2D>("vegeta_standing");
                textActive = true;

            }

            if (timecounter == 22)
            {
                textActive = false;
                _texture = Content.Load<Texture2D>("vegeta_charge");
            }

            if (timecounter == 25)
            {
                for (int i = 0; i < 3; i++)
                {
                    bullets[i].boolFlyLeft = false;
                }

                for (int i = 3; i < 6; i++)
                {
                    bullets[i].boolFlyRight = false;
                }

                timecounter = 0;
            }

            if (health == 0)
            {
                textActive = false;
            }



        }

        //RESET ALLES ZODAT HET OPNIEUW GESPEELD KAN WORDEN ALS DE SPELER OPNIEUW WILT SPELEN NADAT HIJ WINT
        public void resetBoss()
        {
            timecounter = 0;
            textActive = false;

            for (int i = 0; i < 3; i++)
            {
                bullets[i].boolFlyLeft = false;
            }

            for (int i = 3; i < 6; i++)
            {
                bullets[i].boolFlyRight = false;
            }

            TeleportMidden();

            health = 10;



        }




    }
}
