using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "tripleTextChoices", menuName = "Triple Text Choices")]
public class TextTripleChoices : ScriptableObject
{
    public List<TripleWords> words = new List<TripleWords>();

    public TripleWords RandomWords()
    {
        if (words.Count == 0)
        {
            return null;
        }
        return words[Random.Range(0, words.Count)];
    }

    [System.Serializable]
    public class TripleWords
    {
        public string baseWord = "";
        public string twistWord1 = "";
        public string twistWord2 = "";
    }
}
