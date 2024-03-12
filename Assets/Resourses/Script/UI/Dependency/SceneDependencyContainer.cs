using FirePaw.LevelArchitecture;
using FirePaw.SaveSystem;
using UnityEngine;

namespace FirePaw
{
    namespace Dependency
    {
        public class SceneDependencyContainer : Dependency
        {
            [SerializeField] private LevelSequenceController sequenceController;
            [SerializeField] private SaveLoad saveLoad;

            protected override void LinkAll(MonoBehaviour monoBehaviourInScene)
            {
                Link<LevelSequenceController>(sequenceController, monoBehaviourInScene);
                Link<SaveLoad>(saveLoad, monoBehaviourInScene);
            }

            private void Awake() { FindAllObjToBind(); }
        }
    }
}