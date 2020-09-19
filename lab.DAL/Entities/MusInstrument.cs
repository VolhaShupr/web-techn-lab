
namespace lab.DAL.Entities
{
    public class MusInstrument
    {
        public int Id { get; set; }

        public string Name { get; set; } 
        public string Description { get; set; } 
        public string Brand { get; set; } 
        public string Image { get; set; }

        //Навигационные свойства
        /// <summary>
        /// группа
        /// </summary>

        public int GroupId { get; set; }
        public MusInstrumentGroup Group { get; set; }
    }
}
