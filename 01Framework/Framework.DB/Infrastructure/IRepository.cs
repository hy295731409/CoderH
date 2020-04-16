using Framework.DB.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace Framework.DB.Infrastructure
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IRepository<TEntity> where TEntity : DbEntity
    {
        #region 基础操作
        bool Add(TEntity entity, IDbTransaction trans);
        bool Add(TEntity[] entities, IDbTransaction transaction = null);
        bool Delete(TEntity entity, IDbTransaction transaction = null);
        bool Delete(Expression<Func<TEntity, bool>> predicate, IDbTransaction transaction = null);
        bool Update(TEntity entity, IDbTransaction transaction = null);

        /// <summary>
        /// 根据条件批量修改
        /// </summary>
        /// <param name="updateValues">eg:new{A=1,B="abc"}</param>
        /// <param name="predicate"></param>
        /// <param name="currentUserId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool Update(object updateValues, Expression<Func<TEntity, bool>> predicate, dynamic currentUserId = default(dynamic), IDbTransaction transaction = null);
        TEntity Get(dynamic id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll(string whereSql = null, Dictionary<string, object> dicParms = null, string sortBy = default(string), IDbTransaction transaction = null);
        IEnumerable<TEntity> GetList(string sql, object param = null, IDbTransaction transaction = null);
        PagedList<TEntity> GetPageList(Expression<Func<TEntity, bool>> predicate, int pageindex = 1, int pagesize = 20, string orderBy = null);
        IEnumerable<TEntity> In(IEnumerable<dynamic> keys);
        IEnumerable<TEntity> In(string filed, IEnumerable<dynamic> array);
        int Count(Expression<Func<TEntity, bool>> predicate);
        bool Exist(Expression<Func<TEntity, bool>> predicate);
        #endregion




        void Transaction(Action<IDbTransaction> action);
        int SqlExecute(string sql, Parameters parameters = null, IDbTransaction transaction = null);
        TEntity QueryFirstOrDefault<TEntity>(string sql, Parameters parameters = null, IDbTransaction transaction = null);
        IEnumerable<TEntity> SqlQuery<TEntity>(string sql, Parameters parameters = null, IDbTransaction transaction = null);

        #region 分页
        PagedList<T> Page<T>(int pageSize, int pageIndex, string whereSql, string sortBy, Dictionary<string, object> dicParms, IDbTransaction transaction = null) where T : class;
        #endregion
    }
}
