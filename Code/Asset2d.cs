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
    internal class Asset2d
    {
        //float[] _verticesSquare =
        //{
        //    //x   //y   //z
        //    0.5f,0.5f,0.0f,//vertex1
        //    0.5f,-0.5f,0.0f,//
        //    -0.5f,-0.5f,0.0f,
        //    -0.5f,0.5f,0.0f

        //};
        float[] _verticestriangel =
           {
               //x   //y   //z
             

           };
        uint[] _indices = //untuk mengabungkan gambar
        {
            
        };
        int _elementbuffer;
        int _vertexBufferObject;
        int _vertexArrayObject;
        int index;
        int[] _pascal = {};
        Shader _shader;
        Vector3 _color;
        public Asset2d(float[] verctices, uint[] indices)
        {
            _verticestriangel = verctices;
            _indices = indices;
        }
        public Asset2d()
        {
            _verticestriangel = new float[1080];
            index = 0;
        }
        public void Load(string shadervert,string shaderfrag)
        {
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _verticestriangel.Length * sizeof(float), _verticestriangel, BufferUsageHint.StaticDraw);
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            ////Mereperensentasikan satu baris  jika tulisanya 6  = isi baris 6
            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            //GL.EnableVertexAttribArray(0);
            //////Untuk mmebuat komputer membaca warna dari index 3
            //GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            //////GL v lek ^ di isi 1 maka V juga 1
            //GL.EnableVertexAttribArray(1);

            //persegi
            //vertexElementObject = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ArrayBuffer, vertexElementObject);
            //GL.BufferData(BufferTarget.ArrayBuffer, _verticesSquare.Length * sizeof(float), _verticesSquare, BufferUsageHint.StaticDraw);
            //vertexArrayObject = GL.GenVertexArray();
            //GL.BindVertexArray(vertexArrayObject);
            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            //GL.EnableVertexAttribArray(0);
            //_elementbuffer = GL.GenBuffer();
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementbuffer);
            //GL.BufferData(BufferTarget.ElementArrayBuffer,_indices.Length * sizeof(uint), _indices,BufferUsageHint.StaticDraw);

            if (_indices.Length != 0)
            {
                _elementbuffer = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementbuffer);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
                
            }
            //_shader = new Shader("C:/Users/HP-Omen/Documents/petra/GrafKom/pert1/pert1/shader/shader.vert", "C:/Users/HP-Omen/Documents/petra/GrafKom/pert1/pert1/shader/shader.frag");
            _shader = new Shader(shadervert, shaderfrag);
            _shader.Use();
        }
        public void setColor(Vector3 color)
        {
            _color.X = color.X / 255f;
            _color.Y = color.Y / 255f;
            _color.Z = color.Z / 255f;
        }
        public void render(int _lines)
        {
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            if (_indices.Length != 0)
            {
                GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            }
            else
            {

                if (_lines == 0)
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
                }
                else if (_lines == 1)
                {

                    GL.DrawArrays(PrimitiveType.TriangleFan, 0, (_verticestriangel.Length + 1) / 3);
                }
                else if(_lines == 2)
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, index);
                }
                else if(_lines == 3) 
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, (_verticestriangel.Length + 1) / 3);
                }
            }

        }
        public void createCircle(float center_x, float center_y, float radius)
        {
            _verticestriangel = new float[1080];
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                //x
                _verticestriangel[i * 3] = radius * (float)Math.Cos(degInRad) + center_x;
                //y
                _verticestriangel[i * 3 + 1] = radius * (float)Math.Sin(degInRad) + center_y;
                //z
                _verticestriangel[i * 3 + 2] = 0;
            }
        }
        public void createElips(float center_x, float center_y,float radiusx,float radiusy)
        {
            _verticestriangel = new float[1080];
            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                //x
                _verticestriangel[i * 3] = radiusx * (float)Math.Cos(degInRad) + center_x;
                //y
                _verticestriangel[i * 3 + 1] = radiusy * (float)Math.Sin(degInRad) + center_y;
                //z
                _verticestriangel[i * 3 + 2] = 0;
            }
        }
        public void updatemousePosition(float _x, float _y, float _z)
        {

            _verticestriangel[index * 3] = _x;
            _verticestriangel[index * 3 + 1] = _y;  
            _verticestriangel[index * 3 + 2] = _z;
            index++;
            GL.BufferData(BufferTarget.ArrayBuffer, index * 3 * sizeof(float), _verticestriangel, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }
        public List<int> getRow(int RowIndex) 
        {
            List<int> currow = new List<int>();
            currow.Add(1);
            if(RowIndex == 0)
            {
                return currow;
            }
            List<int> prev= getRow(RowIndex - 1);
            for(int i = 1;i< prev.Count; i++)
            {
                int curr = prev[i-1] + prev[i];
                currow.Add(curr);
            }
            currow.Add(1);
            return currow;
        }
        public List<float> CreateCurveBezier()
        {
            List<float> _verticesBezier= new List<float>();
            List<int> pascal = getRow(index - 1);
            _pascal = pascal.ToArray();
            for(float f = 0; f < 1.0f; f += 0.01f)
            {
                Vector2 p = getP(index,f);
                _verticesBezier.Add(p.X);
                _verticesBezier.Add(p.Y);
                _verticesBezier.Add(0);
            }
            return _verticesBezier;
        }
        public Vector2 getP(int n,float f)
        {
            Vector2 p = new Vector2(0,0);
            float[] k = new float[n];
            for(int i = 0; i < n; i++)
            {
                k[i] = (float)Math.Pow((1-f),n-1-i) * (float)Math.Pow(f,i) * _pascal[i];
                p.X += k[i] * _verticestriangel[i * 3];
                p.Y += k[i] * _verticestriangel[i * 3 + 1];
            }
            return p;
        }
        public bool getVerticeslenght()
        {
            if(_verticestriangel[0] == 0)
            {
                return false;
            }
            if ((_verticestriangel.Length + 1) / 3 > 0l)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void setVertices(float[] vertices)
        {
            _verticestriangel = vertices;
        }
    }
   
}
