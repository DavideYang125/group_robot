using SqlSugar;
using Wiwi.GroupRobot.Api.Enums;
using Wiwi.GroupRobot.Common.Config;

namespace Wiwi.GroupRobot.Api.Domain
{
    [SugarTable($"group_robot", "群机器人配置")]
    public class GroupRobotModel : BaseDomain
    {
        public GroupRobotModel()
        {
        }

        public GroupRobotModel(string groupRobotId, string webHook, GroupRobotTypeEnum groupRobotType, int companyId, DateTime createTime)
        {
            GroupRobotId = groupRobotId;
            WebHook = webHook;
            GroupRobotType = groupRobotType;
            CompanyId = companyId;
            CreateTime = createTime;
            UpdateTime = createTime;
            IsDeleted = false;
        }

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(ColumnName = "group_robot_id", ColumnDataType = "varchar", Length = 50, IsPrimaryKey = true, ColumnDescription = "主键")]
        public string GroupRobotId { get; set; }

        /// <summary>
        /// WebHook
        /// </summary>
        [SugarColumn(ColumnName = "web_hook", ColumnDataType = "varchar", IsNullable = true, Length = 200, ColumnDescription = "WebHook")]
        public string WebHook { get; set; }

        /// <summary>
        /// 群机器人类型；企业微信-1；钉钉-2
        /// </summary>
        [SugarColumn(ColumnName = "group_robot_type", ColumnDescription = "群机器人类型")]
        public GroupRobotTypeEnum GroupRobotType { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        [SugarColumn(ColumnName = "company_id", ColumnDescription = "租户id")]
        public int CompanyId { get; set; }
    }
}
