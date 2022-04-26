using LearnOpenTK.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace UTS
{
    internal class windows : GameWindow
    {


       
        Camera camera;
        bool done = true;
       
        Vector3 _objectPos = new Vector3 (0.0f,0.0f, 0.0f);
        float _rotationSpeed = 1f;
        Matrix4 temp = Matrix4.Identity;
        float time_baling = 0f;
        float time_render1 = 0f;
        float time_render2 = 0f;
        float time_render3 = 0f;
       
        int time_translate = 1;
       
        




        public static class Constants
        {
            
            public const string path = "../../../shader/";
        }
       
        
        Asset3d kanva = new Asset3d();

        
        Asset3d draw = new Asset3d();
        Terrain terrain = new Terrain();
        MissileWorm worm = new MissileWorm();
        HeliWorm heliworm = new HeliWorm();
        PlaneWorm planeworm = new PlaneWorm();
        
        public windows(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            //segitiga
            base.OnLoad();
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.0f,0.8f,1.0f,1.0f);
           
            terrain.map = terrain.CreateTerain();
            kanva.AddChild(terrain.map);

            worm.chara = worm.Createworm();
            kanva.AddChild(worm.chara);
            worm.chara.rotate(worm.chara._centerPosition, worm.chara._euler[1], 90f);

            heliworm.chara2 = heliworm.Createheliworm2();
            kanva.AddChild(heliworm.chara2);
            heliworm.chara2.rotate(heliworm.chara2._centerPosition, heliworm.chara2._euler[1], 90f);

            
            kanva.AddChild(draw);

            

            planeworm.chara3 = planeworm.Createplaneworm();
            kanva.AddChild(planeworm.chara3);
            planeworm.chara3.rotate(planeworm.chara3._centerPosition, planeworm.chara3._euler[1], 90f);
            planeworm.chara3.Child[4].rotate(planeworm.chara3.Child[6]._centerPosition, planeworm.chara3.Child[6]._euler[0], 90f);
            planeworm.chara3.Child[5].rotate(planeworm.chara3.Child[7]._centerPosition, planeworm.chara3.Child[7]._euler[0], 270f);

            kanva.Load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            


            camera = new Camera(new Vector3(1, 2, 5), Size.X / Size.Y);







           

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            
            Matrix4 temp2 = Matrix4.Identity;
            
           
            
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            


            //animasi1
            if (rotateheli(done))
            {
                animasiBaling();
            }

            //animasi2
            jalan();

            //awan();

            //animasi3
            if (rotateSayap(done))
            {
                rotatepesawat();

            }





            kanva.render(1, camera.GetViewMatrix(), camera.GetProjectionMatrix());
            



            SwapBuffers();
        }
        protected bool rotatelagi(bool done)
        {
            if (time_render2 <= 13)
            {
                done = false;
                worm.chara.rotate(worm.chara.Child[0]._centerPosition, worm.chara.Child[0]._euler[1], time_render2);
                time_render2 += 1f;
               
            }
            else
            {
                done = true;


            }
            return done;


        }
        protected bool animasimissile(bool done)
        {
            if (time_render2 <= 100)
            {
                done = false;
                worm.chara.rotated(terrain.map.Child[0]._centerPosition, terrain.map.Child[0]._euler[1], 20f);
            }
            else
            {
                done = true;


            }
            return done;


        }

        public void animasiBaling()
        {
            


                done = false;
                if (time_baling <= 30f)
                {
                    heliworm.chara2.Child[8].rotate(heliworm.chara2.Child[7]._centerPosition, heliworm.chara2.Child[8]._euler[1], time_baling);
                    heliworm.chara2.Child[9].rotate(heliworm.chara2.Child[7]._centerPosition, heliworm.chara2.Child[9]._euler[1], time_baling);
                    if (time_baling <= 30f)
                    {
                        heliworm.chara2.translate(0, 0.1f, 0);

                    }
                    time_baling += 1f;
                }
                else
                {
                    heliworm.chara2.Child[8].rotate(heliworm.chara2.Child[7]._centerPosition, heliworm.chara2.Child[8]._euler[1], 30f);
                    heliworm.chara2.Child[9].rotate(heliworm.chara2.Child[7]._centerPosition, heliworm.chara2.Child[9]._euler[1], 30f);
                    heliworm.chara2.rotated(terrain.map.Child[1].Child[0]._centerPosition, terrain.map.Child[1].Child[0]._euler[1], 20f);
                }
                
               
            
          

        }

        protected bool rotateheli(bool done)
        {
            if (time_render1 <= 18)
            {
                done = false;
                heliworm.chara2.rotate(heliworm.chara2.Child[0]._centerPosition, heliworm.chara2.Child[0]._euler[1], time_render1);
                time_render1 += 1f;
            
            }
            else
            {
                done = true;


            }
            return done;
        }
        protected void rotatepesawat()
        {
            
                
            planeworm.chara3.rotate(terrain.map.Child[1].Child[0]._centerPosition, terrain.map.Child[1].Child[0]._euler[1], -30f);




        }
        protected bool rotateSayap(bool done)
        {
            if (time_render3 <= 12f)
            {
                done = false;
                planeworm.chara3.Child[4].rotate(planeworm.chara3.Child[6]._centerPosition, planeworm.chara3.Child[6]._euler[0], -time_render3);
                planeworm.chara3.Child[5].rotate(planeworm.chara3.Child[7]._centerPosition, planeworm.chara3.Child[7]._euler[0], time_render3);
                time_render3 += 1f;
               
            }
            else
            {
                done = true;


            }
            return done;
        }
        public void jalan()
        {

            if (worm.chara._centerPosition.Z < -2 || worm.chara._centerPosition.Z > 2)
            {
                time_translate = -1;

            }
            else if (worm.chara._centerPosition.Z < -0.5 || worm.chara._centerPosition.Z > 0.5)
            {
                worm.chara.Child[5].rotated(worm.chara.Child[4]._centerPosition, worm.chara.Child[5]._euler[0], 20f);
                worm.chara.Child[6].rotated(worm.chara.Child[4]._centerPosition, worm.chara.Child[6]._euler[0], 20f);
            }
            worm.chara.translate(0, 0, 0.01f * time_translate);
        }
      

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            camera.AspectRatio = Size.X / (float)Size.Y;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            camera.Fov = camera.Fov - e.OffsetY;
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();    
            }
            float cameraSpeed = 2f;
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                camera.Position += camera.Front * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                camera.Position -= camera.Front * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                camera.Position -= camera.Right * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                camera.Position += camera.Right * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.Q))
            {
                camera.Position += camera.Up * cameraSpeed * (float)args.Time;
            }
            if (KeyboardState.IsKeyDown(Keys.E))
            {
                camera.Position -= camera.Up * cameraSpeed * (float)args.Time;
            }
           


            var mouse = MouseState;
           
            if (KeyboardState.IsKeyDown(Keys.Comma))
            {
                var axis = new Vector3(0, 1, 0);
                camera.Position -= _objectPos;
                camera.Yaw -= _rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed)
                    .ExtractRotation());
                camera.Position += _objectPos;

                camera._front = -Vector3.Normalize(camera.Position - _objectPos);
            }
            if (KeyboardState.IsKeyDown(Keys.K))
            {
                var axis = new Vector3(1, 0, 0);
                camera.Position -= _objectPos;
                camera.Pitch -= _rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, _rotationSpeed).ExtractRotation());
                camera.Position += _objectPos;
                camera._front = -Vector3.Normalize(camera.Position - _objectPos);
            }
            if (KeyboardState.IsKeyDown(Keys.M))
            {
                var axis = new Vector3(1, 0, 0);
                camera.Position -= _objectPos;
                camera.Pitch += _rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed).ExtractRotation());
                camera.Position += _objectPos;
                camera._front = -Vector3.Normalize(camera.Position - _objectPos);
            }

        }

       
        public Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatix = new Matrix4
            (
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );

            return secretFormulaMatix;
        }
    }
}
