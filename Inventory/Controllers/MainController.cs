using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using Rotativa;
using Rotativa.MVC;

namespace Inventory.Controllers
{
    public class MainController : Controller
    {
        // GET: Main

       
        public ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string email,string password)
        {
            var auth = from login in db.logins.Where(x => x.email == email && x.password == password) select login;
            if (auth.Any())
            {
                Session["email"] = email;
                return RedirectToAction("Index");
            }
            else
            {
                Session["error"] = "Email ou senha,Estao errdas!";
                return RedirectToAction("login");
            }
                
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("login");
        }

        public ActionResult Index()
        {
            //var a = from t in db.Total_Stock_Orders.GroupBy(x => x.date) select t;
            var a = from Total_Stock_Order in db.Total_Stock_Orders.OrderByDescending(x => x.id) select Total_Stock_Order;
            ViewBag.total = (from Total_Stock_Order in db.Total_Stock_Orders select Total_Stock_Order.total_order_amount).Sum();
            return View(a);
        }
        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult AddItem()
        {
            ApplicationDbContext crud = new ApplicationDbContext();
            var data = crud.Items;
            return View(data);
        }
        [HttpPost]
        public ActionResult AddItem(int id, string item_name, float price)
        {
            ApplicationDbContext crud = new ApplicationDbContext();
            int qq = 0;
            TotalItemsQuantity tq = new TotalItemsQuantity();
            tq.item_name = item_name;
            tq.quantity = qq;
            tq.status = "";
            crud.TotalItemsQuantitys.Add(tq);
            crud.SaveChanges();

            Item tbl = new Item();
            tbl.item_id = id;
            tbl.item_name = item_name;
            tbl.price = price;
            crud.Items.Add(tbl);
            int res = crud.SaveChanges();
            if (res > 0)
            {
                var a = from Item in crud.Items.Take(10) select Item;
                return View(a);
            }
            return View();
        }
        //este metedo e para casdaetara todos os produtos
        public ActionResult Index2()
        {
            ApplicationDbContext crud = new ApplicationDbContext();
            var data = crud.Items;
            return View(data);
        }
        [HttpPost]
        public ActionResult Index2(int id,string item_name, float price)
        {
            ApplicationDbContext crud = new ApplicationDbContext();
            Item tbl = new Item();
            tbl.item_id = id;
            tbl.item_name = item_name;
            tbl.price = price;
            crud.Items.Add(tbl);
            int res = crud.SaveChanges();
            if(res>0)
            {
                var a = from Item in crud.Items.Take(10) select Item;
                return View(a);
            }
            return View();
        }

        public ActionResult Edit(int?id)
        {
            ApplicationDbContext crud = new ApplicationDbContext();
            if(id==null)
            {
                return HttpNotFound();
            }
            var check_id = crud.Items.Find(id);
            if(check_id==null)
            {
               return HttpNotFound();
            }
            return View(check_id);
        }

        [HttpPost]
        public ActionResult Edit(int id, string item_name, float price)
        {
            var query = from Item in db.Items.Where(x => x.item_id == id).Take(1) select Item;
            foreach(var q in query)
            {
                q.item_name = item_name;
                q.price = price;

            }
            int y = db.SaveChanges();
            if(y>0)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int?id)
        {
            if(id==null)
            {
                return HttpNotFound();
            }
            var d = db.Items.Find(id);
            if(d==null)
            {
                return HttpNotFound();
            }
            return View(d);
        }
        [HttpGet]
        public ActionResult Delete_Confirmed(int id)
        {
            var delete_id = db.Items.Find(id);
            db.Items.Remove(delete_id);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult AddSupplier()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var data = from Suppliers in db.Supplierss select Suppliers;



            return View(data);
        }

        [HttpPost]
        public ActionResult Insert_Supplier(string sup_name,int mobile ,string email, string address)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Suppliers sp = new Suppliers();
          
            sp.sup_name = sup_name;
            sp.mobile = mobile;
            sp.email = email;
            sp.address = address;

            db.Supplierss.Add(sp);
            int res = db.SaveChanges();

