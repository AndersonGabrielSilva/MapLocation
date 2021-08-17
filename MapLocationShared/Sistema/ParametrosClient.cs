using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapLocation.Shared.Sistema
{
    public class ParametrosClient
    {
        public string Name { get; set; }
        public string UserName { get; set; }

        public void SetInfos(ParametrosClient parametros)
        {
            if (UserName == parametros.UserName)
                return;

            Name = parametros.Name;
            UserName = parametros.UserName;
        }

        public void ClearInfos()
        {
            Name = UserName = string.Empty;
        }
    }
}
