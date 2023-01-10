namespace Kleeeeeer.FSM
{
    public class ExampleState : BaseState<ExampleFSM.State>
    {
        private ExampleComponent _exampleComponent;
        public ExampleState(FSMBase<ExampleFSM.State> fsm, ExampleCompoent exampleCompoent) : base(fsm)
        {
            _exampleComponent = exampleCompoent;
        }

        public override void EnterState() 
        {
            _exampleComponent.TestMethod();
        }
    }
}
