using FirePaw.LevelArchitecture;
using FirePaw.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FirePaw
{
    namespace Dependency
    {
        public class GlobalDependencyContainer : Dependency
        {
            /// <summary>
            /// таки стоит разобраться с паузером. есть подозрение, что он не нужен, прям как мой код.
            /// </summary>
            //[SerializeField] private Pauser pauser;

            [SerializeField] private SaveLoad test;
            [SerializeField] private LevelSequenceController sequenceController;

            private static GlobalDependencyContainer instance;

            private void Awake()
            {
                if (instance != null)
                {
                    Destroy(gameObject);
                    return;
                }

                instance = this;
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += OnSceneLoad;

            }

            private void OnDestroy()
            {
                SceneManager.sceneLoaded -= OnSceneLoad;
            }

            protected override void LinkAll(MonoBehaviour monoBehaviourInScene)
            {
                Link<SaveLoad>(test, monoBehaviourInScene);
                Link<LevelSequenceController>(sequenceController, monoBehaviourInScene);
            }

            private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
            {
                FindAllObjToBind();
            }
        }
    }
}