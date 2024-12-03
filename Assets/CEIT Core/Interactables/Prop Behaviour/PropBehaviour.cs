using UnityEngine;

using CEIT.Persistence;


namespace CEIT.Interactables
{
	[RequireComponent(typeof(Rigidbody))]
	public class PropBehaviour : MonoBehaviour
	{
		[SerializeField] private Prop prop;

		public PropPartBehaviour[] parts { get; private set; }

		public float accumulatedYRotation
		{
			get => m_accumulatedYRotation;
			set => m_accumulatedYRotation = Mathf.Clamp(value, 0f, 360f);
		}
		public bool collidesWhenPlaced => prop ? prop.collidesWhenPlaced : true;
		public string uid => prop ? $"{prop.UID}" : "Original";
		public string instanceUid => prop ? $"{prop.UID}.{gameObject.GetInstanceID()}" : $"InstanceID-{gameObject.GetInstanceID()}";
		public bool usesGravity => prop ? prop.usesGravity : true;
		public bool yExtentIsZero => prop ? prop.yExtentIsZero : false;


		private Bounds completeBounds;
		private new Rigidbody rigidbody;

		private float m_accumulatedYRotation = 0f;
		private float m_previousDrag;


		public void Accomodate()
		{
			rigidbody.useGravity = false;
			rigidbody.freezeRotation = true;
			m_previousDrag = rigidbody.drag;
			rigidbody.drag = 7.5f;
			foreach (var part in parts)
			{
				part.Accomodate();
			}
		}

		public void Align(Vector3 normal)
		{
			float prevYRot = transform.eulerAngles.y;
			transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
			Rotate(prevYRot);
		}

		public void Place()
		{
			rigidbody.useGravity = usesGravity;
			rigidbody.freezeRotation = false;
			rigidbody.drag = m_previousDrag;
			foreach (var part in parts)
			{
				part.Place();
			}
		}

		public void Rotate(float yAngles)
		{
			accumulatedYRotation += yAngles;
			transform.Rotate(Vector3.up * yAngles);
		}


		public void Snap(Vector3 point, Vector3 normal)
		{
			Vector3 boundedWithNormal = new Vector3
			(
				(completeBounds.extents.x * normal).x,
				(completeBounds.extents.y * normal).y,
				(completeBounds.extents.z * normal).z
			);
			transform.position = point + boundedWithNormal;
		}


		#region Unity Messages
		private void Awake()
		{
			rigidbody = GetComponent<Rigidbody>();
			rigidbody.useGravity = usesGravity;
			parts = GetComponentsInChildren<PropPartBehaviour>();
			completeBounds = calculateBounds();
		}
		#endregion


		private Bounds calculateBounds()
		{
			if (parts.Length == 1)
				if (yExtentIsZero)
					return setYExtentToZero(parts[0].meshBounds);
				else
					return parts[0].meshBounds;
			Mesh newMesh = combinePartsMeshes();
			if (yExtentIsZero)
				return setYExtentToZero(newMesh.bounds);
			return newMesh.bounds;
		}

		private Mesh combinePartsMeshes()
		{
			CombineInstance[] combine = new CombineInstance[parts.Length];
			MeshFilter mf;
			for (int i = 0; i < parts.Length; i++)
			{
				mf = parts[i].GetComponent<MeshFilter>();
				combine[i].mesh = mf.sharedMesh;
				combine[i].transform = mf.transform.localToWorldMatrix;
			}
			Mesh newMesh = new Mesh();
			newMesh.CombineMeshes(combine);
			newMesh.Optimize();
			newMesh.RecalculateBounds();
			return newMesh;
		}

		private Bounds setYExtentToZero(Bounds b)
		{
			Bounds newBounds = new Bounds();
			Vector3 newExtents = Vector3.right + Vector3.forward;
			newExtents.x *= newBounds.extents.x;
			newExtents.z *= newBounds.extents.x;
			newBounds.extents = newExtents;
			return newBounds;
		}
	}
}