using Dapper;
//using Dapper.Contrib.Extensions;
using Dapper.Contrib.Extensions.TZ;
using Framework.DB.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Framework.DB.Infrastructure
{
    /// <summary>
    /// 仓储接口的实现
    /// </summary>
    /// <typeparam name="TEntity">TEntity定义的泛型必须继承数据库实体类基类</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : DbEntity
    {
        private readonly object locker = new object();
       
        private IDapperContext _dapperContext;
        protected IDapperContext DbContext
        {
            get
            {
                if (_dapperContext == null)
                {
                    lock (locker)
                    {
                        if(_dapperContext == null)
                            _dapperContext = DbContextFactory.GetDbContext();
                    }
                }
                    
                return _dapperContext;
            }
        }

        private IDbConnection _dbConnection;
        protected IDbConnection DbConnection
        {
            get
            {
                if (_dbConnection == null)
                {
                    _dbConnection = DbContext.Connection;
                }
                    
                if (_dbConnection.State != ConnectionState.Open && _dbConnection.State != ConnectionState.Connecting)
                    _dbConnection.Open();
                return _dbConnection;
            }
        }
        protected ILogger _logger;
        public Repository(ILogger logger)
        {
            _logger = logger;
        }

        #region 基础操作
        public bool Add(TEntity entity, IDbTransaction trans = null)
        {
            if (entity == null)
                return false;
            try
            {
                var res = DbConnection.Insert(entity, trans);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ConnectionClose();
            }
        }
        public bool Add(TEntity[] entities, IDbTransaction transaction = null)
        {
            if (entities == null || entities.Length < 1)
                return false;

            try
            {
                var listGroup = new List<List<TEntity>>();
                var j = 2000;//每 2k 条执行一次
                for (var i = 0; i < entities.Length; i += 2000)
                {
                    var cList = entities.Take(j).Skip(i).ToList();
                    //j += 2000;
                    listGroup.Add(cList);
                }

                foreach (var groupList in listGroup)
                {
                    DbConnection.Insert(groupList.ToArray(), transaction);
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ConnectionClose();
            }
        }

        public bool Delete(TEntity entity, IDbTransaction transaction = null)
        {
            try
            {
                if (entity == null) return false;
                return DbConnection.Delete(entity, transaction);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ConnectionClose();
            }
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null)
        {
            try
            {
                if (predicate == null)
                    return false;
                return DbConnection.DeleteAll<TEntity>(transaction);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ConnectionClose();
            }
        }

        public bool Update(TEntity entity, IDbTransaction transaction = null)
        {
            try
            {
                if (entity == null)
                    return false;

                // 设置公共字段
                // entity.SetUpdateAudit(entity.UpdateUserId > 0 ? entity.UpdateUserId : CurrentUserId);

                return DbConnection.Update(entity, transaction);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ConnectionClose();
            }
        }

        public bool Update(object updateValues, Expression<Func<TEntity, bool>> predicate, dynamic currentUserId = null, IDbTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(dynamic id)
        {
            try
            {
                return DbConnection.Get<TEntity>(id as object);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ConnectionClose();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll(string whereSql, Dictionary<string, object> dicParms = default, string sortBy = default(string), IDbTransaction transaction = null)
        {
            try
            {
                return DbConnection.GetAll<TEntity>(whereSql, dicParms, sortBy, transaction);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ConnectionClose();
            }
        }

        public IEnumerable<TEntity> GetList(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {
                var res = DbConnection.QueryMultiple(sql, param, transaction);
                return res.Read<TEntity>();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ConnectionClose();
            }
        }

        public PagedList<TEntity> GetPageList(Expression<Func<TEntity, bool>> predicate, int pageindex = 1, int pagesize = 20, string orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> In(IEnumerable<dynamic> keys)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> In(string filed, IEnumerable<dynamic> array)
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region 通用方法
        public void Transaction(Action<IDbTransaction> action)
        {
            DbContext.InTransaction = true;
            var conn = DbConnection;
            if (conn.State != ConnectionState.Open && conn.State != ConnectionState.Connecting)
                conn.Open();
            var trans = conn.BeginTransaction();
            try
            {
                action(trans);
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw e;
            }
            finally
            {
                trans.Dispose();
                ConnectionClose();
                DbContext.InTransaction = false;
            }
        }

        public int SqlExecute(string sql, Parameters parameters = null, IDbTransaction transaction = null)
        {
            try
            {
                if (DbConnection.State != ConnectionState.Open && DbConnection.State != ConnectionState.Connecting)
                {
                    DbConnection.Open();
                }
                return DbConnection.Execute(sql, parameters, transaction);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                ConnectionClose();
            }
        }

        public T QueryFirstOrDefault<T>(string sql, Parameters parameters = null, IDbTransaction transaction = null)
        {
            try
            {
                return DbConnection.QueryFirstOrDefault<T>(sql, parameters, transaction);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ConnectionClose();
            }
        }

        public IEnumerable<T> SqlQuery<T>(string sql, Parameters parameters = null, IDbTransaction transaction = null)
        {
            try
            {
                return DbConnection.Query<T>(sql, parameters, transaction);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ConnectionClose();
            }
        }

        public PagedList<T> Page<T>(int pageSize, int pageIndex, string whereSql, string sortBy, Dictionary<string, object> dicParms, IDbTransaction transaction = null) where T : class
        {
            try
            {
                var res = DbConnection.Pager<T>(pageSize, pageIndex, whereSql, sortBy, dicParms, transaction);
                return new PagedList<T>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalCount = res.recordCount,
                    Items = res.list
                };
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ConnectionClose();
            }
        }

        #endregion

        #region private
        private void ConnectionClose()
        {
            if (_dapperContext != null && _dapperContext.InTransaction)
            {
                return;
            }

            if (DbConnection != null && DbConnection.State == ConnectionState.Open)
            {
                DbConnection.Close();
            }
        }


        #endregion
    }
}
