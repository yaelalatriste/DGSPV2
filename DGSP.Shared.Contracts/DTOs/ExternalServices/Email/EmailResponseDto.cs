namespace DGSP.Shared.Contracts.DTOs.ExternalServices.Email
{
    public class EmailResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string RawResponse { get; set; } = string.Empty;
    }
}
