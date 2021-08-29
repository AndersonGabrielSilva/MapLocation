using System;
using System.Collections.Generic;
using System.Text;

namespace MapLocationShared.Interfaces
{
    public interface  IEntityMethods<TModel> where TModel : class
    {
        public TModel Update(TModel model);        
    }
}
