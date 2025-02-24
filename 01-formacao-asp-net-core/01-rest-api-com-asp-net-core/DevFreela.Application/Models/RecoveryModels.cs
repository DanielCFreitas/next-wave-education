namespace DevFreela.Application.Models;

public class PasswordRecoveryInputModel
{
    public string Email { get; set; }
}

public class ValidateRecoveryCodeInputModel
{
    public string Email { get; set; }
    public string Code { get; set; }
}

public class ChangePasswordInputModel
{
    public string Email { get; set; }
    public string Code { get; set; }
    public string NewPassword { get; set; }
}