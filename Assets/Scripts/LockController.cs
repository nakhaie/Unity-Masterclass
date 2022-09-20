using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : Item
{
    public DoorController door;
    
    public override string GetDetail()
    {
        return ItemDetail;
    }
    
    public override void ConditionDone()
    {
        
    }
}
