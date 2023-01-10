using UnityEngine;

namespace Kleeeeeer.FSM
{
    public class BaseState<T> where T : System.Enum
    {
        protected FSMBase<T> fsm;

        public BaseState(FSMBase<T> FSM)
        {
            fsm = FSM;
        }

        public virtual void EnterState()
        { }

        public virtual void ExitState()
        { }

        public virtual void Update()
        { }

        public virtual void OnTriggerEnter(Collider collision)
        { }

        public virtual void OnTriggerExit(Collider collision)
        { }

        public virtual void OnCollilsionEnter(Collision collision)
        { }

        public virtual void OnCollilsionExit(Collision collision)
        { }

        public virtual void FixedUpdate()
        { }

        public virtual void LateUpdate()
        { }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}