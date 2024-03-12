using FirePaw.LevelArchitecture;
using System;
using UnityEngine;

namespace FirePaw
{
    namespace SaveSystem
    {
        public class SaveLoad : SingletonBase<SaveLoad>
        {
            [Serializable]
            public class EpisodeList
            {
                public EpisodFlow episod;
            }

            /// <summary>
            /// открыл массив в паблик, чтобы достучаться чем-то. можно заменить на гет/сет.
            /// и прокинь зависимости.депенда уже залита и готова.
            /// </summary>
            [SerializeField] public EpisodeList[] m_CompletitionData;
            [SerializeField] private LevelSequenceController sequenceController;

            private EpisodFlow currentEpisode;

            public const string filename = "CompletitionData.dat";

            private void Start()
            {
                sequenceController = GetComponent<LevelSequenceController>();
                currentEpisode = sequenceController.CurrentEpisode;
            }

            public void LoadLastEpisod()
            {
                Saver<EpisodeList[]>.TryLoad(filename, ref m_CompletitionData);
            }

            public void SaveEpisod()
            {
                if (Instance)
                    Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode);
            }

            private void SaveResult(EpisodFlow currenEpisod)
            {
                foreach (var id in m_CompletitionData)
                {
                    if (id.episod == currenEpisod)
                    {
                        Saver<EpisodeList[]>.Save(filename, m_CompletitionData);
                    }
                }
            }
        }
    }
}