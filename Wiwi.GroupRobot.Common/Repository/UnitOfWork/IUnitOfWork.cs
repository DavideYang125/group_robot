using SqlSugar;

namespace Wiwi.GroupRobot.Common.Repository
{
    public interface IUnitOfWork
    {
        SqlSugarScope DbClient { get; }

        void BeginTran();

        void CommitTran();
        void RollbackTran();
    }
}
