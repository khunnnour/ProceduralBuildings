using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WingStrategy : ScriptableObject
{
    public abstract Wing GenerateWing(BuildingSettings settings, RectInt bounds, int numStories);
}
