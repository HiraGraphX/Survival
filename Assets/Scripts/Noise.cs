using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {

	public static Dictionary<Vector2, int> GenerateNoiseMap (Vector2 startPos, Vector2 endPos, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
		float [,] tempNoiseMap = new float[18, 18];

		System.Random rng = new System.Random (seed);
		Vector2[] octaveOffsets = new Vector2[octaves];

		for (int i = 0; i < octaves; i++) {
			float offsetX = rng.Next (-100000, 100000) + offset.x;
			float offsetY = rng.Next (-100000, 100000) + offset.y;
			octaveOffsets [i] = new Vector2 (offsetX, offsetY);
		}

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		for (int y = 0; y < 18; y++) {
			for (int x = 0; x < 18; x++) {

				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int o = 0; o < octaves; o++) {
					float sampleX = x / scale * frequency + octaveOffsets[o].x;
					float sampleY = y / scale * frequency + octaveOffsets[o].y;

					sampleX += (16 / scale * frequency) * (startPos.x / 16);
					sampleY += (16 / scale * frequency) * (startPos.y / 16);

					float perlinValue = Mathf.PerlinNoise (sampleX, sampleY);
					noiseHeight += perlinValue * amplitude;
					frequency *= lacunarity;
				}

				if (noiseHeight > maxNoiseHeight) {
					maxNoiseHeight = noiseHeight;
				} else if (noiseHeight < minNoiseHeight) {
					minNoiseHeight = noiseHeight;
				}

				tempNoiseMap[x, y] = noiseHeight;
			}
		}

		Dictionary<Vector2, int> noiseMap = new Dictionary<Vector2, int>();

		for (int y = 0; y < 18; y++) {
			for (int x = 0; x < 18; x++) {
				noiseMap.Add(new Vector2(x, y), Mathf.RoundToInt (tempNoiseMap[x, y] * 16 - 12));
			}
		}

		return noiseMap;
	}

}
