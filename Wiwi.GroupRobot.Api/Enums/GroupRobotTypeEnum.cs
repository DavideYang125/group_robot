using System.ComponentModel;

namespace Wiwi.GroupRobot.Api.Enums
{
    /// <summary>
    /// 群机器人类型
    /// </summary>
    public enum GroupRobotTypeEnum
    {
        [Description("企业微信")] Work = 1,
        [Description("钉钉")] Ding = 2,
    }
}
