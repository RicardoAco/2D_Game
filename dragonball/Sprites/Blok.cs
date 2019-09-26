using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragonball
{
    class Blok : Sprite
    {
        /*DIT DIENT OM EEN OBSTAKEL TE PLAATSEN*/
        public Blok(string imageSrc, Vector2 position, Color color, float speed) : base(imageSrc, position, color, speed)
        {

        }        
    }
}
