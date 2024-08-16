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
    }
}