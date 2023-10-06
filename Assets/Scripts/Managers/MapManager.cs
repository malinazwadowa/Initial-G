using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    public Transform grid;
    public GameObject mapPrefab;
    public Vector2 chunkSize;
    public Queue<GameObject> availableMaps = new Queue<GameObject>();

    private Vector2 startPosition = new Vector2(0,0);
    
    private Transform playersTransform;
    private Vector3 playersRelativePosition;

    private float cameraHeight;
    private float cameraWidth;

    private PositionOnChunk lastPosition = PositionOnChunk.LeftBottomCorner;
    private PositionOnChunk cachedLastPosition;
    
    private Chunk currentChunk;
    private Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk>();


    void Start()
    {
        cachedLastPosition = lastPosition;
        InstantiateMapPrefabs();

        Chunk startChunk = new Chunk();
        startChunk.Initialize(startPosition, chunkSize);
        currentChunk = startChunk;
        startChunk.Activate();
        chunks.Add(startChunk.positionOnMatrix, startChunk);
        InitalizeAdjacentChunks(startChunk);


        playersTransform = PlayerManager.Instance.GetPlayersFeetTransform();
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void Update()
    {
        playersRelativePosition = playersTransform.position - new Vector3(currentChunk.positionOnMatrix.x * chunkSize.x, currentChunk.positionOnMatrix.y * chunkSize.y, 0);

        GetPositionOnChunk();
        CheckForChunkChange(playersRelativePosition);

        if (cachedLastPosition != lastPosition)
        {
            cachedLastPosition = lastPosition;
            ActivateRelevantChunks();
        }
    }
    private void InstantiateMapPrefabs()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject map = Instantiate(mapPrefab, grid);
            map.transform.position = new Vector3(500, 500, 0);
            availableMaps.Enqueue(map);
        }
    }
    private void CheckForChunkChange(Vector3 playersRelativePosition)
    {
        bool chunkChanged = false;
        if (chunkChanged == false)
        {
            if (playersRelativePosition.x < 0)
            {
                Vector2 positionOfNewChunk = currentChunk.positionOnMatrix + new Vector2(-1, 0);
                currentChunk = chunks[positionOfNewChunk];
                chunkChanged = true;
            }
            if (playersRelativePosition.x > 150)
            {
                Vector2 positionOfNewChunk = currentChunk.positionOnMatrix + new Vector2(1, 0);
                currentChunk = chunks[positionOfNewChunk];
                chunkChanged = true;
            }
            if (playersRelativePosition.y < 0)
            {
                Vector2 positionOfNewChunk = currentChunk.positionOnMatrix + new Vector2(0, -1);
                currentChunk = chunks[positionOfNewChunk];
                chunkChanged = true;
            }
            if (playersRelativePosition.y > 100)
            {
                Vector2 positionOfNewChunk = currentChunk.positionOnMatrix + new Vector2(0, 1);
                currentChunk = chunks[positionOfNewChunk];
                chunkChanged = true;
            }
        }

        if (chunkChanged)
        {
            InitalizeAdjacentChunks(currentChunk);
        }
    }

    private void InitalizeAdjacentChunks(Chunk originChunk)
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                Vector2 positionOffset = new Vector2(i, j);
                Vector2 position = originChunk.positionOnMatrix + positionOffset;
                if (!chunks.ContainsKey(position))
                {
                    Chunk chunk = new Chunk();
                    chunk.Initialize(position, chunkSize);
                    chunks.Add(position, chunk);
                }
            }
        }
    }
    public void ActivateRelevantChunks()
    {
        DeactivateAdjacentChunks(currentChunk);
        PositionOnChunk positionOnChunk = GetPositionOnChunk();
        List<Vector2> offsets = new List<Vector2>();

        switch (positionOnChunk)
        {
            case PositionOnChunk.LeftBottomCorner:
                offsets.Add(new Vector2(-1, 0));
                offsets.Add(new Vector2(-1, -1));
                offsets.Add(new Vector2(0, -1));
                break;

            case PositionOnChunk.LeftMiddle:
                offsets.Add(new Vector2(-1, 0));
                break;

            case PositionOnChunk.LeftTopCorner:
                offsets.Add(new Vector2(-1, 0));
                offsets.Add(new Vector2(-1, 1));
                offsets.Add(new Vector2(0, 1));
                break;

            case PositionOnChunk.RightBottomCorner:
                offsets.Add(new Vector2(1, 0));
                offsets.Add(new Vector2(1, -1));
                offsets.Add(new Vector2(0, -1));
                break;

            case PositionOnChunk.RightMiddle:
                offsets.Add(new Vector2(1, 0));
                break;

            case PositionOnChunk.RightTopCorner:
                offsets.Add(new Vector2(0, 1));
                offsets.Add(new Vector2(1, 1));
                offsets.Add(new Vector2(1, 0));
                break;

            case PositionOnChunk.MiddleTop:
                offsets.Add(new Vector2(0, 1));
                break;

            case PositionOnChunk.MiddleBottom:
                offsets.Add(new Vector2(0, -1));
                break;

            default:
                break;
        }

        foreach (Vector2 offset in offsets)
        {
            Vector2 positionOfRelevantChunk = currentChunk.positionOnMatrix + offset;
            chunks[positionOfRelevantChunk].Activate();
        }
    }
    public void DeactivateAdjacentChunks(Chunk originChunk)
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                Vector2 positionOffset = new Vector2(i, j);
                Vector2 position = originChunk.positionOnMatrix + positionOffset;

                if (position != originChunk.positionOnMatrix)
                {
                    chunks[position].Deactivate();
                }
            }
        }
    }

    private PositionOnChunk GetPositionOnChunk()
    {
        //Left side
        if (playersRelativePosition.x < cameraWidth * 1.5f)
        {
            if (playersRelativePosition.y < cameraHeight * 1.5f)
            {
                lastPosition = PositionOnChunk.LeftBottomCorner;
                return PositionOnChunk.LeftBottomCorner;
            }
            if (playersRelativePosition.y > chunkSize.y - cameraHeight * 1.5f)
            {
                lastPosition = PositionOnChunk.LeftTopCorner;
                return PositionOnChunk.LeftTopCorner;
            }
            if (playersRelativePosition.y >= cameraHeight * 1.5f && playersRelativePosition.y <= chunkSize.y - cameraHeight * 1.5f)
            {
                lastPosition = PositionOnChunk.LeftMiddle;
                return PositionOnChunk.LeftMiddle;
            }
        }
        //Right side
        if (playersRelativePosition.x > chunkSize.x - cameraWidth * 1.5f)
        {
            if (playersRelativePosition.y < cameraHeight * 1.5f)
            {
                lastPosition = PositionOnChunk.RightBottomCorner;
                return PositionOnChunk.RightBottomCorner;
            }
            if (playersRelativePosition.y > chunkSize.y - cameraHeight * 1.5f)
            {
                lastPosition = PositionOnChunk.RightTopCorner;
                return PositionOnChunk.RightTopCorner;
            }
            if (playersRelativePosition.y >= cameraHeight * 1.5f && playersRelativePosition.y <= chunkSize.y - cameraHeight * 1.5f)
            {
                lastPosition = PositionOnChunk.RightMiddle;
                return PositionOnChunk.RightMiddle;
            }
        }
        //Middle
        if (playersRelativePosition.x >= cameraWidth * 1.5f && playersRelativePosition.x <= chunkSize.x - cameraWidth * 1.5f)
        {
            if (playersRelativePosition.y < cameraHeight * 1.5f)
            {
                lastPosition = PositionOnChunk.MiddleBottom;
                return PositionOnChunk.MiddleBottom;
            }
            if (playersRelativePosition.y > chunkSize.y - cameraHeight * 1.5f)
            {
                lastPosition = PositionOnChunk.MiddleTop;
                return PositionOnChunk.MiddleTop;
            }
        }
        return lastPosition;
    }
}
