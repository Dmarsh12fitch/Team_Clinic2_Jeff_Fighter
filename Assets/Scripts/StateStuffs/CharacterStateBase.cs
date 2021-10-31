using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jeffu_Fighter
{
    public class CharacterStateBase : StateMachineBehaviour
    {
        private PlayerController playerControllerScript;
        public PlayerController getPlayerController(Animator animator)
        {
            if(playerControllerScript == null)
            {
                playerControllerScript = animator.GetComponentInParent<PlayerController>();
            }
            return playerControllerScript;
        }
    }
}
