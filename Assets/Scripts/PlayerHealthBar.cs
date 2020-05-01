using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Text HealthBar;

    private CharacterHealth  _healthComponent;

    public void Start()
    {
        _healthComponent = gameObject.GetComponent<CharacterHealth>();
    }

    private void Update()
    {
        if (_healthComponent.characterHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        HealthBar.text = "HEALTH: " + _healthComponent.characterHealth;
    }
}
