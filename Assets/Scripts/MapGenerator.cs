using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public GameObject chunkPrefab;

	[Header("Map Settings")]
	public int mapWidth;
	public int mapHeight;
	public int seed;

	[Header("Generation Settings")]
	public float noiseScale;
	public int octaves;
	public float persistance;
	public float lacunarity;
	public Vector2 offset;

	private List<GameObject> chunks = new List<GameObject>();

	public void Start () {
		for (int z = 0; z < mapHeight; z++) {
			for (int x = 0; x < mapWidth; x++) {
				Vector3 chunkPos = new Vector3 (x * 16, 0, z * 16);

				Vector2 startPos = new Vector2(chunkPos.x - 8, chunkPos.z - 8);
				Vector2 endPos = new Vector2(chunkPos.x + 8, chunkPos.z + 8);

				Dictionary<Vector2, int> noiseMap = Noise.GenerateNoiseMap (startPos, endPos, seed, noiseScale, octaves, persistance, lacunarity, offset);

				GameObject chunk = Instantiate (chunkPrefab, chunkPos, Quaternion.identity, transform);
				chunk.name = "Chunk (" + x + "," + z + ")";
				chunk.GetComponent<ChunkController> ().DrawNoiseMap (noiseMap);

				chunks.Add (chunk);
			}
		}
	}

}