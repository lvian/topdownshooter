using UnityEngine;
using System.Collections;

public class Timer {
	protected float timer;
	protected float duration;

	public Timer(float time){
		this.timer = CustomTimer.instance.GameTimer;
		duration = time;
	}

	public void AddTime(float seconds){
		duration += Mathf.Abs(seconds);
	}

	public void ReduceTime(float seconds){
		duration -= Mathf.Abs(seconds);
	}
	
	public bool IsElapsed {
		get {
			return ((CustomTimer.instance.GameTimer - timer) >= duration);
		}
	}

	public void Reset(){
		timer = CustomTimer.instance.GameTimer;
	}

	public float RemainingTime {
		get {
			float elapsed = CustomTimer.instance.GameTimer - timer;
			if(elapsed >= duration)
				return 0;
			else
				return (duration - elapsed);
		}
	}

	public float Duration {
		get {
			return duration;
		}
	}
}
