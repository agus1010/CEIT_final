using UnityEngine;
using UnityEngine.XR;


namespace CEIT.__booting__
{
    public enum HardwareType { PC, VR }


    [CreateAssetMenu(fileName = "New Hardware Type Provider", menuName = "CEIT/Hardware Type Provider")]
    public class HardwareTypeProvider : ScriptableObject
    {
        public HardwareType hardwareType
        { 
            get => XRSettings.enabled ? HardwareType.VR : HardwareType.PC;

		}
    }
}
