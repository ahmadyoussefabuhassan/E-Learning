using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.RefreshTokens
{
    public static class RefreshTokenErrors
    {
        public static readonly Error NotFound = new(
       "RefreshToken.NotFound", "رمز التجديد غير موجود أو غير صالح");

        public static readonly Error Expired = new(
            "RefreshToken.Expired", "انتهت صلاحية الجلسة، يرجى تسجيل الدخول مرة أخرى");

        public static readonly Error AlreadyUsed = new(
            "RefreshToken.AlreadyUsed", "تم استخدام رمز التجديد هذا مسبقاً، لا يمكن إعادة استخدامه");

        public static readonly Error Revoked = new(
            "RefreshToken.Revoked", "لقد تم إلغاء صلاحية هذا الرمز لأسباب أمنية");
    }
}
