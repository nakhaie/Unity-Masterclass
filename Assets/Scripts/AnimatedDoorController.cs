using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedDoorController : DoorController
{
    public Animator DoorAnimator;

    public override void Open()
    {
        DoorAnimator.SetTrigger("Open");
    }
}
