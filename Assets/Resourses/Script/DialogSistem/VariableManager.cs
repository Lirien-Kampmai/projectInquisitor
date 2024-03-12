using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace FirePaw
{
    namespace DialogSistem
    {
        public class DialogVariable
        {
            public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

            private const string saveVarKey = "INK_VAR";

            public void StartListen(Story story)
            {
                VarToStory(story); // вызывается перед вардом, иначе eror.
                story.variablesState.variableChangedEvent += VariableChanged;
            }
            public void EndListen(Story story)
            {

                story.variablesState.variableChangedEvent -= VariableChanged;
            }


            public DialogVariable(Story story)
            {
                if (story == null) Debug.LogError("story not initializing");

                if(PlayerPrefs.HasKey(saveVarKey))
                {
                    string jsonState = PlayerPrefs.GetString(saveVarKey);
                    story.state.LoadJson(jsonState);
                }

                // инициализируем и добавляем переменные в словарик
                variables = new Dictionary<string, Ink.Runtime.Object>();
                foreach (string name in story.variablesState)
                {
                    Ink.Runtime.Object value = story.variablesState.GetVariableWithName(name);
                    variables.Add(name, value);
                    Debug.Log("VariableInit: " + name + " = " + value);
                }
            }

            public void SaveInkVariable(Story story)
            {
                if (story != null)
                {
                    VarToStory(story);
                    PlayerPrefs.SetString(saveVarKey, story.state.ToJson());
                }
            }

            public void ClearInkVariable(Story story)
            {
                if (story != null)
                {
                    PlayerPrefs.DeleteAll();
                    story.state.GoToStart();
                }
            }

            private void VariableChanged(string name, Ink.Runtime.Object value)
            {
                // чек, есть ли имя в контейне, если есть то удалить старое и заменить новым значением.
                if (variables.ContainsKey(name))
                {
                    variables.Remove(name);
                    variables.Add(name, value);
                }
                Debug.Log("VariableChanged: " + name + " = " + value);
            }

            // загружаем переменные в поток
            private void VarToStory(Story story)
            {
                // проходим по всему словарю
                foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
                {
                    // устанавливаем истории ключи(неймы) и значения
                    story.variablesState.SetGlobal(variable.Key, variable.Value);
                }
            }
        }
    }
}