using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS
{
    internal class HeliWorm : Asset3d
    {
        public Asset3d chara2 = new Asset3d();
        float time_render = 0f;
        float time_baling = 0f;

        public Asset3d Createheliworm2()
        {
            //Missileworm2
            //head
            Asset3d draw2 = new Asset3d();
            Asset3d worm2 = new Asset3d();
            //head
            draw2.createEllipsoid2(0.3f, 0.3f, 0.3f, -0.9f, 0.0f, 3.0f, 10, 10);
            draw2.setColor(new Vector3(81, 41, 0));
            worm2.AddChild(draw2);
            //eye1
            draw2 = new Asset3d();
            draw2.createEllipsoid(0.1f, 0.1f, 0.1f, -1.2f, 0.1f, 3.2f);
            draw2.setColor(new Vector3(0, 0, 0));
            worm2.AddChild(draw2);
            //eye2
            draw2 = new Asset3d();
            draw2.createEllipsoid(0.1f, 0.1f, 0.1f, -1.2f, 0.1f, 2.8f);
            draw2.setColor(new Vector3(0, 0, 0));
            worm2.AddChild(draw2);
            //body1
            draw2 = new Asset3d();
            draw2.createEllipsoid2(0.3f, 0.3f, 0.3f, -0.6f, 0.0f, 3.0f, 10, 10);
            draw2.setColor(new Vector3(137, 76, 16));
            worm2.AddChild(draw2);
            //Anunya Capit
            draw2 = new Asset3d();
            draw2.createboxvertices(-0.3f, 0.5f, 3.0f, 0.5f);
            draw2.setColor(new Vector3(81, 41, 0));
            worm2.AddChild(draw2);

            //Capit1
            draw2 = new Asset3d();
            draw2.createHalfEllipsoid(0.03f, 0.1f, 1.0f, -1.5f, 0.5f, 3.15f);
            draw2.setColor(new Vector3(95, 0, 189));
            draw2.rotate(draw2._centerPosition, draw2._euler[1], 100f);
            draw2.rotate(draw2._centerPosition, draw2._euler[2], 90f);
            worm2.AddChild(draw2);

            //Capit2
            draw2 = new Asset3d();
            draw2.createHalfEllipsoid(0.03f, 0.1f, 1.0f, -1.5f, 0.5f, 2.85f);
            draw2.setColor(new Vector3(95, 0, 189));
            draw2.rotate(draw2._centerPosition, draw2._euler[1], 80f);
            draw2.rotate(draw2._centerPosition, draw2._euler[2], 270f);
            worm2.AddChild(draw2);

            //Tangkaiheli
            draw2 = new Asset3d();
            draw2.createRectangularVertices(-0.3f, 0.9f, 3.0f, 0.1f);
            draw2.setColor(new Vector3(135, 70, 70));
            //builder.rotate(builder._centerPosition, builder._euler[1], -90f);
            worm2.AddChild(draw2);

            //Heli1
            draw2 = new Asset3d();
            draw2.createHalfEllipsoid(0.1f, 0.1f, 1.5f, -0.3f, 1.0f, 3.0f);
            draw2.setColor(new Vector3(246, 237, 219));
            //builder.rotate(builder._centerPosition, builder._euler[1], 80f);
            draw2.rotate(draw2._centerPosition, draw2._euler[1], 90f);
            worm2.AddChild(draw2);

            //Heli2
            draw2 = new Asset3d();
            draw2.createHalfEllipsoid(0.1f, 0.1f, 1.5f, -0.3f, 1.0f, 3.0f);
            draw2.setColor(new Vector3(246, 237, 219));
            //builder.rotate(builder._centerPosition, builder._euler[1], 100f);
            //builder.rotate(builder._centerPosition, builder._euler[2], 270f);
            worm2.AddChild(draw2);

            //body2
            draw2 = new Asset3d();
            draw2.createEllipsoid2(0.3f, 0.3f, 0.3f, -0.3f, 0.0f, 3.0f, 10, 10);
            draw2.setColor(new Vector3(137, 76, 16));
            worm2.AddChild(draw2);
            //body3
            draw2 = new Asset3d();
            draw2.createEllipsoid2(0.3f, 0.3f, 0.3f, 0.0f, 0.0f, 3.0f, 10, 10);
            draw2.setColor(new Vector3(137, 76, 16));
            worm2.AddChild(draw2);


            return worm2;
        }

    }
}
