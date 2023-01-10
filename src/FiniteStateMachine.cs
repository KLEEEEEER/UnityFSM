using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kleeeeeer.FSM
{
    public class FSMBase<T> : MonoBehaviour where T : Enum
    {
        [SerializeField] private List<StateLink> _stateLinks;
        private readonly Dictionary<T, BaseState<T>> _states = new Dictionary<T, BaseState<T>>();
        private readonly Dictionary<Type, Component> _cachedComponents = new Dictionary<Type, Component>();

        [SerializeField] private FSMDebugging _debugging;
        public BaseState<T> CurrentState { get; protected set; }
        public T CurrentStateIndex { get; protected set; }

        public void AddState(T index, BaseState<T> state)
        {
            _states.Add(index, state);
        }

        public void RemoveState(T index)
        {
            _states.Remove(index);
        }

        public void TransitionToState(T index)
        {
            if (_states.TryGetValue(index, out var value))
            {
                if (CurrentState != null)
                {
                    CurrentState.ExitState();
                }
                CurrentState = value;
                CurrentStateIndex = index;
                CurrentState.EnterState();

                _debugging.SetCurrentState(CurrentState.ToString());
            }
            else
            {
                Debug.LogError($"No state with index \"{index}\" in fsm.");
            }
        }

        public void NextState(T defaultNextState)
        {
            if (TryGetStateTo(CurrentStateIndex, out T newState))
            {
                TransitionToState(newState);
                return;
            }

            TransitionToState(defaultNextState);
        }

        public bool CompareState(BaseState<T> state)
        {
            return CurrentState == state;
        }

        public bool CompareState(T index)
        {
            if (_states.TryGetValue(index, out var value))
            {
                return CurrentState == value;
            }
            else
            {
                Debug.LogError($"No state with index \"{index}\" in fsm.");
                return false;
            }
        }

        public new bool TryGetComponent<T>(out T component) where T : Component
        {
            var type = typeof(T);
            if (!_cachedComponents.TryGetValue(type, out var value))
            {
                if (base.TryGetComponent<T>(out component))
                    _cachedComponents.Add(type, component);

                return component != null;
            }

            component = (T)value;
            return true;
        }

        public new T GetComponent<T>() where T : Component
        {
            var type = typeof(T);
            T component = null;
            if (!_cachedComponents.TryGetValue(type, out var value))
            {
                if (base.TryGetComponent<T>(out component))
                    _cachedComponents.Add(type, component);

                return component;
            }

            component = (T)value;
            return component;
        }

        private void Update()
        {
            if (CurrentState != null)
                CurrentState.Update();
        }

        private void FixedUpdate()
        {
            if (CurrentState != null)
                CurrentState.FixedUpdate();
        }

        private void LateUpdate()
        {
            if (CurrentState != null)
                CurrentState.LateUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (CurrentState != null)
                CurrentState.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (CurrentState != null)
                CurrentState.OnTriggerExit(other);
        }

        private void OnDestroy()
        {
            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }
        }

        public bool TryGetStateTo(T currentState, out T newState)
        {
            StateLink stateLink = _stateLinks.SingleOrDefault(element => element.From.Equals(currentState));
            if (stateLink != null)
            {
                T state = stateLink.To;
                newState = stateLink.To;
                return true;
            }
            newState = default;
            return false;
        }

        [Serializable]
        public class StateLink
        {
            [SerializeField] private T _from;
            public T From => _from;
            [SerializeField] private T _to;
            public T To => _to;
        }
    }

    [Serializable]
    public class FSMDebugging
    {
        [SerializeField] private string currentState = "";

        public void SetCurrentState(string stateName)
        {
            currentState = stateName;
        }
    }
}