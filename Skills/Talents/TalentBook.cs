using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// Container for talent selection
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class TalentBook : MonoBehaviour
{
    /// <summary>
    /// Root canvas for all gui elements
    /// </summary>
    [SerializeField]
    private Canvas uiCanvas = null;

    /// <summary>
    /// Talent book UI canvas
    /// </summary>
    [SerializeField]
    private Canvas bookCanvas = null;
    /// <summary>
    /// Visual style of the talent buttons
    /// </summary>
    [SerializeField]
    private Toggle buttonPrefab;

    /// <summary>
    /// Number of available talent points if no talent is learned
    /// </summary>
    [SerializeField]
    private int maximumTalentPoints = 2;
    /// <summary>
    /// Currently available talent points
    /// </summary>
    private int talentPoints = 0;
    public int TalentPoints { get { return talentPoints; } }
    /// <summary>
    /// Talents from which the user can choose
    /// </summary>
    private List<Talent> talents = null;
    /// <summary>
    /// Collection of talent toggle buttons, i.e. the visual representation
    /// </summary>
    private List<Toggle> talentUIelements = null;

    public void Awake()
    {
        talents = new List<Talent>();
        talentPoints = maximumTalentPoints;
        talentUIelements = new List<Toggle>();
    }

    public void Start()
    {
        EventManager.Instance.SendMessage<TalentPointsChanged>(new TalentPointsChanged(gameObject, talentPoints));
        //Message<TalentPointsChanged>.Post(new TalentPointsChanged(talentPoints));
    }

    public void AddTalent(Talent t)
    {
        //save the talent in the book
        talents.Add(t);
        Toggle talentToggleButton = CreateTalentButton(t);
        talentUIelements.Add(talentToggleButton);
    }

    private Toggle CreateTalentButton(Talent t)
    {
        //create a ui element to be able to choose a talent
        Toggle talentToggleButton = Instantiate(buttonPrefab) as Toggle;
        talentToggleButton.GetComponentInChildren<Text>().text = t.TalentName;
        //parent toggle to talent ui canvas and adjust its position
        //relative to the parent
        talentToggleButton.transform.SetParent(bookCanvas.transform, false);
        //apply talent icon to the button
        talentToggleButton.image.sprite = t.Icon;

        //button transform
        RectTransform talentRectTransform = talentToggleButton.GetComponent<RectTransform>();

        RectTransform bookCanvasRectTransform = bookCanvas.GetComponent<RectTransform>();
        //prepare a vector for the local position of the button with a x-offset
        float xOffset = 200f;
        float yOffset = 100f;
        Vector3 buttonPosition = new Vector3(bookCanvasRectTransform.anchorMin.x + xOffset, bookCanvasRectTransform.anchorMax.y + yOffset, 0f);

        buttonPosition.y = buttonPosition.y //topmost position
        - talentUIelements.Count * (talentRectTransform.rect.height + 15f); // vertical offset including margin


        talentRectTransform.localPosition = buttonPosition;
        //Vector3 tooltipPosition = talentRectTransform.position;
        //place the tooltip beside the talent
        RectTransform tooltipRectTransform = t.Tooltip.GetComponent<RectTransform>();
        t.Tooltip.transform.SetParent(talentRectTransform.transform, false);
        buttonPosition.y = 0f;        
        buttonPosition.x = 374f;
        tooltipRectTransform.localPosition = buttonPosition;
        
        //236,273 -> targetpos
        //ttrt.position = uiCanvas.transform.TransformVector(new Vector3(236f, 273f, 0f));
        
        //position of pivot relative to anchors
        //ttrt.anchoredPosition = Vector3.zero;

        //tooltipPosition;
        //connect OnEnter and OnLeave events 
        //with displaying or hiding the tooltip
        EventTrigger et = talentToggleButton.GetComponent<EventTrigger>();
        for (int i = 0; i < et.delegates.Count; i++)
        {
            EventTrigger.Entry e = et.delegates[i];
            if (e.eventID == EventTriggerType.PointerEnter)
            {
                //pass event data by lambda expression
                e.callback.AddListener((eventdata) => { t.ShowTooltip(); });
            }
            else if (e.eventID == EventTriggerType.PointerExit)
            {
                e.callback.AddListener((eventdata) => { t.HideTooltip(); });
            }
        }

        talentToggleButton.onValueChanged.AddListener(t.SetState);
        talentToggleButton.GetComponent<TalentChoiceNotifyer>().onTalentChange += ProcessTalentChoice;

        return talentToggleButton;
    }

    /// <summary>
    /// Consumes a talent point during learning
    /// </summary>
    private void LearnTalent()
    {
        talentPoints = Mathf.Clamp(--talentPoints, 0, maximumTalentPoints);
        EventManager.Instance.SendMessage<TalentPointsChanged>(new TalentPointsChanged(gameObject, talentPoints));

        //Message.Post<TalentPointsChanged>(new TalentPointsChanged(talentPoints)); //replace with custom
    }

    /// <summary>
    /// Releases talent points during unlearning a talent
    /// </summary>
    private void UnlearnTalent()
    {
        talentPoints = Mathf.Clamp(++talentPoints, 0, maximumTalentPoints);
        EventManager.Instance.SendMessage<TalentPointsChanged>(new TalentPointsChanged(gameObject, talentPoints));

        //Message.Post<TalentPointsChanged>(new TalentPointsChanged(talentPoints)); //custom todo
    }

    /// <summary>
    /// Checks if there are talent points left to spent
    /// </summary>
    /// <returns>true if talent points are available</returns>
    public bool CanLearn()
    {
        return talentPoints > 0;
    }

    /// <summary>
    /// Callback function that is used to intercept talent selection and activation.
    /// If a talent toggle is chosen and no talent points are available the corresponding toggle state is set to off.
    /// </summary>
    /// <param name="toggle">firing UI object</param>
    /// <param name="toggleState">whether the toggle is on or off</param>
    private void ProcessTalentChoice(GameObject toggle, bool toggleState)
    {
        //get the toggle component as it should be searched
        //in the ui elements list
        Toggle t = toggle.GetComponent<Toggle>();
        for (int i = 0; i < talentUIelements.Count; i++)
        {
            //if there is a match
            if (t == talentUIelements[i])
            {
                //if talent was not learned already
                //toggleState is true as it assumes that the talent can be learned
                if (toggleState)
                {
                    //check if there are talent points left
                    if (CanLearn())
                    {
                        //activate the corresponding talent
                        talents[i].Learn();
                        //consume a talent point
                        LearnTalent();
                    }
                    else
                        t.isOn = false;//keep the toggle inactive

                }
                else
                {
                    //otherwise inactivate the talent
                    talents[i].Unlearn();
                    //release the talent point
                    UnlearnTalent();
                }

                break;
            }
        }
    }
}
