using UnityEngine;
using System.Collections.Generic;

public class InfiniteRunnerScroller : MonoBehaviour
{
    public GameObject chunkPrefab;
    public float scrollSpeed = 5f;
    public float chunkWidth = 20f;   // EXACT width of one chunk
    public int startChunks = 2;

    private List<GameObject> chunks = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < startChunks; i++)
        {
            SpawnChunk(i * chunkWidth);
        }
    }

    void Update()
    {
        MoveChunks();
        HandleSpawning();
        HandleDestroying();
    }

    void MoveChunks()
    {
        foreach (GameObject chunk in chunks)
        {
            chunk.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }
    }

    void HandleSpawning()
    {
        GameObject lastChunk = chunks[chunks.Count - 1];

        // 🔑 spawn ONLY when last chunk reaches x <= 0
        if (lastChunk.transform.position.x <= 0)
        {
            SpawnChunk(lastChunk.transform.position.x + chunkWidth);
        }
    }

    void HandleDestroying()
    {
        GameObject firstChunk = chunks[0];

        if (firstChunk.transform.position.x <= -chunkWidth)
        {
            chunks.RemoveAt(0);
            Destroy(firstChunk);
        }
    }

    void SpawnChunk(float xPos)
    {
        GameObject newChunk = Instantiate(
            chunkPrefab,
            new Vector3(xPos, 0, 0),
            Quaternion.identity
        );

        chunks.Add(newChunk);
    }
}
