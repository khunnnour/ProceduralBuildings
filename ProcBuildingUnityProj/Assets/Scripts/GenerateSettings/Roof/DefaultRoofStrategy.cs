using UnityEngine;

public class DefaultRoofStrategy : RoofStrategy
{
    public override Roof GenerateRoof(BuildingSettings settings, RectInt bounds)
    {
        return new Roof((RoofType) Random.Range(0, 4), (RoofDirection) Random.Range(0, 4));
    }
}
