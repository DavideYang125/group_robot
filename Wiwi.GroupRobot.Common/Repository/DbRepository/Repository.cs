using Wiwi.GroupRobot.Common.Repository;
using SqlSugar;

namespace Wiwi.GroupRobot.Common.Repository
{
    public class Repository<TEntity> : SimpleClient<TEntity>, IRepository<TEntity> where TEntity : class, new()
    {
        public IUnitOfWork UnitOfWork { get; private set; }

        public Repository(IUnitOfWork unitOfWork) : base(unitOfWork.DbClient)
        {
            UnitOfWork = unitOfWork;
        }
    }
}