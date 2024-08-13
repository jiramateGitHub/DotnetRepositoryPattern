namespace RepositoryPattern.Models
{
    public abstract class BaseMessage<T>
    {
        protected string _MessageId = string.Empty;
        public string MessageId { get { return _MessageId; } }
        public string TimeStamp { get { return DateTime.Now.ToString("yyyyMMddHHmmss"); } }
        public int SessionEmpID { get; set; }
        public T body { get; set; }

    }
    public class ResponseMessage<T> : BaseMessage<T>
    {
        public bool Status { get; set; }
        public int StatusId { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public ResponseMessage()
        {
            this._MessageId = Guid.NewGuid().ToString();
        }
        public ResponseMessage(string requestMessageId)
        {
            this._MessageId = requestMessageId;
        }
    }
    public class RequestMessage<T> : BaseMessage<T>
    {
        public string Module { get; set; }
        public RequestMessage()
        {
            this._MessageId = Guid.NewGuid().ToString();
        }
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpiresDay { get; set; }
        public bool IsToken { get; set; }

        public class UserAuthen
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class UserLogin
        {
            public string Username { get; set; }
        }
    }
}
