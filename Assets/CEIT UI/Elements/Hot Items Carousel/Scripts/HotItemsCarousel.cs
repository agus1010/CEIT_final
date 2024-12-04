using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;

using CEIT.Persistence;


namespace CEITUI.Elements
{
	[RequireComponent(typeof(UnityEngine.UI.Toggle))]
	public class HotItemsCarousel : MonoBehaviour
	{
		[SerializeField] private ItemPalette palette;

		public bool Locked { get; set; } = false;

		public ItemPalette Palette
		{
			get => palette;
			set
			{
				palette = value;
				RefreshGraphics();
			}
		}

		[SerializeField] private TMPro.TextMeshProUGUI title;
		[SerializeField] private ItemPreviewer[] previewers;
		[SerializeField] private int centeredPreviewerIndex = 0;
		[SerializeField] private SimpleScrollSnap simpleScrollSnap;

		private UnityEngine.UI.Toggle statusToggle;


		private int _halfPreviewers;
		private Animations.FadeAfterSeconds _titleFadeAnimator;


		public void MoveAPositionInDirection(int direction)
		{
			movePositions(System.Math.Sign(direction));
		}

		public void MoveNext()
		{
			movePositions(1);
		}

		public void MovePrevious()
		{
			movePositions(-1);
		}

		public void RefreshGraphics()
		{
			if(statusToggle != null)
				statusToggle.isOn = palette != null;
			if (palette != null)
			{
				updateAllPreviewers();
				updateTitle();
			}
		}


		private void OnEnable()
		{
			_titleFadeAnimator = title.GetComponent<Animations.FadeAfterSeconds>();
			_halfPreviewers = previewers.Length / 2;
		}

		private void Awake()
		{
			statusToggle = GetComponent<UnityEngine.UI.Toggle>();
		}

		private void Start()
		{
			simpleScrollSnap.GoToPanel(centeredPreviewerIndex);
			RefreshGraphics();
		}

		private int circularOffsettedPreviewerIndex(int offset)
			=> CEIT.MathUtils.CircularOffset(previewers.Length, centeredPreviewerIndex, offset);

		private void movePositions(int positions)
		{
			if (Locked) return;
			centeredPreviewerIndex = circularOffsettedPreviewerIndex(positions);
			int targetOppositeOffset = System.Math.Sign(positions) * _halfPreviewers;
			previewers[circularOffsettedPreviewerIndex(targetOppositeOffset)].SetItem(palette[targetOppositeOffset]);
			simpleScrollSnap.GoToPanel(centeredPreviewerIndex);
			updateTitle();
		}

		private void updateAllPreviewers()
		{
			int offset = -1 * _halfPreviewers;
			int targetPrevPos;
			for (int i = 0; i < previewers.Length; i++)
			{
				targetPrevPos = circularOffsettedPreviewerIndex(offset);
				previewers[targetPrevPos].SetItem(palette[offset]);
				offset += 1;
			}
		}

		private void updateTitle()
		{
			title.text = palette.Current.ItemName;
			_titleFadeAnimator?.Run(2.5f);
		}
	}
}