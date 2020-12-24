using UnityEngine;

public abstract class RoofStrategy:ScriptableObject
{
    public abstract Roof GenerateRoof(BuildingSettings settings, RectInt bounds);
}