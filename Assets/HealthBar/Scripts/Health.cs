using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace HealthBar
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float health;
        [SerializeField]
        private  float maxHealth;
        [SerializeField]
        private Image healthImage;



        private void Start()
        {
            health = maxHealth;
        }

        [ContextMenu("AddHealth")]
        public void AddHealth(float amount)
        {
            health += amount;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            else if (health < 0)
            {
                health = 0;
                //muerto, deberiamos soltar un evento para todos los subscriptos.
            }
            UpdateHealthUI();
        }

        private void UpdateHealthUI()
        {
            healthImage.fillAmount = ((1) / (maxHealth)) * health;
        }

    }
}
