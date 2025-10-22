using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour{
    
    private const int  MAX_HEALTH = 100;
    private int currentHealth;
    private TextMeshProUGUI healthText;

    [SerializeField] private Button HitButton;
    [SerializeField] private Button HealButton;
    [SerializeField] private Button KillButton;
    [SerializeField] private Button ReviveButton;
    private void Start() {
        if (healthText == null)
            healthText = GetComponent<TextMeshProUGUI>();
        currentHealth = MAX_HEALTH;
        healthText.text = currentHealth.ToString();
        CatController.Instance.onHit.AddListener(GetHit);
        CatController.Instance.onHeal.AddListener(GetHeal);
        CatController.Instance.onDie.AddListener(OnKill);
        CatController.Instance.onRevive.AddListener(OnRevive);
        HealButton.interactable = false;
        ReviveButton.interactable = false;
    }
    private void OnDisable() {
        CatController.Instance.onHit.RemoveListener(GetHit);
        CatController.Instance.onHeal.RemoveListener(GetHeal);
        CatController.Instance.onDie.RemoveListener(OnKill);
        CatController.Instance.onRevive.RemoveListener(OnRevive);
    }
    private void GetHit(){
        if(currentHealth > 0)
            SetHealth(currentHealth - 20);
        if (currentHealth <= 0){
            CatController.Instance.onDie.Invoke();
            HealButton.interactable = false;
            HitButton.interactable = false;
            KillButton.interactable = false;
            ReviveButton.interactable = true;
        }
        else
        {
            HealButton.interactable = true;
        }
        healthText.text = currentHealth.ToString();
        
    }
    private void GetHeal() {
        if (currentHealth > 0) {
            SetHealth(currentHealth + 20);
            HealButton.interactable = true;
            if (currentHealth >= MAX_HEALTH)
            {
                currentHealth = MAX_HEALTH;
                HealButton.interactable = false;
            }
            else
            {
                HealButton.interactable = true;
            }
        }
        healthText.text = currentHealth.ToString();
    }
    private void OnKill(){
        SetHealth(0);
        healthText.text = currentHealth.ToString();
        KillButton.interactable = false;
        HealButton.interactable = false;
        ReviveButton.interactable = true;
        HitButton.interactable = false;
    }
    private void OnRevive(){
        SetHealth(100);
        healthText.text = currentHealth.ToString();
        HealButton.interactable = false;
        ReviveButton.interactable = false;
        HitButton.interactable = true;
        KillButton.interactable = true;
    }
    private void SetHealth(int health){ 
        currentHealth = health;
    }
    public int GetCurrentHealth() {
        return currentHealth;
    }
}
