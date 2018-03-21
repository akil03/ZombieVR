using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RunAwayScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		UIManager.instance.Invoke ("RunAway", 5f);
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		UIManager.instance.CancelInvoke ("RunAway");
	}

	#endregion
}
