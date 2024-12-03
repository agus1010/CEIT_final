using UnityEngine;

using CEIT.Persistence;


namespace CEIT.Interactables.Custom
{
	public class GroundOnlySurfaceHistory : SurfaceHistory
	{
		public override void Paint(Surface surface)
		{
			if (!(surface.GetType() == typeof(Soil)))
				return;

			Material newMat = new Material(Current.materials[0]);
			Material extractee = surface.materials[0];

			newMat.mainTexture = surface.materials[0].mainTexture;
			newMat.SetTexture("_NormalMap", extractee.GetTexture("_NormalMap"));
			newMat.SetFloat("_NormalScale", extractee.GetFloat("_NormalScale"));
			
			Surface targetSoil = Instantiate(surface);
			targetSoil.materials = new Material[] { newMat };
			
			base.Paint(targetSoil);
		}
	}
}