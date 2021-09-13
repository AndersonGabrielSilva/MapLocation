using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapLocationShared.Interfaces
{
    public interface ICrudCommands<TModel> where TModel : class
    {
        public Task<TModel> Create(TModel model);

        public Task<TModel> Read(Guid Id);
        public Task<TModel> Read(int IncrementId);

        public Task<IList<TModel>> Read(IEnumerable<Guid> Ids);
        public Task<IList<TModel>> Read(IEnumerable<int> IncrementIds);

        public Task<bool> Delete(TModel model);
        public Task<bool> Delete(Guid Id);
        public Task<bool> Delete(int IncrementId);

        public Task<TModel> Update(TModel model);
    }
}
