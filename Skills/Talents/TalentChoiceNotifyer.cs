using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TalentChoiceNotifyer : MonoBehaviour, IPointerClickHandler 
{
    public delegate void TalentChange(GameObject toggle, bool isActive);
    public TalentChange onTalentChange;

    public void SendToggleState(bool isActive)
    {
        if (onTalentChange != null)
        {
            onTalentChange(gameObject, isActive);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onTalentChange != null)
        {
            onTalentChange(gameObject, GetComponent<Toggle>().isOn);
        }
    }
}
