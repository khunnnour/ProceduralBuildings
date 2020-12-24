using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWingStrategy : WingStrategy
{
    public override Wing GenerateWing(BuildingSettings settings, RectInt bounds, int numStories)
    {
        return new Wing(
            bounds,
            // GenerateStories(settings, bounds, numStories),
            settings.storiesStrategy != null
                ? settings.storiesStrategy.GenerateStories(settings, bounds, numStories)
                : ScriptableObject.CreateInstance<DefaultStoriesStrategy>().GenerateStories(settings, bounds, numStories),
            settings.roofStrategy != null
                ? settings.roofStrategy.GenerateRoof(settings, bounds)
                : ScriptableObject.CreateInstance<DefaultRoofStrategy>().GenerateRoof(settings, bounds)
        );
    }
}
