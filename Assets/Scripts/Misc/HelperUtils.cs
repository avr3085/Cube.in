using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

namespace Misc
{
    /// <summary>
    /// Utility class containing helper functions
    /// </summary>
    public static class HelperUtils
    {
        /// <summary>
        /// mapMax - maximum size of map in either quadrant, to calculate a 1d hash key
        /// mapMax must be greater than the size of the map used in gamplay
        /// otherwise error will appear
        /// </summary>
        private const int mapMax = 52; // The actual map Size is mapMax * 2

        /// <summary>
        /// maxSqrdDistaceCheck - maximum range of distce squared to be checked
        /// </summary>
        private const int maxSqrdDistaceCheck = 100;
        public static int MaxSqrdDistaceCheck => maxSqrdDistaceCheck;

        /// <summary>
        /// Converts Floting point vector3 values to integer format
        /// </summary>
        /// <param name="val">Vector3 Extention method</param>
        /// <returns>Int Vector3 value</returns>
        public static Vector3Int ToFloorInt(this Vector3 val)
        {
            return Vector3Int.FloorToInt(val);
        }
        
        /// <summary>
        /// Converts 2d Vector to single hash value
        /// Hash Formula for only first quadrant = x + (y * mapMax) [will work as long as x, y is positive]
        /// Hash Formula used for all quadrants = (x + mapMax/2) + (y + mapMax/2) * mapMax [works for all position]
        /// Note - since we are not using mapMax/2 will make the calculation slow, mapMax is already halfed
        /// Hence the above Variable "mapMax" == mapMax * 2;
        /// </summary>
        /// <param name="v">Vector3 Extention</param>
        /// <returns>int hash Value</returns>
        public static int ToHash(this Vector3 v)
        {
            Vector3Int val = v.ToFloorInt();
            return ((val.z + mapMax) * (mapMax * 2)) + val.x + mapMax;
        }

        /// <summary>
        /// Create's Bounding Box along the input position
        /// Check which vertex of bounding volume is overlapping the grid
        /// </summary>
        /// <param name="pos">position</param>
        /// <returns>Array of hash Key, overlapping uniform grid cell</returns>
        public static IEnumerable<int> ToBBoxHash(this Vector3 pos)
        {
            int currentHash = -1;
            Vector3 bl = new Vector3(pos.x - 0.5f, 0f, pos.z - 0.5f);
            currentHash = bl.ToHash();
            yield return currentHash;

            Vector3 br = new Vector3(pos.x + 0.5f, 0f, pos.z - 0.5f);
            if(currentHash < br.ToHash())
            {
                currentHash = br.ToHash();
                yield return currentHash;
            }

            Vector3 tl = new Vector3(pos.x - 0.5f, 0f, pos.z + 0.5f);
            if(currentHash < tl.ToHash())
            {
                currentHash = tl.ToHash();
                yield return currentHash;
            }

            Vector3 tr = new Vector3(pos.x + 0.5f, 0f, pos.z + 0.5f);
            if(currentHash < tr.ToHash())
            {
                currentHash = tr.ToHash();
                yield return currentHash;
            }
        }

        /// <summary>
        /// Bresenham’s Line Generation Algorithm
        /// [Not used in the game, cuz of optimaztion problem]
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="returnCentre"></param>
        /// <returns></returns>
        // public static IEnumerable<(int x, int y)> BresenhamLine(int ax, int ay, int bx, int by, bool returnCentre)
        // {
        //     // declare all locals at the top so it's obvious how big the footprint is
        //     int dx, dy, xinc, yinc, side, i, error;

        //     // starting cell is always returned
        //     if(returnCentre)
        //     {
        //     yield return (ax,ay);
        //     }

        //     xinc  = (bx < ax) ? -1 : 1;
        //     yinc  = (by < ay) ? -1 : 1;
        //     dx    = xinc * (bx - ax);
        //     dy    = yinc * (by - ay);

        //     if (dx == dy) // Handle perfect diagonals
        //     {
        //     while (dx --> 0)
        //     {
        //         ax += xinc;
        //         ay += yinc;
        //         yield return (ax,ay);
        //     }

        //     yield break;
        //     }
            
        //     // Handle all other lines
            
        //     side = -1 * ((dx == 0 ? yinc : xinc) - 1);

        //     i     = dx + dy;
        //     error = dx - dy;
            
        //     dx *= 2;
        //     dy *= 2;

        //     while (i --> 0)
        //     {
        //     if (error > 0 || error == side)
        //     {
        //         ax    += xinc;
        //         error -= dy;
        //     }
        //     else
        //     {
        //         ay    += yinc;
        //         error += dx;
        //     }

        //     yield return (ax,ay);
        //     }
        // }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="xSize"></param>
        /// <param name="ySize"></param>
        /// <returns></returns>
        public static IEnumerable<Vector3> ViewField(this Vector3 pos, int mapSize, int xRange = 2, int yRange = 2)
        {
            int xHalved = -(xRange/2);
            int yHalved = -(yRange/2);

            Vector3Int posFloor = pos.ToFloorInt();

            for(int i = 0; i < xRange + 1; i++)
            {
                int currX = xHalved;
                for(int j = 0; j < yRange + 1; j++)
                {
                    Vector3 outPos = new Vector3(posFloor.x + currX, 0f, posFloor.z + yHalved);
                    if(outPos.x > -mapSize && outPos.x < mapSize && outPos.z > -mapSize && outPos.z < mapSize)
                    {
                        yield return new Vector3(posFloor.x + currX, 0f, posFloor.z + yHalved);
                    }

                    currX++;
                }

                yHalved++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mapSize"></param>
        /// <param name="xRange"></param>
        /// <param name="yRange"></param>
        /// <returns></returns>
        public static IEnumerable<int> ViewFieldHash(this Vector3 pos, int mapSize, int xRange = 2, int yRange = 2)
        {
            int xHalved = -(xRange/2);
            int yHalved = -(yRange/2);

            Vector3Int posFloor = pos.ToFloorInt();

            for(int i = 0; i < xRange + 1; i++)
            {
                int currX = xHalved;
                for(int j = 0; j < yRange + 1; j++)
                {
                    Vector3 outPos = new Vector3(posFloor.x + currX, 0f, posFloor.z + yHalved);
                    if(outPos.x > -mapSize && outPos.x < mapSize && outPos.z > -mapSize && outPos.z < mapSize)
                    {
                        yield return outPos.ToHash();
                    }

                    currX++;
                }

                yHalved++;
            }
        }
    }
}