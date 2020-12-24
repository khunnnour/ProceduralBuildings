using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStoriesStrategy : StoriesStrategy
{
    public override Story[] GenerateStories(BuildingSettings settings, RectInt bounds, int numStories)
    {
        Story[] stories = new Story[numStories];

        // for every level, generate a corresponding story
        for (int i = 0; i < numStories; i++)
            stories[i] = settings.storyStrategy != null
                ? settings.storyStrategy.GenerateStory(settings, bounds, i)
                : ScriptableObject.CreateInstance<DefaultStoryStrategy>().GenerateStory(settings, bounds, i);

        return stories;
    }
}
