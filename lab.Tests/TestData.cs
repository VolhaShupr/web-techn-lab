using lab.DAL.Data;
using lab.DAL.Entities;
using System.Collections.Generic;

namespace lab.Tests
{
    class TestData
    {
        public static void FillContext(ApplicationDbContext context)
        {
            context.MusInstrumentGroups.Add(new MusInstrumentGroup{ Name = "fake group" });
            context.AddRange(new List<MusInstrument>
            {
                new MusInstrument{ Id=1, GroupId=1 },
                new MusInstrument{ Id=2, GroupId=1 },
                new MusInstrument{ Id=3, GroupId=2 },
                new MusInstrument{ Id=4, GroupId=2 },
                new MusInstrument{ Id=5, GroupId=3 }
            });
            context.SaveChanges();
        }
        public static List<MusInstrument> GetInstrumentsList()
        {
            return new List<MusInstrument>
            {
                new MusInstrument{ Id=1, GroupId=1 },
                new MusInstrument{ Id=2, GroupId=1 },
                new MusInstrument{ Id=3, GroupId=2 },
                new MusInstrument{ Id=4, GroupId=2 },
                new MusInstrument{ Id=5, GroupId=3 }
            };
        }
        public static IEnumerable<object[]> Params()
        {
            //1-я страница, кол. объектов 3, id первого объекта 1 
            yield return new object[] { 1, 3, 1 };

            //2-я страница, кол. объектов 2, id первого объекта 4 
            yield return new object[] { 2, 2, 4 };

        }
    }
}
