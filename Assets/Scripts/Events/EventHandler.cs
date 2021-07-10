using System;
using System.Collections.Generic;

public delegate void MovementDelegate(float inputx, float inputy, bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect,
    bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown, 
    bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown, 
    bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown, 
    bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight);



public static class EventHandler
{
    public static event Action<InventoryLocation, List<InventoryItem>> InventoryUpdateEvent;

    public static void CallInventoryUpdatedEvent(InventoryLocation _inventoryLocation, List<InventoryItem> inventoryList)
    {
        InventoryUpdateEvent?.Invoke(_inventoryLocation, inventoryList);
    }
    public static event MovementDelegate MovmentEvent;

    public static void CallMovementEvent(float inputx, float inputy, bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect,
        bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown, 
        bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown, 
        bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown, 
        bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingDown,
        bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
    {
        if(MovmentEvent != null)
        {
            MovmentEvent.Invoke(inputx,inputy,isWalking,isRunning,isIdle,isCarrying,
                toolEffect,
                isUsingToolRight,isUsingToolLeft,isUsingToolUp,isUsingToolDown,
                isLiftingToolRight,isLiftingToolLeft,isLiftingToolUp,isLiftingToolDown,
                isPickingRight,isPickingLeft,isPickingUp,isPickingDown,
                isSwingingToolRight,isSwingingToolLeft,isSwingingToolUp,isSwingingDown,
                idleUp,idleDown,idleLeft,idleRight);
        }
    }
    
}