using UnityEngine;

namespace FirePaw
{
    namespace LevelArchitecture
    {
        [CreateAssetMenu]
        public class EpisodFlow : ScriptableObject
        {
            [SerializeField] private string episodName;
            public string     EpisodName => episodName;
        }
    }
}