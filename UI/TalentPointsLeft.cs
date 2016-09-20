using System;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Game event type for sending new amount of talent points
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class TalentPointsChanged : GameEvent
{
    private int count = 0;
    public int Count { get { return count; } }
    public TalentPointsChanged(GameObject sender, int newCount) : base(sender)
    {
        count = newCount;
    }
}

public class TalentPointsLeft : MonoBehaviour
{
    private Text textField = null;

    [SerializeField]
    private string formatString = "Points left: {0}";
    

    void Awake()
    {
        //register for talent point changes
        //Message<TalentPointsChanged>.Add(TalentPointsChangedHandler);
        EventManager.Instance.Register<TalentPointsChanged>(TalentPointsChangedHandler);
    }

    void Start()
    {
        textField = GetComponent<Text>();
    }

    private void TalentPointsChangedHandler(EventArgs changeEvent)
    {
        TalentPointsChanged changedEvent = changeEvent as TalentPointsChanged;
        textField.text = String.Format(formatString, changedEvent.Count);
    }
}
