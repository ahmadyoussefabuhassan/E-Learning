using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.User
{
    public static class UserErrors
    {
        public static readonly Error NotFound = new(
          "User.NotFound", "المستخدم المطلوب غير موجود في النظام");

        public static readonly Error EmailAlreadyExists = new(
            "User.EmailAlreadyExists", "البريد الإلكتروني هذا مستخدم بالفعل من قبل حساب آخر");

        public static readonly Error InvalidCredentials = new(
            "User.InvalidCredentials", "البريد الإلكتروني أو كلمة المرور غير صحيحة");

        public static readonly Error Unauthorized = new(
            "User.Unauthorized", "ليس لديك الصلاحية الكافية للقيام بهذا الإجراء");
        public static Error InvalidEmail = new Error("User.InvalidEmail", 
            "البريد الإلكتروني غير صالح");
        public static Error InvalidPassword = new Error("User.InvalidPassword", 
            "كلمة المرور غير صالحة");
        public static readonly Error InvalidOldPassword = new(
              "User.InvalidOldPassword", "كلمة المرور القديمة التي أدخلتها غير صحيحة.");
        public static readonly Error InvalidResetCode = new(
            "User.InvalidResetCode", "رمز التحقق الذي أدخلته غير صحيح.");

        public static readonly Error ResetCodeExpired = new(
            "User.ResetCodeExpired", "انتهت صلاحية هذا الرمز، يرجى طلب رمز جديد.");
    }
}
