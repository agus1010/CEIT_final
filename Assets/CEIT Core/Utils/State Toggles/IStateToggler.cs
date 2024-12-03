namespace CEIT.Utils
{
    public interface IStateToggler
    {
        public bool Value { get; }
        public void Toggle();
    }
}