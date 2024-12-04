using System.Collections;
using UnityEngine;


namespace CEITUI.Elements
{
	public class LoadingBarBehaviourDebug : LoadingBarBehaviour
	{
		[Range(0f, 1f)][SerializeField] float ammount = 0f;
		[SerializeField] private bool autoFill = false;

		private float m_prevAmmount;


		public override void SetAmmount(float value)
		{
			base.SetAmmount(value);
			ammount = Mathf.Clamp01(value);
		}

		protected override void Start()
		{
			base.Start();
			m_prevAmmount = ammount;
		}

		private void Update()
		{
			if(autoFill)
			{
				ammount = Mathf.Clamp01(ammount + Time.deltaTime);
			}
			if(ammount != m_prevAmmount)
			{
				SetAmmount(ammount);
				m_prevAmmount = ammount;
			}
		}
	}
}