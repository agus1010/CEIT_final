using UnityEngine;
using UnityEngine.Events;

using CEIT.Extensions;
using CEIT.Loading.Models;
using CEIT.Loading.Models.Utils;


namespace CEITUI.Elements.MapSelector.Assets
{
	public class SpawnSelectionController : CEITUIView
	{
		[SerializeField] private ModelBehaviour modelBehaviour;
		[SerializeField] private AddedGroundController addedGroundController;
		
		[SerializeField] private GameObject spawnPointIndication;

		[SerializeField] private CEIT.Persistence.SelectSpawnPointBehaviour interactionBehaviour;

		[SerializeField] private Transform calculationTransform;
		[SerializeField] private Transform rotationToMatch;

		public UnityEvent<Vector3> spawnPointSelected;
		
		public bool spawnPointAvailable { get; private set; } = false;
		public Vector3 chosenGroundPoint { get; private set; } = Vector3.zero;
		
		private bool _isVisible = true;
		public bool isVisible
		{
			get => _isVisible;
			set
			{
				_isVisible = value;
				body.SetActive(_isVisible);
			}
		}

		private Transform movablePoint;


		public Vector3 CalculateFinalWorldPosition()
		{
			movablePoint.localPosition = spawnPointIndication.transform.localPosition;
			return movablePoint.position;
		}

		public void Reset()
		{
			spawnPointAvailable = false;
			spawnPointIndication.SetActive(false);
			chosenGroundPoint = Vector3.zero;
			fireSpawnPointChanged();
		}


		private void Update()
		{
			if (interactionBehaviour.validTarget)
			{
				spawnPointAvailable = true;
				spawnPointIndication.SetActive(true);
				spawnPointIndication.transform.position = interactionBehaviour.shotPosition;

				fireSpawnPointChanged();
			}
		}

		private void Start()
		{
			calculationTransform.position = Vector3.zero;
			calculationTransform.rotation = rotationToMatch.transform.rotation.Inverse();
			calculationTransform.localScale = Vector3.one * 33.333333333f;
			movablePoint = calculationTransform.GetChild(0);
		}


		private void fireSpawnPointChanged()
			=> spawnPointSelected?.Invoke(chosenGroundPoint);
	}
}