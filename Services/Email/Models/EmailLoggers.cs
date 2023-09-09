namespace EmailService.Models{
    public class EmailLoggers{
        public Guid Id {get; set; }
        public string Email {get; set; } = string.Empty;
        public string message {get; set; }= string.Empty;
        public DateTime Created {get; set; } = DateTime.Now;
    }
}