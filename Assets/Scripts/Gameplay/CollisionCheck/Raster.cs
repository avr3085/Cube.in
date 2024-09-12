// using System.Collections.Generic;

// public static partial class Raster
// {
  // old code supports sinle ray only
  // public static IEnumerable<(int x, int y)> Line(int ax, int ay, int bx, int by, bool returnCentre)
  // {
  //   // declare all locals at the top so it's obvious how big the footprint is
  //   int dx, dy, xinc, yinc, side, i, error;

  //   // starting cell is always returned
  //   if(returnCentre)
  //   {
  //     yield return (ax,ay);
  //   }

  //   xinc  = (bx < ax) ? -1 : 1;
  //   yinc  = (by < ay) ? -1 : 1;
  //   dx    = xinc * (bx - ax);
  //   dy    = yinc * (by - ay);

  //   if (dx == dy) // Handle perfect diagonals
  //   {
  //     while (dx --> 0)
  //     {
  //       ax += xinc;
  //       ay += yinc;
  //       yield return (ax,ay);
  //     }

  //     yield break;
  //   }
    
  //   // Handle all other lines
    
  //   side = -1 * ((dx == 0 ? yinc : xinc) - 1);

  //   i     = dx + dy;
  //   error = dx - dy;
    
  //   dx *= 2;
  //   dy *= 2;

  //   while (i --> 0)
  //   {
  //     if (error > 0 || error == side)
  //     {
  //       ax    += xinc;
  //       error -= dy;
  //     }
  //     else
  //     {
  //       ay    += yinc;
  //       error += dx;
  //     }

  //     yield return (ax,ay);
  //   }
  // }

  //supports double ray
  // public static IEnumerable<(int x, int y)> Line(int ax, int ay, int bx, int by, int cx, int cy)
  // {
  //   // declare all locals at the top so it's obvious how big the footprint is
  //   int ax1 = ax;
  //   int ay1 = ay;
  //   int ax2 = ax;
  //   int ay2 = ay;
  //   int dx1, dx2, dy1, dy2, xinc1, xinc2, yinc1, yinc2, side1, side2, i1, i2, error1, error2;

  //   // starting cell is always returned
  //   yield return (ax1,ay1);
  //   // yield return (cx, cy);

  //   xinc1  = (bx < ax1) ? -1 : 1;
  //   yinc1  = (by < ay1) ? -1 : 1;
  //   xinc2  = (cx < ax2) ? -1 : 1;
  //   yinc2  = (cy < ay2) ? -1 : 1;

  //   dx1    = xinc1 * (bx - ax1);
  //   dy1    = yinc1 * (by - ay1);
  //   dx2    = xinc2 * (cx - ax2);
  //   dy2    = yinc2 * (cy - ay2);

  //   if (dx1 == dy1) // Handle perfect diagonals
  //   {
  //     while (dx1 --> 0)
  //     {
  //       ax1 += xinc1;
  //       ay1 += yinc1;
  //       yield return (ax1,ay1);
  //     }

  //     yield break;
  //   }

  //   if (dx2 == dy2) // Handle perfect diagonals
  //   {
  //     while (dx2 --> 0)
  //     {
  //       ax2 += xinc2;
  //       ay2 += yinc2;
  //       yield return (ax2,ay2);
  //     }

  //     yield break;
  //   }
    
  //   // Handle all other lines
    
  //   side1 = -1 * ((dx1 == 0 ? yinc1 : xinc1) - 1);
  //   side2 = -1 * ((dx2 == 0 ? yinc2 : xinc2) - 1);

  //   i1     = dx1 + dy1;
  //   error1 = dx1 - dy1;
  //   i2     = dx2 + dy2;
  //   error2 = dx2 - dy2;
    
  //   dx1 *= 2;
  //   dy1 *= 2;
  //   dx2 *= 2;
  //   dy2 *= 2;

  //   while (i1 --> 0)
  //   {
  //     if (error1 > 0 || error1 == side1)
  //     {
  //       ax1    += xinc1;
  //       error1 -= dy1;
  //     }
  //     else
  //     {
  //       ay1    += yinc1;
  //       error1 += dx1;
  //     }

  //     yield return (ax1,ay1);
  //   }

  //   while (i2 --> 0)
  //   {
  //     if (error2 > 0 || error2 == side2)
  //     {
  //       ax2    += xinc2;
  //       error2 -= dy2;
  //     }
  //     else
  //     {
  //       ay2    += yinc2;
  //       error2 += dx2;
  //     }

  //     yield return (ax2,ay2);
  //   }
  // }

// }