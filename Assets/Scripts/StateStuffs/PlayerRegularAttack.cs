using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jeffu_Fighter
{

    public class PlayerRegularAttack : CharacterStateBase
    {
        private bool hascalledAttack;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attackRegular") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)  //make sure the name here is correct!!! and number!!!
            {

                //call to damage player and shake camera here
                if (!hascalledAttack)
                {
                    hascalledAttack = true;
                    getPlayerController(animator).punch(getPlayerController(animator).baseDmg);
                }
                getPlayerController(animator).idle = true;
                animator.SetBool("attackRegular", false);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            hascalledAttack = false;
        }
    }
}
