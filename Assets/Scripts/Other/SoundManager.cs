using UnityEngine;
using System.Collections;

	public class SoundManager : MonoBehaviour 
	{
		public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
		public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
		public AudioClip music1,music2;
		public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
		public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
		public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
		private bool muteFx;
		private AudioSource[] allAudioSources;
		private float wait;
		
		void Awake ()
		{
			//Check if there is already an instance of SoundManager
			if (instance == null)
				//if not, set it to this.
				instance = this;
			//If instance already exists:
			else if (instance != this)
				//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
				Destroy (gameObject);
			
			//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
			DontDestroyOnLoad (gameObject);
			musicSource.clip = music1;
			wait = music1.length;
			musicSource.Play ();
			muteFx = false;

		}
		
		void Update()
		{

			wait -= Time.deltaTime;
			if(muteFx)
			{
				allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
				foreach( AudioSource audioS in allAudioSources) {
					if(audioS.name != "SoundManager")
						audioS.Stop();
				}
			}
			if(wait <= 0){

				if(musicSource.clip.name == "Music2")
				{
					Debug.Log ("tocou music 2");
					musicSource.clip = music2;
					musicSource.Play ();
					wait = music2.length;
				} else if(musicSource.clip.name == "Music3")
				{
					Debug.Log ("tocou music 3");
					musicSource.clip = music1;
					musicSource.Play ();
					wait = music1.length;
				}
				Debug.Log (wait);
			}
		}
		
		//Used to play single sound clips.
		public void PlaySingle(AudioClip clip)
		{
			//Set the clip of our efxSource audio source to the clip passed in as a parameter.
			efxSource.clip = clip;
			
			//Play the clip.
			efxSource.Play ();
		}
		
		
		//RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
		public void clipOneShotRandomPitch (AudioClip clip)
		{
			
			//Choose a random pitch to play back our clip at between our high and low pitch ranges.
			float randomPitch = Random.Range(lowPitchRange, highPitchRange);
			
			//Set the pitch of the audio source to the randomly chosen pitch.
			efxSource.pitch = randomPitch;
			
			//Play the clip.
			efxSource.PlayOneShot(clip);
		}

		//RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
		public void clipOneShot (AudioClip clip)
		{
			//Play the clip.
			efxSource.PlayOneShot(clip);
		}

	public void MuteMusic()
	{
		if(UIToggle.current.value == false)
		{
			musicSource.Play();
		} else
		{
			musicSource.Pause();
		}
	}

	public void MuteFX()
	{

		if(UIToggle.current.value == false)
		{
			allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
			foreach( AudioSource audioS in allAudioSources) {
				if(audioS.name != "SoundManager")
					audioS.Stop();
			}
			muteFx = UIToggle.current.value;

		} else
		{
			muteFx = UIToggle.current.value;
		}
	}

	}
