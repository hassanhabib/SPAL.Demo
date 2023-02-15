using EFxceptions;
using Microsoft.EntityFrameworkCore;
using SPAL.Demo.Models;
using Standard.SPAL.Storage.Abstractions;
using Standard.SPAL.Storage.EntityFramework.Extensions;

namespace Standard.SPAL.Storage.EntityFramework
{
    public class EntityFrameworkStorageProvider : EFxceptionsContext, IStorageProvider
    {
        public EntityFrameworkStorageProvider(DbContextOptions<EntityFrameworkStorageProvider> options)
            : base(options)
        {
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entitiesAssembly = typeof(BaseModel).Assembly;
            modelBuilder.RegisterAllEntities<BaseModel>(entitiesAssembly);
        }

        public override void Dispose() { }

        public async ValueTask<T> InsertAsync<T>(T @object) where T : class
        {
            this.Entry(@object).State = EntityState.Added;
            await this.SaveChangesAsync();

            return @object;
        }

        public IQueryable<T> SelectAll<T>() where T : class => this.Set<T>();

        public async ValueTask<T> SelectAsync<T>(params object[] @objectIds) where T : class =>
            await this.FindAsync<T>(objectIds);

        public async ValueTask<T> UpdateAsync<T>(T @object) where T : class
        {
            this.Entry(@object).State = EntityState.Modified;
            await this.SaveChangesAsync();

            return @object;
        }

        public async ValueTask<T> DeleteAsync<T>(T @object) where T : class
        {
            this.Entry(@object).State = EntityState.Deleted;
            await this.SaveChangesAsync();

            return @object;
        }
    }
}
