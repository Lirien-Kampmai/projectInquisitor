using UnityEngine;

public class LoaderSetting : MonoBehaviour
{
    [SerializeField] private Settings[] allSettings;

    private void Awake()
    {
        for( int i =0; i < allSettings.Length; i++)
        {
            allSettings[i].Load ();
            allSettings[i].Apply();
        }
    }
}