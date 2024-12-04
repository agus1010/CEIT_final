using UnityEngine;
using UnityEngine.UI;


namespace CEITUI.Elements
{
    [RequireComponent(typeof(Image))]
    public class IconSpriteInserter : MonoBehaviour
    {
		[Header("Mandatory:")]
        public CEIT.Player.PlayerIntents playerIntent = CEIT.Player.PlayerIntents.NONE;
		
		[Header("Optional:")]
		public PlayerIntentIconSet iconSet;
		public IconSetProvider provider;

        private Image image;


        public void SetSpriteFromIconSet(IPlayerIntentsIconsProvider iconSetProvider)
            => image.sprite = iconSetProvider.GetIconForIntent(playerIntent);


		private void Start()
		{
            Reset();
		}

		private void Reset()
		{
			image = GetComponent<Image>();
			if (iconSet != null)
				SetSpriteFromIconSet(iconSet);
			else
				if (provider != null)
					SetSpriteFromIconSet(provider);
		}
	}
}