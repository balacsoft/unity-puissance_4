using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public const int SOUND_CLICK 		= 1;
	public const int SOUND_BEEP 		= 2;
	public const int SOUND_LAST_BEEP 	= 3;
	public const int SOUND_COIN 		= 4;
	public const int SOUND_HIT 			= 5;
	public const int SOUND_GAME_OVER 	= 6;
	public const int SOUND_HEART 		= 7;
	public const int SOUND_FINISH 		= 8;
	public const int SOUND_BONUS 		= 9;
	public const int SOUND_STARSHIP		= 10;
	public const int SOUND_ERROR		= 11;
	public const int SOUND_GREAT		= 12;

	// This singleton on this class
	public static SoundManager gameSoundManager;

	// The sounds to play
	public AudioClip theClickSound;
	public AudioClip theBeepSound;
	public AudioClip theLastBeepSound;
	public AudioClip theCoinSound;
	public AudioClip theHitSound;
	public AudioClip theGameOverSound;	
	public AudioClip theHeartSound;
	public AudioClip theFinishSound;
	public AudioClip theBonusSound;
	public AudioClip theStarshipSound;
	public AudioClip theErrorSound;
	public AudioClip theGreatSound;

	// Audio source
	AudioSource mySoundAudioSource;

	// Set all parameters at awake of the scene
	void Awake () {
		if (gameSoundManager == null) {
			// Create the manager
			DontDestroyOnLoad (gameObject);
			gameSoundManager = this;
		} else if (gameSoundManager != this) {
			// Destroy it to have always only one manager (Singleton Design Pattern)
			Destroy (gameObject);
		}

		// Create AudioSource for sounds
		mySoundAudioSource = gameObject.AddComponent<AudioSource>();
		if (mySoundAudioSource == null) {
			Debug.LogWarning ("Error Creating Sound Audiosource");
		}
		mySoundAudioSource.loop = false;
	}

	public void PlaySound(int theSoundToPlay) {
		AudioClip clip;

		switch (theSoundToPlay) {
		case SOUND_CLICK:
			clip = theClickSound;
			break;
		case SOUND_BEEP:
			clip = theBeepSound;
			break;
		case SOUND_LAST_BEEP:
			clip = theLastBeepSound;
			break;
		case SOUND_COIN:
			clip = theCoinSound;
			break;
		case SOUND_HIT:
			clip = theHitSound;
			break;		
		case SOUND_GAME_OVER:
			clip = theGameOverSound;
			break;
		case SOUND_HEART:
			clip = theHeartSound;
			break;
		case SOUND_FINISH:
			clip = theFinishSound;
			break;		
		case SOUND_BONUS:
			clip = theBonusSound;
			break;		
		case SOUND_STARSHIP:
			clip = theStarshipSound;
			break;
		case SOUND_ERROR:
			clip = theErrorSound;
			break;
		case SOUND_GREAT:
			clip = theGreatSound;
			break;
		default:
			clip = theClickSound;
			break;
		}

		GetComponent<AudioSource>().PlayOneShot(clip);
	}
}