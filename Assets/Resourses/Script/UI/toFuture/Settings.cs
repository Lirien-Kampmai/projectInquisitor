using UnityEngine;

public abstract class Settings : ScriptableObject
{
    [SerializeField] protected string title;
    public string Title => title;

    public virtual bool isMinValue { get; }
    public virtual bool isMaxValue { get; }

    public virtual void SetNextValue()    { }
    public virtual void SetPreviosValue() { }
    public virtual void SetFloatValue  () { }

    public virtual object GetValue()       { return default(object); }
    public virtual string GetStringValue() { return string.Empty;    }

    public virtual void Apply() { }
    public virtual void Load () { }
}