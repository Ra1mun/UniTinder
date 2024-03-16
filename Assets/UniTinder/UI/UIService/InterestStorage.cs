using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniTinder.UI.UIService
{
    public class InterestStorage : ScriptableObject
    {
        [SerializeField] private Interest[] _interests;
        [NonSerialized] private bool _isInit;

        private readonly Dictionary<InterestType, Sprite> _spriteStorage = new Dictionary<InterestType, Sprite>();
        
        private void Init()
        {
            for (int i = 0; i < _interests.Length; i++)
            {
                _spriteStorage.Add(_interests[i].InterestType, _interests[i].InterestSprite);
            }

            _isInit = true;
        }

        public Sprite GetSprite(InterestType type)
        {
            if (!_isInit)
            {
                Init();
            }

            if (_spriteStorage.ContainsKey(type))
            {
                return _spriteStorage[type];
            }

            return null;
        }
    }

    [Serializable]
    public class Interest
    {
        [SerializeField] private InterestType _interestType;
        [SerializeField] private Sprite _interestSprite;

        public InterestType InterestType => _interestType;
        public Sprite InterestSprite => _interestSprite;
    }
}