using System.Collections.Generic;
using UniTinder.UI.UIService;
using UnityEngine;

namespace UniTinder.SessionData
{
    public class SessionData
    {
        private string _nickname;
        private List<InterestType> _interestTypes;
        private Sprite _avatar;
        private Sprite _background;
        
        public void CreateData(string nickname,
            List<InterestType> type,
            Sprite avatar,
            Sprite background)
        {
            _nickname = nickname;
            _interestTypes = type;
            _avatar = avatar;
            _background = background;
        }

        public string GetNickname()
        {
            if (_nickname != null)
            {
                return _nickname;
            }

            return null;
        }

        public List<InterestType> GetInterests()
        {
            if (_interestTypes != null)
            {
                return _interestTypes;
            }

            return null;
        }

        public Sprite GetAvatar()
        {
            if (_avatar != null)
            {
                return _avatar;
            }

            return null;
        }

        public Sprite GetBackground()
        {
            if (_background != null)
            {
                return _background;
            }

            return null;
        }
    }
}