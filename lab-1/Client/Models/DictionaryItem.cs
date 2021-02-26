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

        public string PartOfSpeech { get; set; } = "Пусто";  // часть речи
        public string Kind { get; set; } = "Пусто"; // род
        public string Number { get; set; } = "Пусто"; // число
        public string Case { get; set; } = "Пусто"; // падеж
        public bool IsShort { get; set; } // краткость
        public string Person { get; set; } = "Пусто"; // лицо
        public string Time { get; set; } = "Пусто"; // время
        public string Form { get; set; } = "Пусто"; // вид
        public string Degree { get; set; } = "Пусто"; // степень сравнения
        public string Animality { get; set; } = "Пусто"; // одушевленность

        public bool IsDescription { get; set; }
        public string Description { get; set; } // неформатированная информация
    }
}
