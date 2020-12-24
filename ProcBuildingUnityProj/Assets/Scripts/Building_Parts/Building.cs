using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
	Vector3Int size;
	Wing[] wings;

	public Vector3Int Size
	{
		get { return size; }
	}

	public Wing[] Wings
	{
		get { return wings; }
	}

	public Building(int sizeX, int sizeY, int sizeZ, Wing[] wings)
	{
		size = new Vector3Int(sizeX, sizeY, sizeZ);
		this.wings = wings;
	}

	public override string ToString()
	{
		string bldg = "Building:(" + size.ToString() + "; " + wings.Length + ")\n";
		foreach (Wing w in wings)
		{
			bldg += "\t" + w.ToString() + "\n";
		}

		return bldg;
	}
}
