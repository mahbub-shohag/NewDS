using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public class MyFilter : ActionFilterAttribute
        {
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                DsDbContext context = new DsDbContext();
                var menus = context.menus.SqlQuery("select * from dbo.Menus where ParentId!=0").ToList();
                //filterContext.Controller.ViewBag.menus = menus;
                string html = "";
                foreach (var aMenu in menus)
                {
                    string subMenuHtml = "";
                    if (aMenu.ParentId == 1)
                    {

                        html += "<li class='dropdown'><a href='/"+aMenu.Controller+"/"+aMenu.Action+"'>" + aMenu.Caption;
                        foreach (var subMenu in menus)
                        {
                            if (subMenu.ParentId == aMenu.Id)
                            {
                                subMenuHtml += "<li><a href='/Content/Browse/" + subMenu.Id + "'>" + subMenu.Caption + "<a/></li>";
                            }
                        }
                        if (subMenuHtml != "")
                        {
                            html = html + "<span class='caret'></span></a><ul class='dropdown-menu'>" + subMenuHtml + "</ul>" + "</li>";
                        }
                        else
                        {
                            html = html + "</a></li>";
                        }

                    }

                }
                //IHtmlString htmlStr = new HelperResult(html);
                filterContext.Controller.ViewBag.html = html;
            }
        }
        protected void Application_Start()
        {
            Database.SetInitializer<DsDbContext>(null);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new MyFilter(), 0);
        }
    }
}
