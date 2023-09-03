using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    private JumpRequester _jumpRequester;
    private IsGrounded _isGrounded;

    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, JumpRequester jumpRequester, IsGrounded isGrounded) : base(
        currentContext, playerStateFactory)
    {
        SetSubState(playerStateFactory.Idle()); // Como grounded será o primeiro estado, já setamos aqui o primeiro subState
        playerStateFactory.Idle().EnterState(); // Necessário, pois o SetSubState() não o faz

        this. _jumpRequester = jumpRequester;
        this. _isGrounded = isGrounded;

        _isRootState = true;
    }

    public override void EnterState()
    {
        //Debug.Log("--> grounded state");
        _jumpRequester.PerformJumpEvent.AddListener(GoToAscendingState);
        _isGrounded._onNotGrounded.AddListener(GoToDescendingState);        // O PerformJumpEvent é chamado primeiro, se não teria que fazer uma verificação
    }
 
    public override void ExitState()
    {
        _jumpRequester.PerformJumpEvent.RemoveListener(GoToAscendingState);
        _isGrounded._onNotGrounded.RemoveListener(GoToDescendingState);
    }

    public override void FixedUpdateState() { }

    public void GoToAscendingState()
    {
        SwitchStates(_factory.Ascending());
    }

    public void GoToDescendingState()
    {
        SwitchStates(_factory.Descending());
    }
}
