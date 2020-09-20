using lab.DAL.Entities;
using lab.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace lab.Controllers
{
    public class ProductController : Controller
    {
        public List<MusInstrument> _instruments;
        List<MusInstrumentGroup> _instrumentGroups;

        int _pageSize;

        public ProductController()
        {
            _pageSize = 3;
            SetupData();
        }

        public IActionResult Index(int? group, int page = 1)
        {
            //Поместить список групп во ViewData 
            ViewData["Groups"] = _instrumentGroups;
            //Получить id текущей группы и поместить в TempData
            ViewData["CurrentGroup"] = group ?? 0;

            var filteredItems = _instruments.Where(item => !group.HasValue || item.GroupId == group.Value);

            return View(ListViewModel<MusInstrument>.GetModel(filteredItems, page, _pageSize));
        }

        ///<summary>
        ///Инициализация списков
        ///</summary>
        private void SetupData()
        {
            _instrumentGroups = new List<MusInstrumentGroup>
            {
                new MusInstrumentGroup { Id=1, Name="Guitars" }, 
                new MusInstrumentGroup { Id=2, Name="Keys" }, 
                new MusInstrumentGroup { Id=3, Name="Winds" }, 
                new MusInstrumentGroup { Id=4, Name="Drums" },
                new MusInstrumentGroup { Id=5, Name="Strings" }, 
                new MusInstrumentGroup { Id=6, Name="Percursion" }
            };

            _instruments = new List<MusInstrument>
            {
                new MusInstrument { 
                    Id = 1, Name="Kalimba", 
                    Description="17 keys kalimba", 
                    Brand = "VBH", GroupId=6, Image="kalimba.png" 
                },
                new MusInstrument { 
                    Id = 2, Name="Octopus soprano ukulele - sky blue", 
                    Description="Fitted with Aquila Nylgut strings", 
                    Brand = "Octopus", GroupId=1, Image="ukulele.jpeg" 
                },
                new MusInstrument { 
                    Id = 3, Name="Hangpan Drum HD1", 
                    Description="MEINL Sonic Energy Harmonic Art Handpan instruments are hand made one at a time using top grade german steel.", 
                    Brand = "Meinl", GroupId=4, Image="hangpan.png" 
                }, 
                new MusInstrument { 
                    Id = 4, Name="Hurdy Gurdy w/Gig Bag", 
                    Description="Amaze your friends with the mechanical operation of the Hurdy Gurdy", 
                    Brand ="hurdfin", GroupId=5, Image="hurdy_gurdy.jpg" 
                }, 
                new MusInstrument { 
                    Id = 5, Name="Rainstick", 
                    Description="The Meinl RS1BK-L Rainstick is a rainstick, made from bamboo, and comes in black. Natural Bamboo makes these rainsticks produce a pleasant effect offering plenty of projection and a long sustain. They can also be played as if they are large shakers.", 
                    Brand ="Meinl", GroupId=6, Image="rainstick.jpg" 
                }

            };
        }
    }
}
