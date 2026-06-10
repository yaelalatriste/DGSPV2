namespace DGSP.Shared.Contracts.DTOs.ExternalServices.Email
{
    public class EmailRequestDto
    {
        public int Provider { get; set; }
        public string From { get; set; } = "";
        public string FromDisplayName { get; set; } = "";
        public List<string> To { get; set; } = new();
        public List<string> cc { get; set; } = new();
        public List<string> bcc { get; set; } = new();
        public string Subject { get; set; } = "";
        public string Body { get; set; } = "";
        public bool IsBodyHtml { get; set; }
        public List<EmailAttachmentDto> attachments { get; set; } = new();
        public int importance { get; set; }
    }
}
