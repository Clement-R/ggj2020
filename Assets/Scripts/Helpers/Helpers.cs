using UnityEngine;

public static class Helpers
{

    public static bool PositionIsOver(Camera p_camera, Collider2D p_collider, Vector3 p_screenPosition)
    {
        var worldPosition = p_camera.ScreenToWorldPoint(p_screenPosition);
        return p_collider.OverlapPoint(worldPosition);
    }

    public static Color SetA(this Color p_color, float p_a)
    {
        return new Color(p_color.r, p_color.g, p_color.b, p_a);
    }

    public static Color DarkerShade(this Color p_color)
    {
        float h;
        float s;
        float v;
        Color.RGBToHSV(p_color, out h, out s, out v);
        s += 0.2f;
        return Color.HSVToRGB(h, s, v);
    }
}