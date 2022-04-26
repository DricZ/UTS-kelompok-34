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
    internal class Asset3d
    {
        //float[] _verticesSquare =
        //{
        //    //x   //y   //z
        //    0.5f,0.5f,0.0f,//vertex1
        //    0.5f,-0.5f,0.0f,//
        //    -0.5f,-0.5f,0.0f,
        //    -0.5f,0.5f,0.0f

        //};
        List<Vector3> _vertices = new List<Vector3>();
        List<uint>_indices = new List<uint>();
        int _elementbuffer;
        int _vertexBufferObject;
        int _vertexArrayObject;
        Shader _shader;
        //Matrix4 _view;//camera
        //Matrix4 _projectionMatrix;//settingan camera
        Matrix4 temp;//merubah transformasi
        public Vector3 _centerPosition;
        public List<Vector3> _euler;
        public List<Asset3d> Child;
        Vector3 _color;
        public Asset3d(List<Vector3>vertices,List<uint>indices)
        {
            _vertices = vertices;
            _indices = indices;
            setdefault();
        }
        public static class Constants
        {
            //public const string path = "C:/Users/HP-Omen/Documents/petra/GrafKom/pert1/pert1/shader/";
            public const string path = "../../../shader/";
        }
        public void setdefault()
        {
            temp = Matrix4.Identity;
            _euler = new List<Vector3>();
            //sumbu X
            _euler.Add(new Vector3(1, 0, 0));
            //sumbu y
            _euler.Add(new Vector3(0, 1, 0));
            //sumbu z
            _euler.Add(new Vector3(0, 0, 1));
            
            _centerPosition = new Vector3(0, 0, 0);
            Child = new List<Asset3d>();

        }
        public Asset3d()
        {
            _vertices = new List<Vector3>();
            setdefault();
        }

            public void Load(string shadervert,string shaderfrag,float size_x,float size_y)
        {
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * Vector3.SizeInBytes, _vertices.ToArray(), BufferUsageHint.StaticDraw);
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            if (_indices.Count != 0)
            {
                _elementbuffer = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementbuffer);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Count * sizeof(uint), _indices.ToArray(), BufferUsageHint.StaticDraw);
                
            }


            //_shader = new Shader("C:/Users/HP-Omen/Documents/petra/GrafKom/pert1/pert1/shader/shader.vert", "C:/Users/HP-Omen/Documents/petra/GrafKom/pert1/pert1/shader/shader.frag");
            _shader = new Shader(shadervert, shaderfrag);
            _shader.Use();
            //_view = Matrix4.CreateTranslation(0.0f,0.0f, -3.0f);
            //_projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), size_x / (float)size_y, 0.1f, 1000.0f)

            foreach (var item in Child)
            {
                item.Load(shadervert, shaderfrag, size_x, size_y);
            }
           
           
        }
        public void setColor(Vector3 color)
        {
            _color.X = color.X / 255f;
            _color.Y = color.Y / 255f;
            _color.Z = color.Z / 255f;
        }

        public void render(int _lines, Matrix4 camera_view,Matrix4 camera_projection)
        {
        
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            _shader.SetMatrix4("model", temp);
            _shader.SetMatrix4("view", camera_view);
            _shader.SetMatrix4("projection", camera_projection);
            _shader.SetVector3("objectColor", _color);


            if (_indices.Count != 0)
            {
               
                if (_lines == 0)
                {
                    GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
                }
                else if (_lines == 1)
                {

                    GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
                }
                else if (_lines == 2)
                {
                    GL.DrawElements(PrimitiveType.LineStrip, _indices.Count, DrawElementsType.UnsignedInt, 0);
                }
            }
            else
            {

                if (_lines == 0)
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Count);
                }
                else if (_lines == 1)
                {

                    GL.DrawArrays(PrimitiveType.TriangleFan, 0, _vertices.Count) ;
                }
                else if(_lines == 2) 
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, _vertices.Count);
                }
                else if (_lines == 3)
                {
                    GL.DrawArrays(PrimitiveType.LineStripAdjacency, 0, _vertices.Count);
                }
            }
            foreach (var item in Child)
            {
                item.render(_lines,camera_view,camera_projection);
            }

        }
        //version 2 rotate
        public void rotated(Vector3 pivot, Vector3 vector, float angle)
        {
            var radAngle = MathHelper.DegreesToRadians(angle);

            var arbRotationMatrix = new Matrix4
                (
                new Vector4((float)(Math.Cos(radAngle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(radAngle))), (float)(vector.X * vector.Y * (1.0f - Math.Cos(radAngle)) + vector.Z * Math.Sin(radAngle)), (float)(vector.X * vector.Z * (1.0f - Math.Cos(radAngle)) - vector.Y * Math.Sin(radAngle)), 0),
                new Vector4((float)(vector.X * vector.Y * (1.0f - Math.Cos(radAngle)) - vector.Z * Math.Sin(radAngle)), (float)(Math.Cos(radAngle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(radAngle))), (float)(vector.Y * vector.Z * (1.0f - Math.Cos(radAngle)) + vector.X * Math.Sin(radAngle)), 0),
                new Vector4((float)(vector.X * vector.Z * (1.0f - Math.Cos(radAngle)) + vector.Y * Math.Sin(radAngle)), (float)(vector.Y * vector.Z * (1.0f - Math.Cos(radAngle)) - vector.X * Math.Sin(radAngle)), (float)(Math.Cos(radAngle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(radAngle))), 0),
                Vector4.UnitW
                );

            temp *= Matrix4.CreateTranslation(-pivot);
            temp *= arbRotationMatrix;
            temp *= Matrix4.CreateTranslation(pivot);

            for (int i = 0; i < 3; i++)
            {
                _euler[i] = Vector3.Normalize(getRotationResult(pivot, vector, radAngle, _euler[i], true));
            }

            _centerPosition= getRotationResult(pivot, vector, radAngle, _centerPosition);

            foreach (var i in Child)
            {
                i.rotate(pivot, vector, angle);
            }
        }
        //ver 1 rotation
        public void rotate(Vector3 pivot, Vector3 vector, float angle)
        {
            //pivot -> mau rotate di titik mana
            //vector -> mau rotate di sumbu apa? (x,y,z)
            //angle -> rotatenya berapa derajat?
            var real_angle = angle;
            angle = MathHelper.DegreesToRadians(angle);

            //mulai ngerotasi
            for (int i = 0; i < _vertices.Count; i++)
            {
                _vertices[i] = getRotationResult(pivot, vector, angle, _vertices[i]);
            }
            //rotate the euler direction
            for (int i = 0; i < 3; i++)
            {
                _euler[i] = getRotationResult(pivot, vector, angle, _euler[i], true);

                //NORMALIZE
                //LANGKAH - LANGKAH
                //length = akar(x^2+y^2+z^2)
                float length = (float)Math.Pow(Math.Pow(_euler[i].X, 2.0f) + Math.Pow(_euler[i].Y, 2.0f) + Math.Pow(_euler[i].Z, 2.0f), 0.5f);
                Vector3 temporary = new Vector3(0, 0, 0);
                temporary.X = _euler[i].X / length;
                temporary.Y = _euler[i].Y / length;
                temporary.Z = _euler[i].Z / length;
                _euler[i] = temporary;
            }
            _centerPosition = getRotationResult(pivot, vector, angle, _centerPosition);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * Vector3.SizeInBytes,
                _vertices.ToArray(), BufferUsageHint.StaticDraw);
            foreach (var item in Child)
            {
                item.rotate(pivot, vector, real_angle);
            }
        }
        public Vector3 getRotationResult(Vector3 pivot, Vector3 vector, float angle, Vector3 point, bool isEuler = false)
        {
            Vector3 temp, newPosition;

            if (isEuler)
            {
                temp = point;
            }
            else
            {
                temp = point - pivot;
            }

            newPosition.X =
                temp.X * (float)(Math.Cos(angle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(angle))) +
                temp.Y * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) - vector.Z * Math.Sin(angle)) +
                temp.Z * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) + vector.Y * Math.Sin(angle));

            newPosition.Y =
                temp.X * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) + vector.Z * Math.Sin(angle)) +
                temp.Y * (float)(Math.Cos(angle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(angle))) +
                temp.Z * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) - vector.X * Math.Sin(angle));

            newPosition.Z =
                temp.X * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) - vector.Y * Math.Sin(angle)) +
                temp.Y * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) + vector.X * Math.Sin(angle)) +
                temp.Z * (float)(Math.Cos(angle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(angle)));

            if (isEuler)
            {
                temp = newPosition;
            }
            else
            {
                temp = newPosition + pivot;
            }
            return temp;
        }

        public void resetEuler()
        {
            _euler[0] = new Vector3(1, 0, 0);
            _euler[1] = new Vector3(0, 1, 0);
            _euler[2] = new Vector3(0, 0, 1);
        }
        public void addChild(float radiusX, float radiusY, float radiusZ, float x, float y, float z, int sectorCount, int stackCount)
        {
            Asset3d newChild = new Asset3d();
            newChild.createEllipsoid2(radiusX,radiusY,radiusZ,x, y, z,sectorCount,stackCount);
            Child.Add(newChild);
            Console.WriteLine(Child.Count);
        }
        public void AddChild (Asset3d item)
        {
            Child.Add (item);
        }
        public void createCurveBezier()
        {
            //ini nyoba di tiga titik
            //_vertices.Add(new Vector3(0, 0, 0));
            //_vertices.Add(new Vector3(1, 0, 0));
            //_vertices.Add(new Vector3(2, -1, 0));

            List<Vector3> _verticesBezier = new List<Vector3>();
            List<int> pascal = new List<int>();
            if (_vertices.Count > 1)
            {
                pascal = getRow(_vertices.Count);
                for (float t = 0; t <= 1.0f; t += 0.005f)
                {
                    Vector3 p = getP(pascal, t);
                    _verticesBezier.Add(p);
                }
            }
            _vertices = _verticesBezier;
        }
        
        
        public Vector3 getP(List<int> pascal, float t)
        {
            Vector3 p = new Vector3(0, 0, 0);
            for (int i = 0; i < _vertices.Count; i++)
            {
                float temp = (float)Math.Pow((1 - t), _vertices.Count - 1 - i) * (float)Math.Pow(t, i) * pascal[i];
                p += temp * _vertices[i];
            }
            return p;
        }
        public List<int> getRow(int rowIndex)
        {
            List<int> currow = new List<int>();
            currow.Add(1);
            if (rowIndex == 0)
            {
                return currow;
            }

            List<int> prev = getRow(rowIndex - 1);
            for (int i = 1; i < prev.Count; i++)
            {
                int curr = prev[i - 1] + prev[i];
                currow.Add(curr);
            }
            currow.Add(1);
            return currow;
        }
        public void translate(float x, float y, float z)
        {
            temp *= Matrix4.CreateTranslation(x, y, z);

            _centerPosition.X += x;
            _centerPosition.Y += y;
            _centerPosition.Z += z;

            foreach (var i in Child)
            {
                i.translate(x, y, z);
            }
        }

        public void createboxvertices(float x , float y , float z, float length)
        {
            _centerPosition.X = x;
            _centerPosition.Y = y;  
            _centerPosition.Z = z;
            Vector3 tmp_vector;

            tmp_vector.X = x - length / 2.0f;
            tmp_vector.Y = y + length / 2.0f;
            tmp_vector.Z = z - length / 2.0f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x + length / 2.0f;
            tmp_vector.Y = y + length / 2.0f;
            tmp_vector.Z = z - length / 2.0f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x - length / 2.0f;
            tmp_vector.Y = y - length / 2.0f;
            tmp_vector.Z = z - length / 2.0f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x + length / 2.0f;
            tmp_vector.Y = y - length / 2.0f;
            tmp_vector.Z = z - length / 2.0f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x - length / 2.0f;
            tmp_vector.Y = y + length / 2.0f;
            tmp_vector.Z = z + length / 2.0f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x + length / 2.0f;
            tmp_vector.Y = y + length / 2.0f;
            tmp_vector.Z = z + length / 2.0f;
            _vertices.Add(tmp_vector);

            //titik 7
            tmp_vector.X = x - length / 2.0f;
            tmp_vector.Y = y - length / 2.0f;
            tmp_vector.Z = z + length / 2.0f;
            _vertices.Add(tmp_vector);
            //titik 8
            tmp_vector.X = x + length / 2.0f;
            tmp_vector.Y = y - length / 2.0f;
            tmp_vector.Z = z + length / 2.0f;
            _vertices.Add(tmp_vector);

            _indices = new List<uint>
            {
                //SEGITIGA DEPAN 1
                0,1,2,
                //SEGITIGA DEPAN 2
                1,2,3,
                //SEGITIGA ATAS 1
                0,4,5,
                //SEGITIGA ATAS 2
                0,1,5,
                //SEGITIGA KANAN 1
                1,3,5,
                //SEGITIGA KANAN 2
                3,5,7,
                //SEGITIGA KIRI 1
                0,2,4,
                //SEGITIGA KIRI 2
                2,4,6,
                //SEGITIGA BELAKANG 1
                4,5,6,
                //SEGITIGA BELAKANG 2
                5,6,7,
                //SEGITIGA BAWAH 1
                2,3,6,
                //SEGITIGA BAWAH 2
                3,6,7

            };




        }
        public void createwingvertices(float x, float y, float z, float length)
        {
            _centerPosition.X = x;
            _centerPosition.Y = y;
            _centerPosition.Z = z;
            Vector3 tmp_vector;

            tmp_vector.X = x - length / 2.0f;
            tmp_vector.Y = y + length / 6.0f;
            tmp_vector.Z = z - length / 0.5f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x + length / 2.0f;
            tmp_vector.Y = y + length / 6.0f;
            tmp_vector.Z = z - length / 0.5f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x - length / 2.0f;
            tmp_vector.Y = y - length / 6.0f;
            tmp_vector.Z = z - length / 0.5f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x + length / 2.0f;
            tmp_vector.Y = y - length / 6.0f;
            tmp_vector.Z = z - length / 0.5f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x - length / 2.0f;
            tmp_vector.Y = y + length / 6.0f;
            tmp_vector.Z = z + length / 0.5f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x + length / 2.0f;
            tmp_vector.Y = y + length / 6.0f;
            tmp_vector.Z = z + length / 0.5f;
            _vertices.Add(tmp_vector);

            //titik 7
            tmp_vector.X = x - length / 2.0f;
            tmp_vector.Y = y - length / 6.0f;
            tmp_vector.Z = z + length / 0.5f;
            _vertices.Add(tmp_vector);
            //titik 8
            tmp_vector.X = x + length / 2.0f;
            tmp_vector.Y = y - length / 6.0f;
            tmp_vector.Z = z + length / 0.5f;
            _vertices.Add(tmp_vector);

            _indices = new List<uint>
            {
                //SEGITIGA DEPAN 1
                0,1,2,
                //SEGITIGA DEPAN 2
                1,2,3,
                //SEGITIGA ATAS 1
                0,4,5,
                //SEGITIGA ATAS 2
                0,1,5,
                //SEGITIGA KANAN 1
                1,3,5,
                //SEGITIGA KANAN 2
                3,5,7,
                //SEGITIGA KIRI 1
                0,2,4,
                //SEGITIGA KIRI 2
                2,4,6,
                //SEGITIGA BELAKANG 1
                4,5,6,
                //SEGITIGA BELAKANG 2
                5,6,7,
                //SEGITIGA BAWAH 1
                2,3,6,
                //SEGITIGA BAWAH 2
                3,6,7

            };




        }
        public void createHalfEllipsoid(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z)
        {

            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;

            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = pi; u >= 0; u -= pi / 360)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 360)
                {
                    temp_vector.X = _x + (float)Math.Cos(v) * (float)Math.Cos(u) * radiusX;
                    temp_vector.Y = _y + (float)Math.Cos(v) * (float)Math.Sin(u) * radiusY;
                    temp_vector.Z = _z + (float)Math.Sin(v) * radiusZ;
                    _vertices.Add(temp_vector);

                }
            }
        }
        public void createRectangularVertices(float x, float y, float z, float length)
        {
            _centerPosition.X = x;
            _centerPosition.Y = y;
            _centerPosition.Z = z;
            Vector3 tmp_vector;

            tmp_vector.X = x - length / 2.0f;
            tmp_vector.Y = y + length / 0.6f;
            tmp_vector.Z = z - length / 2.0f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x + length / 2.0f;
            tmp_vector.Y = y + length / 0.6f;
            tmp_vector.Z = z - length / 2.0f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x - length / 2.0f;
            tmp_vector.Y = y - length / 0.6f;
            tmp_vector.Z = z - length / 2.0f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x + length / 2.0f;
            tmp_vector.Y = y - length / 0.6f;
            tmp_vector.Z = z - length / 2.0f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x - length / 2.0f;
            tmp_vector.Y = y + length / 0.6f;
            tmp_vector.Z = z + length / 2.0f;
            _vertices.Add(tmp_vector);

            tmp_vector.X = x + length / 2.0f;
            tmp_vector.Y = y + length / 0.6f;
            tmp_vector.Z = z + length / 2.0f;
            _vertices.Add(tmp_vector);

            //titik 7
            tmp_vector.X = x - length / 2.0f;
            tmp_vector.Y = y - length / 0.6f;
            tmp_vector.Z = z + length / 2.0f;
            _vertices.Add(tmp_vector);
            //titik 8
            tmp_vector.X = x + length / 2.0f;
            tmp_vector.Y = y - length / 0.6f;
            tmp_vector.Z = z + length / 2.0f;
            _vertices.Add(tmp_vector);

            _indices = new List<uint>
            {
                //SEGITIGA DEPAN 1
                0,1,2,
                //SEGITIGA DEPAN 2
                1,2,3,
                //SEGITIGA ATAS 1
                0,4,5,
                //SEGITIGA ATAS 2
                0,1,5,
                //SEGITIGA KANAN 1
                1,3,5,
                //SEGITIGA KANAN 2
                3,5,7,
                //SEGITIGA KIRI 1
                0,2,4,
                //SEGITIGA KIRI 2
                2,4,6,
                //SEGITIGA BELAKANG 1
                4,5,6,
                //SEGITIGA BELAKANG 2
                5,6,7,
                //SEGITIGA BAWAH 1
                2,3,6,
                //SEGITIGA BAWAH 2
                3,6,7

            };

        }

        public void createEllipsoid(float radiusX,float radiusY,float radiusZ,float x, float y , float z)
        {

            float pi = (float)Math.PI;
            Vector3 tmp_vector;
            for (float v= -pi ; v <= pi ; v+=pi / 360)
            {
                for (float u = -pi / 2;  u <= pi / 2;u+= pi / 360)
                {
                    tmp_vector.X = x + (float)Math.Cos(v) * (float)Math.Cos(u) * radiusX;
                    tmp_vector.Y = y + (float)Math.Cos(v) * (float)Math.Sin(u) * radiusY;
                    tmp_vector.Z = z + (float)Math.Sin(v) * radiusZ;
                    _vertices.Add(tmp_vector);
                }
            }
        }
        public void createEllipsoid2(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int sectorCount, int stackCount)
        {
            float pi = (float)Math.PI;
            _centerPosition = new Vector3(_x, _y, _z);
            Vector3 temp_vector;
            float sectorStep = 2 * (float)Math.PI / sectorCount;
            float stackStep = (float)Math.PI / stackCount;
            float sectorAngle, StackAngle,x,y,z;

            for (int i = 0; i <= stackCount; ++i)
            {
                StackAngle = pi / 2 - i * stackStep;
                x = radiusX * (float)Math.Cos(StackAngle) ;
                y = radiusY * (float)Math.Cos(StackAngle) ;
                z = radiusZ * (float)Math.Sin(StackAngle) ;

                for (int j = 0; j <= sectorCount; ++j)
                {
                    sectorAngle = j * sectorStep;

                    temp_vector.X = x * (float)Math.Cos(sectorAngle) + _x;
                    temp_vector.Y = y * (float)Math.Sin(sectorAngle) + _y;
                    temp_vector.Z = z + _z;
                    _vertices.Add(temp_vector);
                }
            }
           

            uint k1, k2;
            for (int i = 0; i < stackCount; ++i)
            {
                k1 = (uint)(i * (sectorCount + 1));
                k2 = (uint)(k1 + sectorCount + 1);
                for (int j = 0; j < sectorCount; ++j, ++k1, ++k2)
                {
                    if (i != 0)
                    {
                        _indices.Add(k1);
                        _indices.Add(k2);
                        _indices.Add(k1 + 1);
                    }
                    if (i != (stackCount - 1))
                    {
                        _indices.Add(k1 + 1);
                        _indices.Add(k2);
                        _indices.Add(k2 + 1);
                    }
                }
            }

        }
        public void crreateHalfEllipsoid(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z)
        {

            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;

            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = pi; u >= 0; u -= pi / 360)
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 360)
                {
                    temp_vector.X = _x + (float)Math.Cos(v) * (float)Math.Cos(u) * radiusX;
                    temp_vector.Y = _y + (float)Math.Cos(v) * (float)Math.Sin(u) * radiusY;
                    temp_vector.Z = _z + (float)Math.Sin(v) * radiusZ;
                    _vertices.Add(temp_vector);

                }
            }
        }
        public void createCylinder2(float top_radius, float bot_radius, float height, float _x, float _y, float _z)
        {
            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;
            float pi = (float)Math.PI;
            Vector3 temp_vector;

            for (float i = -pi / 2; i <= pi / 2; i += pi / 360)
            {
                for (float j = -pi; j <= pi; j += pi / 360)
                {

                    temp_vector.Y = top_radius * (float)Math.Cos(i) * (float)Math.Sin(j) + _centerPosition.Y;
                    if (temp_vector.Y < _centerPosition.Y)
                    {
                        temp_vector.Y = _centerPosition.Y - height * 0.5f;
                        temp_vector.X = bot_radius * (float)Math.Cos(i) * (float)Math.Cos(j) + _centerPosition.X;
                        temp_vector.Z = bot_radius * (float)Math.Sin(i) + _centerPosition.Z;
                    }
                    else
                    {
                        temp_vector.X = top_radius * (float)Math.Cos(i) * (float)Math.Cos(j) + _centerPosition.X;
                        temp_vector.Y = _centerPosition.Y + height * 0.5f;
                        temp_vector.Z = top_radius * (float)Math.Sin(i) + _centerPosition.Z;
                    }
                    _vertices.Add(temp_vector);
                }
            }
        }
        public void createEllipticCone(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z)
        {

            _centerPosition.X = _x;
            _centerPosition.Y = _y;
            _centerPosition.Z = _z;

            float pi = (float)Math.PI;
            Vector3 temp_vector;
            for (float u = -pi; u <= pi; u += pi / 300)
            {
                for (float v = 0; v <= 5; v += pi / 300)
                {
                    temp_vector.X = _x + v * (float)Math.Cos(u) * radiusX;
                    temp_vector.Y = _y + v * (float)Math.Sin(u) * radiusY;
                    temp_vector.Z = _z + v * radiusZ;
                    _vertices.Add(temp_vector);

                }
            }
        }
        public void createElliptic2(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int sectorCount, int stackCount)
        {
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            float sectorStep = 2 * (float)Math.PI / sectorCount;
            float stackStep = (float)Math.PI / stackCount;
            float sectorAngle, StackAngle, x, y, z;

            for (int i = 0; i <= stackCount/2; ++i)
            {
                StackAngle = pi / 2 - i * stackStep;
                x = radiusX * StackAngle;
                y = radiusY * StackAngle;
                z = radiusZ * StackAngle;

                for (int j = 0; j <= sectorCount; ++j)
                {
                    sectorAngle = j * sectorStep;

                    temp_vector.X = x * (float)Math.Cos(sectorAngle) + _x;
                    temp_vector.Y = y * (float)Math.Sin(sectorAngle) + _y;
                    temp_vector.Z = z + _z;
                    _vertices.Add(temp_vector);
                }
            }


            uint k1, k2;
            for (int i = 0; i < stackCount; ++i)
            {
                k1 = (uint)(i * (sectorCount + 1));
                k2 = (uint)(k1 + sectorCount + 1);
                for (int j = 0; j < sectorCount; ++j, ++k1, ++k2)
                {
                    //if (i != 0)
                    //{
                        _indices.Add(k1);
                        _indices.Add(k2);
                        _indices.Add(k1 + 1);
                    //}
                    //if (i != (stackCount - 1))
                    //{
                        _indices.Add(k1 + 1);
                        _indices.Add(k2);
                        _indices.Add(k2 + 1);
                    //}
                }
            }

        }


    }
   
}
