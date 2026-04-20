using MediatR;

namespace Auth.Application.Auth.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password) : IRequest<string>;