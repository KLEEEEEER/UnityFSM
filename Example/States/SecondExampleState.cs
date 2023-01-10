namespace Kleeeeeer.FSM
{
    public class SecondExampleState : BaseState<ExampleFSM.State>
    {
        public SecondExampleState(FSMBase<ExampleFSM.State> fsm) : base(fsm)
        {
        }

        public override void EnterState() 
        {
        }
    }
}
