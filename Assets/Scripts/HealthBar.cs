using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Slider))]
public class HealthBar : MonoBehaviour
{

	public static HealthBar instance;
	public GameObject Player;
	public Text HealthText;
	private Slider slider;
	public int maxHealth = 100;
	public int minHealth = 0;
	public int health;

	void Awake()
	{
		instance = this;
		slider = GetComponent<Slider>();
	}

    private void Start()
    {
		health = Player.GetComponent<ItemPicker>().health;
		this.SetMaxValue(100);
    }

    public void SetMaxValue(float health)
	{
			slider.maxValue = health;
			slider.value = health;
			HealthText.text = slider.value.ToString();
	}

	public void SetHealth(float health)
	{
			slider.value = health;
			HealthText.text = slider.value.ToString();

	}

	public void AddHealth(float health)
	{

			slider.value = slider.value + health;
			HealthText.text = slider.value.ToString();
	}
    private void Update()
    {
		health = Player.GetComponent<ItemPicker>().health;
		this.SetHealth(health);
    }
}