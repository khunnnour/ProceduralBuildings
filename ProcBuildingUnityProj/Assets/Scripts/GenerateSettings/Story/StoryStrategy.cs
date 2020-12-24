using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoryStrategy : ScriptableObject
{
    public abstract Story GenerateStory(BuildingSettings settings, RectInt bounds, int level);
}
