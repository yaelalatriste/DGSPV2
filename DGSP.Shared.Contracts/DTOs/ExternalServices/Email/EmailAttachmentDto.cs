namespace DGSP.Shared.Contracts.DTOs.ExternalServices.Email
{
    public class EmailAttachmentDto
    {
        public string fileName { get; set; } = "";
        public string contentType { get; set; } = "";
        public string contentBase64 { get; set; } = "";
    }
}
