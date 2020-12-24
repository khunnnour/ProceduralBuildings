using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStoryStrategy : StoryStrategy
{
    public override Story GenerateStory(BuildingSettings settings, RectInt bounds, int level)
    {
        return new Story(level,
            //GenerateWalls(settings, bounds, level)
            settings.wallStrategy != null
                ? settings.wallStrategy.GenerateWalls(settings, bounds, level)
                : ScriptableObject.CreateInstance<DefaultWallStrategy>().GenerateWalls(settings, bounds, level)
        );
    }
}