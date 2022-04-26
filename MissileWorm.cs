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
    internal class MissileWorm : Asset3d
    {
       public Asset3d chara = new Asset3d();
       public Asset3d Createworm()
        {
            //Missileworm
            //head
            Asset3d draw = new Asset3d();
            Asset3d worm = new Asset3d();
            draw.createEllipsoid2(0.3f, 0.3f, 0.3f, -0.9f, 0.0f, 0.0f, 10, 10);
            draw.setColor(new Vector3(255, 0, 0));
            worm.AddChild(draw);
            //eye1
            draw = new Asset3d();
            draw.createEllipsoid(0.1f, 0.1f, 0.1f, -1.2f, 0.1f, 0.2f);
            draw.setColor(new Vector3(0, 0, 0));
            worm.AddChild(draw);
            
            //eye2
            draw = new Asset3d();
            draw.createEllipsoid(0.1f, 0.1f, 0.1f, -1.2f, 0.1f, -0.2f);
            draw.setColor(new Vector3(0, 0, 0));
            worm.AddChild(draw);
            //body1
            draw = new Asset3d();
            draw.createEllipsoid2(0.3f, 0.3f, 0.3f, -0.6f, 0.0f, 0.0f, 10, 10);
            draw.setColor(new Vector3(0, 255, 0));
            worm.AddChild(draw);
            //Weapon
            //MissilePod
            draw = new Asset3d();
            draw.createboxvertices(-0.3f, 0.5f, 0.0f, 0.5f);
            draw.setColor(new Vector3(255, 160, 122));
            worm.AddChild(draw);
            //MissileCylinder
            draw = new Asset3d();
            draw.createCylinder2(0.1f, 0.1f, 0.5f, -0.7f, 0.6f, 0.15f);
            draw.setColor(new Vector3(255, 0, 0));
            draw.rotate(draw._centerPosition, draw._euler[2], 90f);
            worm.AddChild(draw);
            //MissileCylinder
            draw = new Asset3d();
            draw.createCylinder2(0.1f, 0.1f, 0.5f, -0.7f, 0.6f, -0.15f);
            draw.setColor(new Vector3(255, 0, 0));
            draw.rotate(draw._centerPosition, draw._euler[2], 90f);
            worm.AddChild(draw);
            //body2
            draw = new Asset3d();
            draw.createEllipsoid2(0.3f, 0.3f, 0.3f, -0.3f, 0.0f, 0.0f, 10, 10);
            draw.setColor(new Vector3(0, 255, 0));
            worm.AddChild(draw);
            //body3
            draw = new Asset3d();
            draw.createEllipsoid2(0.3f, 0.3f, 0.3f, 0.0f, 0.0f, 0.0f, 10, 10);
            draw.setColor(new Vector3(0, 255, 0));
            worm.AddChild(draw);

            //antena
            draw = new Asset3d(new List<Vector3> { (-0.75f, 0.05f, 0f), (-0.75f, 0.1f, 0f), (-0.6f, 0.15f, 0f) }, new List<uint> { });
            draw.setColor(new Vector3(0, 0, 0));
            draw.createCurveBezier();
            worm.AddChild(draw);







            return worm;

        }
        
        
    }
}
