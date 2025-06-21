using System;
using System.Collections.Generic;
using System.Linq;

namespace _GardenOfDreams.Scripts.SaveTools.Models
{
    public class DropItemData : ProgressData<DropItemSubData>
    {
        private DropItemSubData _dropItemSubData;
        
        private bool IsDropItemSubDataNull()
        {
            return _dropItemSubData == null;
        }

        public void PickupItem(int id)
        {
            if (IsDropItemSubDataNull())
            {
                _dropItemSubData  = new DropItemSubData()
                {
                   DropItemSubDatas = new List<DropItemSubData.DropItemSubDataArray>
                   {
                       new DropItemSubData.DropItemSubDataArray(){ ID = id, IsPickup = true}
                   }
                };
                
                return;
            }
            
            DropItemSubData.DropItemSubDataArray item = _dropItemSubData.DropItemSubDatas
                .FirstOrDefault(x => x.ID == id);

            if (item != null)
            {
                item.IsPickup = true;
            }
            else
            {
                _dropItemSubData.DropItemSubDatas.Add(new DropItemSubData.DropItemSubDataArray()
                {
                    ID = id,
                    IsPickup = true
                });
            }
        }
        public bool IsPickupItem(int id)
        {
            if (IsDropItemSubDataNull())
            {
                return false;
            }
            
            DropItemSubData.DropItemSubDataArray item = _dropItemSubData.DropItemSubDatas
                .FirstOrDefault(x => x.ID == id);

            return item != null && item.IsPickup;
        }
        
        public override DropItemSubData GetProgressModel()
        {
            return _dropItemSubData;
        }

        public override void SetProgressModel(DropItemSubData state)
        {
            _dropItemSubData = state;
        }
    }
    
    [Serializable]
    public class DropItemSubData
    {
        public List<DropItemSubDataArray> DropItemSubDatas;

        public class DropItemSubDataArray
        {
            public int ID;
            public bool IsPickup;
        }
    }
}