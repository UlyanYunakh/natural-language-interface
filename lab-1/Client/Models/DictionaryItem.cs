using System;

namespace Client.Models
{
    [Serializable]
    public class DictionaryItem
    {
        public int Id { get; set; }
        public string Word { get; set; } // слово

        public bool IsWordform { get; set; } // словоформа
        public int Frequency { get; set; } // частота встречаемости

        public string PartOfSpeech { get; set; } // часть речи
        public string Kind { get; set; } // род
        public string Number { get; set; } // число
        public string Case { get; set; } // падеж
        public bool IsShort { get; set; } // краткость
        public string Person { get; set; } // лицо
        public string Time { get; set; } // время
        public string Form { get; set; } // вид
        public string Degree { get; set; } // степень сравнения
        public string Animality { get; set; } // одушевленность

        public bool IsDescription { get; set; }
        public string Description { get; set; } // неформатированная информация
    }
}
