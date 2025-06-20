using System;
using _GardenOfDreams.Scripts.Utilities;
using UnityEngine;

namespace _GardenOfDreams.Scripts.SaveTools.Models
{
    public class PlayerData : ProgressData<PlayerSubData>, IDisposable
    {
        private PlayerSubData _playerSubData;

        private bool IsPlayerSubDataNull()
        {
            return _playerSubData == null;
        }

        public void SaveHealth(float health)
        {
            if (IsPlayerSubDataNull())
            {
                _playerSubData = new PlayerSubData
                {
                    Health = health,
                    Position = default
                };
            }

            _playerSubData.Health = health;
        }
        
        public void SavePosition(Vector3 position)
        {
            if (IsPlayerSubDataNull())
            {
                _playerSubData = new PlayerSubData
                {
                    Health = -1,
                    Position = new SerializableVector3(position)
                };
            }

            _playerSubData.Position.UpdateValue(position);
        }
        
        public bool TryGetHealth(out float health)
        {
            health = -1;
            
            if (IsPlayerSubDataNull())
                return false;

            if (_playerSubData.Health == -1)
                return false;

            health = _playerSubData.Health;
            return true;
        }
        
        public bool TryGetPosition(out Vector3 position)
        {
            position = default;
            
            if (IsPlayerSubDataNull())
                return false;

            position = _playerSubData.Position.ToVector3();
            return true;
        }

        public override PlayerSubData GetProgressModel()
        {
           return _playerSubData;
        }

        public override void SetProgressModel(PlayerSubData state)
        {
            _playerSubData = state;
        }

        public void Dispose()
        {
            
        }
    }

    [Serializable]
    public class PlayerSubData
    {
        public float Health;
        public SerializableVector3 Position;
    }
}