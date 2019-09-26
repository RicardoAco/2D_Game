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
    /*DIT IS HET EERSTE LEVEL VOLLEDIG GETEKEND*/
    class levelOne
    {


        //NEXT LEVEL BOOL
        public bool nextLevelBool = false;
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
        Vector2 startPositionPlayer = new Vector2(20, 500);
        //POSITION ENEMY 1
        Vector2 startPositionEnemy_1 = new Vector2(525, 499);
        Vector2 startPositionEnemy_2 = new Vector2(215, 261);
        Vector2 startPositionEnemy_3 = new Vector2(220, 70);

        //POSITION TRIGGER
        Vector2 posTrigger = new Vector2(900, 90);

        #region POSITION BLOCKS
        //POSISTON BLOCKS ONDERAAN
        Vector2 PB0 = new Vector2(160, 521);
        Vector2 PB1 = new Vector2(200, 485);
        Vector2 PB2 = new Vector2(920, 500);
        Vector2 PB3 = new Vector2(881, 521);
        //POSISTON BLOCKS MIDDEN
        Vector2 PB4 = new Vector2(0, 281);
        Vector2 PB5 = new Vector2(720, 410);

        //HOOG
        Vector2 PB6 = new Vector2(0, 321);

        //HOOGST
        Vector2 PB7 = new Vector2(240, 130);
        Vector2 PB8 = new Vector2(100, 170);
        Vector2 PB9 = new Vector2(210, 130);
        Vector2 PB10 = new Vector2(440, 130);
        Vector2 PB11 = new Vector2(780, 130);
        Vector2 PB12 = new Vector2(210, 321);
        Vector2 PB13 = new Vector2(920, 321);
        Vector2 PB14 = new Vector2(500, 321);
        #endregion

        //POSISTION BACKGROUND
        Vector2 _backgroundPosition = new Vector2(0, 0);

        //SPRITES
        public Player player;
        public Enemy[] buu = new Enemy[3];
        Blok grass_collision;
        Blok collision_left;
        Blok collision_right;

        //ARRAY BLOCKS
        Blok[] blokjes = new Blok[100];

        //NEXT LEVEL
        Rooshi trigger;


        public void InitInitialize()
        {
            //POSITIE BLOKJES
            #region POSISTION BLOCKS
            //INIT BLOCKS ONDERAAN
            blokjes[1] = new Blok("ground_dirt_small", PB0, kleur, 0f); //YES
            blokjes[0] = new Blok("ground_dirt_smaller", PB1, kleur, 0f); //YES

            blokjes[2] = new Blok("ground_dirt_block_high", PB2, kleur, 0f); //YES
            blokjes[3] = new Blok("ground_dirt_block", PB3, kleur, 0f); //YES

            //INIT BLOCKS MIDDEN
            blokjes[6] = new Blok("ground_dirt_smaller", PB6, kleur, 0f); //YES
            blokjes[12] = new Blok("ground_dirt_small", PB12, kleur, 0f); //YES
            blokjes[13] = new Blok("ground_dirt_smaller", PB13, kleur, 0f); //YES
            blokjes[5] = new Blok("ground_dirt_smaller", PB5, kleur, 0f); //YES
            blokjes[14] = new Blok("ground_dirt_small", PB14, kleur, 0f);

            //HOOG
            blokjes[4] = new Blok("ground_dirt_block_high", PB4, kleur, 0f);

            //HOOGST
            blokjes[7] = new Blok("ground_dirt_small", PB7, kleur, 0f);
            blokjes[8] = new Blok("ground_dirt_smaller", PB8, kleur, 0f);
            blokjes[9] = new Blok("ground_dirt_block_high", PB9, kleur, 0f);
            blokjes[10] = new Blok("ground_dirt_small", PB10, kleur, 0f);
            blokjes[11] = new Blok("ground_dirt_small", PB11, kleur, 0f);
            #endregion

            //INIT GRASS
            grass_collision = new Blok("grass_collision", _grass_collision, kleur, 0f);

            //INIT BARRIERS
            collision_left = new Blok("collision_left_right", _collision_left, kleur, 0f);
            collision_right = new Blok("collision_left_right", _collision_right, kleur, 0f);

            //INIT PLAYER
            player = new Player("idle_right", startPositionPlayer, kleur, 3f);

            //INIT ENEMY
            buu[0] = new Enemy("buu_right", startPositionEnemy_1, kleur, 0.5f, 260);
            buu[1] = new Enemy("buu_right", startPositionEnemy_2, kleur, 0.5f, 270);
            buu[2] = new Enemy("buu_right", startPositionEnemy_3, kleur, 0.5f, 660);

            //NEXT LEVEL TRIGGER
            trigger = new Rooshi("rooshi", posTrigger, kleur, 0);

            //ADD SPRITES TO LIST OF _SPRITES
            _sprites.Add(player);
            _sprites.Add(buu[0]);
            _sprites.Add(buu[1]);
            _sprites.Add(buu[2]);

            _sprites.Add(grass_collision);
            _sprites.Add(collision_left);
            _sprites.Add(collision_right);

            for (int i = 0; i < 15; i++)
            {
                _sprites.Add(blokjes[i]);
            }
            _sprites.Add(trigger);

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
            //UPDATE ALL SPRITE OBJECTS AT ONCE
            foreach (var sprite in _sprites)
                sprite.Update(Content, gameTime, _sprites);

            //PLAYER vs MAJIN BUU
            for (int i = 0; i < 3; i++)
            {
                //VIJAND AANRAKEN
                if (player.Rectangle.Right >= buu[i].Rectangle.Left - 2 && player.Rectangle.Left <= buu[i].Rectangle.Right + 2 && player.Rectangle.Bottom > buu[i].Rectangle.Top - 2 && player.Rectangle.Top < buu[i].Rectangle.Bottom + 2)
                {
                    player.resetPosPlayer(0, 355);
                }
            }

            //MAJIN BUU vs BULLET
            for (int i = 0; i < 3; i++)
            {
                //VIJAND AANRAKEN
                if (player.bullet.Rectangle.Right >= buu[i].Rectangle.Left - 2 && player.bullet.Rectangle.Left <= buu[i].Rectangle.Right + 2 && player.bullet.Rectangle.Bottom > buu[i].Rectangle.Top - 2 && player.bullet.Rectangle.Top < buu[i].Rectangle.Bottom + 2)
                {
                    player.bullet.RespawnBullet();
                    buu[i].DiePosition();
                }
            }

            //MASTER ROOSHI
            if (player.Rectangle.Right >= trigger.Rectangle.Left - 2 && player.Rectangle.Left <= trigger.Rectangle.Right + 2 && player.Rectangle.Bottom > trigger.Rectangle.Top - 2 && player.Rectangle.Top < trigger.Rectangle.Bottom + 2)
            {
                nextLevelBool = true;
            }

            //BULLET vs BLOCKS
            foreach (var sprite in _sprites)
            {
                if (player.bullet.Rectangle.Right >= sprite.Rectangle.Left - 2 && player.bullet.Rectangle.Left <= sprite.Rectangle.Right + 2 && player.bullet.Rectangle.Bottom > sprite.Rectangle.Top - 2 && player.bullet.Rectangle.Top < sprite.Rectangle.Bottom + 2)
                {
                    player.bullet.RespawnBullet();
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

        //RESET DE PLAYER ZIJN POSITIE INDIEN NODIG IN LEVEL 1

    }
}
