using UnityEngine;

namespace MiscUtils
{
    public static class UtilsMethod
    {
        private const int mapMax = 110; // must be greater than the grid size in the game

        public static Vector3Int ToFloorInt(this Vector3 val)
        {
            return Vector3Int.FloorToInt(val);
        }
        
        /// <summary>
        /// Converts 2d grid into a hash value.
        /// hash formula for positive grid = x + y * 2 [Note the value of the grid never gets negative]
        /// hash formula for a quad grid = (x + cellSize / 2) + (y + cellSize / 2) * cellSize; [Note - this formula works for -ve to +ve range of x and y]
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static int GetHash(this Vector3 v)
        {
            Vector3Int val = v.ToFloorInt();
            return ((val.z + mapMax) * mapMax * 2) + val.x + mapMax;
        }
    }
}