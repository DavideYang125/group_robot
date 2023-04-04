using SqlSugar;

namespace Wiwi.GroupRobot.Common.Repository
{
    public interface IRepository<TEntity> : ISimpleClient<TEntity> where TEntity : class, new()
    {
        public IUnitOfWork UnitOfWork { get; }
    }
}
