using System.Collections.Generic;

namespace lab.DAL.Entities
{
    public class MusInstrumentGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        ///<summary>
        ///Навигационное свойство 1-ко-многим
        ///</summary>
        public List<MusInstrument> MusInstruments { get; set; }
    }
}
