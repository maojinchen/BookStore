using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using BookStore.Models;
namespace BookStore.Controllers
{
    public class MyadminController : Controller
    {
        // GET: Myadmin 后台管理
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 用户管理模块
        /// </summary>
        /// <returns></returns>
        public ActionResult Users(int pageIndex = 1, int pageSize = 5, string search = "")
        {
            using (BookShopDB db = new BookShopDB())
            {
                var list1 = db.Users.Where(a => a.UserRoleId < 3)
                    .Where(a => a.UserStateId == 1).Where(a => a.LoginId.Contains(search) ||        //对昵称模糊查询
                      a.Phone.Contains(search) ||      //手机号码模糊查询
                      a.Address.Contains(search) || a.Mail.Contains(search)).Include("UserRoles").ToList();
                var totalRows = list1.Count();   //获取总记录数
                var totalPages = Math.Ceiling(totalRows * 1.00 / pageSize);     //计算总页数
                var roles = db.Users
                    .Where(a => a.UserRoleId < 3).Where(a => a.UserStateId == 1).Where(a => a.LoginId.Contains(search) ||        //对昵称模糊查询
                    a.Phone.Contains(search) ||      //手机号码模糊查询
                    a.Address.Contains(search) || a.Mail.Contains(search))        //地址和邮箱模糊查询
                    .OrderBy(p => p.Id).Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Include("UserRoles")
                    .ToList();
                ViewBag.search = search;
                ViewBag.pageIndex = pageIndex;
                ViewBag.totalPages = totalPages;
                ViewBag.pageSize = pageSize;
                return View(roles);
            }
        }
        /// <summary>
        /// 查找指定用户ID进行修改
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        public ActionResult UserEdit(int? id)
        {
            try
            {
                using (BookShopDB db = new BookShopDB())
                {
                    var users = db.Users.Find(id);
                    return View(users);
                }
            }
            catch (Exception)
            {
                return View("/Myadmin/Users");
            }
        }
        /// <summary>
        /// 用户POST提交修改
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserEdit(Users users)
        {
            users.Uptime = DateTime.Now;
            using (BookShopDB db = new BookShopDB())
            {
                db.Entry(users).State = EntityState.Modified;
                try
                {
                    int c = db.SaveChanges();
                    if (c > 0)
                    {
                        ViewBag.Msgs = "修改成功";
                    }
                    else
                    {
                        ViewBag.Msgs = "修改失败";
                    }
                }
                catch (Exception)
                {
                    ViewBag.Msgs = "数据库在执行修改详细信息操作时，出现异常。";
                }
                return View(users);
            }
        }
        /// <summary>
        /// 用户注销删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Users(int id)
        {
            int count = 0;
            using (BookShopDB db = new BookShopDB())
            {
                Users list = db.Users.Find(id);
                list.UserStateId = 2;
                list.Uptime = DateTime.Now;
                db.Entry(list).State = EntityState.Modified;
                try
                {
                    count = db.SaveChanges();
                }
                catch (Exception)
                {
                    count = 2;
                }
            }
            return Content(count.ToString());
        }
        /// <summary>
        /// 员工管理模块
        /// </summary>
        /// <returns></returns>
        public ActionResult Employees(int pageIndex = 1, int pageSize = 5, string search = "")
        {
            using (BookShopDB db = new BookShopDB())
            {
                var list1 = db.Users.Where(a => a.UserRoleId == 3)
                    .Where(a => a.UserStateId == 1).Where(a => a.LoginId.Contains(search) ||        //对昵称模糊查询
                      a.Phone.Contains(search) ||      //手机号码模糊查询
                      a.Address.Contains(search) || a.Mail.Contains(search)).Include("UserRoles").ToList();
                var totalRows = list1.Count();   //获取总记录数
                var totalPages = Math.Ceiling(totalRows * 1.00 / pageSize);     //计算总页数
                var roles = db.Users
                    .Where(a => a.UserRoleId == 3).Where(a => a.UserStateId == 1).Where(a => a.LoginId.Contains(search) ||        //对昵称模糊查询
                    a.Phone.Contains(search) ||      //手机号码模糊查询
                    a.Address.Contains(search) || a.Mail.Contains(search))        //地址和邮箱模糊查询
                    .OrderBy(p => p.Id).Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Include("UserRoles")
                    .ToList();
                ViewBag.pageIndex = pageIndex;
                ViewBag.totalPages = totalPages;
                ViewBag.pageSize = pageSize;
                ViewBag.search = search;
                return View(roles);
            }
        }
        /// <summary>
        /// 根据员工ID修改员工信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public ActionResult EmployeesEdit(int? id)
        {
            using (BookShopDB db = new BookShopDB())
            {
                var users = db.Users.Find(id);
                return View(users);
            }
        }
        /// <summary>
        /// POST提交修改员工
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EmployeesEdit(Users users)
        {
            users.Uptime = DateTime.Now;
            using (BookShopDB db = new BookShopDB())
            {
                db.Entry(users).State = EntityState.Modified;
                try
                {
                    int c = db.SaveChanges();
                    if (c > 0)
                    {
                        ViewBag.Msgs = "修改成功";
                    }
                    else
                    {
                        ViewBag.Msgs = "修改失败";
                    }
                }
                catch (Exception)
                {
                    ViewBag.Msgs = "数据库在执行修改详细信息操作时，出现异常。";
                }
                return View(users);
            }

        }
        /// <summary>
        /// 员工添加
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeesAdd(string id = "", string LoginId = "")
        {
            if (id == "")
            {
                return View();
            }
            else if (id == "LoginId")
            {
                int count = 0;
                using (BookShopDB db = new BookShopDB())
                {
                    var list = db.Users.Where(a => a.LoginId == LoginId).ToList();
                    count = list.Count();

                }
                return Content($"{count}");
            }
            else
            {
                int count = 0;
                using (BookShopDB db = new BookShopDB())
                {
                    var list = db.Users.Where(a => a.Mail == LoginId).ToList();
                    count = list.Count();

                }
                return Content($"{count}");
            }
        }
        /// <summary>
        /// 保存添加员工基本信息，对密码值进行加密
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EmployeesAdd(Users users)
        {
            int count = 0;
            //public string Md5Decrypt(string Source)
            //{
            //    //将解密字符串转换成字节数组   
            //    byte[] bytIn = System.Convert.FromBase64String(Source);
            //    //给出解密的密钥和偏移量，密钥和偏移量必须与加密时的密钥和偏移量相同   
            //    byte[] iv = { 102, 16, 93, 156, 78, 4, 218, 32 };//定义偏移量   
            //    byte[] key = { 55, 103, 246, 79, 36, 99, 167, 3 };//定义密钥   
            //    DESCryptoServiceProvider mobjCryptoService = new DESCryptoServiceProvider();
            //    mobjCryptoService.Key = iv;
            //    mobjCryptoService.IV = key;
            //    //实例流进行解密   
            //    System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);
            //    ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            //    CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            //    StreamReader strd = new StreamReader(cs, Encoding.Default);
            //    return strd.ReadToEnd();
            //}
            try
            {
                users.RegisterTime = DateTime.Now;
                users.Uptime = DateTime.Now;
                string strSource = users.LoginPwd;
                byte[] bytIn = System.Text.Encoding.Default.GetBytes(strSource);
                //建立加密对象的密钥和偏移量           
                byte[] iv = { 102, 16, 93, 156, 78, 4, 218, 32 };//定义偏移量   
                byte[] key = { 55, 103, 246, 79, 36, 99, 167, 3 };//定义密钥   
                                                                  //实例DES加密类   
                DESCryptoServiceProvider mobjCryptoService = new DESCryptoServiceProvider
                {
                    Key = iv,
                    IV = key
                };
                ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
                //实例MemoryStream流加密密文件   
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();
                users.LoginPwd = Convert.ToBase64String(ms.ToArray());
                users.UserRoleId = 3;
                users.UserStateId = 1;
                Random rad = new Random();//实例化随机数产生器rad
                int value = rad.Next(1000, 10000);//用rad生成大于等于1000，小于等于9999的随机数
                users.hs_pwd = value.ToString();//存储到数据库中
                using (BookShopDB db = new BookShopDB())
                {
                    db.Users.Add(users);
                    count = db.SaveChanges();
                    if (count > 0)
                    {
                        ViewBag.txet = "添加成功！";
                        return View();
                    }
                    else
                    {
                        ViewBag.txet = "添加失败！";
                        return View();

                    }
                }
            }
            catch (Exception)
            {
                ViewBag.txet = "程序报错，添加失败！";
                return View();
            }
        }
        /// <summary>
        /// 书籍管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Books(int pageIndex = 1, int pageSize = 5, string search = "")
        {
            using (BookShopDB db = new BookShopDB())
            {
                ViewBag.category = db.Categories.ToList();
                var list1 = db.Books.Where(a => a.Title.Contains(search)) //对书籍模糊查询
                    .Where(p=>p.Author.Contains(search))    //对书籍作者模糊查询和ISBN模糊查询
                    .Include("Publishers").ToList();
                var totalRows = list1.Count();   //获取总记录数
                var totalPages = Math.Ceiling(totalRows * 1.00 / pageSize);     //计算总页数
                var roles = db.Books.Where(a => a.Title.Contains(search)) //对书籍模糊查询
                    .Where(p => p.Author.Contains(search))    //对书籍作者模糊查询和ISBN模糊查询
                    .OrderBy(p => p.Id).Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).Include("Publishers").ToList();
                ViewBag.pageIndex = pageIndex;
                ViewBag.totalPages = totalPages;
                ViewBag.pageSize = pageSize;
                ViewBag.search = search;
                return View(roles);
            }
        }
        //注释阿达啊实打实大师的
    }
}