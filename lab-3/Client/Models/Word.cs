using System.Collections.Generic;

namespace Client.Models
{
    public class WordModel
    {
        public string Word { get; set; }
        public List<string> Synonym { get; set; }
        public List<string> RelatedTo { get; set; }
        public List<string> Antonym { get; set; }
        public List<string> Hyponym { get; set; }
        public List<string> Hyperonym { get; set; }
        public List<string> Definition { get; set; }
    }
}