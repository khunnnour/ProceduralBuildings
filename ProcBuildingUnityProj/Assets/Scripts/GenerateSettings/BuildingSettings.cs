using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building Generation/Building Settings")]
public class BuildingSettings : ScriptableObject
{
    public Vector3Int buildingSize;
    public WingsStrategy wingsStrategy;
    public WingStrategy wingStrategy;
    public StoriesStrategy storiesStrategy;
    public StoryStrategy storyStrategy;
    public WallStrategy wallStrategy;
    public RoofStrategy roofStrategy;
    
    public Vector3Int Size
    {
        get { return buildingSize; }
    }
}
