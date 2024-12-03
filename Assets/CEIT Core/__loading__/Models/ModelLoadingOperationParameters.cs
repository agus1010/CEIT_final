using System.IO;
using UnityEngine;


namespace CEIT.Loading
{
	[CreateAssetMenu(fileName = "New Map Loading Operation Paramters", menuName = "CEIT/Core/Parameters/Map Loading Operation Parameters")]
	public class ModelLoadingOperationParameters : ScriptableObject
	{
		[SerializeField] private FileInfo _mapFile = null;
		[SerializeField] private bool _addGround = false;
		[SerializeField] private float _groundHeight = 0f;
		[SerializeField] private Vector3 _spawnPosition = Vector3.zero;
		[SerializeField] private bool _addProps = false;
		[SerializeField] private FileInfo _propsFile = null;

		public virtual FileInfo mapFile
		{
			get => _mapFile;
			set => _mapFile = value;
		}

		public virtual bool addGround
		{
			get => _addGround;
			set => _addGround = value;
		}

		public virtual float groundHeight
		{
			get => _groundHeight;
			set => _groundHeight = value;
		}

		public virtual Vector3 spawnPosition
		{
			get => _spawnPosition;
			set => _spawnPosition = value;
		}

		public virtual bool addProps
		{
			get => _addProps;
			set => _addProps = value;

		}

		public virtual FileInfo propsFile
		{
			get => _propsFile;
			set => _propsFile = value;
		}


		public virtual void CopyProperties(ModelLoadingOperationParameters otherParams)
		{
			mapFile = otherParams.mapFile;
			addGround = otherParams.addGround;
			groundHeight = otherParams.groundHeight;
			spawnPosition = otherParams.spawnPosition;
			addProps = otherParams.addProps;
			propsFile = otherParams.propsFile;
		}

		public virtual void Reset()
		{
			_mapFile = null;
			_addGround = false;
			_groundHeight = 0f;
			_spawnPosition = Vector3.zero;
			_addProps = false;
			_propsFile = null;
		}
	}
}