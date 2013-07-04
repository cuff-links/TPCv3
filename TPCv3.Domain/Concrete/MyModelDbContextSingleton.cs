using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPCv3.Domain.Concrete{
    public class MyModelDbContextSingleton{
        private static readonly EfDbContext instance = new EfDbContext();

        static MyModelDbContextSingleton(){
        }

        private MyModelDbContextSingleton(){
        }

        public static EfDbContext Instance{
            get { return instance; }
        }
    }
}