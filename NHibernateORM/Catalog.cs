using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateORM
{
    public class Catalog
    {
        virtual public string author { get; set; }

        virtual public string book { get; set; }

        virtual public int id { get; set; }
    }
}
