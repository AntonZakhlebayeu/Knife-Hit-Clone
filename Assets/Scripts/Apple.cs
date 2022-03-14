using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static Apple Instance { get; private set; }

    [SerializeField]
    private GameObject[] appleParticles;

    private void Awake()
    {
        Instance = this;
    }

    public static void AppleDestroyed(GameObject other)
    {
        Variables.apple++;
        DataManager.SetAmountOfApples(Variables.apple);
        GameUI.UpdateAppleText(Variables.apple);

		Transform tempTransform = other.GetComponent<Transform>();
		Destroy(other);

		foreach(GameObject particle in Instance.appleParticles)
		{
			GameObject appleParticle = Instantiate(particle, new Vector2(tempTransform.position.x + Random.Range(0.10f, 0.33f), tempTransform.position.y + Random.Range(0.10f, 0.33f	)), Quaternion.identity);
			Rigidbody2D tempRigidbody = appleParticle.GetComponent<Rigidbody2D>();
			tempRigidbody.AddForce(new Vector2(Random.Range(-2, 3), Random.Range(-2, 3) ) * Random.Range(1, 3), ForceMode2D.Impulse);
			var impulse = (Random.Range(-150, 151) * Mathf.Deg2Rad ) * tempRigidbody.inertia;
			tempRigidbody.AddTorque(impulse, ForceMode2D.Impulse);
			Destroy(appleParticle, 3f);
		}
    }
}
