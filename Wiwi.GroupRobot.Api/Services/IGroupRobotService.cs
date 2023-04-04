using Wiwi.GroupRobot.Api.Models;
using Wiwi.GroupRobot.Common.Model;

namespace Wiwi.GroupRobot.Api.Api.Services
{
    public interface IGroupRobotService
    {
        Task<ApiResult> AddGroupRobot(AddGroupRobotConfigInput input);
        Task<ApiResult> Delete(string id);
        Task<ApiResult<GroupRobotDto>> GetGroupRobotDetail(int companyId, int robotType);
        Task<ApiResult> PushDingGroupMsg(PushGroupMsgInput input);
        Task<ApiResult> PushWorkWxGroupMsg(PushGroupMsgInput input);
        Task<ApiResult> UpdateGroupRobot(UpdateGroupRobotConfigInput input);
    }
}
