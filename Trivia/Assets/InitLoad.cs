//copied this code from a preview OUYA game
//and made modifications
//Link: https://github.com/ouya/unity-2d-demo/blob/master/project/Assets/Scripts/InitLoad.cs

using UnityEngine;
using System.Collections;

public class InitLoad : MonoBehaviour {
    
	void Start()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		WaitForOuyaSdk();
		#endif
		Application.LoadLevel("NumberOfPlayers");
	}
	
	#if UNITY_ANDROID && !UNITY_EDITOR
	IEnumerable WaitForOuyaSdk()
	{
		while (!OuyaSDK.isIAPInitComplete())
		{
			yield return null;
		}
	}
	#endif
}
