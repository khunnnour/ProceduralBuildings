using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWallStrategy : WallStrategy
{
    private int maxDoors = 2; // maximum number of doors allowed on lvl one
    private int windowWeight = 2, emptyWeight = 4, doorWeight = 1; // weights to be used in rng gen

    public override Wall[] GenerateWalls(BuildingSettings settings, RectInt bounds, int level)
    {
        Wall[] walls = new Wall[(bounds.size.x + bounds.size.y) * 2];
        int rng;
        // check if on ground level (only ground level can have doors)
        if (level == 0)
        {
            int numDoors = 0;
            // cycle thru every wall and pick a random type
            for (int i = 0; i < walls.Length; i++)
            {
                //  if reached max doors, then don't consider them
                if (numDoors < maxDoors)
                {
                    rng = Random.Range(0, doorWeight + windowWeight + emptyWeight);
                    if (rng < doorWeight)
                    {
                        walls[i] = Wall.Door;
                        numDoors++;
                    }
                    else if (rng < windowWeight)
                        walls[i] = Wall.Window;

                    // no need for else bc walls defualt to empty
                }
                else
                {
                    rng = Random.Range(0, windowWeight + emptyWeight);
                    if (rng < windowWeight)
                        walls[i] = Wall.Window;
                }
            }
            
            // if no doors were generated; then make random wall a door
            if (numDoors == 0)
                walls[Random.Range(0, walls.Length)] = Wall.Door;
        }
        else
        {
            for (int i = 0; i < walls.Length; i++)
            {
                // no need to consider doors on higher levels
                rng = Random.Range(0, windowWeight + emptyWeight);
                if (rng < windowWeight)
                    walls[i] = Wall.Window;
            }
        }

        return walls;
    }
}
