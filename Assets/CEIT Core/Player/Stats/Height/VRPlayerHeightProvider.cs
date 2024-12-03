using Unity.XR.CoreUtils;


namespace CEIT.Player.Stats
{
	public class VRPlayerHeightProvider : PlayerHeightProvider
	{
		public XROrigin xrOrigin;
		public override float Height => xrOrigin.CameraInOriginSpaceHeight;
	}
}