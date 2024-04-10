using UnityEngine;
using System.Collections.Generic;

public class SnakeMovement : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField, Range(1,10)] private int moveSpeed = 1;
    [SerializeField, Range(1,100)] private int rotationSpeed = 50;

    [Space(10), Header("GPU Data"), SerializeField] private Mesh playerMesh;
    [SerializeField] private Material playerMaterial;

    [Space(10), Header("Broadcasting Channel"), SerializeField] private Vector2EventListener targetPositionListener = default;
    [Space(10), Header("Listening Channel"), SerializeField] private IntEventListener inputAxisListener = default;

    //private variables
    private List<Snake> snakeBody = new List<Snake>();
    private List<Vector3> snakeTrail = new List<Vector3>();
    private int trailIndex = -1;
    private float waitTime = 0.2f;
    private float currentTime = 0f;
    private int snakeLength = 0;

    #region RenderProperty
    // private Matrix4x4[] matrix;
    private List<Matrix4x4> matrix = new List<Matrix4x4>();
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
        Snake snake = new Snake(transform.position);
        snakeBody.Add(snake);

        snakeTrail.Add(transform.position);
        matrix.Add(Matrix4x4.TRS(snakeBody[0].position, Quaternion.Euler(snakeBody[0].rotation), Vector3.one));
        trailIndex++;
        snakeLength++;
    }

    private void RotatePlayer(int inputAxis)
    {
        //rotate player to a degree
        snakeBody[0].Rotate(inputAxis * rotationSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddTail();
        }

        //move player to the right Direction
        snakeBody[0].MoveRight(moveSpeed * Time.deltaTime);
        targetPositionListener.Raise(snakeBody[0].position);

        if(currentTime >= waitTime)
        {
            currentTime = 0f;
            for(int i = trailIndex; i > 0; i--)
            {
                snakeTrail[i] = snakeTrail[i - 1];
            }

            snakeTrail[0] = snakeBody[0].position;
        }else
        {
            currentTime += Time.deltaTime;
        }

        DrawSnake();
    }

    private void LateUpdate()
    {
        if(snakeLength == 1) return;

        for(int i = 1; i < snakeLength; i++)
        {
            Vector2 dir = (snakeTrail[i] - snakeBody[i].position).normalized;
            snakeBody[i].MoveRight(moveSpeed * Time.deltaTime);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            snakeBody[i].rotation = Vector3.Lerp(snakeBody[i].rotation, new Vector3(0f, 0f, angle), rotationSpeed * Time.deltaTime);
        }
    }

    private void DrawSnake()
    {
        for(int i = 0; i< snakeLength; i++)
        {
            matrix[i] = Matrix4x4.TRS(snakeBody[i].position, Quaternion.Euler(snakeBody[i].rotation), Vector3.one);
        }

        Graphics.DrawMeshInstanced(playerMesh, 0, playerMaterial, matrix.ToArray(), snakeLength);
    }

    private void AddTail()
    {
        Snake snake = new Snake(snakeBody[trailIndex].position);
        snake.rotation = snakeBody[trailIndex].rotation;

        snakeBody.Add(snake);
        snakeTrail.Add(snake.position);
        matrix.Add(Matrix4x4.TRS(snakeBody[0].position, Quaternion.Euler(snakeBody[0].rotation), Vector3.one));
        trailIndex++;
        snakeLength++;
    }

    #if UNITY_EDITOR
    //Visual Debug
    private void OnDrawGizmos()
    {
        if(!Application.isPlaying) return;

        Gizmos.color = Color.red;
        for(int i = 0; i< snakeLength; i++)
        {
            Gizmos.DrawSphere(snakeTrail[i], .1f);
        }
    }

    #endif
}

internal class Snake
{
    public Vector3 position;
    public Vector3 rotation;

    private readonly float MAX_ROTATION_DEGREE = 359f;

    public Snake(Vector2 position)
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