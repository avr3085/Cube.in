using UnityEngine;
using System.Collections.Generic;
using MiscUtils;

/// <summary>
/// This class Create a AABB around the user object.
/// a square is created around the player which will continuously check for the collision in the grid's
/// overlapped cell.
/// </summary>

public static class BBoxCC
{
    /// <summary>
    /// Return the array of all the cell in the grid, which is being overlapped by AABB
    /// </summary>
    /// <param name="position"> Player's Current Position</param>
    /// <returns></returns>
    public static IEnumerable<int> GetCollisionHash(Vector3 pos)
    {
        int currentHash = -1;
        Vector3 bl = new Vector3(pos.x - 0.5f, 0f, pos.z - 0.5f);
        currentHash = bl.GetHash();
        yield return currentHash;

        Vector3 br = new Vector3(pos.x + 0.5f, 0f, pos.z - 0.5f);
        if(currentHash < br.GetHash())
        {
            currentHash = br.GetHash();
            yield return currentHash;
        }

        Vector3 tl = new Vector3(pos.x - 0.5f, 0f, pos.z + 0.5f);
        if(currentHash < tl.GetHash())
        {
            currentHash = tl.GetHash();
            yield return currentHash;
        }

        Vector3 tr = new Vector3(pos.x + 0.5f, 0f, pos.z + 0.5f);
        if(currentHash < tr.GetHash())
        {
            currentHash = tr.GetHash();
            yield return currentHash;
        }
    }
}