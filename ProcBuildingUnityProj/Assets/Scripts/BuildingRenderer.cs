using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRenderer : MonoBehaviour
{
    public float scale = 1f;
    public Transform[] floorPrefabs;
    public Transform[] wallPrefabs;
    public Transform[] roofPrefabs;
    Transform bldgFolder;

    private float hScale;
    private Vector3[] rotationOffset;

    private void Awake()
    {
        hScale = scale * 0.5f;
        rotationOffset = new Vector3[]
        {
            new Vector3(-scale, 270f, 0f),
            new Vector3(0f, 0f, 0f),
            new Vector3(0f, 90, -scale),
            new Vector3(-scale, 180, -scale)
        };
    }

    public void Render(Building bldg)
    {
        bldgFolder = new GameObject("Building").transform;
        foreach (Wing wing in bldg.Wings)
        {
            RenderWing(wing);
        }
    }

    private void RenderWing(Wing wing)
    {
        Transform wingFolder = new GameObject("Wing").transform;
        wingFolder.SetParent(bldgFolder);
        foreach (Story story in wing.Stories)
        {
            RenderStory(story, wing, wingFolder);
        }

        RenderRoof(wing, wingFolder);
    }

    private void RenderStory(Story story, Wing wing, Transform wingFolder)
    {
        Transform storyFolder = new GameObject("Story " + story.Level).transform;
        storyFolder.SetParent(wingFolder);
        for (int x = wing.Bounds.min.x; x < wing.Bounds.max.x; x++)
        {
            for (int y = wing.Bounds.min.y; y < wing.Bounds.max.y; y++)
            {
				PlaceFloor(x, y, story.Level, storyFolder, floorPrefabs[(int)story.Floors[x + y * wing.Bounds.max.x]]);

                //south wall
                if (y == wing.Bounds.min.y)
                {
                    Transform wall = wallPrefabs[(int) story.Walls[x - wing.Bounds.min.x]];
                    PlaceSouthWall(x, y, story.Level, storyFolder, wall);
                }

                //east wall
                if (x == wing.Bounds.min.x + wing.Bounds.size.x - 1)
                {
                    Transform wall = wallPrefabs[(int) story.Walls[wing.Bounds.size.x + y - wing.Bounds.min.y]];
                    PlaceEastWall(x, y, story.Level, storyFolder, wall);
                }

                //north wall
                if (y == wing.Bounds.min.y + wing.Bounds.size.y - 1)
                {
                    Transform wall =
                        wallPrefabs[
                            (int) story.Walls[
                                wing.Bounds.size.x * 2 + wing.Bounds.size.y - (x - wing.Bounds.min.x + 1)]];
                    PlaceNorthWall(x, y, story.Level, storyFolder, wall);
                }

                //west wall
                if (x == wing.Bounds.min.x)
                {
                    Transform wall =
                        wallPrefabs[
                            (int) story.Walls[
                                (wing.Bounds.size.x + wing.Bounds.size.y) * 2 - (y - wing.Bounds.min.y + 1)]];
                    PlaceWestWall(x, y, story.Level, storyFolder, wall);
                }
            }
        }
    }

	private void PlaceFloor(int x, int y, int level, Transform storyFolder, Transform floor)
    {
        Transform f = Instantiate(floor,
            storyFolder.TransformPoint(new Vector3(x * -scale, 0f + level * scale, y * -scale)), Quaternion.identity);
        f.SetParent(storyFolder);
    }

    private void PlaceSouthWall(int x, int y, int level, Transform storyFolder, Transform wall)
    {
        Transform w = Instantiate(
            wall,
            storyFolder.TransformPoint(
                new Vector3(
                    x * -scale,
                    hScale + level * scale,
                    y * scale + hScale
                )
            ),
            Quaternion.Euler(0, 90, 0));
        w.SetParent(storyFolder);
    }

    private void PlaceEastWall(int x, int y, int level, Transform storyFolder, Transform wall)
    {
        Transform w = Instantiate(
            wall,
            storyFolder.TransformPoint(
                new Vector3(
                    x * -scale - hScale,
                    hScale + level * scale,
                    y * -scale
                )
            ),
            Quaternion.identity);
        w.SetParent(storyFolder);
    }

    private void PlaceNorthWall(int x, int y, int level, Transform storyFolder, Transform wall)
    {
        Transform w = Instantiate(
            wall,
            storyFolder.TransformPoint(
                new Vector3(
                    x * -scale,
                    hScale + level * scale,
                    y * -scale - hScale
                )
            ),
            Quaternion.Euler(0, 90, 0));
        w.SetParent(storyFolder);
    }

    private void PlaceWestWall(int x, int y, int level, Transform storyFolder, Transform wall)
    {
        Transform w = Instantiate(
            wall,
            storyFolder.TransformPoint(
                new Vector3(
                    x * -scale + hScale,
                    hScale + level * scale,
                    y * -scale
                )
            ),
            Quaternion.identity);
        w.SetParent(storyFolder);
    }

    private void RenderRoof(Wing wing, Transform wingFolder)
    {
        for (int x = wing.Bounds.min.x; x < wing.Bounds.max.x; x++)
        {
            for (int y = wing.Bounds.min.y; y < wing.Bounds.max.y; y++)
            {
                PlaceRoof(x, y, wing.Stories.Length, wingFolder, wing.GetRoof.Type, wing.GetRoof.Direction);
            }
        }
    }

    private void PlaceRoof(int x, int y, int level, Transform wingFolder, RoofType type, RoofDirection direction)
    {
        Transform r;
        r = Instantiate(
            roofPrefabs[(int) type],
            wingFolder.TransformPoint(
                new Vector3(
                    x * -scale,
                    level * scale + hScale,
                    y * -scale
                )
            ),
            Quaternion.Euler(0f, rotationOffset[(int) direction].y, 0f)
        );
        r.SetParent(wingFolder);
    }
}