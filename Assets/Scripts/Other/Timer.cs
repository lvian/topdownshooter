using UnityEngine;
using System.Collections;

public class Timer {
	protected float timer;
	protected float curTimer;

	public Timer(float time){
		this.timer = CustomTimer.instance.GameTimer;
		curTimer = time;
	}
	
	public bool IsDelayTimeElapsed(){
		return ((CustomTimer.instance.GameTimer - timer) >= curTimer);
	}
}
