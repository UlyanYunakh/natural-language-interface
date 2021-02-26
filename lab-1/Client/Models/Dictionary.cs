using System.Collections.Generic;
using System;
using System.Linq;

namespace Client.Models
{
    [Serializable]
    public class Dictionary
    {
        public List<DictionaryItem> Items { get; set; }
        private int _count;

        public Dictionary()
        {
            Items = new List<DictionaryItem>();
            _count = 1;
        }

        public void Save(DictionaryItem item)
        {
            if (item.Id != 0)
            {
                Change(item);
            }
            else
            {
                item.Id = _count;
                _count++;
                Items.Add(item);
            }
        }

        private void Change(DictionaryItem item)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Id == item.Id)
                {
                    Items[i] = item;
                    return;
                }
            }
        }

        public void Remove(int? id) => Items.Remove(Find(id));

        public DictionaryItem Find(int? id) => Items.Where(i => i.Id == id).FirstOrDefault();

        public void RecountItems()
        {
            _count = 1;
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Id = _count;
                _count++;
            }
        }
    }
}