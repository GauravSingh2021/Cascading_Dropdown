using Cascading_Dropdown.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cascading_Dropdown.Controllers
{
    public class DemoController : Controller
    {
        private readonly DemoDbContext dbContext;
        public DemoController(DemoDbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            List<Country> countrylist = new List<Country>();

            //--------Getting data from database using EF core-------------
            countrylist = (from country in dbContext.Country
                           select country).ToList();

            //----------Inserting Select Item in List------------
            countrylist.Insert(0, new Country { CountryId = 0, CountryName = "Select" });

            //---------Assigning countrylist to ViewBag.ListofCountry----------
            ViewBag.ListofCountry = countrylist;
            return View();
        }
        public JsonResult GetState(int CountryId)
        {
            List<State> statelist = new List<State>();

            //--------Getting data from database using EF Core-------------
            statelist = (from state in dbContext.State
                         where state.CountryId == CountryId
                         select state).ToList();

            //----------Inserting Select Item in List--------
            statelist.Insert(0, new State { StateId = 0, StateName = "Select" });

            return Json(new SelectList(statelist, "StateId", "StateName"));
        }
        public JsonResult GetCity(int StateId)
        {
            List<City> citylist = new List<City>();

            //--------Getting data from database using EF Core-------------
            citylist = (from city in dbContext.City
                         where city.StateId == StateId
                         select city).ToList();

            //----------Inserting Select Item in List--------
            citylist.Insert(0, new City { CityId = 0, CityName = "Select" });

            return Json(new SelectList(citylist, "CityId", "CityName"));
        }
    }
}

