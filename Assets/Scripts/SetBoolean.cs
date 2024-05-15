using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolean : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool currentValue = animator.GetBool("Sitting");
        animator.SetBool("Sitting", !currentValue);
    }

}
