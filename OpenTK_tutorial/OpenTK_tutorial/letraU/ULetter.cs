using OpenTK.Graphics.OpenGL4;
using OpenTK_tutorial.letraU;

public class ULetter
{
    private int vertexBufferObject;
    private int vertexArrayObject;
    private int elementBufferObject;
    private TransformHandler transformHandler;

    public ULetter(TransformHandler transformHandler)
    {
        this.transformHandler = transformHandler;
        Initialize();
    }

    private void Initialize()
    {
        vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(vertexArrayObject);

        vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Vertices.Length * sizeof(float), VertexData.Vertices, BufferUsageHint.StaticDraw);

        elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, VertexData.Indices.Length * sizeof(int), VertexData.Indices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
    }

    public void Draw(int shaderProgram)
    {
        GL.UseProgram(shaderProgram);
        GL.BindVertexArray(vertexArrayObject);

        // Obtener la matriz de transformación desde TransformHandler
        var transform = transformHandler.GetTransformationMatrix();
        int transformLocation = GL.GetUniformLocation(shaderProgram, "transform");
        GL.UniformMatrix4(transformLocation, false, ref transform);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.DrawElements(PrimitiveType.Triangles, VertexData.Indices.Length, DrawElementsType.UnsignedInt, 0);
    }

    public void Cleanup()
    {
        GL.DeleteBuffer(vertexBufferObject);
        GL.DeleteBuffer(elementBufferObject);
        GL.DeleteVertexArray(vertexArrayObject);
    }
}

