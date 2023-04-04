using System.ComponentModel.DataAnnotations;
using Wiwi.GroupRobot.Api.Enums;
using Wiwi.GroupRobot.Common.Helper;

namespace Wiwi.GroupRobot.Api.Models
{
    public class PushGroupMsgInput
    {
        /// <summary>
        /// 推送的内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public int CompanyId { get; set; }
    }

    public class PushWorkWechatGroupVm : PushGroupVm
    {
    }

    public class PushGroupVm
    {
        public string msgtype { get; set; } = "text";

        public TextInfo text { get; set; }

        public class TextInfo
        {
            public string content { get; set; }
        }
    }

    public class PushDingGroupVm : PushGroupVm
    {

    }

    public class GroupRobotDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string GroupRobotId { get; set; }

        /// <summary>
        /// WebHook
        /// </summary>
        public string WebHook { get; set; }

        /// <summary>
        /// 群机器人类型；企业微信-1；钉钉-2
        /// </summary>
        public GroupRobotTypeEnum GroupRobotType { get; set; }

        /// <summary>
        /// 类型描述
        /// </summary>
        public string GroupRobotTypeDec => EnumHelper.GetEnumDesc<GroupRobotTypeEnum>(GroupRobotType.GetHashCode());

        /// <summary>
        /// 租户id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    public class AddGroupRobotConfigInput
    {
        /// <summary>
        /// webhook id
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string WebHook { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 群机器人类型；企业微信-1；钉钉-2
        /// </summary>
        public int GroupRobotType { get; set; }
    }

    public class UpdateGroupRobotConfigInput
    {
        /// <summary>
        /// webhook id
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string WebHook { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public string GroupRobotId { get; set; }
    }
}
