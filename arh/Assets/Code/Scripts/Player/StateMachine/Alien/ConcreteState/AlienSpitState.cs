using UnityEngine;
using Player.StateMachine.Alien;

public class AlienSpitState : AlienBaseState
{
    private AlienStateMachine _alienStateMachine;

    private Rigidbody2D _edibleObjectRigidBody;

    public AlienSpitState(AlienStateMachine currentContext, AlienStateFactory alienStateFactory)
        : base(currentContext, alienStateFactory)
    {
        IsRootState = true;
        _alienStateMachine = Ctx;
        _edibleObjectRigidBody = Ctx.currentEdibleObject.GetComponent<Rigidbody2D>();
    }

    public override void EnterState()
    {
        // **** RODAR ANIMAÇÂO ****
        Spit();
        //Debug.Log("Cuspi o Objeto");
        GoToGrounded();
    }

    private void Spit() // Cospe o Objeto com uma certa força
    {
        Ctx.hasStoredObject = false;
        Ctx.currentEdibleObject.transform.position = Ctx.eatPointObject.transform.position;
        Ctx.currentEdibleObject.SetActive(true);

        if (_alienStateMachine.Sprite.transform.localScale.x <= 0)
        {
            _edibleObjectRigidBody.velocity = new Vector2(Ctx.spitForce * -1, _edibleObjectRigidBody.velocity.y);

        }
        else
        {
            _edibleObjectRigidBody.velocity = new Vector2(Ctx.spitForce, _edibleObjectRigidBody.velocity.y);

        }
    }

    protected override void UpdateState()
    {
        
    }

    protected override void PhysicsUpdateState()
    {

    }

    protected override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {

    }

    public sealed override void InitializeSubState()
    {

    }

    private void GoToGrounded()
    {
        SwitchState(Factory.Grounded());
    }
}
