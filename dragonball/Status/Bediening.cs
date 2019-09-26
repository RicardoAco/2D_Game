using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragonball
{

    /*HIER WORDT DE BEDIENING GECONTROLEERD*/
    public abstract class Bediening
    {
        public bool left { get; set; }
        public bool right { get; set; }

        public bool space { get; set; }

        public bool down { get; set; }

        public bool start { get; set; }

        public bool exit { get; set; }

        public bool shoot { get; set; }

        public bool state;
        public abstract void Update();
    }

    /*ZODRA EEN INPUT WORDT INGEVOERD VAN HET TOESTENBORD DAN ZAL ER EEN BOLEAN VAN TERUG WORDEN GEGEVEN*/
    public class BedieningPijltjes : Bediening
    {

        public override void Update()
        {
            KeyboardState stateKey = Keyboard.GetState();

            if (stateKey.IsKeyDown(Keys.Left))
            {
                left = true;
            }
            if (stateKey.IsKeyUp(Keys.Left))
            {
                left = false;
            }

            if (stateKey.IsKeyDown(Keys.Right))
            {
                right = true;
            }
            if (stateKey.IsKeyUp(Keys.Right))
            {
                right = false;
            }

            if (stateKey.IsKeyDown(Keys.Space))
            {
                space = true;
            }
            if (stateKey.IsKeyUp(Keys.Space))
            {
                space = false;
            }

            if (stateKey.IsKeyDown(Keys.Down))
            {
                down = true;
            }
            if (stateKey.IsKeyUp(Keys.Down))
            {
                down = false;
            }

            if (stateKey.IsKeyDown(Keys.S))
            {
                start = true;
            }
            if (stateKey.IsKeyUp(Keys.S))
            {
                start = false;
            }

            if (stateKey.IsKeyDown(Keys.E))
            {
                exit = true;
            }
            if (stateKey.IsKeyUp(Keys.E))
            {
                exit = false;
            }

            if (stateKey.IsKeyDown(Keys.D))
            {
                shoot = true;
            }
            if (stateKey.IsKeyUp(Keys.D))
            {
                shoot = false;
            }

        }
    }
}
