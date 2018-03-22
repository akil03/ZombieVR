using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReplayScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler  {

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		UIManager.instance.ImageFill ();
		UIManager.instance.Invoke ("Replay", 3f);
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		UIManager.instance.ImageReverseFill ();
		UIManager.instance.CancelInvoke ("Replay");
	}

	#endregion

}
