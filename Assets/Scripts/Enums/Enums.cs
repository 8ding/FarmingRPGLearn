using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolEffect
{
    none,
    watering
}

public enum Direction
{
    up,
    down,
    left,
    right,
    none
}

public enum ItemType
{
    Seed,
    Commodity,
    Watering_tool,
    Hoeing_tool,
    Chopping_tool,
    Breaking_tool,
    Reaping_tool,
    Colleting_tool,
    Reapable_Scenary,
    Furniture,
    none,
    enumcount
}

public enum InventoryLocation
{
    player,
    chest,
    count,
}
