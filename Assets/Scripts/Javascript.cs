using System.Runtime.InteropServices;
using UnityEngine;

public class Javascript : MonoBehaviour {

	[DllImport("__Internal")]
	private static extern void EmitJSEvent(string eventName, string arg1, string arg2, string arg3);

	public static void Emit (string eventName, string arg1, string arg2, string arg3) {
#if UNITY_WEBGL == true && UNITY_EDITOR == false
		EmitJSEvent(eventName, arg1, arg2, arg3);
#endif
	}
}
