using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 200f;
    public float currentHealth = 200f;
    public OverheadCanvasController overheadCanvasController;
    public UIManager uiManager;

    public void ModifyHealth(float aValue)
    {
        currentHealth += aValue;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        } else if (currentHealth < 0)
        {
            currentHealth = 0;
        }


        if (overheadCanvasController != null)
            overheadCanvasController.SetProgressbar(currentHealth / maxHealth);
        else if (uiManager != null)
        {
            uiManager.SetProgressbar(currentHealth / maxHealth);

        }

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (gameObject.tag != "Player")
            gameObject.SetActive(false);
        else
        {
            GetComponent<PlayerMovement>().isDead = true;
            uiManager.SetMessageText("Kalk Yerine Yat");
        }
    }
    

}
