using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingGenerator
{
	public static Building Generate(BuildingSettings settings)
	{
		// return new Building(settings.Size.x, settings.Size.y, GenerateWings(settings));
		return new Building(
			settings.Size.x, settings.Size.y,settings.Size.z,
			settings.wingsStrategy != null
				? settings.wingsStrategy.GenerateWings(settings)
				: ScriptableObject.CreateInstance<DefaultWingsStrategy>().GenerateWings(settings)
		);
	}

	// static Wing[] GenerateWings(BuildingSettings settings)
	// {
	// 	return new Wing[]
	// 	{
	// 		//GenerateWing(settings, new RectInt(0,0,settings.Size.x, settings.Size.y),1)
	// 		settings.wingStrategy != null
	// 			? settings.wingStrategy.GenerateWing(
	// 				settings,
	// 				new RectInt(0, 0, settings.Size.x, settings.Size.y),
	// 				1)
	// 			: ScriptableObject.CreateInstance<DefaultWingStrategy>().GenerateWing(
	// 				settings, 
	// 				new RectInt(0, 0, settings.Size.x, settings.Size.y), 
	// 				1),
	// 	};
	// }

	// static Wing GenerateWing(BuildingSettings settings, RectInt bounds, int numStories)
	// {
	// 	return new Wing(
	// 		bounds,
	// 		// GenerateStories(settings, bounds, numStories),
	// 		settings.storiesStrategy != null
	// 	 			? settings.storiesStrategy.GenerateStories(settings, bounds, 1)
	// 	 			: ScriptableObject.CreateInstance<DefaultStoriesStrategy>().GenerateStories(settings, bounds, 1),
	// 		settings.roofStrategy != null ? 
	// 			settings.roofStrategy.GenerateRoof(settings,bounds) : 
	// 			ScriptableObject.CreateInstance<DefaultRoofStrategy>().GenerateRoof(settings,bounds)
	// 	);
	// }

	// static Story[] GenerateStories(BuildingSettings settings, RectInt bounds, int numStories)
	// {
	// 	//return new Story[] {GenerateStory(settings, bounds, 1)};
	// 	return new Story[]
	// 	{
	// 		settings.storyStrategy != null
	// 			? settings.storyStrategy.GenerateStory(settings, bounds, 1)
	// 			: ScriptableObject.CreateInstance<DefaultStoryStrategy>().GenerateStory(settings, bounds, 1)
	// 	};
	// }

	// static Story GenerateStory(BuildingSettings settings, RectInt bounds, int level)
	// {
	// 	return new Story(0,
	// 		//GenerateWalls(settings, bounds, level)
	// 		settings.wallStrategy != null
	// 			? settings.wallStrategy.GenerateWalls(settings, bounds, level)
	// 			: ScriptableObject.CreateInstance<DefaultWallStrategy>().GenerateWalls(settings, bounds, level)
	// 	);
	// }

	// static Wall[] GenerateWalls(BuildingSettings settings, RectInt bounds, int level)
	// {
	// 	return new Wall[(bounds.size.x + bounds.size.y) * 2];
	// }

	// static Roof GenerateRoof(BuildingSettings settings,RectInt bounds)
	// {
	// 	return new Roof();
	// }
}
