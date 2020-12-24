using System.Collections.Generic;
using UnityEngine;

public class TileSpawnRecord
{
	// tile prefab to spawn
	public GameObject tilePrefab;
	// how many times it was rotated
	public int rotations;

	public TileSpawnRecord()
	{
		tilePrefab = null;
		rotations = -1;
	}
}

public class MapManager : MonoBehaviour
{
	public Transform mapHolder;
	public Vector3 mapDim = Vector3.one;
	[Tooltip("The size (in units) of the tiles")]
	public float mapToWorldCoeff = 1f;
	public List<GameObject> tilePrefabs;

	private TileSpawnRecord[] map;
	private int numTiles;
	private float worldToMapCoeff;

	private void Start()
	{
		worldToMapCoeff = 1f / mapToWorldCoeff;

		// initialize map
		numTiles = Mathf.FloorToInt(mapDim.x * mapDim.y * mapDim.z);
		map = new TileSpawnRecord[numTiles];

		//Debug.Log("Initialized map of size " +numTiles);

		// seed the map
		SeedMap();

		// generate rest of the map
		GenerateMap();

		// spawn the entire map
		SpawnMap();
	}

	// Get all children or seed map with 
	private void SeedMap()
	{
		// if not children, generate random tile
		if (mapHolder.childCount == 0)
		{
			// spawn random tile from list
			// - Parented to map holder, put at (0,0,0)
			//GameObject pref = Instantiate(tilePrefabs[UnityEngine.Random.Range(0, tilePrefabs.Count)], Vector3.zero, mapHolder.rotation, mapHolder);

			// put in the map
			map[0] = new TileSpawnRecord
			{
				tilePrefab = tilePrefabs[Random.Range(0, tilePrefabs.Count)],
				rotations = 0
			};
		}
		else
		{
			// get teh children and addd them to the correct slot in the map
			for (int i = 0; i < mapHolder.childCount; i++)
			{
				Transform child = mapHolder.GetChild(i);
				// convert world position into a map coordinate
				Vector3 mapCoord = Vector3Int.RoundToInt(child.position * worldToMapCoeff);
				// reposition in case it's not lined up correctly
				child.position = mapCoord * mapToWorldCoeff;
				// get the index
				int index = MapCoord_to_Index(mapCoord);
				// put it in the map
				map[index] = new TileSpawnRecord
				{
					tilePrefab = child.gameObject,
					rotations = Mathf.FloorToInt(child.rotation.eulerAngles.y / 90f)
				};

				//Debug.Log("GO "+child.name+" seeded in slot "+index);
			}
		}
	}

	// Generate the rest of the map
	private void GenerateMap()
	{
		// - PREPPING - //
		// create the lists (holds index of map tile)s
		List<int> openList = new List<int>();
		List<int> closedList = new List<int>();

		// initialize the closed list with seed tiles
		for (int i = 0; i < map.Length; i++)
		{
			if (map[i] != null)
				closedList.Add(i);
		}

		// initialize the open list with open neighbors of closed
		for (int i = 0; i < closedList.Count; i++)
		{
			FindOpenNeighbors(ref openList, Index_to_MapCoord(closedList[i]));
		}

		// - MAIN BODY - //
		// cycle until the open list is empty
		List<int> neighbors = new List<int>();
		while (openList.Count > 0)
		{
			//Debug.Log("Open list: " + openList.Count + "\nClosed list: " + closedList.Count);

			// get first index in open
			int current = openList[0];
			Debug.Log("== == == == Processing index " + current + " == == == ==");
			Vector3 origin = Index_to_MapCoord(current); // get the map coord

			// get the closed neighbors
			neighbors.Clear();
			FindClosedNeighbors(ref neighbors, origin);

			// -- make list of tiles that can fit in this spot -- //
			List<TileSpawnRecord> possibleMatches = new List<TileSpawnRecord>();
			foreach (GameObject g in tilePrefabs)
			{
				// get the tile from the prefab
				Tile t = g.GetComponent<Tile>();
				//	t.SetConnectors( .GetConnectors());

				bool fits = true;
				// test every rotation
				for (int i = 0; i < 4; i++)
				{
					foreach (int temp in neighbors)
					{
						//Debug.Log("Checking neighbor at index " + temp + " against " + g.name);
						// make a new instance of a tile and get the connectors from the prefab
						Tile oT = map[temp].tilePrefab.GetComponent<Tile>();
						/*Tile oT = new Tile();
						oT.SetConnectors(map[temp].tilePrefab.GetComponent<Tile>().GetConnectors());*/
						oT.Rotate(map[temp].rotations); // rotate it the correct number of rotations 
						Vector3 neighborMapCoord = Index_to_MapCoord(temp);

						Vector3 dir = neighborMapCoord - origin;

						fits = CanConnect(t, oT, dir);

						oT.ResetConnectors(); // reset to avoid editing prefab
						
						//Debug.Log(neighborMapCoord.ToString("F0") + " - " + origin.ToString("F0") + " = " +dir.ToString("F0") + " | " + fits);

						// if the tile cannot fit with this neighbor then break out
						// (cannot be used if even one side doesn't work)
						if (!fits) break;
					}

					// if all neighbors were checked and the tile fits then add it
					if (fits)
					{
						TileSpawnRecord tRec = new TileSpawnRecord
						{
							tilePrefab = g,
							rotations = i
						};
						Debug.Log("Added record: " + tRec.tilePrefab + " w/ " + tRec.rotations + " rots");
						possibleMatches.Add(tRec);
					}

					// rotate the tile once
					t.Rotate(1);
				}
				// reset the connectors
				//t.ResetConnectors();
			}

			// -- Populate the map -- //
			// log warning if no tile fit
			if (possibleMatches.Count == 0)
			{
				Debug.LogWarning("Unable to fit tile at index " + current);
				// select a random prefab from the list to put in the map
				map[current] = new TileSpawnRecord
				{
					tilePrefab = tilePrefabs[Random.Range(0, tilePrefabs.Count)],
					rotations = 0
				};
			}
			else
			{
				// put a random tile from list of tiles that fit in the map
				map[current] = possibleMatches[Random.Range(0, possibleMatches.Count)];
			}

			// -- Prep for next run -- //
			closedList.Add(current); // add current index to the closed list
			openList.RemoveAt(0); // remove current index from the open list
			FindOpenNeighbors(ref openList, origin); // get the neighbors of filled slot
		}
	}

