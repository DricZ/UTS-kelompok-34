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
    internal class PlaneWorm : Asset3d
    {
        public Asset3d chara3 = new Asset3d();
        public Asset3d Createplaneworm()
        {
            Asset3d draw3 = new Asset3d();
            Asset3d worm3 = new Asset3d();
            //head
            draw3.createEllipsoid2(0.3f, 0.3f, 0.3f, -0.9f, 0.0f, -3.0f, 10, 10);
            draw3.setColor(new Vector3(121, 4, 199));
            worm3.AddChild(draw3);
            //eye1
            draw3 = new Asset3d();
            draw3.createEllipsoid(0.1f, 0.1f, 0.1f, -1.2f, 0.1f, -2.8f);
            draw3.setColor(new Vector3(6, 17, 79));
            worm3.AddChild(draw3);

            
          
            //eye2
            draw3 = new Asset3d();
            draw3.createEllipsoid(0.1f, 0.1f, 0.1f, -1.2f, 0.1f, -3.2f);
            draw3.setColor(new Vector3(6, 17, 79));
            worm3.AddChild(draw3);
            //worm31
            draw3= new Asset3d();
            draw3.createEllipsoid2(0.3f, 0.3f, 0.3f, -0.6f, 0.0f, -3.0f, 10, 10);
            draw3.setColor(new Vector3(157, 44, 232));
            worm3.AddChild(draw3);
            //sayapkanan
            draw3= new Asset3d();
            draw3.createwingvertices(-0.3f, 0.21f,-2.0f, 0.3f);
            draw3.setColor(new Vector3(218, 151, 254));
            worm3.AddChild(draw3);
            //sayapkiri
            draw3= new Asset3d();
            draw3.createwingvertices(-0.3f, 0.21f, -4.0f, 0.3f);
            draw3.setColor(new Vector3(218, 151, 254));
            worm3.AddChild(draw3);
            //Cylinder kanan
            draw3= new Asset3d();
            draw3.createCylinder2(0.2f, 0.2f, 0.5f, -0.3f, 0.15f, -2.65f);
            draw3.setColor(new Vector3(217, 0, 119));
            draw3.rotate(draw3._centerPosition, draw3._euler[2], 90f);
            worm3.AddChild(draw3);
            //Cylinder kiri
            draw3= new Asset3d();
            draw3.createCylinder2(0.2f, 0.2f, 0.5f, -0.3f, 0.15f, -3.45f);
            draw3.setColor(new Vector3(217, 0, 119));
            draw3.rotate(draw3._centerPosition, draw3._euler[2], 90f);
            worm3.AddChild(draw3);
            //sungut
            draw3= new Asset3d();
            draw3.createCylinder2(0.01f, 0.01f, 0.8f, -1.0f, 0.15f, -2.9f);
            draw3.setColor(new Vector3(0, 0, 255));
            draw3.rotate(draw3._centerPosition, draw3._euler[2], 30f);
            draw3.rotate(draw3._centerPosition, draw3._euler[0], 30f);
            worm3.AddChild(draw3);
            //sungut
            draw3= new Asset3d();
            draw3.createCylinder2(0.01f, 0.01f, 0.8f, -1.0f, 0.15f, -3.1f);
            draw3.setColor(new Vector3(0, 0, 255));
            draw3.rotate(draw3._centerPosition, draw3._euler[2], 30f);
            draw3.rotate(draw3._centerPosition, draw3._euler[0], -30f);
            worm3.AddChild(draw3);
            //worm332
            draw3= new Asset3d();
            draw3.createEllipsoid2(0.3f, 0.3f, 0.3f, -0.3f, 0.0f, -3.0f, 10, 10);
            draw3.setColor(new Vector3(157, 44, 232));
            worm3.AddChild(draw3);
            //worm333
            draw3= new Asset3d();
            draw3.createEllipsoid2(0.3f, 0.3f, 0.3f, 0.0f, 0.0f, -3.0f, 10, 10);
            draw3.setColor(new Vector3(157, 44, 232));
            worm3.AddChild(draw3);

            //draw3 = new Asset3d(new List<Vector3> { (0.5f, 0.05f, -3f), (0.4f, 0.1f, -3f), (0.3f, 0.15f, -3f) }, new List<uint> { });
            //draw3.setColor(new Vector3(0, 0, 0));
            //draw3.createCurveBezier();
            //worm3.AddChild(draw3);

            return worm3;
        }
    }
}
