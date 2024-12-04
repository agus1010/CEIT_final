using UnityEngine;


namespace CEITUI.Elements.MapSelector
{
    public class FlowController : MonoBehaviour
    {
        [Header("Scene References:")]
        public HeaderController headerController;
        public MapSelectorView[] viewsInOrder;

        [Header("Debug:")]
        [SerializeField] private int _step = 0;
        public int step { get => _step; private set => _step = value; }
        public MapSelectorView currentView => viewsInOrder[step];


		public void NextStep()
            => ChangeStep(step + 1);

		public void PreviousStep()
            => ChangeStep(step - 1);

        public void ChangeStep(int step)
        {
			currentView.isCurrentlyActive = false;
            this.step = step;
			headerController.UpdateGraphics(step, currentView.headerData);
			currentView.isCurrentlyActive = true;
		}


        private void Start()
        {
            for (int i = 0; i < viewsInOrder.Length; i++)
                viewsInOrder[i].isCurrentlyActive = false;
            ChangeStep(step);
		}
	}
}
