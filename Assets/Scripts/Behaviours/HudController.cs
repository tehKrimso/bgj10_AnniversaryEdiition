using System;
using Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviours
{
    public class HudController : MonoBehaviour, IService
    {
        public ButtonAndCooldownText takeControl;
        public ButtonAndCooldownText useAbility;
        public ButtonAndCooldownText move;

        

        public void SetActiveTakeControl(bool isActive)
        {
            SwitchState(takeControl, isActive);
        }
        
        public void SetActiveTowerButtons(bool isActive)
        {
            SwitchState(useAbility, isActive);
            SwitchState(move, isActive);
        }

        public void UpdateAbilityCooldown(float cooldownLeft)
        {
            useAbility.cooldownText.text =  cooldownLeft.ToString();
        }
        
        public void UpdateMoveCooldown(float cooldownLeft)
        {
            useAbility.cooldownText.text =  cooldownLeft.ToString();
        }

        private void SwitchState(ButtonAndCooldownText target, bool state)
        {
            target.button.enabled = state;
            target.cooldownText.enabled = state;
        }
    }

    [Serializable]
    public class ButtonAndCooldownText
    {
        public Button button;
        public TextMeshProUGUI cooldownText;
    }
}