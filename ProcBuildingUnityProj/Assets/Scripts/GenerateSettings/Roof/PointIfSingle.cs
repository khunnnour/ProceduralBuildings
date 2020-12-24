using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building Generation/Roof/Point If Single")]
public class PointIfSingle : RoofStrategy
{
    public override Roof GenerateRoof(BuildingSettings settings, RectInt bounds)
    {
        if (bounds.size.x == 1 && bounds.size.y == 1)
            return new Roof(RoofType.Point);
        else
        {
            return new Roof((RoofType) Random.Range(1, 4));
        }
    }
}
