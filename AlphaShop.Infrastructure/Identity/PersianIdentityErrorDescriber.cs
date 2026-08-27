using Microsoft.AspNetCore.Identity;

namespace AlphaShop.Infrastructure.Identity
{

    public class PersianIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
            => new IdentityError { Code = nameof(DefaultError), Description = "یک خطای ناشناخته رخ داده است." };

        public override IdentityError ConcurrencyFailure()
            => new IdentityError { Code = nameof(ConcurrencyFailure), Description = "رکورد جاری پیش‌تر توسط کاربر دیگری ویرایش شده است و تغییرات شما اعمال نشد." };

        public override IdentityError PasswordMismatch()
            => new IdentityError { Code = nameof(PasswordMismatch), Description = "رمز عبور وارد شده نادرست است." };

        public override IdentityError InvalidToken()
            => new IdentityError { Code = nameof(InvalidToken), Description = "توکن وارد شده نامعتبر و یا منقضی شده است." };

        public override IdentityError RecoveryCodeRedemptionFailed()
            => new IdentityError { Code = nameof(RecoveryCodeRedemptionFailed), Description = "بازیابی با کد بازیابی ناموفق بود." };

        public override IdentityError LoginAlreadyAssociated()
            => new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "این حساب کاربری پیش‌تر به کاربر دیگری متصل شده است." };

        public override IdentityError InvalidUserName(string? userName)
            => new IdentityError { Code = nameof(InvalidUserName), Description = $"نام کاربری '{userName}' نامعتبر است. نام کاربری تنها می‌تواند شامل حروف انگلیسی و اعداد باشد." };

        public override IdentityError InvalidEmail(string? email)
            => new IdentityError { Code = nameof(InvalidEmail), Description = $"فرمت ایمیل '{email}' نامعتبر است." };

        public override IdentityError DuplicateUserName(string userName)
            => new IdentityError { Code = nameof(DuplicateUserName), Description = $"نام کاربری '{userName}' قبلاً توسط شخص دیگری ثبت شده است." };

        public override IdentityError DuplicateEmail(string email)
            => new IdentityError { Code = nameof(DuplicateEmail), Description = $"ایمیل '{email}' قبلاً در سیستم ثبت شده است." };

        public override IdentityError InvalidRoleName(string? role)
            => new IdentityError { Code = nameof(InvalidRoleName), Description = $"نام نقش '{role}' نامعتبر است." };

        public override IdentityError DuplicateRoleName(string role)
            => new IdentityError { Code = nameof(DuplicateRoleName), Description = $"نقش '{role}' از قبل در سیستم وجود دارد." };

        public override IdentityError UserAlreadyHasPassword()
            => new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "برای این حساب کاربری پیش‌تر رمز عبور تنظیم شده است." };

        public override IdentityError UserLockoutNotEnabled()
            => new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "قابلیت قفل شدن حساب برای این کاربر فعال نیست." };

        public override IdentityError UserAlreadyInRole(string role)
            => new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"کاربر در حال حاضر دارای نقش '{role}' می‌باشد." };

        public override IdentityError UserNotInRole(string role)
            => new IdentityError { Code = nameof(UserNotInRole), Description = $"کاربر دارای نقش '{role}' نیست." };

        public override IdentityError PasswordTooShort(int length)
            => new IdentityError { Code = nameof(PasswordTooShort), Description = $"رمز عبور باید حداقل شامل {length} کاراکتر باشد." };

        public override IdentityError PasswordRequiresNonAlphanumeric()
            => new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "رمز عبور باید حداقل شامل یک کاراکتر ویژه (مانند @, #, !, * و...) باشد." };

        public override IdentityError PasswordRequiresDigit()
            => new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "رمز عبور باید حداقل شامل یک عدد (0 تا 9) باشد." };

        public override IdentityError PasswordRequiresLower()
            => new IdentityError { Code = nameof(PasswordRequiresLower), Description = "رمز عبور باید حداقل شامل یک حرف کوچک انگلیسی (a-z) باشد." };

        public override IdentityError PasswordRequiresUpper()
            => new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "رمز عبور باید حداقل شامل یک حرف بزرگ انگلیسی (A-Z) باشد." };

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
            => new IdentityError { Code = nameof(PasswordRequiresUniqueChars), Description = $"رمز عبور باید حداقل شامل {uniqueChars} کاراکتر متمایز (غیر تکراری) باشد." };
    }
}
