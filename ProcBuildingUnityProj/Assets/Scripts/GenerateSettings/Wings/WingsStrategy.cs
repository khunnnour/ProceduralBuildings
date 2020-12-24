using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WingsStrategy : ScriptableObject
{
    public abstract Wing[] GenerateWings(BuildingSettings settings);
}
