using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float jumpForce;
	public float liftingForce;

	public bool jumped;
	public bool doubleJumped;
	public bool lot;

	private Rigidbody2D rb;
	public float startingY;

	// Na początku
	void Start () {
		//Zaczynamy od pobrania Rigidbody oraz zapamiętania początkowej pozycji gracza (z małym przesunięciem w górę), będzie nam ona potrzebna do sprawdzenia czy gracz już wylądował.
		rb = gameObject.GetComponent<Rigidbody2D> ();
		startingY = transform.position.y + 0.03f;
	}

	// Co klatkę
	void Update () {
		if (!GameManager.instance.inGame) return;
		//Najpierw sprawdzamy czy gracz jest na ziemi (jeżeli pamiętamy, że skakał, ale już jest w pozycji niższej niż zapamiętana startowa to ustawiamy flagi jumped i doubleJumped na false)
		if (jumped && transform.position.y <= startingY) 
		{
			jumped = false;
			doubleJumped = false;
		}

		//Pobieramy input przyciśnięcie przycisku myszy w Unity jest równoznaczne z dotknięciem ekranu. 
		if (Input.GetMouseButtonDown (0))
		{
			//Jeśli gracz chce skoczyć (dotknął ekranu) i jeszcze nie skakał nadajemy mu prędkość skierowaną w górę równą polu jumpForce i ustawiamy odpowiednią flagę
			if (!jumped) 
			{
				rb.velocity = (new Vector2 (0f, jumpForce));
				jumped = true;
			} else if (!doubleJumped)
			{
				//Analogicznie jeżeli jesteśmy już po pierwszym skoku, ale nadal możemy zrobić drugi
				rb.velocity = (new Vector2 (0f, jumpForce));
				doubleJumped = true;
			}
		}

		//Jeśli gracz cały czas przytrzymuje palec na ekranie to co klatkę dodajemy siłę liftingForce przemnożoną przez czas od ostatniej klatki (Żeby zniwelować wpływ wahał framerate na grę). Powoduje to powolniejsze opadanie.
		if (Input.GetMouseButton (0)) 
		{
			rb.AddForce(new Vector2 (0f, liftingForce * Time.deltaTime));
			lot = true;
        }
        else
        {
			lot = false;
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") && !GameManager.instance.isImmortal)
        {
			PlayerDeath();
        }
		else if (other.CompareTag("Coin"))
        {
			GameManager.instance.CoinCollection();
			Destroy(other.gameObject);
        }
		else if (other.CompareTag("Immortality"))
        {
			GameManager.instance.ImmortalityStart();
			Destroy(other.gameObject);
        }
		else if (other.CompareTag("Magnet"))
		{
			GameManager.instance.MagnetCollected();
			Destroy(other.gameObject);
		}
		else if (other.CompareTag("Platform"))
		{
			jumped = false;
			doubleJumped = false;
		}
	}

    void PlayerDeath()
    {
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		GameManager.instance.GameOver();
    }

}

