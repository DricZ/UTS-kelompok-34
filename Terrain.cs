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
  
    internal class Terrain : Asset3d
    {
        public Asset3d map = new Asset3d();
        public Asset3d CreateTerain()
        {
            Asset3d draw = new Asset3d();
            Asset3d terrain = new Asset3d();
            Asset3d cloud = new Asset3d();
            Asset3d tree = new Asset3d();
            //Terrain
            //ground
            draw.createCylinder2(10f, 10f, 2f, 0f, -1.5f, -2f);
            draw.setColor(new Vector3(50, 100, 0));
            terrain.AddChild(draw);

            //tree
            //wood
            draw = new Asset3d();
            draw.createCylinder2(0.5f, 0.5f, 2f, -6f, 0.1f, 0f);
            draw.setColor(new Vector3(200, 100, 0));
            tree.AddChild(draw);
            
            //leaves
            draw = new Asset3d();
            draw.createboxvertices(-6f, 1.5f, 0.0f, 1.5f);
            draw.setColor(new Vector3(50, 200, 0));
            tree.AddChild(draw);

            //tree
            //wood
            draw = new Asset3d();
            draw.createCylinder2(0.5f, 0.5f, 2f, 6f, 0.1f, 0f);
            draw.setColor(new Vector3(200, 100, 0));
            tree.AddChild(draw);
            //leaves
            draw = new Asset3d();
            draw.createboxvertices(6f, 1.5f, 0.0f, 1.5f);
            draw.setColor(new Vector3(50, 200, 0));
            tree.AddChild(draw);

            //tree
            //wood
            draw = new Asset3d();
            draw.createCylinder2(0.5f, 0.5f, 2f, 6f, 0.1f, 3f);
            draw.setColor(new Vector3(200, 100, 0));
            tree.AddChild(draw);
            //leaves
            draw = new Asset3d();
            draw.createboxvertices(6f, 1.5f, 3.0f, 1.5f);
            draw.setColor(new Vector3(50, 200, 0));
            tree.AddChild(draw); ;

            //tree
            //wood
            draw = new Asset3d();
            draw.createCylinder2(0.5f, 0.5f, 2f, 6f, 0.1f, 5f);
            draw.setColor(new Vector3(200, 100, 0));
            tree.AddChild(draw);
            //leaves
            draw = new Asset3d();
            draw.createboxvertices(6f, 1.5f, 5.0f, 1.5f);
            draw.setColor(new Vector3(50, 200, 0));
            tree.AddChild(draw);
            terrain.AddChild(tree);

            //awan
            draw = new Asset3d();
            draw.createEllipsoid2(0.8f, 0.8f, 0.8f, 0f, 5f, -4, 10, 10);
            draw.setColor(new Vector3(66, 177, 255));
            cloud.AddChild(draw);

            draw = new Asset3d();
            draw.createEllipsoid2(0.5f, 0.5f, 0.5f, 0.9f, 4.8f, -4, 10, 10);
            draw.setColor(new Vector3(66, 177, 255));
            cloud.AddChild(draw);

            draw = new Asset3d();
            draw.createEllipsoid2(0.5f, 0.5f, 0.5f, -0.9f, 4.8f, -4, 10, 10);
            draw.setColor(new Vector3(66, 177, 255));
            cloud.AddChild(draw);
            terrain.AddChild(cloud);

            //awan2
            draw = new Asset3d();
            draw.createEllipsoid2(0.8f, 0.8f, 0.8f, 10f, 5f, -4, 10, 10);
            draw.setColor(new Vector3(66, 177, 255));
            cloud.AddChild(draw);

            draw = new Asset3d();
            draw.createEllipsoid2(0.5f, 0.5f, 0.5f, 11.0f, 4.8f, -4, 10, 10);
            draw.setColor(new Vector3(66, 177, 255));
            cloud.AddChild(draw);

            draw = new Asset3d();
            draw.createEllipsoid2(0.5f, 0.5f, 0.5f, 9.0f, 4.8f, -4, 10, 10);
            draw.setColor(new Vector3(66, 177, 255));
            cloud.AddChild(draw);

            //awan3
            draw = new Asset3d();
            draw.createEllipsoid2(0.8f, 0.8f, 0.8f, -10f, 5f, -4, 10, 10);
            draw.setColor(new Vector3(66, 177, 255));
            cloud.AddChild(draw);

            draw = new Asset3d();
            draw.createEllipsoid2(0.5f, 0.5f, 0.5f, -9f, 4.8f, -4, 10, 10);
            draw.setColor(new Vector3(66, 177, 255));
            cloud.AddChild(draw);

            draw = new Asset3d();
            draw.createEllipsoid2(0.5f, 0.5f, 0.5f, -11f, 4.8f, -4, 10, 10);
            draw.setColor(new Vector3(66, 177, 255));
            cloud.AddChild(draw);

            terrain.AddChild(cloud);

            return terrain;
        }

    }
}
