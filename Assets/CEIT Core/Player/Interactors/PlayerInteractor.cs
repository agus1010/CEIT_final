using UnityEngine;

using CEIT.Persistence;


namespace CEIT.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        public PlayerPointer pointer;

        [SerializeField] private Interaction _interaction;
        public Interaction interaction { get => _interaction; private set => _interaction = value; }
        public InteractionBehaviour behaviour => interaction != null? interaction.Behaviour : null;

        [SerializeField] private bool canPerformActions = false;
        public bool CanPerformActions { get => canPerformActions; set => canPerformActions = value; }

		[Header("Debug:")]
        [SerializeField] private bool debug = false;


        public void InterruptCurrentInteraction()
		{
            interaction?.Behaviour?.Stop();
            interaction?.Behaviour?.Initialize(interaction, pointer);
		}

        public void TryPerformPrimary(bool newValue)
		{
            if (debug) printDebuggingFormattedMessage("Primary {0}", newValue);
            if (CanPerformActions)
                behaviour?.TryPerformPrimary(newPrimaryValue: newValue);
		}

        public void TryPerformSecondary(bool newValue)
		{
            if(debug) printDebuggingFormattedMessage("Secondary {0}", newValue);
            if(CanPerformActions)
                behaviour?.TryPerformSecondary(newSecondaryValue: newValue);
        }

        public void TryPerformChangeInteractionValue(int direction)
		{
            if (debug) printDebuggingFormattedMessage($"Interaction value change in direction: {0}", direction);
            if(CanPerformActions)
                behaviour?.TryPerformChangeInteractionValue(direction);
		}


        public void SetInteraction(Interaction interaction)
        {
            if (this.interaction != null)
                this.interaction.Behaviour.Stop();
            this.interaction = interaction;
            if(interaction.Behaviour != null)
                this.interaction.Behaviour.Initialize(interaction, pointer);
        }

        public void SetInteraction(InteractionPalette interactionPalette)
            => SetInteraction(interactionPalette.Current as Interaction);
        

		private void Update()
		{
            behaviour?.OnUpdate();
		}

		private void FixedUpdate()
		{
			behaviour?.OnFixedUpdate();
		}


        private void printDebuggingFormattedMessage(string message, object value)
		{
            print(string.Format(message, value) + (!CanPerformActions ? " (LOCKED)" : ""));
		}

    }
}