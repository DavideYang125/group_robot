using KaiKuo.Message.Api.Models.Result;
using Wiwi.GroupRobot.Api.Api.Services;
using Wiwi.GroupRobot.Api.Domain;
using Wiwi.GroupRobot.Api.Enums;
using Wiwi.GroupRobot.Api.Models;
using Wiwi.GroupRobot.Common.Exceptions;
using Wiwi.GroupRobot.Common.Extensions;
using Wiwi.GroupRobot.Common.Model;
using Wiwi.GroupRobot.Common.Repository;

namespace Wiwi.GroupRobot.Api.Services
{
    public class GroupRobotService : IGroupRobotService
    {
        private readonly HttpClient _client;
        private readonly ILogger<GroupRobotService> _logger;
        private readonly IRepository<GroupRobotModel> _repository;
        public GroupRobotService(IHttpClientFactory httpClientFactory, ILogger<GroupRobotService> log, IRepository<GroupRobotModel> repository)
        {
            _client = httpClientFactory.CreateClient();
            _logger = log;
            _repository = repository;
        }

        public async Task<ApiResult> Delete(string id)
        {
            var robot = await _repository.GetFirstAsync(x => x.GroupRobotId == id);
            robot.IsDeleted = true;

            var result = await _repository.UpdateAsync(robot);
            if (result)
            {
                return ApiResult.Success();
            }
            else
                return ApiResult.Fail("删除失败");
        }

        public async Task<ApiResult> AddGroupRobot(AddGroupRobotConfigInput input)
        {
            if (input.CompanyId <= 0) throw new CustomException("租户id不正确");
            if (string.IsNullOrEmpty(input.WebHook))
            {
                throw new CustomException("web hook不能为空");
            }
            if (input.GroupRobotType != (int)GroupRobotTypeEnum.Work && input.GroupRobotType != (int)GroupRobotTypeEnum.Ding) throw new CustomException("群类型配置不正确");
            var exist = await _repository.IsAnyAsync(x => x.CompanyId == input.CompanyId && !x.IsDeleted && x.GroupRobotType == (GroupRobotTypeEnum)input.GroupRobotType);
            if (exist)
            {
                throw new CustomException("当前租户已存在配置，请勿重复添加");
            }
            var model = new GroupRobotModel(Guid.NewGuid().ToString(), input.WebHook, (GroupRobotTypeEnum)input.GroupRobotType, input.CompanyId, DateTime.Now);
            var result = await _repository.InsertAsync(model);
            if (result)
            {
                return ApiResult.Success();
            }
            else
                return ApiResult.Fail("添加失败");
        }

        public async Task<ApiResult> UpdateGroupRobot(UpdateGroupRobotConfigInput input)
        {
            var robot = await _repository.GetFirstAsync(x => x.GroupRobotId == input.GroupRobotId);

            if (string.IsNullOrEmpty(input.WebHook))
            {
                throw new CustomException("web hook不能为空");
            }
            robot.WebHook = input.WebHook;
            var result = await _repository.UpdateAsync(robot);
            if (result)
            {
                return ApiResult.Success(); ;
            }
            else
                return ApiResult.Fail("修改失败");
        }

        public async Task<ApiResult<GroupRobotDto>> GetGroupRobotDetail(int companyId, int robotType)
        {
            var robot = await _repository.GetFirstAsync(x => x.CompanyId == companyId && !x.IsDeleted && x.GroupRobotType == (GroupRobotTypeEnum)robotType);
            if (robot is null)
            {
                throw new CustomException("当前租户未配置企业微信群机器人");
            }
            var result = new GroupRobotDto()
            {
                CompanyId = robot.CompanyId,
                CreateTime = robot.CreateTime,
                GroupRobotId = robot.GroupRobotId,
                GroupRobotType = robot.GroupRobotType,
                WebHook = robot.WebHook,
            };
            return ApiResult<GroupRobotDto>.Success(result);
        }

        public async Task<ApiResult> PushWorkWxGroupMsg(PushGroupMsgInput input)
        {
            if (input.CompanyId <= 0) throw new CustomException("租户id不正确");
            var robot = await _repository.GetFirstAsync(x => x.CompanyId == input.CompanyId && !x.IsDeleted && x.GroupRobotType == GroupRobotTypeEnum.Work);
            if (robot is null)
            {
                throw new CustomException("请先配置企业微信群机器人");
            }
            var webhook = robot.WebHook;
            var payload = new PushWorkWechatGroupVm() { text = new PushWorkWechatGroupVm.TextInfo() { content = input.Message } };
            var result = await _client.PostAsync<PushWorkWechatGroupVm, BaseWeChatResult>(webhook, payload);
            if (result != null && result.ErrorCode == 0)
            {
                return ApiResult.Success();
            }
            else
            {
                _logger.LogError($"企业微信群机器人消息发送失败，错误码：{result?.ErrorCode}；errmsg：{result?.ErrorMessage}");
                return ApiResult.Fail("企业微信群机器人消息发送失败");
            }
        }

        public async Task<ApiResult> PushDingGroupMsg(PushGroupMsgInput input)
        {
            if (input.CompanyId <= 0) throw new CustomException("租户id不正确");
            var robot = await _repository.GetFirstAsync(x => x.CompanyId == input.CompanyId && !x.IsDeleted && x.GroupRobotType == GroupRobotTypeEnum.Ding);
            if (robot is null)
            {
                throw new CustomException("请先配置钉钉群机器人");
            }
            var webhook = robot.WebHook;
            var payload = new PushDingGroupVm() { text = new PushDingGroupVm.TextInfo() { content = input.Message } };

            var result = await _client.PostAsync<PushDingGroupVm, BaseWeChatResult>(webhook, payload);
            if (result != null && result.ErrorCode == 0)
            {
                return ApiResult.Success();
            }
            else
            {
                _logger.LogError($"钉钉群机器人消息发送失败，错误码：{result?.ErrorCode}；errmsg：{result?.ErrorMessage}");
                return ApiResult.Fail("钉钉群机器人消息发送失败");
            }
        }
    }
}
