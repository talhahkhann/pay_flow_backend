using System.Globalization;
using MediatR;
using PaymentSystem.Application.Common.Models;

public record VerifyEmailCommand(string Email , string Otp) : IRequest<Result<string>>;