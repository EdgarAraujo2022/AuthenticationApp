namespace AuthenticationService.Services.Dtos
{
    public record UserMeDto(Guid Id, string Email, string Role);
}