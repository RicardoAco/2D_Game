using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace dragonball
{
    class Healthbar : Sprite
    {
        //TEKENT DE HEALTHBAR
        int healthBarHealth;
        string healthString;
        public Healthbar(string imageSrc, Vector2 position, Color color, float speed) : base(imageSrc, position, color, speed)
        {
        }


    }
}
