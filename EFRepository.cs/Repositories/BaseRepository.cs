using EFRepository.cs.Data;
using MapLocationShared.Entities;
using MapLocationShared.Interfaces;
using MapLocationShared.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFRepository.cs.Repositories
{
    public class BaseRepository<TModel> : ICrudCommands<TModel>
           where TModel : Entity, IEntityMethods<TModel>
    {
        #region Atributos
        protected DbSet<TModel> DbEntity { get; set; }

        protected MapLocationDbContext Context { get; set; }
        #endregion

        #region Contrutor
        public BaseRepository(MapLocationDbContext Context)
        {
            this.Context = Context;
        }
        #endregion

        #region Metodos Crud
        public Task<TModel> Create(TModel model)
        {
            if (model.Valid)
                DbEntity.Add(model);

            return Task.FromResult(model);
        }

        public async Task<TModel> Read(Guid Id)
        {
            if (Id == null)
                return null;

            return await DbEntity.FindAsync(Id);

        }

        public Task<TModel> Read(int IncrementId)
        {
            if (IncrementId == 0 || IncrementId < 0)
                return null;

            return DbEntity.FirstOrDefaultAsync(x => x.IncrementId == IncrementId);
        }

        public async Task<IList<TModel>> Read(IEnumerable<Guid> Ids)
        {
            if (Ids.IsNullOrEmpty())
                return null;

            return await DbEntity.Where(x => Ids.Contains(x.Id))
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<IList<TModel>> Read(IEnumerable<int> IncrementIds)
        {
            if (IncrementIds.IsNullOrEmpty())
                return null;

            return await DbEntity.Where(x => IncrementIds.Contains(x.IncrementId))
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<bool> Delete(TModel model)
        {
            if (model == null)
                return false;

            return await Delete(model.Id);
        }

        public async Task<bool> Delete(Guid Id)
        {
            if (Id == null)
                return false;

            var modelRm = await DbEntity.FindAsync(Id);

            return modelRm == null ? false : modelRm.Inactivate();
            

        }

        public async Task<bool> Delete(int IncrementId)
        {
            if (IncrementId == 0 || IncrementId < 0)
                return false;

            var modelRm = await DbEntity.FirstOrDefaultAsync(x => x.IncrementId == IncrementId);

            return modelRm == null ? false : modelRm.Inactivate();
        }

        public async Task<TModel> Update(TModel model)
        {
            if (model == null)
                return null;

            var modelUp = await DbEntity.FindAsync(model.Id);
            modelUp.Update(model);

            return modelUp;
        }
        #endregion
    }
}
