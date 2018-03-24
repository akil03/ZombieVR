using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RunAwayScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		UIManager.instance.ImageFill ();
		UIManager.instance.Invoke ("RunAway", 2.5f);
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		UIManager.instance.ImageReverseFill ();
		UIManager.instance.CancelInvoke ("RunAway");
	}

	#endregion
}
