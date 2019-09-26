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

    
    /*HIER WORDT GEKEKEN OF ER GESPEELD MAG WORDEN OF NIET EN OOK CONTROLEERT DEZE IN WELK LEVEL JE ZIT EN INDIEN HET UITGESPEELD IS DAN WORDT ALLES GERESET*/
    public class CheckGameStatus
    {
      
        public Menu gameMenu = new Menu();

        levelOne firstLevel = new levelOne();
        LevelTwo secondLevel = new LevelTwo();
        FinalLevel finalLevel = new FinalLevel();

        public void InitInitialize()
        {
            firstLevel.InitInitialize();
            secondLevel.InitInitialize();
            finalLevel.InitInitialize();
        }

        public void LoadContent(ContentManager Content)
        {
            gameMenu.LoadContent(Content);
            firstLevel.LoadContent(Content);
            secondLevel.LoadContent(Content);
            finalLevel.LoadContent(Content);
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            gameMenu.Update(gameTime);

            //SPEL START
            if (gameMenu.play)
            {
                UpdateLevels(gameTime, Content);
            }

            //RESET
            if (finalLevel.EndMenu)
            {
                gameMenu.play = false;
                gameMenu.texture = Content.Load<Texture2D>("endMenu");
                firstLevel.player.resetPosPlayer(0, 355);
                secondLevel.player.resetPosPlayer(0, 355);
            }
            if (gameMenu.menuInput.start)
            {
                secondLevel.finalLevel = false;
                firstLevel.nextLevelBool = false;
                finalLevel.EndMenu = false;
            }



        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!gameMenu.play)
            {
                gameMenu.Draw(spriteBatch);
            }


            if (gameMenu.play)
            {
                DrawLevels(spriteBatch);

            }

            //reset
            if (gameMenu.menuInput.start)
            {
                for (int i = 0; i < 3; i++)
                {
                    firstLevel.buu[i].RespawnPosition();
                    secondLevel.buu[i].RespawnPosition();
                }

                finalLevel.boss.resetBoss();
            }

        }

        //KIJKT NA IN WELK LEVEL JE MOET ZITTEN DOOR DE BOOLEAN EN VOERT DEZE DE UPDATES VAN DIT LEVEL UIT
        private void UpdateLevels(GameTime gameTime, ContentManager Content)
        {

            if (firstLevel.nextLevelBool == false && finalLevel.EndMenu == false)
            {
                firstLevel.Update(gameTime, Content);
            }

            if (firstLevel.nextLevelBool == true && finalLevel.EndMenu == false)
            {
                secondLevel.Update(gameTime, Content);
            }
            if (secondLevel.finalLevel == true && finalLevel.EndMenu == false)
            {
                finalLevel.Update(gameTime, Content);
            }


        }

        //KIJKT NA IN WELK LEVEL JE MOET ZITTEN DOOR DE BOOLEAN EN VOERT DEZE DE DRAW VAN DIT LEVEL UIT
        private void DrawLevels(SpriteBatch spriteBatch)
        {

            if (firstLevel.nextLevelBool == false && finalLevel.EndMenu == false)
            {
                firstLevel.Draw(spriteBatch);
            }
            if (firstLevel.nextLevelBool == true && finalLevel.EndMenu == false)
            {
                secondLevel.Draw(spriteBatch);
            }
            if (secondLevel.finalLevel == true && finalLevel.EndMenu == false)
            {
                finalLevel.Draw(spriteBatch);
            }


        }




    }
}
