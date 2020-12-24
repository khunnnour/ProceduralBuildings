using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWingsStrategy : WingsStrategy
{
    public override Wing[] GenerateWings(BuildingSettings settings)
    {
        return new Wing[]
        {
            //GenerateWing(settings, new RectInt(0,0,settings.Size.x, settings.Size.y),1)
            settings.wingStrategy != null
                ? settings.wingStrategy.GenerateWing(
                    settings,
                    new RectInt(0, 0, settings.Size.x, settings.Size.z),
                    settings.Size.y)
                : ScriptableObject.CreateInstance<DefaultWingStrategy>().GenerateWing(
                    settings,
                    new RectInt(0, 0, settings.Size.x, settings.Size.z),
                    settings.Size.y),
        };
    }
}
