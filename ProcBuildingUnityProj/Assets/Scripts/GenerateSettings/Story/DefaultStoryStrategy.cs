using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultStoryStrategy : StoryStrategy
{
	public override Story GenerateStory(BuildingSettings settings, RectInt bounds, int level)
	{
		// TODO: move to floor strategy
		Floor[] floors = new Floor[bounds.size.x * bounds.size.y];
		int numStairs = 0, maxStairs = 2;

		// populate floors
		for (int i = 0; i < floors.Length; i++)
		{
			// if not ground floor, 1 in 11 chance it has a stairwell; else: empty
			if (level > 0 && Random.Range(0, 11) == 0 && numStairs < maxStairs)
			{
				floors[i] = Floor.Stairs;
				numStairs++;
			}
			else
				floors[i] = Floor.Empty;
		}
		// ensure at least one set of stairs was made
		if (level > 0 && numStairs == 0) floors[Random.Range(0, floors.Length)] = Floor.Stairs;

		return new Story(level,
			//GenerateWalls(settings, bounds, level)
			settings.wallStrategy != null
				? settings.wallStrategy.GenerateWalls(settings, bounds, level)
				: ScriptableObject.CreateInstance<DefaultWallStrategy>().GenerateWalls(settings, bounds, level),
			floors
		);
	}
}