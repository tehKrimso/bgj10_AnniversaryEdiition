using UnityEngine;

namespace Behaviours.Towers
{
    public class ControlVisualSwitcher : MonoBehaviour
    {
        public SpriteRenderer baseVisual;
        public SpriteRenderer controllerVisual;

        public void SwitchControlState(bool isUnderControl)
        {
            if (isUnderControl)
            {
                baseVisual.gameObject.SetActive(false);
                controllerVisual.gameObject.SetActive(true);
            }
            else
            {
                baseVisual.gameObject.SetActive(true);
                controllerVisual.gameObject.SetActive(false);
            }
        }
    }
}
