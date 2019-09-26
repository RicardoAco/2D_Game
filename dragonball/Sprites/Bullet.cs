using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace dragonball
{
    public class Bullet : Sprite
    {
        public bool boolFlyRight;
        public bool boolFlyLeft;

        public Bullet(string imageSrc, Vector2 position, Color color, float speed) : base(imageSrc, position, color, speed)
        {

        }

        public void FlyingBullet()
        {
            FlyRight();
            FlyLeft();
        }

        public void FlyRight()
        {
            if (boolFlyRight)
            {
                Position.X += Speed;

            }
        }

        public void FlyLeft()
        {
            if (boolFlyLeft)
            {
                Position.X -= Speed;

            }
        }

        public void RespawnBullet()
        {
            Position.X = 2000; //OUT OF MAP
            Position.Y = 2000; //OUT OF MAP

            boolFlyRight = false;
            boolFlyLeft = false;
        }
    }
}
