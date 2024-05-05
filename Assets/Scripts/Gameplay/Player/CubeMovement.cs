using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField, Range(1,10)] private int moveSpeed = 1;
    [SerializeField, Range(1,100)] private int rotationSpeed = 50;

    [Space(10), Header("GPU Data"), SerializeField] private Mesh cubeMesh;
    [SerializeField] private Material cubeMaterial;

    [Space(10), Header("Broadcasting Channel"), SerializeField] private Vector2EventListener targetPositionListener = default;
    [Space(10), Header("Listening Channel"), SerializeField] private IntEventListener inputAxisListener = default;

    public Transform tempText;
    private Cube cube;

    #region RenderProperty
    private Matrix4x4[] matrix;
    #endregion

    private void OnEnable()
    {
        inputAxisListener.onEventRaised += RotatePlayer;
    }

    private void OnDisable()
    {
        inputAxisListener.onEventRaised -= RotatePlayer;
    }

    private void Start()
    {
        matrix = new Matrix4x4[1];
        cube = new Cube(transform.position);
        // matrix[0] = Matrix4x4.TRS(cube.position, Quaternion.Euler(cube.rotation), Vector3.one);
    }

    private void RotatePlayer(int inputAxis)
    {
        //rotate player to a degree
        cube.Rotate(inputAxis * rotationSpeed * Time.deltaTime);
    }

    private void Update()
    {
        //move player to the right Direction
        cube.MoveRight(moveSpeed * Time.deltaTime);
        targetPositionListener.Raise(cube.position);

        tempText.localPosition = cube.position;
        Vector3 tempRot = new Vector3(cube.rotation.x, cube.rotation.y, cube.rotation.z - 90f);
        tempText.localRotation = Quaternion.Euler(tempRot);
        DrawCube();
    }

    private void DrawCube()
    {
        matrix[0] = Matrix4x4.TRS(cube.position, Quaternion.Euler(cube.rotation), Vector3.one * 2f);
        Graphics.DrawMeshInstanced(cubeMesh, 0, cubeMaterial, matrix, 1);
    }

    #region Visual Debug
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(!Application.isPlaying) return;
    }

    #endif
    #endregion
}

internal class Cube
{
    public Vector3 position;
    public Vector3 rotation;

    private readonly float MAX_ROTATION_DEGREE = 359f;

    public Cube(Vector2 position)
    {
        this.position = position;
        rotation = Vector3.zero;
    }

    public void Rotate(float direction)
    {
        rotation += new Vector3(0f, 0f, direction);
        if(rotation.z > MAX_ROTATION_DEGREE || rotation.z < -MAX_ROTATION_DEGREE)
        {
            rotation.z = 0f;
        }
    }

    public Vector3 Right()
    {
        Quaternion currentRot = Quaternion.Euler(rotation);
        Vector3 finalPos = currentRot * Vector3.right;
        return finalPos;
    }

    public void MoveRight(float speed)
    {
        position += Right() * speed;
    }
}