using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LIMUPA.Models;

namespace LIMUPA.ViewComponents
{
 
    public class SliderViewComponent:ViewComponent 
    {
        private readonly DataContext _db;

        public SliderViewComponent(DataContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            List<Slider> data = _db.Sliders.ToList();
            return View(data);
        }  

    }
}
