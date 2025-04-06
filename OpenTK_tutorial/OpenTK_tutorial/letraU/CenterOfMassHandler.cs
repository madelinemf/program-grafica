using OpenTK.Mathematics;

public class CenterOfMassHandler
{
    private Vector3 centerOfMass;
    private Vector3 position;

    public CenterOfMassHandler(Vector3 initialCenterOfMass, Vector3 initialPosition)
    {
        centerOfMass = initialCenterOfMass;
        position = initialPosition;
    }

    public Vector3 CenterOfMass
    {
        get => centerOfMass;
        set => centerOfMass = value;
    }

    public Vector3 Position
    {
        get => position;
        set => position = value;
    }

    // Calcula la transformación final considerando el centro de masa
    public Matrix4 GetTransformMatrix()
    {
        Matrix4 translationToCenterOfMass = Matrix4.CreateTranslation(-centerOfMass); // Mover al centro de masa
        Matrix4 translation = Matrix4.CreateTranslation(position);  // Mover a la posición final
        return translation * translationToCenterOfMass;
    }

    public void Move(Vector3 delta)
    {
        position += delta;
    }
}

