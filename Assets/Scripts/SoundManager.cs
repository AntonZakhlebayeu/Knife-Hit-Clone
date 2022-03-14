using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	[SerializeField]
	private AudioSource soundSouce;
	[SerializeField]
	private AudioClip clickSound;
	[SerializeField]
	private AudioClip hitSound;

	private void Awake()
	{
		Instance = this;
	}

	public void PlayClickSound()
	{
		soundSouce.PlayOneShot(clickSound);
	}

	public void PLayHitSound()
	{
		soundSouce.PlayOneShot(hitSound);
	}
}
