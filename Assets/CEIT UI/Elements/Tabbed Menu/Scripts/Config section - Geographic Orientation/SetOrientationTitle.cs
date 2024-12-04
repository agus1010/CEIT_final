using UnityEngine;


namespace CEITUI.Elements.GeographicOrientation
{
    public class SetOrientationTitle : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI title;

        [SerializeField] private string formatTemplate;

        public void SetToValue(float angle)
        {
            string cardinalPoint;

            if (angle >= 315 || angle < 45) cardinalPoint = "Este";
            else if (angle >= 45 && angle < 135) cardinalPoint = "Norte";
            else if (angle >= 135 && angle < 225) cardinalPoint = "Oeste";
            else cardinalPoint = "Sur";

            title.text = string.Format(formatTemplate, cardinalPoint.ToString());
        }
    }
}