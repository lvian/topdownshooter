using UnityEngine;
using System.Collections;

public class Timer {
	protected float timer;
	protected float curTimer;

	public Timer(float time){
		this.timer = CustomTimer.instance.GameTimer;
		curTimer = time;
	}
	
	public bool IsElapsed(){
		return ((CustomTimer.instance.GameTimer - timer) >= curTimer);
	}

	public void Reset(){
		timer = CustomTimer.instance.GameTimer;
	}

	public float RemainingTime {
		get {
			float elapsed = CustomTimer.instance.GameTimer - timer;
			if(elapsed >= curTimer)
				return 0;
			else
				return (curTimer - elapsed);
		}
	}

	public float Duration {
		get {
			return curTimer;
		}
	}
}
