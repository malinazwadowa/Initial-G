using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    public Transform grid;
    public GameObject mapPrefab;
    public Vector2 chunkSize;
    [HideInInspector] public Queue<GameObject> availableMaps = new Queue<GameObject>();

    private Vector2 positionOnMatrixOfInitialChunk = new Vector2(0, 0);
    private Chunk currentChunk;
    private Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk>();

    private float cameraHeight;
    private float cameraWidth;

    private SectorOfChunk lastSector = SectorOfChunk.LeftBottomCorner;
    private List<Vector2> offsetsOfNeededChunks;

    private readonly List<Vector2> offsetsOfNeighboringChunks = new List<Vector2>
    {
        new Vector2(-1, -1),
        new Vector2(-1, 0),
        new Vector2(-1, 1),
        new Vector2(0, -1),
        new Vector2(0, 1),
        new Vector2(1, -1),
        new Vector2(1, 0),
        new Vector2(1, 1)
    };


    void Start()
    {
        offsetsOfNeededChunks = new List<Vector2>();
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;

        InstantiateMapPrefabs();

        Chunk startChunk = new Chunk();

        startChunk.Initialize(positionOnMatrixOfInitialChunk, chunkSize);

        currentChunk = startChunk;
        chunks.Add(startChunk.positionOnMatrix, startChunk);
        
        InitalizeAdjacentChunks(startChunk);
        
        startChunk.Activate();
    }

    private void Update()
    {
        UpdateCurrentChunk();

        SectorOfChunk currentSector = GetCurrentSectorOnChunk();

        if (lastSector != currentSector)
        {
            lastSector = currentSector;
            offsetsOfNeededChunks = GetOffsetsOfNeededChunks(currentSector);
            UpdateActiveChunks(currentChunk, offsetsOfNeededChunks);
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

    private void UpdateCurrentChunk()
    {
        Vector3 cameraRelativePosition = Camera.main.transform.position - new Vector3(chunkSize.x * currentChunk.positionOnMatrix.x, chunkSize.y * currentChunk.positionOnMatrix.y, 0);
        bool chunkChanged = false;
        Vector2 offsetOfEnteredChunk = Vector2.zero;

        if(chunkChanged == false)
        {
            if (cameraRelativePosition.x < 0)
            {
                offsetOfEnteredChunk = new Vector2(-1, 0);
                chunkChanged = true;
            }
            if (cameraRelativePosition.x > 150)
            {
                offsetOfEnteredChunk = new Vector2(1, 0);
                chunkChanged = true;
            }
            if (cameraRelativePosition.y < 0)
            {
                offsetOfEnteredChunk = new Vector2(0, -1);
                chunkChanged = true;
            }
            if (cameraRelativePosition.y > 100)
            {
                offsetOfEnteredChunk = new Vector2(0, 1);
                chunkChanged = true;
            }
        }

        if (chunkChanged)
        {
            chunkChanged = false;
            Vector2 positionOnMatrixOfNewChunk = currentChunk.positionOnMatrix + offsetOfEnteredChunk;
            currentChunk = chunks[positionOnMatrixOfNewChunk];
            currentChunk.Activate();
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

    public void UpdateActiveChunks(Chunk originChunk, List<Vector2> offsetsOfChunksToActivate)
    {
        foreach (Vector2 offset in offsetsOfNeighboringChunks)
        {
            if (!offsetsOfChunksToActivate.Contains(offset))
            {
                Vector2 position = originChunk.positionOnMatrix + offset;
                chunks[position].Deactivate();
            }
        }

        foreach (Vector2 offset in offsetsOfChunksToActivate)
        {
            Vector2 position = originChunk.positionOnMatrix + offset;
            chunks[position].Activate();
        }
    }

    private List<Vector2> GetOffsetsOfNeededChunks(SectorOfChunk positionOnChunk)
    {
        List<Vector2> offsets = new List<Vector2>();

        switch (positionOnChunk)
        {
            case SectorOfChunk.LeftBottomCorner:
                offsets.Add(new Vector2(-1, 0));
                offsets.Add(new Vector2(-1, -1));
                offsets.Add(new Vector2(0, -1));
                break;

            case SectorOfChunk.LeftMiddle:
                offsets.Add(new Vector2(-1, 0));
                break;

            case SectorOfChunk.LeftTopCorner:
                offsets.Add(new Vector2(-1, 0));
                offsets.Add(new Vector2(-1, 1));
                offsets.Add(new Vector2(0, 1));
                break;

            case SectorOfChunk.RightBottomCorner:
                offsets.Add(new Vector2(1, 0));
                offsets.Add(new Vector2(1, -1));
                offsets.Add(new Vector2(0, -1));
                break;

            case SectorOfChunk.RightMiddle:
                offsets.Add(new Vector2(1, 0));
                break;

            case SectorOfChunk.RightTopCorner:
                offsets.Add(new Vector2(0, 1));
                offsets.Add(new Vector2(1, 1));
                offsets.Add(new Vector2(1, 0));
                break;

            case SectorOfChunk.MiddleTop:
                offsets.Add(new Vector2(0, 1));
                break;

            case SectorOfChunk.MiddleBottom:
                offsets.Add(new Vector2(0, -1));
                break;

            default:
                break;
        }

        return offsets;
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

    private SectorOfChunk GetCurrentSectorOnChunk()
    {
        Vector3 cameraRelativePosition = Camera.main.transform.position - new Vector3(currentChunk.positionOnMatrix.x * chunkSize.x, currentChunk.positionOnMatrix.y * chunkSize.y, 0);

        //Left side
        if (cameraRelativePosition.x < cameraWidth * 1.5f)
        {
            if (cameraRelativePosition.y < cameraHeight * 1.5f)
            {
                return SectorOfChunk.LeftBottomCorner;
            }
            if (cameraRelativePosition.y > chunkSize.y - cameraHeight * 1.5f)
            {
                return SectorOfChunk.LeftTopCorner;
            }
            if (cameraRelativePosition.y >= cameraHeight * 1.5f && cameraRelativePosition.y <= chunkSize.y - cameraHeight * 1.5f)
            {
                return SectorOfChunk.LeftMiddle;
            }
        }
        //Right side
        if (cameraRelativePosition.x > chunkSize.x - cameraWidth * 1.5f)
        {
            if (cameraRelativePosition.y < cameraHeight * 1.5f)
            {
                return SectorOfChunk.RightBottomCorner;
            }
            if (cameraRelativePosition.y > chunkSize.y - cameraHeight * 1.5f)
            {
                return SectorOfChunk.RightTopCorner;
            }
            if (cameraRelativePosition.y >= cameraHeight * 1.5f && cameraRelativePosition.y <= chunkSize.y - cameraHeight * 1.5f)
            {
                return SectorOfChunk.RightMiddle;
            }
        }
        //Middle
        if (cameraRelativePosition.x >= cameraWidth * 1.5f && cameraRelativePosition.x <= chunkSize.x - cameraWidth * 1.5f)
        {
            if (cameraRelativePosition.y < cameraHeight * 1.5f)
            {
                return SectorOfChunk.MiddleBottom;
            }
            if (cameraRelativePosition.y > chunkSize.y - cameraHeight * 1.5f)
            {
                return SectorOfChunk.MiddleTop;
            }
        }
        return lastSector;
    }
}
