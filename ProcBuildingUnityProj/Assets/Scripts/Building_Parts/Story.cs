using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story
{
	int level;
	Wall[] walls;

	public int Level
	{
		get { return level; }
	}

	public Wall[] Walls
	{
		get { return walls; }
	}

	public Story(int level, Wall[] walls)
	{
		this.level = level;
		this.walls = walls;
	}

	public override string ToString()
	{
		string story = "Story " + level + ":\n";
		story += "\t\tWalls: ";
		foreach (Wall w in walls)
		{
			story += w.ToString() + ", ";
		}

		return story;
	}
}
