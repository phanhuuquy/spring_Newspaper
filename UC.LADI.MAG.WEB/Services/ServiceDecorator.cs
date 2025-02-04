using UC.Core.Interfaces;
using System;
using System.Reflection;
using UC.LADI.MAG.WEB.Services;

namespace UC.LADI.MAG.WEB.Services
{
    class ServiceDecorator<TKeyId, TEntity>
    {
        private IRepositoryBase<TKeyId, TEntity> _Repository;
		public ServiceDecorator(IServiceWrapper repository)
		{
            #region config repository
            if (typeof(TEntity) == typeof(Entities.mag_collection))
            {
                _Repository = (IRepositoryBase<TKeyId, TEntity>)repository.mag_collection;
            }
            else if (typeof(TEntity) == typeof(Entities.mag_event))
            {
                _Repository = (IRepositoryBase<TKeyId, TEntity>)repository.mag_event;
            }
            if (typeof(TEntity) == typeof(Entities.mag_image))
            {
                _Repository = (IRepositoryBase<TKeyId, TEntity>)repository.mag_image;
            }
            if (typeof(TEntity) == typeof(Entities.mag_item))
            {
                _Repository = (IRepositoryBase<TKeyId, TEntity>)repository.mag_item;
            }
            #endregion
            if (_Repository == null)
			{
				throw new Exception("Class ServiceDecorator not configured yet");
			}
		}

		#region base service
		public async Task<List<TEntity>> GetEntitiesAsync(string? columnsQuery,string? whereQuery, string? orderQuery)
        {
            return await _Repository.GetEntitiesAsync(columnsQuery, whereQuery, orderQuery, null);
        }
        public async Task<TEntity> GetEntityByIdAsync(TKeyId id)
        {
            return await _Repository.GetEntityByIdAsync(id, null);
        }
        public async Task<TEntity> InsertEntityAsync(TEntity entity)
        {
            return await _Repository.InsertEntityAsync(entity, null);
        }
        public async Task<TEntity> UpdateEntityAsync(TEntity entity)
        {
            return await _Repository.UpdateEntityAsync(entity, null);
        }
        public async Task DeleteEntityAsync(TKeyId id)
        {
            await _Repository.DeleteEntityAsync(id, null);
        }
        #endregion
    }
}
