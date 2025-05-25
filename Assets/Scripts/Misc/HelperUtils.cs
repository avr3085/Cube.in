using UnityEngine;
using System.Collections.Generic;

namespace Misc
{
    /// <summary>
    /// Utility class containing helper functions
    /// </summary>
    public static class HelperUtils
    {
        /// <summary>
        /// mapSize - maximum size of map in either quadrant, to calculate a 1d hash key
        /// mapSize must be greater than the size of the map used in gamplay
        /// otherwise error will appear
        /// </summary>
        private const int mapSize = 52; // The actual map Size is mapSize * 2
        private const int boundsOffset = 48; // This must always be less than the size of the map
        private const float maxMagnitudeOffset = 1000f; // Maximum magnitude offset between two vectors

        public static float MaxMagnitudeOffset => maxMagnitudeOffset;
        public static int BoundsOffset => boundsOffset;

        public static Vector3Int ToFloorInt(this Vector3 val)
        {
            return Vector3Int.FloorToInt(val);
        }

        /// <summary>
        /// Converts 2d Vector to single hash value
        /// Hash Formula for only first quadrant = x + (y * mapSize) [will work as long as x, y is positive]
        /// Hash Formula used for all quadrants = (x + mapSize/2) + (y + mapSize/2) * mapSize [works for all position]
        /// Note - since we are not using mapSize/2 will make the calculation slow, mapSize is already halfed
        /// Hence the above Variable "mapSize" == mapSize * 2;
        /// </summary>
        /// <param name="v">Vector3 Extention</param>
        /// <returns>int hash Value</returns>
        public static int ToHash(this Vector3 v)
        {
            Vector3Int val = v.ToFloorInt();
            return ((val.z + mapSize) * (mapSize * 2)) + val.x + mapSize;
        }

        /// <summary>
        /// Create's Bounding Box along the input position
        /// Check which vertex of bounding volume is overlapping the grid
        /// </summary>
        /// <param name="pos">position</param>
        /// <returns>Array of hash Key, overlapping uniform grid cell</returns>
        public static IEnumerable<int> ToBBoxHash(this Vector3 pos)
        {
            Vector3 bl = new Vector3(pos.x - 0.5f, 0f, pos.z - 0.5f);
            yield return bl.ToHash();

            Vector3 br = new Vector3(pos.x + 0.5f, 0f, pos.z - 0.5f);
            yield return br.ToHash();

            Vector3 tl = new Vector3(pos.x - 0.5f, 0f, pos.z + 0.5f);
            yield return tl.ToHash();

            Vector3 tr = new Vector3(pos.x + 0.5f, 0f, pos.z + 0.5f);
            yield return tr.ToHash();
        }

        /// <summary>
        /// Return list of all the position vector around the bot, in Squared Field
        /// Use Visual Debug to check the results
        /// </summary>
        /// <param name="pos">Current position of the bot</param>
        /// <param name="boxSize">Boxed visible Range, Size must be even number</param>
        /// <param name="mapSize">max allowed Visible Map Size</param>
        /// <returns></returns>
        public static IEnumerable<Vector3> BoxVisionV3(this Vector3 pos, int boxSize = 2, int bounds = boundsOffset)
        {
            int xHalved = -(boxSize / 2);
            int yHalved = -(boxSize / 2);

            Vector3Int posFloor = pos.ToFloorInt();

            for (int i = 0; i < boxSize + 1; i++)
            {
                int currX = xHalved;
                for (int j = 0; j < boxSize + 1; j++)
                {
                    Vector3 outPos = new Vector3(posFloor.x + currX, 0f, posFloor.z + yHalved);
                    if (outPos.x > -bounds && outPos.x < bounds && outPos.z > -bounds && outPos.z < bounds)
                    {
                        yield return new Vector3(posFloor.x + currX, 0f, posFloor.z + yHalved);
                    }

                    currX++;
                }

                yHalved++;
            }
        }
        
        /// <summary>
        /// Returns All the hash area around the bot
        /// </summary>
        /// <param name="pos">Bot current position</param>
        /// <param name="boxSize">Boxed visible Range, Size must be even number</param>
        /// <returns></returns>
        public static IEnumerable<int> BoxVisionHash(this Vector3 pos, int boxSize = 2, int bounds = boundsOffset)
        {
            int xHalved = -(boxSize / 2);
            int yHalved = -(boxSize / 2);

            Vector3Int posFloor = pos.ToFloorInt();

            for (int i = 0; i < boxSize + 1; i++)
            {
                int currX = xHalved;
                for (int j = 0; j < boxSize + 1; j++)
                {
                    Vector3 outPos = new Vector3(posFloor.x + currX, 0f, posFloor.z + yHalved);
                    if (outPos.x > -bounds && outPos.x < bounds && outPos.z > -bounds && outPos.z < bounds)
                    {
                        yield return outPos.ToHash();
                    }

                    currX++;
                }

                yHalved++;
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
    }
}