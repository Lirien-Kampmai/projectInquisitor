using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using FirePaw.UI;
using FirePaw.LevelArchitecture;
using FirePaw.SaveSystem;

namespace FirePaw
{
    namespace DialogSistem
    {
        public class DialogManager : MonoBehaviour, IDependency<LevelSequenceController>, IDependency<SaveLoad>
        {
            [SerializeField] private TextAsset  inkFile;
            [SerializeField] private GameObject prefabButtonChoise;
            [SerializeField] private GameObject choiseArea;
            [SerializeField] private Animator   portraitAnimationSpeaker;
            [SerializeField] private Animator   backgroundAnimationPict;
            [SerializeField] private UIButton   buttonNext;
            [SerializeField] private Text       nameTag;
            [SerializeField] private Text       message;

            private static Story  story;
            private static Choice choiceSelected;

            private const string SPEAKER_TAG = "speaker";
            private const string PICT_TAG = "pict";
            private const string SCENE_TAG = "scene";
            private const string SCENE_G0 = "isProlog";

            private DialogVariable varuableManager;

            /// <summary>
            /// Всё что в дебаге требует либо допила, либо выпила. 
            /// </summary>
            [Header("Debug")]
            private SaveLoad saveLoad;
            private LevelSequenceController sequenceController;
            public void CreateDependency(LevelSequenceController obj) => sequenceController = obj;
            public void CreateDependency(SaveLoad obj) => saveLoad = obj;

            private void Start()
            {
                IniStory();
                FlowStory();
                buttonNext.PointerClick += OnClickNextFlow;
            }

            private void OnDestroy()
            {
               buttonNext.PointerClick -= OnClickNextFlow;
            }
            private void IniStory()
            {
                if (story == null) story = new Story(inkFile.text);

                varuableManager = new DialogVariable(story);
                varuableManager.StartListen(story);

                choiceSelected = null;
            }

            private void OnDisable()
            {
                varuableManager.EndListen(story);
            }

            public void OnClickNextFlow(UIButton arg0)
            {
                FlowStory();
            }

            private void FlowStory()
            {
                if (story.canContinue)
                {
                    AdvancedDialog();
                    AutoSaveFlowOnChoises();

                    if (story.currentChoices.Count != 0)
                        StartCoroutine(ShowChoises());
                }
                else
                    FinishDialog();
            }

            IEnumerator ShowChoises()
            {
                Debug.Log("You are need choises");

                List<Choice> choices = story.currentChoices;

                for (int i = 0; i < choices.Count; i++)
                {
                    GameObject temp = Instantiate(prefabButtonChoise, choiseArea.transform);
                    temp.transform.GetChild(0).GetComponent<Text>().text = choices[i].text;
                    temp.AddComponent<Selectable>();
                    temp.GetComponent<Selectable>().element = choices[i];
                    temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
                }

                choiseArea.SetActive(true);
                buttonNext.Interactable = false;

                yield return new WaitUntil(() => { return choiceSelected != null; });
                
                AdvancedFromDecision();
            }

            private void AdvancedFromDecision()
            {
                choiseArea.SetActive(false);
                buttonNext.Interactable = true;

                for (int i = 0; i < choiseArea.transform.childCount; i++)
                    Destroy(choiseArea.transform.GetChild(i).gameObject);

                choiceSelected = null;
                AdvancedDialog();
            }

            public static void SetDecision(object element)
            {
                choiceSelected = (Choice)element;
                story.ChooseChoiceIndex(choiceSelected.index);
            }

            private void FinishDialog()
            {
                Debug.Log("FinishDialog");

                ClearFlow();
                InvokeMenuScene();
            }

            private void AdvancedDialog()
            {
                string currentSuggestion = story.Continue();
                ParseTags(story.currentTags);
                SetSpeakerText(currentSuggestion);
            }

            private void ParseTags(List<string> currentTag)
            {
                foreach (string tag in currentTag)
                {
                    string tagKey = tag.Split(' ')[0];
                    string tagValue = tag.Split(" ")[1];

                    Debug.Log(tagValue);

                    switch (tagKey)
                    {
                        case SPEAKER_TAG:
                            SetSpeakerNameByTag    (tagValue);
                            SetSpeakerPortraitByTag(tagValue);
                            break;
                        case PICT_TAG:
                            SetBackgroundByTag(tagValue);
                            break;
                        case SCENE_TAG:
                            InvokeScene(tagValue, 2);
                            break;
                        default:
                            Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                            break;
                    }
                }
            }

            private void InvokeScene(string tagValue, int index)
            {
                EpisodFlow episod = null;

                if(tagValue == SCENE_G0)
                    episod = saveLoad.m_CompletitionData[index].episod;

                sequenceController.StartEpisode(episod);
            }

            private void InvokeResScene()
            {
                EpisodFlow episod = null;
                episod = saveLoad.m_CompletitionData[1].episod;
                sequenceController.StartEpisode(episod);
            }

            public void InvokeMenuScene()
            {
                EpisodFlow episod = null;
                episod = saveLoad.m_CompletitionData[0].episod;
                sequenceController.StartEpisode(episod);
            }

            private void SetBackgroundByTag(string backgroundPict) { backgroundAnimationPict. Play(backgroundPict); }
            private void SetSpeakerPortraitByTag(string speaker)   { portraitAnimationSpeaker.Play(speaker); }
            private void SetSpeakerNameByTag(string speaker)       { nameTag.text = speaker; }
            private void SetSpeakerText(string messageText)        { message.text = messageText; }
            private void AutoSaveFlowOnChoises ()                  { varuableManager.SaveInkVariable(story); }
            public void ClearFlow()
            {
                InvokeResScene();
                varuableManager.ClearInkVariable(story);
                story = null;
                IniStory();
            }
        }
    }
}