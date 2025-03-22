using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Proyecto_Grafica_U
{
    public class Game : GameWindow
    {
        private int vertexBufferObject;
        private int vertexArrayObject;
        private int elementBufferObject;
        private int shaderProgramObject;
        private float[] vertices;
        private int[] indices;

        private Matrix4 projectionMatrix;
        private Matrix4 viewMatrix;
        private Matrix4 modelMatrix;

        private Vector3 objectPosition = new Vector3(0.0f, 0.0f, 0.0f);
        private Vector3 center = new Vector3(-0.025f, -0.15f, 0.0f);

        public Game()
            : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.CenterWindow(new Vector2i(800, 600));
        }

        protected override void OnLoad()
        {
            GL.ClearColor(new Color4(0.2f, 0.2f, 0.4f, 1f));

            vertices = new float[]
            {
                -0.5f,  0.5f, 0.0f, -0.4f,  0.5f, 0.0f, -0.4f, -0.5f, 0.0f, -0.5f, -0.5f, 0.0f,
                -0.4f,  0.6f, 0.0f, -0.3f,  0.6f, 0.0f, -0.3f, -0.4f, 0.0f, -0.4f, -0.4f, 0.0f,
                 0.3f,  0.5f, 0.0f,  0.4f,  0.5f, 0.0f,  0.4f, -0.5f, 0.0f,  0.3f, -0.5f, 0.0f,
                 0.4f,  0.6f, 0.0f,  0.5f,  0.6f, 0.0f,  0.5f, -0.4f, 0.0f,  0.4f, -0.4f, 0.0f,
                -0.4f, -0.5f, 0.0f,  0.3f, -0.5f, 0.0f,  0.3f, -0.6f, 0.0f, -0.4f, -0.6f, 0.0f
            };

            indices = new int[]
            {
                0, 1, 2, 0, 2, 3, 4, 5, 6, 4, 6, 7, 0, 4, 7, 2, 6, 7,
                8, 9, 10, 8, 10, 11, 12, 13, 14, 12, 14, 15, 8, 12, 15, 10, 14, 15,
                16, 17, 18, 16, 18, 19
            };

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            shaderProgramObject = GL.CreateProgram();
            int vertexShaderObject = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderObject, "#version 330 core\nlayout (location = 0) in vec3 aPosition;\nuniform mat4 model;\nuniform mat4 view;\nuniform mat4 projection;\nvoid main(){ gl_Position = projection * view * model * vec4(aPosition, 1.0); }");
            GL.CompileShader(vertexShaderObject);

            int fragmentShaderObject = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderObject, "#version 330 core\nout vec4 pixelColor;\nvoid main(){ pixelColor = vec4(0.8f, 0.8f, 0.1f, 1.0f); }");
            GL.CompileShader(fragmentShaderObject);

            GL.AttachShader(shaderProgramObject, vertexShaderObject);
            GL.AttachShader(shaderProgramObject, fragmentShaderObject);
            GL.LinkProgram(shaderProgramObject);
            GL.DeleteShader(vertexShaderObject);
            GL.DeleteShader(fragmentShaderObject);

            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Size.X / (float)Size.Y, 0.1f, 100.0f);
            viewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            modelMatrix = Matrix4.Identity;

            base.OnLoad();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            var input = KeyboardState;
            if (input.IsKeyDown(Keys.W)) objectPosition.Y += 0.01f;
            if (input.IsKeyDown(Keys.S)) objectPosition.Y -= 0.01f;
            if (input.IsKeyDown(Keys.A)) objectPosition.X -= 0.01f;
            if (input.IsKeyDown(Keys.D)) objectPosition.X += 0.01f;
            if (input.IsKeyDown(Keys.Q)) objectPosition.Z += 0.01f;
            if (input.IsKeyDown(Keys.E)) objectPosition.Z -= 0.01f;
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(shaderProgramObject);
            modelMatrix = Matrix4.CreateTranslation(objectPosition - center);
            GL.UniformMatrix4(GL.GetUniformLocation(shaderProgramObject, "model"), false, ref modelMatrix);
            GL.UniformMatrix4(GL.GetUniformLocation(shaderProgramObject, "view"), false, ref viewMatrix);
            GL.UniformMatrix4(GL.GetUniformLocation(shaderProgramObject, "projection"), false, ref projectionMatrix);
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
