using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TheLearningAgentClient;
using TheLearningAgentClient.Models;

namespace TheLearningAgentClient
{
    partial class user
    {
        public override string ToString()
        {
            return
                "לקוח מספר: " + this.user_id +", "+ GetFullName();
        }

        internal string GetFullName()
        {
            return this.name.Trim() + " " + this.last_name.Trim();
        }
    }


    class DataBaseManager
    {
        static DataClasses1DataContext db = new DataClasses1DataContext();

        internal static List<user_ref_limit> GetUserLimitations(int user_id)
        {
            var q = from u in db.user_ref_limits

                    where (u.user_id == user_id)

                    select u;

            return q.ToList();
        }

        internal static int GetNumberOfReciepts(int user_id)
        {
            var q = from u in db.receipt_ref_users

                    where (u.user_id == user_id)

                    select u;
            return q.Count();
        }


        private static List<int> GetResultsFromPython(int userId, bool LoyalCust)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "Python.exe";

                string ScriptFilePath = AppDomain.CurrentDomain.BaseDirectory;



                if (LoyalCust)
                {
                    ScriptFilePath += @"Scripts\LoyalCust\Shopping_Agent.py";
                }
                else
                {
                    ScriptFilePath += @"Scripts\NewCust\single_user.py";
                }
                
                start.Arguments = string.Format("\"{0}\" {1}", ScriptFilePath, userId);

