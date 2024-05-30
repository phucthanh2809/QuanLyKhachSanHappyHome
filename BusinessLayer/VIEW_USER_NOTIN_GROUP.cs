using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class VIEW_USER_NOTIN_GROUP
    {
        Entities db;
        public VIEW_USER_NOTIN_GROUP()
        {
            db = Entities.CreateEntities();
        }
        public List<V_USER_NOTIN_GROUP1>getUserNotInGroup(string madvi, string macty)
        {
            return db.V_USER_NOTIN_GROUP1.Where(x => x.MADVI == madvi && x.MACTY == macty && x.ISGROUP == false && x.DISABLED == false).ToList();
        }
    } 
}
