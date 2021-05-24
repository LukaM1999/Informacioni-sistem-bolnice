using System;
using System.Collections.ObjectModel;
using PropertyChanged;
namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Recept
    {
        public string ReceptId { get; set; }
        public ObservableCollection<Terapija> terapije = new();

        public Recept() { }
        public Recept(string id)
        {
            ReceptId = id;
        }

        public void DodajTerapiju(Terapija novaTerapija)
        {
            terapije.Add(novaTerapija);
        }
    }
}