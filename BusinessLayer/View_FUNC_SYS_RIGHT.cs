using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
namespace BusinessLayer
{
    public class View_FUNC_SYS_RIGHT
    {
        Entities db;
    public View_FUNC_SYS_RIGHT()
        {
            db = Entities.CreateEntities(); 
        }
        public List<V_FUNC_SYS_RIGHT> getFuncByUser(int idUser)
        {
            return db.V_FUNC_SYS_RIGHT.Where(x => x.IDUSER == idUser).OrderBy(x => x.SORT).ToList();
        }
    }
    
}
