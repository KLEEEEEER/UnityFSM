# UnityFSM
My simple implementation of MonoBehaviour finite state machine. It just helping  to separate code in states and nothing more.

To use it you need to: 

* Create your class, that inherhit from FiniteStateMachine class (it will work as MonoBehaviour).
* Create state files that inherhit from BaseState class.
* Add your states to your created state machine in Awake method.
* Add your machine to GameObject and start working with it.

You can see example of finite state machine class in "Examples/ExampleFSM.cs" file
and examples of states in "Examples/States" folder.