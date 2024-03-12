using UnityEngine;
using UnityEngine.UI;

namespace FirePaw
{
    namespace LevelArchitecture
    {
        public class StartEpisod : MonoBehaviour
        {
            [SerializeField] private EpisodFlow episod;

            public void OnStartEpisodeButton() { LevelSequenceController.Instance.StartEpisode(episod); }
        }
    }
}