                Directory.CreateDirectory(@"c:\temp\");

                File.AppendAllText(@"c:\temp\log.txt", "\n");
                File.AppendAllText(@"c:\temp\log.txt", start.Arguments);
                Console.WriteLine(start.Arguments);

                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.CreateNoWindow = true;
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        File.AppendAllText(@"c:\temp\log.txt", "\n");
                        File.AppendAllText(@"c:\temp\log.txt", "before");
                        string result = reader.ReadToEnd();
                        File.AppendAllText(@"c:\temp\log.txt", "\n ans: ");
                        File.AppendAllText(@"c:\temp\log.txt", result);
                        File.AppendAllText(@"c:\temp\log.txt", "\n");
                        File.AppendAllText(@"c:\temp\log.txt", "after");
                        Console.Write(result);
                        List<int> list = GetProductsFromAnswer(result);
                        File.AppendAllText(@"c:\temp\log.txt", "\n");
                        foreach (var item in list)
                        {
                            File.AppendAllText(@"c:\temp\log.txt", ","+item);
                        }
                        return list;

                    }
                }
            }
            catch (Exception e)
            {
                File.AppendAllText(@"c:\temp\log.txt","\n");
                File.AppendAllText(@"c:\temp\log.txt", e.ToString());
                Console.WriteLine("{0} Exception caught.", e);
            }
            

            return new List<int>();
        }

        private static List<int> GetProductsFromAnswer(string result)
        {
            List<int> ans = new List<int>();
            char[] AnsSeparators = { '\n', '\r' };
            var lines = result.Split(AnsSeparators).ToList();
            foreach (var line in lines)
            {
                if(line.StartsWith("Ans:"))
                {
                    char[] ItemSeparators= { ',', ' ', '[', ']' };
                    var productIds = line.Split(ItemSeparators).ToList();
                    productIds.RemoveAll(x => x == "");
                    foreach (var productId in productIds)
                    {
                        try
                        {
                            ans.Add(Int32.Parse(productId));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("{0} Exception caught.", e);
                        }

                    }
                }
            }

            return ans;
        }

        internal static List<product> GetRecomendationsFromGeneralSmartAgent(int userId)
        {
            List<int> ProductIds = GetResultsFromPython(userId,false);

            return db.products.Where(p => ProductIds.Contains(p.product_id)).ToList();
            //return GetAllProductsByCategory(1);
        }

        internal static List<product> GetRecomendationsFromPersonalSmartAgent(int userId)
        {
           List<int> ProductIds = GetResultsFromPython(userId,true);

            return db.products.Where(t => ProductIds.Contains(t.product_id)).ToList();

            //return GetAllProductsByCategory(2);
        }

        internal static user GetValidUser(string user, string pass)
        {
            var q = from u in db.users

                    where (u.user_name.ToLower() == user.ToLower())

                    select u;

            foreach (var User in q)
            {
                if (User.password.TrimEnd() == pass || SecurePasswordHasher.Verify(pass, User.password.TrimEnd()) )
                {
                    return User;
                }
            }

            return null;
        }

        internal static bool DoesUserNameExistsAlready(string userName)
        {
            var q = from u in db.users

                    where ((u.user_name.ToLower() == userName.ToLower()))

                    select u;

            return q.Any();
        }

        internal static List<limit> GetLimitations()
        {
             var q = from u in db.limits

                           select u;
            return q.ToList() ;
        }

        internal static List<product_component> GetComponentsList()
        {
            var u = from p in db.product_components
                    select p;

            return u.ToList();
        }

        internal static void AddUser(user u)
        {
            db.users.InsertOnSubmit(u);
            db.SubmitChanges();
        }

        internal static user UpdateUser(user m_user)
        {
            user u = null;
            try
            {
                u = db.users.Single(c => c.user_id == m_user.user_id);
                u.user_name = m_user.user_name;
                u.address = m_user.address;
                u.last_name = m_user.last_name;
                u.name = m_user.name;
                u.password = m_user.password;
                u.phone = m_user.phone;
                u.user_name = m_user.user_name;
                u.user_ref_limits = m_user.user_ref_limits;

                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }

            return u;
        }

        internal static user GetUser(int user_id)
        {
            var q = from u in db.users

                    where (u.user_id == user_id)

                    select u;
            return q.First();
        }

        internal static void AddUserLimitations(List<user_ref_limit> list)
        {
            db.user_ref_limits.InsertAllOnSubmit(list);
            db.SubmitChanges();
        }

        internal static int SubmitOrder(Order order)
        {
            
                receipt rcpt = new receipt();
                rcpt.sum = (decimal)order.Total;
                rcpt.date = DateTime.Now;
                db.receipts.InsertOnSubmit(rcpt);
                db.SubmitChanges();

                if (rcpt.receipt_id != 0)
                {
                    receipt_ref_user rru = new receipt_ref_user();
                    rru.receipt_id = rcpt.receipt_id;
                    rru.user_id = order.UserId;
                    db.receipt_ref_users.InsertOnSubmit(rru);
                    db.SubmitChanges();

                    foreach (var item in order.Cart)
                    {
                        receipet_ref_product rrp = new receipet_ref_product();
                        rrp.product_id = item.GetProduct().product_id;
                        rrp.quantity = item.m_qty;
                        rrp.receipt_id = rcpt.receipt_id;
                        db.receipet_ref_products.InsertOnSubmit(rrp);
                        db.SubmitChanges();
                    }
                }

            return rcpt.receipt_id;


        }

        internal static List<category_product> GetAllDepartments()
        {
            var q = from c in db.category_products
                    select c;

            return q.ToList();
        }

        internal static List<product> GetAllProductsByCategory(int cat)
        {
            var u = from p in db.products
                    where p.category_id == cat
                    select p;


            return u.ToList();
        }

        internal static List<components_ref_product> GetAllComponentsByProduct(int product)
        {
            var u = from p in db.components_ref_products
                    where p.product_id == product
                    select p;

            return u.ToList();
        }

        internal static List<product> GetAllProductsByCategoryAndComponents(int cat, List<int> componentList)
        {
            if (componentList == null)
                componentList = new List<int>();

            List<product> AllProducts = (from p in db.products
                                         where (p.category_id == cat)
                                         select p).ToList();

            List<product> ToRemove = (from crp in db.components_ref_products
                                      join p in db.products on crp.product_id equals p.product_id
                                      where (p.category_id == cat && componentList.Contains(crp.component_id))
                                      select p).Distinct().ToList();
            foreach (var item in ToRemove)
            {
                AllProducts.Remove(item);
            }

            return AllProducts;
        }

        internal static product GetProductById(int ProductId)
        {
            try
            {
                return db.products.Single(c => c.product_id == ProductId);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            
            return null;
            }
        }

        internal static List<product> GetProductsLike(string text, List<int> componentList)
        {
            if (componentList == null)
                componentList = new List<int>();

            List<product> AllProducts = (from p in db.products
                                      where (SqlMethods.Like(p.product_name, "%" + text + "%"))
                                      select p).ToList();
            List<product> ToRemove = (from crp in db.components_ref_products
                                      join p in db.products on crp.product_id equals p.product_id
                                      where (SqlMethods.Like(p.product_name, "%" + text + "%") && componentList.Contains(crp.component_id))
                                      select p).Distinct().ToList();

            foreach (var item in ToRemove)
            {
                AllProducts.Remove(item);
            }

            return AllProducts;
        }

        internal static void RemoveUserPreference(user_ref_limit url)
        {
            user_ref_limit current = null;
            try
            {
                current = db.user_ref_limits.Single(c => c.limit_id == url.limit_id && c.user_id == url.user_id);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            
            return;
            }

            db.user_ref_limits.DeleteOnSubmit(current);

            db.SubmitChanges();
        }

        internal static void UpdateUserPreference(user_ref_limit url)
        {
            user_ref_limit current=null;
            try
            {
                current = db.user_ref_limits.Single(c => c.limit_id == url.limit_id && c.user_id == url.user_id);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

            if (current == null)
            {
                db.user_ref_limits.InsertOnSubmit(url);
                db.SubmitChanges();
            }
            else
            {
                current.limit_id = url.limit_id;
                current.user_id = url.user_id;
                current.Partial = url.Partial;
                db.SubmitChanges();
            }
        }

    }


    public sealed class SecurePasswordHasher
    {
        private const int SaltSize = 16;

        private const int iterations = 10000;

        private const int HashSize = 20;

        public static string Hash(string password)
        {
            //create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            //create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            //combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            //convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            //format hash with extra information
            return base64Hash;
        }

        public static bool Verify(string password, string hashedPassword)
        {
            //get hashbytes
            var hashBytes = Convert.FromBase64String(hashedPassword);

            //get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            //create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            //get result
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