	private void SpawnMap()
	{
		// clear out the seeds
		for (int i = mapHolder.childCount - 1; i > 0; i--)
		{
			Destroy(mapHolder.GetChild(i));
		}

		Debug.Log("~~~ ~~~ ~~~ Generating Map ~~~ ~~~ ~~~");
		// spawn each tile record
		for (int j = 0; j < numTiles; j++)
		{
			/* instantiate object:
			 * - Use provided prefab
			 * - Set correct position based on psoition in the map
			 * - Set correct rotation based on rotations in record
			 * - parent to the map holder
			 */
			GameObject temp = Instantiate(
				map[j].tilePrefab,
				Index_to_MapCoord(j) * mapToWorldCoeff,
				Quaternion.Euler(0f, map[j].rotations * 90f, 0f),
				mapHolder);
			
			Tile tInst = temp.GetComponent<Tile>();
			tInst.Rotate(map[j].rotations); // Rotate the tile component
			
			Debug.Log("Instantiated tile "+j+" w/ "+map[j].rotations+" rots");
		}
	}

	// HELPERS --
	// Adds tiles neighboring [coord] to list that are open
	private void FindOpenNeighbors(ref List<int> list, Vector3 coord)
	{
		int tempIndex;
		Vector3[] offsets = { Vector3.left, Vector3.right, Vector3.forward, Vector3.back, Vector3.up, Vector3.down };
		Vector3 testCoord;

		// cycle thru every offset
		for (int i = 0; i < offsets.Length; i++)
		{
			// find the offset coord
			testCoord = coord + offsets[i];

			// ensure it is in the boundaries
			if (testCoord.x >= 0 && testCoord.x < mapDim.x &&
				testCoord.y >= 0 && testCoord.y < mapDim.y &&
				testCoord.z >= 0 && testCoord.z < mapDim.z)
			{
				// find the index of that offset coord
				tempIndex = MapCoord_to_Index(testCoord);

				// check its not already in the list AND has no tile yet (ie, not in closed list)
				if (!InList(ref list, tempIndex) && map[tempIndex] == null)
				{
					// add index to list
					list.Add(tempIndex);
				}
			}
		}
	}
	// Adds tiles neighboring [coord] to list that are closed
	private void FindClosedNeighbors(ref List<int> list, Vector3 coord)
	{
		int tempIndex;
		Vector3[] offsets = { Vector3.left, Vector3.right, Vector3.forward, Vector3.back, Vector3.up, Vector3.down };
		Vector3 testCoord;

		// cycle thru every offset
		for (int i = 0; i < offsets.Length; i++)
		{
			// find the offset coord
			testCoord = coord + offsets[i];

			// ensure it is in the boundaries
			if (testCoord.x >= 0 && testCoord.x < mapDim.x &&
				testCoord.y >= 0 && testCoord.y < mapDim.y &&
				testCoord.z >= 0 && testCoord.z < mapDim.z)
			{
				// find the index of that offset coord
				tempIndex = MapCoord_to_Index(testCoord);

				// check its not already in the list AND has a tile (ie, in closed list)
				if (!InList(ref list, tempIndex) && map[tempIndex] != null)
				{
					// add index to list
					list.Add(tempIndex);
				}
			}
		}
	}

	private bool InList(ref List<int> list, int search)
	{
		return list.Contains(search);
	}

	// checks if the two tiles can connect in the provided direction
	private bool CanConnect(Tile tile1, Tile tile2, Vector3 dir)
	{
		if (dir == Vector3.right)
		{
			//Debug.Log(tile1.GetRightConnector()+" =? "+tile2.GetLeftConnector());
			return (tile1.GetRightConnector() == tile2.GetLeftConnector());
		}

		if (dir == Vector3.left)
		{
			//Debug.Log(tile1.GetLeftConnector() + " =? " + tile2.GetRightConnector());
			return (tile1.GetLeftConnector() == tile2.GetRightConnector());
		}

		if (dir == Vector3.forward)
			return (tile1.GetForConnector() == tile2.GetBackConnector());
		
		if (dir == Vector3.back)
			return (tile1.GetBackConnector() == tile2.GetForConnector());
		
		if (dir == Vector3.up)
			return (tile1.GetTopConnector() == tile2.GetBottConnector());
		
		if (dir == Vector3.down)
			return (tile1.GetBottConnector() == tile2.GetTopConnector());

		Debug.LogWarning("Invalid direction passed");
		return false;
	}

	// CONVERTERS --
	// convert map coord into an index
	private int MapCoord_to_Index(Vector3 coord)
	{
		return (int)((coord.y * mapDim.x * mapDim.z) + (coord.z * mapDim.x) + coord.x);
	}

	// convert an index into a map coord
	private Vector3 Index_to_MapCoord(int index)
	{
		Vector3 coord = Vector3.zero;
		int sqr = (int)(mapDim.x * mapDim.z);

		coord.y = Mathf.Floor(index / (float)sqr);
		index = index % sqr;

		coord.z = Mathf.Floor(index / mapDim.x);
		index = index % (int)mapDim.x;

		coord.x = index;

		return coord;
	}
}