using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
	public Rigidbody2D knifeRigidbody;

	[SerializeField]
	public bool attachedToWood;

	private bool knifeHitKnife = false;

	private void Start()
	{
		Vibration.Init();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<Wood>() != null && !this.attachedToWood && !knifeHitKnife)
		{
			knifeRigidbody.velocity = Vector2.zero;
			transform.parent = other.transform;
			attachedToWood = true;

			GameController.Instance.OnSuccessfullKnifeHit();
			knifeRigidbody.GetComponent<ParticleSystem>().Play();
			Wood.spawnedWood.GetComponent<Animator>().Play("Hit");

			Variables.score++;
			GameUI.UpdateScoreText(Variables.score);
			DataManager.SetHighScore(Variables.score);

			Vibration.Vibrate(50);
		}

		if (other.gameObject.GetComponent<Knife>() != null && other.gameObject.GetComponent<Knife>().attachedToWood == true)
		{
			knifeRigidbody.gravityScale = 1;

			knifeRigidbody.velocity = Vector2.zero;
			knifeRigidbody.AddForce(new Vector2(Random.Range(-2, 3), -5) * Random.Range(1, 3), ForceMode2D.Impulse);

			var impulse = (Random.Range(-150, 151) * Mathf.Deg2Rad * 9) * knifeRigidbody.inertia;
			knifeRigidbody.AddTorque(impulse, ForceMode2D.Impulse);

			DataManager.SetHighScore(Variables.score);

			knifeHitKnife = true;

			Variables._isGameWon = false;
			GameController.Instance.StartGameOverSequence(Variables._isGameWon);
		}

		if (other.gameObject.GetComponent<Apple>() != null)
		{
			Apple.AppleDestroyed(other.gameObject);
		}
	}
}
