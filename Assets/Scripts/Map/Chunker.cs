using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunker : SingletonMonoBehaviour<Chunker>
{
    float cameraHeight;
    float cameraWidth;

    public Transform grid;
    public GameObject mapPrefab;
    public Vector2 startPosition;

    Transform playersTransform;
    //Vector3 chunkPosition = new Vector3(0,0,0);
    Vector2 chunkSize = new Vector2(150, 100);
    Vector3 playersRelativePosition;

    PositionOnChunk lastPosition = PositionOnChunk.LeftBottomCorner;
    PositionOnChunk cachedLastPosition;
    Chunk currentChunk;

    public Queue<GameObject> availableMaps = new Queue<GameObject>();

    public Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk>();
    public List<Chunk> adjacentChunks = new List<Chunk>();

    // Start is called before the first frame update
    void Start()
    {
        cachedLastPosition = lastPosition;
        for (int i = 0; i < 4; i++)
        {
            GameObject map = Instantiate(mapPrefab, grid);
            map.transform.position = new Vector3(500, 500, 0);
            availableMaps.Enqueue(map);
        }
        Chunk startChunk = new Chunk();
        startChunk.Initialize(startPosition, chunkSize);
        currentChunk = startChunk;
        startChunk.Activate();
        chunks.Add(startChunk.positionOnMatrix, startChunk);
        InitalizeAdjacentChunks(startChunk);
        Debug.Log(chunks.Count);

        playersTransform = PlayerManager.Instance.GetPlayersTransform();
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(chunks.Count);
            foreach (Chunk chunk in chunks.Values)
            {
                Debug.Log(chunk.positionOnMatrix);
                
            }
        }

        playersRelativePosition = playersTransform.position - new Vector3(currentChunk.positionOnMatrix.x * chunkSize.x, currentChunk.positionOnMatrix.y * chunkSize.y, 0);

        GetPositionOnChunk();
        CheckForChunkChange(playersRelativePosition);
        if (cachedLastPosition != lastPosition)
        {
            cachedLastPosition = lastPosition;
            ActivateRelevantChunks();
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
            chunkChanged = false;
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
                    Debug.Log("Deactivated chunk with id: " + position);
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
                //bottom left corner
                Debug.Log("bottom left corner");
                lastPosition = PositionOnChunk.LeftBottomCorner;
                return PositionOnChunk.LeftBottomCorner;
            }
            if (playersRelativePosition.y > chunkSize.y - cameraHeight * 1.5f)
            {
                //top left corner
                Debug.Log("top left corner");
                lastPosition = PositionOnChunk.LeftTopCorner;
                return PositionOnChunk.LeftTopCorner;
            }
            if (playersRelativePosition.y >= cameraHeight * 1.5f && playersRelativePosition.y <= chunkSize.y - cameraHeight * 1.5f)
            {
                //left side middle
                Debug.Log("left side middle");
                lastPosition = PositionOnChunk.LeftMiddle;
                return PositionOnChunk.LeftMiddle;
            }
        }
        //Right side
        if (playersRelativePosition.x > chunkSize.x - cameraWidth * 1.5f)
        {
            if (playersRelativePosition.y < cameraHeight * 1.5f)
            {
                //bottom right corner
                Debug.Log("bottom right corner");
                lastPosition = PositionOnChunk.RightBottomCorner;
                return PositionOnChunk.RightBottomCorner;
            }
            if (playersRelativePosition.y > chunkSize.y - cameraHeight * 1.5f)
            {
                //top right corner
                Debug.Log("top right corner");
                lastPosition = PositionOnChunk.RightTopCorner;
                return PositionOnChunk.RightTopCorner;
            }
            if (playersRelativePosition.y >= cameraHeight * 1.5f && playersRelativePosition.y <= chunkSize.y - cameraHeight * 1.5f)
            {
                //right side middle
                Debug.Log("right side middle");
                lastPosition = PositionOnChunk.RightMiddle;
                return PositionOnChunk.RightMiddle;
            }
        }
        //Middle
        if (playersRelativePosition.x >= cameraWidth * 1.5f && playersRelativePosition.x <= chunkSize.x - cameraWidth * 1.5f)
        {
            if (playersRelativePosition.y < cameraHeight * 1.5f)
            {
                //bottom middle
                Debug.Log("bottom middle");
                lastPosition = PositionOnChunk.MiddleBottom;
                return PositionOnChunk.MiddleBottom;
            }
            if (playersRelativePosition.y > chunkSize.y - cameraHeight * 1.5f)
            {
                //top middle
                Debug.Log("top middle");
                lastPosition = PositionOnChunk.MiddleTop;
                return PositionOnChunk.MiddleTop;
            }
        }
        return lastPosition;
    }
}
public enum PositionOnChunk
{
    LeftBottomCorner,
    LeftMiddle,
    LeftTopCorner,
    RightBottomCorner,
    RightMiddle,
    RightTopCorner,
    MiddleTop,
    MiddleBottom
}