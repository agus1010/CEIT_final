using CEIT.Interactables.Custom;
using UnityEngine;


namespace CEIT.Loading.Models.Utils
{
	public class AddedGroundController : MonoBehaviour
	{
		public MeshFilter addedGround;
		public ModelBehaviour modelBehaviour;

		public float heightExtent => modelBehaviour.model.bounds.extents.y + groundBounds.extents.y;


		private bool _isVisible = false;
		public bool isVisible
		{
			get => _isVisible;
			set
			{
				_isVisible = value;
				addedGround.gameObject.SetActive(value);
			}
		}


		private float _height = 0f;
		public float height
		{
			get => _height;
			set
			{
				_height = value;
				addedGround.transform.localPosition = new Vector3
				(
					addedGround.transform.localPosition.x,
					_height,
					addedGround.transform.localPosition.z
				);
			}
		}

		public float normalizedHeight => height / heightExtent;

		private Bounds groundBounds => addedGround.sharedMesh.bounds;



		public void ConfigGroundFromParameters(ModelLoadingOperationParameters parameters)
		{
			//isVisible = parameters.addGround;
			isVisible = true;
			bool addGround = parameters.addGround;
			addedGround.GetComponent<MeshRenderer>().enabled = addGround;
			addedGround.GetComponent<Interactables.SurfaceHistory>().enabled = addGround;
			if (addGround)
				height = parameters.groundHeight * heightExtent;
			else
			{
				SetGroundToLowerBoundaries(modelBehaviour.model.bounds);
				addedGround.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
			}
		}

		public void SetGroundToLowerBoundaries(Bounds modelBounds)
			=> height = -1 * heightExtent;


		private void OnDrawGizmosSelected()
		{
			Vector3 center = addedGround.transform.position;
			Gizmos.color = new Color(255, 0, 0, 0.25f);
			Gizmos.DrawCube(center, groundBounds.size);
			Gizmos.DrawWireCube(center, groundBounds.size);
		}
	}
}