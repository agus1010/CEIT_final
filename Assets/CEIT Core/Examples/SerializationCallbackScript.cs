using System;
using System.Collections.Generic;
using UnityEngine;


public class SerializationCallbackScript : MonoBehaviour, ISerializationCallbackReceiver
{
    public List<int> _keys;
    public List<string> _values;

    //Unity doesn't know how to serialize a Dictionary
    public Dictionary<int, string> _myDictionary;

	public void Reset()
	{
        print("RESET");
        _keys = new List<int> { 3, 4, 5 };
        _values = new List<string> { "I", "Love", "Unity" };

        _myDictionary = new Dictionary<int, string>();

        for (int i = 0; i < _keys.Count; i++)
		{
            _myDictionary[_keys[i]] = _values[i];   
		}
	}


	public void OnBeforeSerialize()
    {
        print("ON BEFORE SERIALIZE");
        _keys.Clear();
        _values.Clear();

        foreach (var kvp in _myDictionary)
        {
            _keys.Add(kvp.Key);
            _values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        print("ON AFTER SERIALIZE");
        _myDictionary = new Dictionary<int, string>();

        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            _myDictionary.Add(_keys[i], _values[i]);
    }

    void OnGUI()
    {
        foreach (var kvp in _myDictionary)
            GUILayout.Label("Key: " + kvp.Key + " value: " + kvp.Value);
    }
}