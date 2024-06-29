using System.Collections.Generic;

public static partial class Raster
{
  public static IEnumerable<(int x, int y)> Line(int ax, int ay, int bx, int by)
  {
    // declare all locals at the top so it's obvious how big the footprint is
    int dx, dy, xinc, yinc, side, i, error;

    // starting cell is always returned
    yield return (ax,ay);

    xinc  = (bx < ax) ? -1 : 1;
    yinc  = (by < ay) ? -1 : 1;
    dx    = xinc * (bx - ax);
    dy    = yinc * (by - ay);

    if (dx == dy) // Handle perfect diagonals
    {
      while (dx --> 0)
      {
        ax += xinc;
        ay += yinc;
        yield return (ax,ay);
      }

      yield break;
    }
    
    // Handle all other lines
    
    side = -1 * ((dx == 0 ? yinc : xinc) - 1);

    i     = dx + dy;
    error = dx - dy;
    
    dx *= 2;
    dy *= 2;

    while (i --> 0)
    {
      if (error > 0 || error == side)
      {
        ax    += xinc;
        error -= dy;
      }
      else
      {
        ay    += yinc;
        error += dx;
      }

      yield return (ax,ay);
    }
  }

}