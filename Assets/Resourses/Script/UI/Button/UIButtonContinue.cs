using FirePaw.LevelArchitecture;
using UnityEngine;

namespace FirePaw
{
    namespace UI
    {
        public class UIButtonContinu : MonoBehaviour, IDependency<LevelSequenceController>
        {
            private LevelSequenceController sequenceController;
            public void CreateDependency(LevelSequenceController obj) => sequenceController = obj;

            private void Start() { sequenceController = GetComponent<LevelSequenceController>(); }
            public void Continue() { sequenceController.StartEpisode(sequenceController.CurrentEpisode); }
        }
    }
}