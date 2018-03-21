using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReadyScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler	
{

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		UIManager.instance.Invoke ("StartGame", 5f);
	}

	#endregion

	#region IPointerExitHandler implementation
	public void OnPointerExit (PointerEventData eventData)
	{
		UIManager.instance.CancelInvoke ("StartGame");
	}
	#endregion

}


