using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Connector
{
    Invalid = -1,
    R = 0,
    G = 1,
    B,
	Y,
	C
}

public enum TileConnectorType
{
	Invalid = -1,
	Equilat = 0,
	Isoscel = 1,
	Scalene
}

public class Tile : MonoBehaviour
{
	[SerializeField] private int rotations = 0;

	[Tooltip(
		"Uses types of triangles to determine how the connectors are oriented. Equilateral means all sides have same connectors etc.")]
	public TileConnectorType connectorDescription;

	[Header("Side Connectors")] [SerializeField]
	private Connector leftConnector;

	[SerializeField] private Connector rightConnector;
	[SerializeField] private Connector forConnector;
	[SerializeField] private Connector backConnector;

	[Header("Vertical Connectors")]
	[SerializeField] private Connector topConnector;
	[SerializeField] private Connector bottomConnector;

	//private Transform _transform;
	
	// separate block to hold 
	
	private Connector curr_leftConnector;
	private Connector curr_rightConnector;
	private Connector curr_forConnector;
	private Connector curr_backConnector;
	private Connector curr_topConnector;
	private Connector curr_bottomConnector;
	/**/

	public Tile()
	{
		rotations = 0;
		ResetConnectors();
	}

	public Connector[] GetConnectors()
	{
		return new Connector[]
			{leftConnector, rightConnector, forConnector, backConnector, topConnector, bottomConnector};
	}

	public void SetConnectors(Connector[] conns)
	{
		leftConnector = conns[0];
		rightConnector = conns[0];
		forConnector = conns[0];
		backConnector = conns[0];
		topConnector = conns[0];
		bottomConnector = conns[0];
	}

	//public void SetTransform(Transform t)
	//{
	//	_transform = t;
	//}

	public override string ToString()
	{
		return "Left: " + leftConnector.ToString() +
		       ", Right: " + rightConnector.ToString() +
		       ", For: " + forConnector.ToString() +
		       ", Back: " + backConnector.ToString();
	}

	// rotate tile n times clockwise
	public void Rotate(int n)
	{
		rotations += n;
		
		for (int i = 0; i < n; i++)
		{
			Connector holder = curr_leftConnector;

			// rotate connectors
			curr_leftConnector = curr_backConnector;
			curr_backConnector = curr_rightConnector;
			curr_rightConnector = curr_forConnector;
			curr_forConnector = holder;
		}
		/**/
	}

	public Connector GetRightConnector()
	{
		//return rightConnector;
		return curr_rightConnector;
	}

	public Connector GetLeftConnector()
	{
		//return leftConnector;
		return curr_leftConnector;
	}

	public Connector GetForConnector()
	{
		//return forConnector;
		return curr_forConnector;
	}

	public Connector GetBackConnector()
	{
		//return backConnector;
		return curr_backConnector;
	}

	public Connector GetTopConnector()
	{
		//return topConnector;
		return curr_topConnector;
	}

	public Connector GetBottConnector()
	{
		//return bottomConnector;
		return curr_bottomConnector;
	}

	public void ResetConnectors()
	{
		rotations = 0;
		curr_leftConnector = leftConnector;
		curr_rightConnector = rightConnector;
		curr_forConnector = forConnector;
		curr_backConnector = backConnector;
		curr_topConnector = topConnector;
		curr_bottomConnector = bottomConnector;
	}/**/

	private void OnDrawGizmos()
	{
		float rad = 0.1f, off = 0.5f;
		var position = transform.position;

		Gizmos.color = GetConnectorColor(topConnector);
		Gizmos.DrawWireSphere(position + Vector3.up * off, rad);

		Gizmos.color = GetConnectorColor(bottomConnector);
		Gizmos.DrawWireSphere(position - Vector3.up * off, rad);

		Gizmos.color = GetConnectorColor(forConnector);
		Gizmos.DrawWireSphere(position + Vector3.forward * off, rad);

		Gizmos.color = GetConnectorColor(backConnector);
		Gizmos.DrawWireSphere(position - Vector3.forward * off, rad);

		Gizmos.color = GetConnectorColor(leftConnector);
		Gizmos.DrawWireSphere(position + Vector3.left * off, rad);

		Gizmos.color = GetConnectorColor(rightConnector);
		Gizmos.DrawWireSphere(position - Vector3.left * off, rad);
	}

	private Color GetConnectorColor(Connector c)
	{
		switch (c)
		{
			case Connector.R:
				return Color.red;
			case Connector.G:
				return Color.green;
			case Connector.B:
				return Color.blue;
			case Connector.Y:
				return Color.yellow;
			case Connector.C:
				return Color.cyan;
			case Connector.Invalid:
			default:
				return Color.white;
		}
	}
}
