using UnityEngine;

namespace MiscUtils
{
    public static class UtilsMethod
    {
        private const int mapMax = 100;

        public static Vector3 ToFloor(this Vector3 val)
        {
            return new Vector3(Mathf.FloorToInt(val.x), 0f, Mathf.FloorToInt(val.z));
        }
        
        public static int GetHash(this Vector3 v)
        {
            var val = v.ToFloor();
            return (((int)val.z + mapMax) * (mapMax * 2)) + ((int)val.x + mapMax);
        }

        // public static int GetHashKey(this Vector3 position, int mapWidth)
        // {
        //     int x = Mathf.FloorToInt(position.x);
        //     int y = Mathf.FloorToInt(position.z);
        //     return (x * mapWidth) + y;
        // }

        // public static Vector2 ToV2(this Vector3 val)
        // {
        //     return new Vector2(val.x, val.y);
        // }
        
        // public static Vector3 ToV3(this Vector2 val)
        // {
        //     return new Vector3(val.x, val.y, 0f);
        // }
    }
}