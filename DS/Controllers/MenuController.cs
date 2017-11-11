using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DS.Models;
using DS;
using DS.POCO;
using System.Reflection;
namespace DS.Controllers
{
    public class MenuController : Controller
    {
        private DsDbContext context = new DsDbContext();

        // GET: /Menu/
        public ActionResult Index()
        {
            return View(context.menus.ToList());
        }

        // GET: /Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = context.menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: /Menu/Create
        public ActionResult Create()
        {
            //Get the existing controller an action list.
            //Assembly asm = Assembly.GetExecutingAssembly();
            var CAS = GetList();
            @ViewBag.Controller = new SelectList(CAS, "Controller", "Controller");
            //@ViewBag.Action = new SelectList(AS, "Controller", "Action");
            @ViewBag.ParentId = new SelectList(context.menus, "Id", "Caption");
            return View();
        }

        public List<ControllerAction> GetList()
        {
            Assembly asm = Assembly.GetAssembly(typeof(DS.MvcApplication));
            List<ControllerAction> ControllerActions = new List<ControllerAction>();
            var CA = asm.GetTypes()
                    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();
            int counter = 1;
            foreach (var item in CA)
            {
                ControllerAction aControllerActione = new ControllerAction();
                aControllerActione.Controller = item.Controller.Substring(0, item.Controller.Length - 10);
                aControllerActione.Attributes = item.Attributes;
                aControllerActione.ReturnType = item.ReturnType;
                aControllerActione.Action = item.Action;
                aControllerActione.Id = counter++;
                ControllerActions.Add(aControllerActione);
            }
                List<ControllerAction> CAS = new List<ControllerAction>();
                CAS = ControllerActions.GroupBy(x => x.Controller).Select(grp => grp.First()).ToList();
                return CAS;
        }
        [HttpPost]
        public JsonResult Actions(string id){
            Assembly asm = Assembly.GetAssembly(typeof(DS.MvcApplication));
            List<ControllerAction> ControllerActions = new List<ControllerAction>();
            var CA = asm.GetTypes()
                    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();
            int counter = 1;
            foreach (var item in CA)
            {
                ControllerAction aControllerActione = new ControllerAction();
                aControllerActione.Controller = item.Controller.Substring(0, item.Controller.Length - 10);
                aControllerActione.Attributes = item.Attributes;
                aControllerActione.ReturnType = item.ReturnType;
                aControllerActione.Action = item.Action;
                aControllerActione.Id = counter++;
                ControllerActions.Add(aControllerActione);
            }
            List<string> Actions = new List<string>();
            var actionlist = from p in ControllerActions where(p.Controller==id) select p.Action;
            //var ac = ControllerActions.Where(x =>x.Controller == "HomeController").Select(n=>n.Action);
            foreach (string aAciont in actionlist)
            {
                Actions.Add(aAciont);
            }
            return Json(Actions, JsonRequestBehavior.AllowGet);
            //return Actions;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Caption,ParentId,DisplayPosition,Controller,Action")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                context.menus.Add(menu);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: /Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = context.menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            var CAS = GetList();
            @ViewBag.Controller = new SelectList(CAS, "Controller", "Controller");
            @ViewBag.ParentId = new SelectList(context.menus, "Id", "Caption");
            return View(menu);
        }

        // POST: /Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)//[Bind(Include="Id,Caption,ParentId,DisplayPosition,Controller,Action")]
        {
            if (ModelState.IsValid)
            {
                context.Entry(menu).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: /Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = context.menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: /Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = context.menus.Find(id);
            context.menus.Remove(menu);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
