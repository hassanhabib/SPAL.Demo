using EFxceptions;
using Microsoft.EntityFrameworkCore;
using SPAL.Demo.Models;
using Standard.SPAL.Storage.Abstractions;
using Standard.SPAL.Storage.EntityFramework.Extensions;

namespace Standard.SPAL.Storage.EntityFramework
{
    public partial class EntityFrameworkStorageProvider : EFxceptionsContext, IStorageProvider
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

        public ValueTask<T> InsertAsync<T>(T @object) where T : class =>
            TryCatch(async () =>
            {
                this.Entry(@object).State = EntityState.Added;
                await this.SaveChangesAsync();

                return @object;
            });

        public IQueryable<T> SelectAll<T>() where T : class => TryCatch(this.Set<T>);

        public ValueTask<T> SelectAsync<T>(params object[] @objectIds) where T : class =>
            TryCatch<T>(async () =>
            {
                return await this.FindAsync<T>(objectIds);
            });

        public ValueTask<T> UpdateAsync<T>(T @object) where T : class =>
            TryCatch(async () =>
            {
                this.Entry(@object).State = EntityState.Modified;
                await this.SaveChangesAsync();

                return @object;
            });

        public ValueTask<T> DeleteAsync<T>(T @object) where T : class =>
            TryCatch(async () =>
            {
                this.Entry(@object).State = EntityState.Deleted;
                await this.SaveChangesAsync();

                return @object;
            });
    }
}
