using System;
using System.Collections.ObjectModel;
namespace Model
{
    public class Recept
    {
        public string id { get; set; }

        public ObservableCollection<Terapija> Terapije = new();

        public Recept()
        {
        }
        public Recept(string ID)
        {
            id = ID;
        }
    }
}