using System.Collections.Generic;
using UnityEngine;

namespace _Game.Source.Data.StaticData.UI
{
    [CreateAssetMenu(fileName = "RuneSpriteCatalogue_SO", menuName = "StaticData/UI/RuneSpriteCatalogue")]
    public class RuneSpritesCatalogue_SO: ScriptableObject
    {
        [SerializeField] private DictionaryInspector<string, Sprite> _runeSprites;
        public Dictionary<string, Sprite> GetRuneSprites() => _runeSprites.GetDictionary();
    }
}