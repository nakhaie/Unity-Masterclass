using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedDoorController : DoorController
{
    public Animator DoorAnimator;
    private static readonly int OpenKey = Animator.StringToHash("Open");

    public override void Open()
    {
        DoorAnimator.SetTrigger(OpenKey);
    }
}
