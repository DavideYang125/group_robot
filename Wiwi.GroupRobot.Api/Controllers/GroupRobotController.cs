using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiwi.GroupRobot.Api.Api.Services;
using Wiwi.GroupRobot.Api.Models;
using Wiwi.GroupRobot.Common.Model;

namespace KaiKuo.Message.Api.Controllers
{
    /// <summary>
    /// 群机器人消息
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GroupRobotController : ControllerBase
    {
        private readonly ILogger<GroupRobotController> _logger;
        private readonly IGroupRobotService _service;

        public GroupRobotController(ILogger<GroupRobotController> logger, IGroupRobotService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// 发送企业微信群机器人消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResult>> PushWorkWxGroupMsg(PushGroupMsgInput input)
        {
            return await _service.PushWorkWxGroupMsg(input);
        }

        /// <summary>
        /// 发送钉钉群机器人消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResult>> PushDingGroupMsg(PushGroupMsgInput input)
        {
            return await _service.PushDingGroupMsg(input);
        }

        /// <summary>
        /// 添加群机器人配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResult>> AddGroupRobot(AddGroupRobotConfigInput input)
        {
            return await _service.AddGroupRobot(input);
        }

        /// <summary>
        /// 修改群机器人配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResult>> UpdateGroupRobot(UpdateGroupRobotConfigInput input)
        {
            return await _service.UpdateGroupRobot(input);
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<ApiResult>> Delete( string id)
        {
            return await _service.Delete(id);
        }

        /// <summary>
        /// 查看群机器人配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResult<GroupRobotDto>>> GetGroupRobotDetail(int companyId, int robotType)
        {
            var result = await _service.GetGroupRobotDetail(companyId, robotType);
            return result;
        }
    }
}
