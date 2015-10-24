using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCN.Modules.Base.BusinessEntity;
using CCN.Modules.Base.Interface;
using Cedar.Core.IoC;

namespace CCN.WebAPI.Areas.Base.Controllers
{
    public class BaseController : Controller
    {
        private readonly IBaseManagementService _baseservice;

        public BaseController()
        {
            _baseservice = ServiceLocatorFactory.GetServiceLocator().GetService<IBaseManagementService>();
        }

        // GET: Base/Base
        public ActionResult Index()
        {
            var provList = _baseservice.GetProvList("");
            return View(provList);
        }

        public ActionResult GetProvList()
        {
            var list = _baseservice.GetProvList("");
            return View(list);
        }

        public ActionResult GetCityList(int provid)
        {
            var list = _baseservice.GetCityList(provid, "");
            return View(list);
        }

        public ActionResult GetBeandList()
        {
            var list = _baseservice.GetCarBrand("");
            return View(list);
        }

        public ActionResult GetSeriesList(int brandid)
        {
            var list = _baseservice.GetCarSeries(brandid);
            return View(list);
        }

        public ActionResult GetModelList(int seriesid)
        {
            var list = _baseservice.GetCarModel(seriesid);
            return View(list);
        }
    }
}