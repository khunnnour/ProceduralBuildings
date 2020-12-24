using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof
{
	RoofType type;
	RoofDirection direction;

	public RoofType Type
	{
		get { return type; }
	}

	public RoofDirection Direction
	{
		get { return direction; }
	}

	public Roof(RoofType type = RoofType.Slope, RoofDirection direction = RoofDirection.North)
	{
		this.type = type;
		this.direction = direction;
	}

	public override string ToString()
	{
		return "Roof: " + type.ToString() +
		       ((type == RoofType.Peak || type == RoofType.Slope) ? ", " + direction.ToString() : "");
	}
}

public enum RoofType {
	Point,
	Peak,
	Slope,
	Flat
}

public enum RoofDirection {
	North,
	East,
	South,
	West
}