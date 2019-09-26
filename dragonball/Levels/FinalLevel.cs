using dragonball.Sprites;
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

    /*LEVEL 2 VOLLEDIG UITGETEKEND*/
    class FinalLevel
    {

        //TIMER

        //BOOLEAN VOOR ENDMENU TE DISPLAYEN NA BOSS VERSLAGEN IS
        public bool EndMenu = false;

        //COLOR IN VARIABELE
        Color kleur = Color.White;

        //LIST AANMAKEN VOOR ALLE SPRITE OBJECTEN IN TE STEKEN
        List<Sprite> _sprites = new List<Sprite>();

        //GRASS AND COLLISION BARRIERS
        Vector2 _grass_up = new Vector2(0, 50);
        Vector2 _grass_collision = new Vector2(0, 551); //INVISIBLE
        Vector2 _collision_left = new Vector2(-10, 0); //BLUE AND INVISIBLE OF THE LEFT SIDE
        Vector2 _collision_right = new Vector2(960, -80); //BLUE AND INVISIBLE OF THE RIGHT SIDE
        Texture2D _up_grass;
        Texture2D _background;

        //POSITION PLAYER
        Vector2 startPositionPlayer = new Vector2(0, 300);
        //POSITION ENEMY
        Vector2 startPositionBoss = new Vector2(360, 360);



        //POSISTION BACKGROUND
        Vector2 _backgroundPosition = new Vector2(0, 0);

        //SPRITES
        public Player player;
        public BossEnemy boss;
        Blok grass_collision;
        Blok collision_left;
        Blok collision_right;

        //ARRAY BLOCKS
        Blok[] blokjes = new Blok[100];


        #region POSITION BLOCKS
        //POSISTON BLOCKS
        Vector2 PB0 = new Vector2(0, 350);
        Vector2 PB1 = new Vector2(850, 486);
        Vector2 PB2 = new Vector2(800, 521);

        Vector2 PB3 = new Vector2(300, 486);
        Vector2 PB4 = new Vector2(200, 521);
        Vector2 PB5 = new Vector2(400, 521);
        Vector2 PB6 = new Vector2(340, 451);
        Vector2 PB7 = new Vector2(890, 448);

        #endregion
        public void InitInitialize()
        {
            #region POSISTION BLOCKS
            //INIT BLOCKS ONDERAAN
            blokjes[7] = new Blok("ground_dirt_block", PB0, kleur, 0f);
            blokjes[1] = new Blok("ground_dirt_smaller", PB1, kleur, 0f);
            blokjes[6] = new Blok("ground_dirt_small", PB2, kleur, 0f);

            blokjes[3] = new Blok("ground_dirt_small", PB3, kleur, 0f);
            blokjes[4] = new Blok("ground_dirt_small", PB4, kleur, 0f);
            blokjes[5] = new Blok("ground_dirt_small", PB5, kleur, 0f);
            blokjes[2] = new Blok("ground_dirt_smaller", PB6, kleur, 0f);
            blokjes[0] = new Blok("ground_dirt_smaller", PB7, kleur, 0f);
            #endregion

            //INIT GRASS
            grass_collision = new Blok("grass_collision", _grass_collision, kleur, 0f);
            //INIT BARRIERS

            collision_left = new Blok("collision_left_right", _collision_left, kleur, 0f);
            collision_right = new Blok("collision_left_right", _collision_right, kleur, 0f);

            //INIT PLAYER
            player = new Player("idle_right", startPositionPlayer, kleur, 3f);

            //INIT BOSS
            boss = new BossEnemy("vegeta_charge", startPositionBoss, kleur, 0);

            //ADD SPRITES TO LIST OF _SPRITES
            _sprites.Add(player);
            _sprites.Add(boss);
            _sprites.Add(grass_collision);
            _sprites.Add(collision_left);
            _sprites.Add(collision_right);

            for (int i = 0; i < 8; i++)
            {
                _sprites.Add(blokjes[i]);
            }
        }

        public void LoadContent(ContentManager Content)
        {


            //LOAD ALL SPRITE OBJECTS AT ONCE
            foreach (var sprite in _sprites)
                sprite.LoadContent(Content);

            //ACHTERGROND TOEVOEGEN
            _background = Content.Load<Texture2D>("dbz_background");

            //GRAS TOEVOEGEN
            _up_grass = Content.Load<Texture2D>("grass_up");
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {

            //ENDMENU
            if (boss.health == 0)
            {
                player.resetPosPlayer(0, 300);
                EndMenu = true;
            }
            //UPDATE ALL SPRITE OBJECTS AT ONCE
            foreach (var sprite in _sprites)
                sprite.Update(Content, gameTime, _sprites);

            //PLAYER vs BOSS BULLET
            foreach (Bullet bullet in boss.bullets)
            {
                if (player.Rectangle.Right >= bullet.Rectangle.Left - 2 && player.Rectangle.Left <= bullet.Rectangle.Right + 2 && player.Rectangle.Bottom > bullet.Rectangle.Top - 2 && player.Rectangle.Top < bullet.Rectangle.Bottom + 2)
                {
                    player.resetPosPlayer(0, 300);
                    bullet.RespawnBullet();
                    bullet.boolFlyLeft = true;
                    bullet.boolFlyRight = true;
                }
            }

            //PLAYER vs BOSS
            if (player.Rectangle.Right >= boss.Rectangle.Left - 2 && player.Rectangle.Left <= boss.Rectangle.Right + 2 && player.Rectangle.Bottom > boss.Rectangle.Top - 2 && player.Rectangle.Top < boss.Rectangle.Bottom + 2)
            {
                player.resetPosPlayer(0, 300);
            }


            //BOSS vs BULLET
            //VIJAND AANRAKEN
            if (player.bullet.Rectangle.Right >= boss.Rectangle.Left - 2 && player.bullet.Rectangle.Left <= boss.Rectangle.Right + 2 && player.bullet.Rectangle.Bottom > boss.Rectangle.Top - 2 && player.bullet.Rectangle.Top < boss.Rectangle.Bottom + 2)
            {
                player.bullet.RespawnBullet();
                boss.health--;
                if (boss.health <= 0)
                {
                    boss.DiePosition();
                }

            }

            //PLAYER BULLET vs BLOCKS
            foreach (var sprite in _sprites)
            {
                if (player.bullet.Rectangle.Right >= sprite.Rectangle.Left - 2 && player.bullet.Rectangle.Left <= sprite.Rectangle.Right + 2 && player.bullet.Rectangle.Bottom > sprite.Rectangle.Top - 2 && player.bullet.Rectangle.Top < sprite.Rectangle.Bottom + 2)
                {
                    player.bullet.RespawnBullet();
                }
            }

            //BOSS BULLET vs BLOCKS
            foreach (var sprite in _sprites)
            {
                foreach (var bullet in boss.bullets)
                {
                    if (bullet.Rectangle.Right >= sprite.Rectangle.Left - 2 && bullet.Rectangle.Left <= sprite.Rectangle.Right + 2 && bullet.Rectangle.Bottom > sprite.Rectangle.Top - 2 && bullet.Rectangle.Top < sprite.Rectangle.Bottom + 2)
                    {
                        bullet.RespawnBullet();
                        bullet.boolFlyLeft = true;
                        bullet.boolFlyRight = true;
                    }
                }
            }




        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //ACHTERGROND TEKENEN (BACKGROUND LAYER)
            spriteBatch.Draw(_background, _backgroundPosition, Color.White);

            //DRAW ALL SPRITE OBJECTS AT ONCE
            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);

            //GRASS UPPER LAYER
            spriteBatch.Draw(_up_grass, _grass_up, Color.White);
        }
    }
}
