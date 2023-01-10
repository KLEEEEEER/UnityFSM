using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kleeeeeer.FSM
{
    public class ExampleFSM : FSMBase<ExampleFSM.State>
    {
        [SerializeField] private TestComponent _testComponent;
        private void Awake()
        {
            AddState(State.Example, new ExampleState(this, _testComponent));

            TransitionToState(State.Example);
        }

        public enum State
        {
           Example 
        }
    }
}
