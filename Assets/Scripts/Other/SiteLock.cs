using UnityEngine;
using System.Collections;
using System.Text;

public class SiteLock : MonoBehaviour {
	
	/** if it is a webplayer, then the domain must contain any
	 * one or more of these strings, or it will be redirected */
	public string[] domainMustContain;
	
	/** this is where to redirect the webplayer page if none of
	 * the strings in domainMustContain are found.
	 */
	public string redirectURL;
	
	void Awake() {
		#if UNITY_WEBPLAYER
		if (domainMustContain.Length > 0)
		{
			StringBuilder buf = new StringBuilder();
			
			for(int i = 0; i < domainMustContain.Length; i++)
			{
				string domain = domainMustContain[i];
				
				if (i > 0)
				{
					buf.Append(" && ");
				}
				buf.Append("(document.location.host.indexOf('"+domain+"') == -1)");
			}
			string criteria = buf.ToString();
			Application.ExternalEval("if("+criteria+") { document.location='"+redirectURL+"'; }");
		}
		#endif
	}
}