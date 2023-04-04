using Newtonsoft.Json;

namespace KaiKuo.Message.Api.Models.Result
{
    public class BaseWeChatResult
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("errcode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }
    }
}
