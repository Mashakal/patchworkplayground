using UnityEngine;
using System;

public static class ColorLibrary {


    // Methods.

	public static Color[] LeafColors()
	{
		int numColors = 30;
		Color[] leafColors = new Color[numColors];

		for (int i = 0; i < numColors; i++) {
			float hue = ((UnityEngine.Random.value * 105) + 50.0f) / 360.0f;
			float saturation = (UnityEngine.Random.value * 0.4f) + 0.6f;
			float value = (UnityEngine.Random.value * 0.1f) + 0.9f;
			leafColors [i] = Color.HSVToRGB (hue, saturation, value);
		}

		return leafColors;
	}

	public static Color[] FlowerColors()
	{
		int numColors = 30;
		Color[] flowerColors = new Color[numColors];

		for (int i = 0; i < numColors; i++) {
			float hue = (((UnityEngine.Random.value * 90)-30) % 360) / 360.0f;
			float saturation = (UnityEngine.Random.value * 0.4f) + 0.6f;
			float value = (UnityEngine.Random.value * 0.1f) + 0.9f;
			flowerColors [i] = Color.HSVToRGB (hue, saturation, value);
		}

		return flowerColors;
	}

}
