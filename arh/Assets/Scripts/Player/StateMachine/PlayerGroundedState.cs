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
        SetSubState(playerStateFactory.Idle()); // Como grounded ser� o primeiro estado, j� setamos aqui o primeiro subState
        playerStateFactory.Idle().EnterState(); // Necess�rio, pois o SetSubState() n�o o faz

        this. _jumpRequester = jumpRequester;
        this. _isGrounded = isGrounded;

        _isRootState = true;
    }

    public override void EnterState()
    {
        //Debug.Log("--> grounded state");
        _jumpRequester.PerformJumpEvent.AddListener(GoToAscendingState);
        _isGrounded._onNotGrounded.AddListener(GoToDescendingState);        // O PerformJumpEvent � chamado primeiro, se n�o teria que fazer uma verifica��o
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
