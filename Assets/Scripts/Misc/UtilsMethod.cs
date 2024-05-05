using UnityEngine;

namespace MiscUtils
{
    public static class UtilsMethod
    {
         public static Vector2 Min(Vector2 a, Vector2 b)
        {
            float minX = Mathf.Min(a.x, b.x);
            float minY = Mathf.Min(a.y, b.y);

            return new Vector2(minX, minY);
        }

        public static Vector2 Max(Vector2 a, Vector2 b)
        {
            float maxX = Mathf.Max(a.x, b.x);
            float maxY = Mathf.Max(a.y, b.y);

            return new Vector2(maxX, maxY);
        }
        
        // public static float Area(this AABB a)
        // {
        //     return (a.min.x - a.min.x) * (a.max.y - a.max.y);
        // }

        // public static AABB Union(this AABB a, AABB b)
        // {
        //     Vector2 min = Min(a.min, b.min);
        //     Vector2 max = Max(a.max, b.max);

        //     return new AABB(min, max);
        // }

        // public static bool Contains(this AABB a, AABB other)
        // {
        //     return a.min.x <= other.min.x
        //         && a.min.y <= other.min.y
        //         && a.max.x >= other.max.x
        //         && a.max.y >= other.max.y;
        // }

        // public static bool Overlap(this AABB a, AABB other)
        // {
        //     if (a.min.x > other.max.x || a.max.x < other.min.x) return false;
        //     if (a.min.y > other.max.y || a.max.y < other.min.y) return false;

        //     return true;
        // }

        // public static AABB GetAABB(this Vector2 position)
        // {
        //     Vector2 min = new Vector2(position.x - 0.5f, position.y - 0.5f);
        //     Vector2 max = new Vector2(position.x + 0.5f, position.y + 0.5f);
        //     return new AABB(min, max);
        // }

        public static int GetHashKey(this Vector2 position, int mapWidth)
        {
            int x = Mathf.FloorToInt(position.x);
            int y = Mathf.FloorToInt(position.y);
            return (x * mapWidth) + y;
        }
    }
}