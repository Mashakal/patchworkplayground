using UnityEngine;

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

}
