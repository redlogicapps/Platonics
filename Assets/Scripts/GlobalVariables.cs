using UnityEngine;
using System.Collections;

public class GlobalVariables {
	private static bool isRandomized = false;
	private static int index = 0;

	public static Color[] colors = new Color[]{
		new Color (0 / 255.0f, 72 / 255.0f, 255 / 255.0f),
		new Color (0 / 255.0f, 240 / 255.0f, 255 / 255.0f),
		new Color (114 / 255.0f, 221 / 255.0f, 0 / 255.0f),
		new Color (255 / 255.0f, 0 / 255.0f, 228 / 255.0f),
		new Color (255 / 255.0f, 0 / 255.0f, 0 / 255.0f),
		new Color (255 / 255.0f, 246 / 255.0f, 0 / 255.0f)
	};

	public static Color[] randomColors = new Color[] {
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5],
		colors[0], colors[1], colors[2], colors[3], colors[4], colors[5]
	};



	public static void RandomizeColors() {
		index = 0;
		Color temp;
		for (int i = 0; i < 90; i++) {
			int randomValue1 = Random.Range(0, 89);
			int randomValue2 = Random.Range(0, 89);
			temp = randomColors[randomValue2];
			randomColors[randomValue2] = randomColors[randomValue1];
			randomColors[randomValue1] = temp;
		}
	}

	public static Color GetNextColor() {
		if (!isRandomized) {
			RandomizeColors();
			isRandomized = true;
		}
		return randomColors [index++];

	}
}
