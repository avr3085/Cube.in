using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ResManager : MonoBehaviour
{
    [SerializeField, Range(10, 100)] private int mapSize = 10;
    [SerializeField, Range(1, 1023)] private int entityCount = 10;
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;

    [Header("Data Channel"), SerializeField] private ResColorSwatch resColorSwatch = default;

    private UGrid uGrid;
    private MaterialPropertyBlock materialPropertyBlock;

    public static ResManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Vector4[] colors = new Vector4[entityCount];

        uGrid = new UGrid(entityCount, mapSize);
        materialPropertyBlock = new MaterialPropertyBlock();

        for(int i = 0; i< entityCount; i++)
        {
            var randColor = Random.Range(0, resColorSwatch.swatchCount);
            colors[i] = resColorSwatch.swatches[randColor];
        }

        materialPropertyBlock.SetVectorArray("_Colors", colors);
        
    }

    private void Update()
    {
        Graphics.DrawMeshInstanced(mesh, 0, material, uGrid.PoisitonMatrix, uGrid.NodeCount, materialPropertyBlock);
    }

    private void OnDisable()
    {
        uGrid.DeInit();
    }

    #region Visual Debug
    #if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        if(!Application.isPlaying) return;

        Gizmos.color = Color.green;
        foreach(KeyValuePair<int, int> val in uGrid.GridMap)
        {
            int index = val.Value;
            while(index != -1)
            {
                DrawBox(uGrid.GridNode[index].position);
                index = uGrid.GridNode[index].parent;
            }
        }

        Debug.Log(uGrid.GridMap.Count);
    }

    private void DrawBox(Vector2 position)
    {
        Vector2 lowerBound = new Vector2(position.x - 0.5f, position.y - 0.5f);
        Vector2 upperBound = new Vector2(position.x + 0.5f, position.y + 0.5f);
        DrawBox(lowerBound, upperBound);
    }

    private void DrawBox(Vector2 lb, Vector2 ub)
    {
        Gizmos.DrawLine(lb, new Vector2(ub.x, lb.y));
        Gizmos.DrawLine(new Vector2(ub.x, lb.y), ub);
        Gizmos.DrawLine(ub, new Vector2(lb.x, ub.y));
        Gizmos.DrawLine(new Vector2(lb.x, ub.y), lb);
    }

    #endif
    #endregion
}