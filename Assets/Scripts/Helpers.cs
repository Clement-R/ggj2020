using UnityEngine;

public static class Helpers
{

    public static bool PositionIsOver(Camera p_camera, Collider2D p_collider, Vector3 p_screenPosition)
    {
        var worldPosition = p_camera.ScreenToWorldPoint(p_screenPosition);
        return p_collider.OverlapPoint(worldPosition);
    }
}