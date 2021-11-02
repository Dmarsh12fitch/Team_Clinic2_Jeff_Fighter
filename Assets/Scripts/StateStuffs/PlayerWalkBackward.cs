using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jeffu_Fighter
{
    public class PlayerWalkBackward : CharacterStateBase
    {

        private Vector3 finalPos;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            finalPos = new Vector3(getPlayerController(animator).transform.position.x - 1, getPlayerController(animator).transform.position.y, getPlayerController(animator).transform.position.z);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            getPlayerController(animator).transform.Translate(-0.0068f, 0, 0);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("WalkBackward") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                getPlayerController(animator).idle = true;
                animator.SetBool("moveBackward", false);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            getPlayerController(animator).transform.position = finalPos;

        }
    }
}
