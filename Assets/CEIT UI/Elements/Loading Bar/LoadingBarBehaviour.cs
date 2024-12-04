using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace CEITUI.Elements
{
    public class LoadingBarBehaviour : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
		[SerializeField] private TextMeshProUGUI ammountText;
        [SerializeField] private Image fill;

        public UnityEvent<float> OnValueChanged;
        public UnityEvent OnCompletion;

        public float Ammount { get; private set; } = 0f;
        public string TitleText
        {
            get => title.text;
            set
            {
                string toAppend = string.IsNullOrEmpty(value)? "" : $": \'\'{value}\'\' ";
				title.text = $"Cargando{toAppend}...";
            }
        }

        public virtual void SetAmmount(float value)
		{
            if (value < 0f || value > 1f)
                return;
            Ammount = value;
            ammountText.text = (value * 100f).ToString("0.##") + "%";
            fill.fillAmount = Ammount;
            OnValueChanged?.Invoke(Ammount);
            if (Ammount == 1f)
                OnCompletion?.Invoke();
        }


        protected virtual void Start()
        {
            SetAmmount(0f);
        }
	}
}