using System;
using System.Collections.ObjectModel;
namespace Model
{
    public class Recept
    {
        public string id { get; set; }

        public ObservableCollection<Terapija> Terapije { get; set; }

        public Recept()
        {
        }
        public Recept(string ID)
        {
            id = ID;
        }
    }
}