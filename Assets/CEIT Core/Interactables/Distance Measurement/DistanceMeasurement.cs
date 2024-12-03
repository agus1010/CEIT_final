using System;
using UnityEngine;
using TMPro;


namespace CEIT.Assets.Interactables
{
	[RequireComponent(typeof(LineRenderer))]
	public class DistanceMeasurement : MonoBehaviour
	{
		[SerializeField] float _distance = 0f;
		public float Distance => _distance;

		[SerializeField] LineRenderer _lineRenderer;
		[SerializeField] Transform _uiParent;
		[SerializeField] TextMeshProUGUI _measurementText;

		Vector3 _startPosition;
		Vector3 _endPosition;

		// Privates UTILS
		Vector3 yOffStart => yOffsetPosition(_startPosition);
		Vector3 yOffEnd => yOffsetPosition(_endPosition);

		private Vector3 yOffsetPosition(Vector3 pos)
		{
			return pos + 0.2f * Vector3.up;
		}


		public void StartMeasuring(Vector3 position)
		{
			_startPosition = position;
			_lineRenderer.SetPosition(0, position);
			setChildGraphicsRaycastTargetValue(false);
		}

		public void UpdateMeasurement(Vector3 position)
		{
			setLastPoint(
				position,
				// update ui position to ALMOST THE END of the line
				() => _uiParent.position = Vector3.Lerp(yOffEnd, yOffStart, 0.05f)
			);
		}

		public void StopMeasuring(Vector3 position)
		{
			setLastPoint(
				position,
				// update ui position to the MIDDLE of the line
				() => _uiParent.position = Vector3.Lerp(yOffStart, yOffEnd, 0.5f)
			);
			setChildGraphicsRaycastTargetValue(true);
		}

		public void CancelMeasuring()
		{
			_lineRenderer.SetPosition(1, _startPosition);
			_uiParent.gameObject.SetActive(false);
		}

		public void LookAt(Transform target)
		{
			_uiParent.transform.LookAt(target);
		}

		private void setLastPoint(Vector3 position, Action uiPositionStrategy)
		{
			_endPosition = position;
			_lineRenderer.SetPosition(1, position);
			_distance = Vector3.Distance(_startPosition, _endPosition);

			uiPositionStrategy();
			_measurementText.text = _distance.ToString("#0.##") + " m";
		}

		private void setChildGraphicsRaycastTargetValue(bool value)
		{
			foreach (var graphic in _uiParent.GetComponentsInChildren<UnityEngine.UI.Graphic>())
			{
				graphic.raycastTarget = value;
			}
		}
	}
}