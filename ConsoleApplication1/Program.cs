using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Manager.Models;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("INIZIO TEST with autoCommit {0}", DateTime.Now);
            var db = new AspNetMyIdentityTestEntities();
            EasyRepo.Repo<Users> repoUsers = new EasyRepo.Repo<Users>(db);
            repoUsers.OnInsertingEventHandler += repoUsers_OnInsertingEventHandler;
            repoUsers.OnInsertedEventHandler += repoUsers_OnInsertedEventHandler;
            repoUsers.OnUpdatingEventHandler += repoUsers_OnUpdatingEventHandler;
            repoUsers.OnUpdatedEventHandler += repoUsers_OnUpdatedEventHandler;
            repoUsers.OnDeletingEventHandler += repoUsers_OnDeletingEventHandler;
            repoUsers.OnDeletedEventHandler += repoUsers_OnDeletedEventHandler;

            UserFilter f = new UserFilter();
            var itemsFounded = repoUsers.Find((i) =>
            {
                return (string.IsNullOrEmpty(f.Nominativo) ? true : i.Nominativo.Contains(f.Nominativo))
                    && (string.IsNullOrEmpty(f.Id) ? true : i.Id.Equals(f.Id));
            });

            

            foreach (var item in itemsFounded)
            {
                Console.WriteLine("{0}", item);
            }

            Users u = new Users()
            {
                Firstname = "Cristopher",
                Surname = "Fasolin",
                Mail = "c.fasolin@easy-net.biz",
                Id = Guid.NewGuid().ToString(),
                Discriminator = "",
            };

            repoUsers.Insert(u);

            //if (insertUser(repoUsers, u))
            //    if (updateUser(repoUsers, u))

            //deleteUser(repoUsers, u);


            Console.Read();
        }

        private static bool deleteUser(EasyRepo.Repo<Users> repoUsers, Users u)
        {

            try
            {
                repoUsers.Delete(u);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine("Cancellazione fallita: {0}", e);
                return false;

            }
        }

        private static bool updateUser(EasyRepo.Repo<Users> repoUsers, Users u)
        {
            try
            {
                u.Discriminator = "Modified";
                repoUsers.Update(u);
                return true;


            }
            catch (Exception e)
            {
                Console.WriteLine("Modifica fallita: {0}", e);
                return false;

            }
        }

        private static bool insertUser(EasyRepo.Repo<Users> repoUsers, Users u)
        {
            try
            {
                repoUsers.Insert(u);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine("Inserimento fallito: {0}", e);
                return false;
            }
        }





        static void repoUsers_OnDeletedEventHandler(object sender, Users e)
        {
            Console.WriteLine("[{0}] Ho cancellato: {1}", DateTime.Now, e);

        }

        static void repoUsers_OnDeletingEventHandler(object sender, Users e)
        {
            Console.WriteLine("[{0}] Sto per cancellare: {1}", DateTime.Now, e);

        }

        static void repoUsers_OnUpdatedEventHandler(object sender, Users e)
        {
            Console.WriteLine("[{0}] Ho modificato: {1}", DateTime.Now, e);
        }

        static void repoUsers_OnUpdatingEventHandler(object sender, Users e)
        {
            Console.WriteLine("[{0}] Sto per modificare: {1}", DateTime.Now, e);
        }

        static void repoUsers_OnInsertedEventHandler(object sender, Users e)
        {
            Console.WriteLine("[{0}] Ho inserito: {1}", DateTime.Now, e);
        }

        static void repoUsers_OnInsertingEventHandler(object sender, Users e)
        {
            Console.WriteLine("[{0}] Sto per inserire: {1}", DateTime.Now, e);
        }

    }
}
