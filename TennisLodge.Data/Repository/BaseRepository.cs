using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TennisLodge.Data.Repository.Interfaces;
using static TennisLodge.GCommon.ApplicationConstants;
using static TennisLodge.Data.Common.ExceptionMessages;

namespace TennisLodge.Data.Repository
{
    public abstract class BaseRepository<TEntity, TKey>
        : IRepository<TEntity, TKey>, IAsyncRepository<TEntity, TKey>
        where TEntity : class
    {
        protected readonly TennisLodgeDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(TennisLodgeDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        public TEntity? GetById(TKey id)
        {
            return DbSet
                .Find(id);
        }
        public ValueTask<TEntity?> GetByIdAsync(TKey id)
        {
            return this.DbSet
                .FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            this.DbSet
                 .Add(entity);

            this.DbContext
                .SaveChanges();
        }

        public async Task AddAsync(TEntity entity)
        {
            await this.DbSet
                .AddAsync(entity);

            await this.DbContext
                .SaveChangesAsync();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.DbSet
                .AddRange(entities);

            this.DbContext
                .SaveChanges();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await this.DbSet
                .AddRangeAsync(entities);

            await this.DbContext
                .SaveChangesAsync();
        }

        public TEntity? FirstOrDefailt(Func<TEntity, bool> predicate)
        {
            return this.DbSet
                .FirstOrDefault(predicate);
        }

        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.DbSet
                .ToArray();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            TEntity[] entities = await this.DbSet
                .ToArrayAsync();

            return entities;
        }

        public IQueryable<TEntity> GetAllAttached()
        {
            return this.DbSet
                .AsQueryable();
        }


        public void SaveChanges()
        {
            this.DbContext
                .SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await this.DbContext
                .SaveChangesAsync();
        }

        public TEntity? SinglesOrDefault(Func<TEntity, bool> predicate)
        {
            return this.DbSet
                .SingleOrDefault(predicate);
        }

        public Task<TEntity?> SinglesOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet
                .SingleOrDefaultAsync(predicate);
        }

        public bool Update(TEntity entity)
        {
            try
            {
                this.DbSet.Attach(entity);
                this.DbSet.Entry(entity).State = EntityState.Modified;
                this.DbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                this.DbSet.Attach(entity);
                this.DbSet.Entry(entity).State = EntityState.Modified;
                await this.DbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(TEntity entity)
        {
            this.PerformSoftDeleteOfEntity(entity);

            return this.Update(entity);
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            this.PerformSoftDeleteOfEntity(entity);

            return this.UpdateAsync(entity);
        }


        private PropertyInfo? GetIsDeletedProperty(TEntity entity)
        {
            return typeof(TEntity)
                .GetProperties()
                .FirstOrDefault(pi => pi.PropertyType == typeof(bool) &&
                pi.Name == IsDeletedPropertyName);
        }

        private void PerformSoftDeleteOfEntity(TEntity entity)
        {
            PropertyInfo? isDeletedProperty = GetIsDeletedProperty(entity);
            if (isDeletedProperty == null)
            {
                throw new InvalidOperationException(SoftDeleteOnNonSoftDeletableEntity);
            }
            isDeletedProperty.SetValue(entity, true);
        }

        public int Count()
        {
            return this.DbSet
                .Count();
        }

        public Task<int> CountAsync()
        {
            return this.DbSet
                .CountAsync();
        }
    }
}
