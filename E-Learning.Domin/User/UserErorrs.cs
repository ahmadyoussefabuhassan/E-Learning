using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.User
{
    public static class UserErorrs
    {
        public static readonly Error NotFound = new(
          "User.NotFound", "المستخدم المطلوب غير موجود في النظام");

        public static readonly Error EmailAlreadyExists = new(
            "User.EmailAlreadyExists", "البريد الإلكتروني هذا مستخدم بالفعل من قبل حساب آخر");

        public static readonly Error InvalidCredentials = new(
            "User.InvalidCredentials", "البريد الإلكتروني أو كلمة المرور غير صحيحة");

        public static readonly Error Unauthorized = new(
            "User.Unauthorized", "ليس لديك الصلاحية الكافية للقيام بهذا الإجراء");
    }
}