            if(res>0)
            {

                ViewData["status"] = "inserido com sucesso";
                ViewBag.status = "inserido com sucesso";

                //var data = from Suppliers in db.Supplierss select Suppliers;

                //return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                List<Suppliers> supp = db.Supplierss.ToList();
                return View(supp);
            }
            else
            {
                ViewData["fail_status"] = "falha ao inserir";
            }
            return RedirectToAction("AddSupplier");
            

        }

       [HttpGet]
        public ActionResult Delete_Supplier(int?id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if(id==null)
            {
                return HttpNotFound();
            }
            var check = db.Supplierss.Find(id);
            if(check==null)
            {
                return HttpNotFound();
            }
            return View(check);
        }
        [HttpGet]
        public ActionResult Delete_Supplier_Confirmed(int id)
        {
            Suppliers sp = new Suppliers();
            var d = db.Supplierss.Find(id);
            db.Supplierss.Remove(d);
            db.SaveChanges();
            ViewBag.delete_status = "Data Deleted Successfully";
            return RedirectToAction("AddSupplier");
        }
       
       // [HttpPost]
        public ActionResult Item_Stock ()
        {

            // ViewBag["item_table"] = from Item in db.Items select Item;
            // ViewBag["supllier_table"] = from Suppliers in db.Supplierss select Suppliers;

            dynamic model = new ExpandoObject();
            var inv = from Total_Stock_Order in db.Total_Stock_Orders.OrderByDescending(x => x.invoice_no_t).Take(1) select Total_Stock_Order.invoice_no_t;
            model.item_data = db.Items.ToList();
            model.supplier_data = db.Supplierss.ToList();
            model.mystock_data = from MyStock in db.MyStocks.OrderByDescending(x => x.invoice).Take(1) select MyStock;
            model.myst_all_data = db.MyStockNotifications.ToList();
            model.total_s_o = db.Total_Stock_Orders.ToList();
            model.invoice_4m_total = db.Total_Stock_Orders.OrderByDescending(x => x.invoice_no_t).Take(1).ToList();

            return View(model);
            
        }

        public ActionResult PurchaseItems()
        {
            dynamic model = new ExpandoObject();
            var inv = from Total_Stock_Order in db.Total_Stock_Orders.OrderByDescending(x => x.invoice_no_t).Take(1) select Total_Stock_Order.invoice_no_t;
            model.item_data = db.Items.ToList();
            model.supplier_data = db.Supplierss.ToList();
            model.mystock_data = from MyStock in db.MyStocks.OrderByDescending(x => x.invoice).Take(1) select MyStock;
            model.myst_all_data = db.MyStockNotifications.ToList();
            model.total_s_o = db.Total_Stock_Orders.ToList();
            model.invoice_4m_total = db.Total_Stock_Orders.OrderByDescending(x => x.invoice_no_t).Take(1).ToList();
           

            return View(model);
        }

        
        [HttpPost]
        public ActionResult GetPrice(string id)
        {
            List<Item> it =db.Items.Where(x => x.item_name == id).ToList();
            return View(it);
        }

        [HttpPost]
        public ActionResult Load_Previous_Ordered_Item(int invoice_no)
        {
            var a = from MyStock in db.MyStocks.Where(x => x.invoice == invoice_no) select MyStock;
            return View(a);
        }

        [HttpPost]
        public ActionResult Find_Stock_Of_Item(string item_n)
        {
            var a = from TotalItemsQuantity in db.TotalItemsQuantitys.Where(x => x.item_name == item_n) select TotalItemsQuantity;
            return View(a);
        }

        [HttpPost]
        public ActionResult Save_New_Order(int id,int invoice_no,DateTime bill_date,string supplier_name,DateTime receiveing_date,int carrying_charge,string item_name,int quantity,int price,int total_price_per_item,int total_amount)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            ViewBag.preQuantity = from TotalItemsQuantity in db.TotalItemsQuantitys.Where( z=> z.item_name == item_name) select TotalItemsQuantity;
            foreach(var mm in ViewBag.preQuantity)
            {
                ViewBag.c = mm.quantity;
            }
            int pp = Convert.ToInt32(ViewBag.c);
            int total = quantity + pp;
            
            var itemid = db.TotalItemsQuantitys.Where(x => x.item_name == item_name).ToList();
            foreach(var vm in itemid)
            {
                vm.quantity = total;
                db.SaveChanges();
            }

            MyStockNotification md = new MyStockNotification();
            md.id = id;
            md.invoice = invoice_no;
            md.bill_date = bill_date;
            md.supplier_name = supplier_name;
            md.receive_date = receiveing_date;
            md.carrying_charge = carrying_charge;
            md.item_name = item_name;
            md.quantity = quantity;
            md.price = price;
            md.total_price_per_item = total_price_per_item;
            md.total_amount = total_amount;
            db.MyStockNotifications.Add(md);
            //db.SaveChanges();


            MyStock ms = new MyStock();
            ms.id = id;
            ms.invoice = invoice_no;
            ms.bill_date = bill_date;
            ms.supplier_name = supplier_name;
            ms.receive_date = receiveing_date;
            ms.carrying_charge = carrying_charge;
            ms.item_name = item_name;
            ms.quantity = quantity;
            ms.price = price;
            ms.total_price_per_item = total_price_per_item;
            ms.total_amount = total_amount;

            db.MyStocks.Add(ms);
            
            int res = db.SaveChanges();
            if(res>0)
            {
                List<MyStock> myst = db.MyStocks.Where(x => x.invoice == invoice_no).ToList();
                return View(myst);
                //return RedirectToAction("Index");
            }
            else
            {
                ViewBag.notstored = "value not stored";
                return View();
            }

           
        }
        [HttpGet]
        public ActionResult Remove_Order(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            MyStock mt = db.MyStocks.Find(id);
            db.MyStocks.Remove(mt);
            int r = db.SaveChanges();
            if (r > 0)
            {
                return RedirectToAction("PurchaseItems");
            }
            return RedirectToAction("PurchaseItems");
        }

        [HttpPost]
        public ActionResult Total_Order_Price(int id)
        {
            // MyStock mt = from MyStock in db.MyStocks where MyStock.invoice == 1 group MyStock by MyStock.invoice into g select new { Total = g.Sum(x => x.total_price_per_item) };
            // MyStock mt = from MyStock in db.MyStocks.GroupBy(a => a.invoice).Select(a => new { Amount = a.Sum(b => b.invoice), id = a.Key }).ToList() select MyStock;
            //MyStock mm = from MyStock in db.MyStocks.Sum(x=>x.total_price_per_item) select new {total=}
            List<MyStock> mst = db.MyStocks.Where(x => x.invoice == id).ToList();
            return View(mst);
        }

        [HttpPost]
        public ActionResult Carry_Charge(int id)
        {
            // MyStock mt = from MyStock in db.MyStocks where MyStock.invoice == 1 group MyStock by MyStock.invoice into g select new { Total = g.Sum(x => x.total_price_per_item) };
            // MyStock mt = from MyStock in db.MyStocks.GroupBy(a => a.invoice).Select(a => new { Amount = a.Sum(b => b.invoice), id = a.Key }).ToList() select MyStock;
            //MyStock mm = from MyStock in db.MyStocks.Sum(x=>x.total_price_per_item) select new {total=}
            List<MyStock> mst = db.MyStocks.Where(x => x.invoice == id).Take(1).ToList();
            return View(mst);
        }

        [HttpPost]
        public ActionResult Delete_Item(int id, int invoice, int quantity, int item_id_in_sell)
        {
            //MyStock mm = db.MyStocks.Find(id);
            //db.MyStocks.Remove(mm);
            //db.SaveChanges();

           

            ViewBag.preQuantity = from TotalItemsQuantity in db.TotalItemsQuantitys.Where(z => z.id == id) select TotalItemsQuantity;
            foreach (var mm in ViewBag.preQuantity)
            {
                ViewBag.c = mm.quantity;
            }
            int pp = Convert.ToInt32(ViewBag.c);
            int total = pp - quantity;
            var itemid = db.TotalItemsQuantitys.Where(x => x.id == id).ToList();
            foreach (var vm in itemid)
            {
                vm.quantity = total;
                db.SaveChanges();
            }


            MyStock mdd = db.MyStocks.Find(item_id_in_sell);
            db.MyStocks.Remove(mdd);
            //db.SaveChanges();
            //here i am deleting order item if user dont want to confirm that order
            //MyStock md = db.MyStocks.Find(item_id_in_sell);
            //db.MyStocks.Remove(md);
            int res = db.SaveChanges();
            if (res > 0)
            {
                var query = from v in db.MyStocks.Where(x => x.invoice == invoice) select v;
                return View(query);
           }


            return RedirectToAction("Item_Stock");
        }


        [HttpPost]
        public ActionResult Show_After_Delete(int invoice)
        {
            List<MyStock> mm = db.MyStocks.Where(x => x.invoice == invoice).ToList();
            return View(mm);
        }

        [HttpPost]
        public ActionResult Final_Submit_Order (int invoice_no,int total_amount_t)
        {
            //var total = int.Parse(total_amount);
           
            ViewBag.status = "Itens salvos com sucesso";
            Total_Stock_Order ts = new Total_Stock_Order();
            ts.invoice_no_t = invoice_no;
            ts.total_order_amount = total_amount_t;
            ts.date = DateTime.Now.Date;
            db.Total_Stock_Orders.Add(ts);
            int res = db.SaveChanges();
            if(res>0)
            {

                //var del = from MyStockNotification in db.MyStockNotifications.Where(x => x.id > 0) select MyStockNotification;
                //var check = db.MyStockNotifications.Find(del);
                //db.MyStockNotifications.Remove(check);
                //db.SaveChanges();
                return RedirectToAction("InvoiceDetails", "Main", new { @a = invoice_no });

                //dynamic model = new ExpandoObject();
                //model.SingleMystock = db.MyStocks.Where(x => x.invoice == invoice_no).Take(1).ToList();
                //model.items = db.MyStocks.Where(c => c.invoice == invoice_no).ToList();
                //model.total = from Total_Stock_Order in db.Total_Stock_Orders.Where(d => d.invoice_no_t == invoice_no).ToList() select Total_Stock_Order;

                //return View(model);

                // var  t = from Total_Stock_Order in db.Total_Stock_Orders.Where(x => x.invoice_no_t == invoice_no) select Total_Stock_Order;
                //var t = (from MyStock in db.MyStocks
                //         join
                //            Total_Stock_Order in db.Total_Stock_Orders
                //            on MyStock.invoice equals Total_Stock_Order.invoice_no_t
                //            select new
                //         {
                //             invoice = MyStock.invoice,
                //             billdate=MyStock.bill_date,
                //             supplier=MyStock.supplier_name,
                //             receive_date=MyStock.receive_date,
                //             carry_charge=MyStock.carrying_charge,
                //             total_amount=Total_Stock_Order.total_order_amount,
                //             data=Total_Stock_Order.date
                //         }).ToList();


            }
           // var mt = from MyStock in db.MyStocks.Where(x => x.invoice==invoice) select MyStock;
            return View();
        }
        //this is the previous invoice without admin panel
        public ActionResult Invoice(int a)
        {
            dynamic model = new ExpandoObject();
            model.SingleMystock = db.MyStocks.Where(x => x.invoice == a).Take(1).ToList();
            model.items = db.MyStocks.Where(c => c.invoice == a).ToList();
            model.total = from Total_Stock_Order in db.Total_Stock_Orders.Where(d => d.invoice_no_t == a).ToList() select Total_Stock_Order;

            return View(model);
        }

        public ActionResult InvoiceDetails(int a)
        {
            dynamic model = new ExpandoObject();
            model.SingleMystock = db.MyStocks.Where(x => x.invoice == a).Take(1).ToList();
            model.items = db.MyStocks.Where(c => c.invoice == a).ToList();
            model.total = from Total_Stock_Order in db.Total_Stock_Orders.Where(d => d.invoice_no_t == a).ToList() select Total_Stock_Order;

            return View(model);
        }
        public ActionResult GetReceipt(int invoice)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<MyStock> mm = db.MyStocks.Where(x => x.invoice == invoice).ToList();
            return View(mm);
        }
        public ActionResult Invoice_in_pdf(int a)
        {
            dynamic model = new ExpandoObject();
            model.SingleMystock = db.MyStocks.Where(x => x.invoice == a).Take(1).ToList();
            model.items = db.MyStocks.Where(c => c.invoice == a).ToList();
            model.total = from Total_Stock_Order in db.Total_Stock_Orders.Where(d => d.invoice_no_t == a).ToList() select Total_Stock_Order;

            return View(model);
        }
        public ActionResult PrintAll(int a)
        {
            var q = new ActionAsPdf("Invoice_in_pdf",new { @a=a});
            return q;
        }

        public ActionResult NewOrder()
        {
            var a = db.MyStockNotifications.ToList();
            foreach(var b in a)
            {
                db.MyStockNotifications.Remove(b);
            }
            db.SaveChanges();

            return RedirectToAction("PurchaseItems");
        }

        public ActionResult MyStock()
        {
            dynamic model = new ExpandoObject();
            //select distinct(item_name) from mystocks;
            //model.items = (from p in db.MyStocks select p.item_name).Distinct().ToList();

            //this is working perfectly
           // model.items= from c in db.MyStocks  group c by c.item_name into uniqueIds select  uniqueIds.FirstOrDefault();

            model.items = from c in db.TotalItemsQuantitys select c;
           // ViewBag.quantity= (from emp in db.MyStocks.Where(x=>x.item_name== "Jk Wall Putti") select emp.quantity).Sum();
             //model.stock=from emp in db.MyStocks

             //                group emp by emp.item_name into empg

             //                select new

             //                {

             //                    Item_name = empg.Key,

             //                    Quantity = empg.Sum(x => x.quantity)

             //                };
            return View(model);
        }

        public ActionResult Sells()
        {
            dynamic model = new ExpandoObject();
            var inv = from Total_Customer_Order in db.Total_Customer_Orders.OrderByDescending(x => x.invoice_no_tc).Take(1) select Total_Customer_Order.invoice_no_tc;
            model.item_data = db.Items.ToList();
            model.supplier_data = db.Supplierss.ToList();
            model.mystock_data = from MyStock in db.MyStocks.OrderByDescending(x => x.invoice).Take(1) select MyStock;
            model.myst_all_data = db.MyStockNotifications.ToList();
            model.total_s_o = db.Total_Stock_Orders.ToList();
            model.invoice_4m_total = db.Total_Customer_Orders.OrderByDescending(x => x.invoice_no_tc).Take(1).ToList();


            return View(model);
        }

        [HttpPost]
        public ActionResult Find_ID_Of_Item(string item_n)
        {
            var d = from k in db.Items.Where(x => x.item_name == item_n) select k;
            return View(d);
        }

        [HttpPost]
        public ActionResult Save_Customer_Order(int id, int invoice_no, string bill_date, string customer_name, string item_name, int quantity, int price, int total_price_per_item, int total_amount,string current_date)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            //here the quantity will be minus from total stock'
            //here the total_amount parameter is the id of the item;
            ViewBag.preQuantity = from TotalItemsQuantity in db.TotalItemsQuantitys.Where(z => z.id == total_amount) select TotalItemsQuantity;
            foreach (var mm in ViewBag.preQuantity)
            {
                ViewBag.c = mm.quantity;
            }
            int pp = Convert.ToInt32(ViewBag.c);
            int total = pp - quantity;

            var itemid = db.TotalItemsQuantitys.Where(x => x.item_name == item_name).ToList();
            foreach (var vm in itemid)
            {
                vm.quantity = total;
                db.SaveChanges();
            }



            Sell_Item md = new Sell_Item();
            md.id = id;
            md.invoice = invoice_no;
            md.bill_date = bill_date;
            md.customer_name = customer_name;
           
            md.item_name = item_name;
            md.quantity = quantity;
            md.price = price;
            md.total_price_per_item = total_price_per_item;
            md.total_amount = total_amount;
            md.date = current_date;
            db.Sell_Items.Add(md);
           
            int res = db.SaveChanges();
            if (res > 0)
            {
                List<Sell_Item> myst = db.Sell_Items.Where(x => x.invoice == invoice_no).ToList();
                return View(myst);
                //return RedirectToAction("Index");
            }
            else
            {
                ViewBag.notstored = "value not stored";
                return View();
            }


            

        }

        [HttpPost]
        public ActionResult Delete_Customer_Item(int id,int invoice,int quantity,int item_id_in_sell)
        {
            //MyStock mm = db.MyStocks.Find(id);
            //db.MyStocks.Remove(mm);
            //db.SaveChanges();

            ViewBag.preQuantity = from TotalItemsQuantity in db.TotalItemsQuantitys.Where(z => z.id == id) select TotalItemsQuantity;
            foreach (var mm in ViewBag.preQuantity)
            {
                ViewBag.c = mm.quantity;
            }
            int pp = Convert.ToInt32(ViewBag.c);
            int total = pp + quantity;
            var itemid = db.TotalItemsQuantitys.Where(x => x.id == id).ToList();
            foreach (var vm in itemid)
            {
                vm.quantity = total;
                db.SaveChanges();
            }

            //here i am deleting order item if user dont want to confirm that order
            Sell_Item md = db.Sell_Items.Find(item_id_in_sell);
            db.Sell_Items.Remove(md);
           int res= db.SaveChanges();
            if(res>0)
            {
                var query = from v in db.Sell_Items.Where(x => x.invoice == invoice) select v;
                return View(query);
            }

            return RedirectToAction("Sells");
        }

        [HttpPost]
        public ActionResult Show_After_Delete_Customer(int invoice)
        {
            List<Sell_Item> mm = db.Sell_Items.Where(x => x.invoice == invoice).ToList();
            return View(mm);
        }


        [HttpPost]
        public ActionResult Total_Customer_Order_Price(int id)
        {
            // MyStock mt = from MyStock in db.MyStocks where MyStock.invoice == 1 group MyStock by MyStock.invoice into g select new { Total = g.Sum(x => x.total_price_per_item) };
            // MyStock mt = from MyStock in db.MyStocks.GroupBy(a => a.invoice).Select(a => new { Amount = a.Sum(b => b.invoice), id = a.Key }).ToList() select MyStock;
            //MyStock mm = from MyStock in db.MyStocks.Sum(x=>x.total_price_per_item) select new {total=}
            List<Sell_Item> mst = db.Sell_Items.Where(x => x.invoice == id).ToList();
            return View(mst);
        }

        [HttpPost]
        public ActionResult Final_Customer_Order(int invoice_no, int total_amount_t,string current_date)
        {
            //var total = int.Parse(total_amount);

            ViewBag.status = "Itens salvos com sucesso";
            Total_Customer_Order ts = new Total_Customer_Order();
            ts.invoice_no_tc = invoice_no;
            ts.total_order_amount = total_amount_t;
            ts.date = current_date;
            db.Total_Customer_Orders.Add(ts);
            int res = db.SaveChanges();
            if (res > 0)
            {

               
                return RedirectToAction("InvoiceDetailsCustomer", "Main", new { @a = invoice_no });

                
            }
            // var mt = from MyStock in db.MyStocks.Where(x => x.invoice==invoice) select MyStock;
            return View();
        }

        public ActionResult InvoiceDetailsCustomer(int a)
        {
            dynamic model = new ExpandoObject();
            model.SingleMystock = db.Sell_Items.Where(x => x.invoice == a).Take(1).ToList();
            model.items = db.Sell_Items.Where(c => c.invoice == a).ToList();
            model.total = from Total_Customer_Order in db.Total_Customer_Orders.Where(d => d.invoice_no_tc == a).ToList() select Total_Customer_Order;

            return View(model);
        }

        public ActionResult Invoice_in_pdf_Customer(int a)
        {
            dynamic model = new ExpandoObject();
            model.SingleMystock = db.Sell_Items.Where(x => x.invoice == a).Take(1).ToList();
            model.items = db.Sell_Items.Where(c => c.invoice == a).ToList();
            model.total = from Total_Customer_Order in db.Total_Customer_Orders.Where(d => d.invoice_no_tc == a).ToList() select Total_Customer_Order;

            return View(model);
        }
        public ActionResult PrintAllCustomerDetails(int a)
        {
            var q = new ActionAsPdf("Invoice_in_pdf_Customer", new { @a = a });
            return q;
        }
    }

}