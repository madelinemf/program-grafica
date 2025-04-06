using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class TransformHandler
{
    private Vector3 position;
    private float rotationAngle;
    private float scaleFactor;

    public TransformHandler(Vector3 initialPosition)
    {
        position = initialPosition;
        rotationAngle = 0.0f;
        scaleFactor = 1.0f;
    }

    public Vector3 Position => position;
    public float RotationAngle => rotationAngle;
    public float ScaleFactor => scaleFactor;

    // Método para mover la letra "U" con las teclas de dirección
    public void HandleMovement(KeyboardState keyboardState, float moveSpeed)
    {
        if (keyboardState.IsKeyDown(Keys.Up))
            position.Y += moveSpeed;  // Mover hacia arriba
        if (keyboardState.IsKeyDown(Keys.Down))
            position.Y -= moveSpeed;  // Mover hacia abajo
        if (keyboardState.IsKeyDown(Keys.Left))
            position.X -= moveSpeed;  // Mover hacia la izquierda
        if (keyboardState.IsKeyDown(Keys.Right))
            position.X += moveSpeed;  // Mover hacia la derecha
    }

    // Método para rotar la letra "U" con las teclas de flecha
    public void HandleRotation(KeyboardState keyboardState, float rotationSpeed)
    {
        if (keyboardState.IsKeyDown(Keys.A))
            rotationAngle += rotationSpeed;  // Rotar hacia la izquierda (en sentido antihorario)
        if (keyboardState.IsKeyDown(Keys.D))
            rotationAngle -= rotationSpeed;  // Rotar hacia la derecha (en sentido horario)
    }

    // Método para cambiar el tamaño (escala) de la letra "U" con teclas específicas
    public void HandleScaling(KeyboardState keyboardState, float scalingSpeed)
    {
        if (keyboardState.IsKeyDown(Keys.W))
            scaleFactor += scalingSpeed;  // Aumentar el tamaño
        if (keyboardState.IsKeyDown(Keys.S))
            scaleFactor -= scalingSpeed;  // Reducir el tamaño
    }

    // Calcula la matriz de transformación considerando las traslaciones, rotaciones y escalados
    public Matrix4 GetTransformationMatrix()
    {
        Matrix4 translation = Matrix4.CreateTranslation(position);  // Traslación
        Matrix4 rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotationAngle));  // Rotación
        Matrix4 scaling = Matrix4.CreateScale(scaleFactor);  // Escalado

        return scaling * rotation * translation;  // Composición de las transformaciones
    }
}

