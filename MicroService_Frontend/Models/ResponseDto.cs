namespace Frontend.Models{
    public class ResponseDto{
        internal bool Success;

        public object? Result {get; set; }
        public bool IsSuccess {get; set; } = true;
        public string Message {get; set; } =string.Empty;
        public object Data { get; internal set; }
    }
}