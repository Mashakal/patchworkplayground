using UnityEngine;
using System;

public static class ColorLibrary {

    // Colors.
    private static Color greenYellow = new Color(0.176f, 0.176f, 0);
    private static Color healthyGreen = new Color(0, 0.214f, 0.34f);

    // Getters.
    public static Color GreenYellow
    {
        get { return greenYellow; }
    }

    public static Color HealthyGreen
    {
        get { return healthyGreen; }
    }


    // Methods.
    public static Color[] LeafColors()
    {
        return new Color[] { greenYellow, healthyGreen };
    }

	public static Color[] FlowerColors()
	{
		int numColors = 20;
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
