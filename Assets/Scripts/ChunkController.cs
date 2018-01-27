using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour {

	private MeshFilter meshFilter;
	private Mesh mesh;
	private List<Vector3> vertices = new List<Vector3> ();
	private List<int> triangles = new List<int> ();
	private List<Vector2> uv = new List<Vector2> ();

	public Dictionary<Vector2, int> noiseMap;

	public void DrawNoiseMap (Dictionary<Vector2, int> givenNoiseMap) {

		noiseMap = givenNoiseMap;

		meshFilter = GetComponent<MeshFilter> ();
		mesh = new Mesh ();

		if (name == "Chunk (1,5)") {
			Debug.Log ("1: " + noiseMap [new Vector2 (17, 16)]);
		} else if (name == "Chunk (2,5)") {
			Debug.Log ("2: " + noiseMap [new Vector2 (1, 16)]);
		}

		for (int z = -8; z < 8; z++) {
			for (int x = -8; x < 8; x++) {

				for (int y = 0; y <= noiseMap[new Vector2(x + 9, z + 9)]; y++) {
					//Top
					if (y == noiseMap[new Vector2(x + 9, z + 9)]) {
						int vertCount = vertices.Count;

						vertices.AddRange (new Vector3[] {
							new Vector3 (x, 	y, z + 1),
							new Vector3 (x + 1, y, z + 1),
							new Vector3 (x, 	y, z),
							new Vector3 (x + 1, y, z)
						});

						triangles.AddRange (new int[] {
							vertCount + 0, vertCount + 1, vertCount + 2,
							vertCount + 1, vertCount + 3, vertCount + 2
						});

						uv.AddRange (new Vector2[] {
							new Vector2 (x, 	z + 1),
							new Vector2 (x + 1, z + 1),
							new Vector2 (x,		z),
							new Vector2 (x + 1, z)
						});
					}
					//Front
					if (y > noiseMap [new Vector2(x + 9, z + 10)]) {
						int vertCount = vertices.Count;

						vertices.AddRange (new Vector3[] {
							new Vector3 (x + 1, y, 		z + 1),
							new Vector3 (x, 	y, 		z + 1),
							new Vector3 (x + 1, y - 1, 	z + 1),
							new Vector3 (x, 	y - 1, 	z + 1)
						});

						triangles.AddRange (new int[] {
							vertCount + 0, vertCount + 1, vertCount + 2,
							vertCount + 1, vertCount + 3, vertCount + 2
						});

						uv.AddRange (new Vector2[] {
							new Vector2 (x + 1, y + 1),
							new Vector2 (x, 	y + 1),
							new Vector2 (x + 1, y),
							new Vector2 (x, 	y)
						});
					}
					//Right
					if (y > noiseMap [new Vector2(x + 10, z + 9)]) {
						int vertCount = vertices.Count;

						vertices.AddRange (new Vector3[] {
							new Vector3 (x + 1, y, 		z),
							new Vector3 (x + 1,	y, 		z + 1),
							new Vector3 (x + 1, y - 1, 	z),
							new Vector3 (x + 1,	y - 1, 	z + 1)
						});

						triangles.AddRange (new int[] {
							vertCount + 0, vertCount + 1, vertCount + 2,
							vertCount + 1, vertCount + 3, vertCount + 2
						});

						uv.AddRange (new Vector2[] {
							new Vector2 (x + 1, y + 1),
							new Vector2 (x, 	y + 1),
							new Vector2 (x + 1, y),
							new Vector2 (x, 	y)
						});
					}
					//Back
					if (y > noiseMap [new Vector2(x + 9, z + 8)]) {
						int vertCount = vertices.Count;

						vertices.AddRange (new Vector3[] {
							new Vector3 (x, 	y, 		z),
							new Vector3 (x + 1, y, 		z),
							new Vector3 (x, 	y - 1, 	z),
							new Vector3 (x + 1, y - 1, 	z)
						});

						triangles.AddRange (new int[] {
							vertCount + 0, vertCount + 1, vertCount + 2,
							vertCount + 1, vertCount + 3, vertCount + 2
						});

						uv.AddRange (new Vector2[] {
							new Vector2 (x + 1, y + 1),
							new Vector2 (x, 	y + 1),
							new Vector2 (x + 1, y),
							new Vector2 (x, 	y)
						});
					}
					//Left
					if (y > noiseMap [new Vector2(x + 8, z + 9)]) {
						int vertCount = vertices.Count;

						vertices.AddRange (new Vector3[] {
							new Vector3 (x, y, 		z + 1),
							new Vector3 (x,	y, 		z),
							new Vector3 (x, y - 1, 	z + 1),
							new Vector3 (x,	y - 1, 	z)
						});

						triangles.AddRange (new int[] {
							vertCount + 0, vertCount + 1, vertCount + 2,
							vertCount + 1, vertCount + 3, vertCount + 2
						});

						uv.AddRange (new Vector2[] {
							new Vector2 (x + 1, y + 1),
							new Vector2 (x, 	y + 1),
							new Vector2 (x + 1, y),
							new Vector2 (x, 	y)
						});
					}
				}
			}
		}

		mesh.vertices = vertices.ToArray ();
		mesh.triangles = triangles.ToArray ();
		mesh.uv = uv.ToArray ();
		mesh.RecalculateNormals ();

		meshFilter.mesh = mesh;
	}
}
