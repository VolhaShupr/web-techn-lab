using lab.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace lab.Components
{
    public class MenuViewComponent : ViewComponent
    {
        //Инициализация списка элементов меню
        private List<MenuItem> _menuItems = new List<MenuItem>
        {
            new MenuItem{ Controller="Home", Action="Index", Text="Lab 3"}, 
            new MenuItem{ Controller="Product", Action="Index", Text="Products"},
            new MenuItem{ IsPage=true, Area="Admin", Page="/Index", Text="Admin"}
        };

        public IViewComponentResult Invoke()
        {
            //Получение значений сегментов маршрута
            var routeValues = ViewContext.RouteData.Values;
            var controller = routeValues["controller"];
            var page = routeValues["page"];
            var area = routeValues["area"];

            foreach (var item in _menuItems)
            {
                //Название контроллера совпадает?
                var _matchController = controller?.Equals(item.Controller) ?? false;

                //Название зоны совпадает?

                var _matchArea = area?.Equals(item.Area) ?? false;
                //Если есть совпадение, то сделать элемент меню активным
                //(применить соответствующий класс CSS)
                if (_matchController || _matchArea)
                {
                    item.Active = "active";
                }
            }
            return View(_menuItems);
        }
    }
}
