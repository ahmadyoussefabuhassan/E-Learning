

using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Roles
{
    public static class RoleErrors
    {
        public static readonly Error NotFound = new(
         "Role.NotFound", "الصلاحية المطلوبة غير موجودة في النظام");

        public static readonly Error AccessDenied = new(
            "Role.AccessDenied", "ليس لديك الصلاحيات الكافية للوصول إلى هذا القسم");
    }
}
