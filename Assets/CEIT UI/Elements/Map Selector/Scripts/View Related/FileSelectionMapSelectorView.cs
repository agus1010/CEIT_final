using UnityEngine;
using UnityEngine.Events;


namespace CEITUI.Elements.MapSelector
{
	public class FileSelectionMapSelectorView : MapSelectorView
    {
        public UnityEvent<System.IO.FileInfo> OnFileChosen;


        public void FireOnFileChosen(System.IO.FileInfo fileInfo)
            => OnFileChosen?.Invoke(fileInfo);
    }
}
