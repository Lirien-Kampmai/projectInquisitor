
using UnityEngine.SceneManagement;

namespace FirePaw
{
    namespace LevelArchitecture
    {
        public class LevelSequenceController : SingletonBase<LevelSequenceController>
        {
            public EpisodFlow CurrentEpisode { get; private set; }

            public void StartEpisode(EpisodFlow episod)
            {
                CurrentEpisode = episod;
                SceneManager.LoadScene(episod.EpisodName);
            }

            public void RestartLevelDebug() { SceneManager.LoadScene(CurrentEpisode.EpisodName); }
        }
    }
}