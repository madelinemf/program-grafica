using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

class Game : GameWindow
{
    private int shaderProgram;
    private ULetter letter;
    private TransformHandler transformHandler;
    private float moveSpeed = 0.5f; // Velocidad de movimiento ajustada
    private float rotationSpeed = 50.0f; // Velocidad de rotación ajustada
    private float scalingSpeed = 0.2f; // Velocidad de escala ajustada

    private readonly string vertexShaderSource = @"
    #version 330 core
    layout (location = 0) in vec3 aPosition;
    uniform mat4 transform;
    void main()
    {
        gl_Position = transform * vec4(aPosition, 1.0);
    }
    ";

    private readonly string fragmentShaderSource = @"
    #version 330 core
    out vec4 FragColor;
    void main()
    {
        FragColor = vec4(1.0, 0.4, 0.7, 1.0);
    }
    ";

    public Game() : base(GameWindowSettings.Default, new NativeWindowSettings()
    {
        Size = new Vector2i(800, 600),
        Title = "Mover Letra U con Transformaciones"
    })
    { }

    protected override void OnLoad()
    {
        base.OnLoad();

        // Compilación de shaders
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        shaderProgram = GL.CreateProgram();
        GL.AttachShader(shaderProgram, vertexShader);
        GL.AttachShader(shaderProgram, fragmentShader);
        GL.LinkProgram(shaderProgram);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);

        // Crear el TransformHandler y la letra "U"
        transformHandler = new TransformHandler(new Vector3(0.0f, 0.0f, 0.0f));
        letter = new ULetter(transformHandler);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        // Multiplicar por deltaTime para suavizar las transformaciones
        float deltaTime = (float)args.Time; // El tiempo en segundos desde el último fotograma

        // Manejar las entradas del teclado para mover, rotar y escalar
        transformHandler.HandleMovement(KeyboardState, moveSpeed * deltaTime);
        transformHandler.HandleRotation(KeyboardState, rotationSpeed * deltaTime);
        transformHandler.HandleScaling(KeyboardState, scalingSpeed * deltaTime);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Dibujar la letra "U" con la transformación aplicada
        letter.Draw(shaderProgram);

        SwapBuffers();
    }

    protected override void OnUnload()
    {
        letter.Cleanup();
        GL.DeleteProgram(shaderProgram);
        base.OnUnload();
    }
}
