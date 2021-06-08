using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;
using Repozitorijum;

namespace Repozitorijum
{
    public class FeedbackRepo : IRepozitorijum
    {
        private const string Putanja = "../../../json/feedbackovi.json";

        private static readonly Lazy<FeedbackRepo> Lazy = new(() => new FeedbackRepo());
        public static FeedbackRepo Instance => Lazy.Value;

        public ObservableCollection<Feedback> Feedbackovi { get; set; }

        public ObservableCollection<object> Deserijalizacija()
        {
            lock (Feedbackovi)
                Feedbackovi = JsonConvert.DeserializeObject<ObservableCollection<Feedback>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {Feedbackovi};
        }

        public void Serijalizacija()
        {
            lock (Feedbackovi) 
                File.WriteAllText(Putanja, JsonConvert.SerializeObject(Feedbackovi, Formatting.Indented));
        }

        public FeedbackRepo()
        {
            Feedbackovi = new ObservableCollection<Feedback>();
        }

        public void DodajFeedback(Feedback popunjenFeedback)
        {
            Feedbackovi.Add(popunjenFeedback);
            Serijalizacija();
        }
    }
}
