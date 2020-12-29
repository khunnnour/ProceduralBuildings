using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story
{
	int level;
	Wall[] walls;
	Floor[] floors;

	public int Level { get { return level; } }

	public Wall[] Walls { get { return walls; } }

	public Floor[] Floors { get { return floors; } }

	public Story(int level, Wall[] walls, Floor[] floors)
	{
		this.level = level;
		this.walls = walls;
		this.floors = floors;
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
