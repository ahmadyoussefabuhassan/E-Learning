using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Units
{
    public static class UnitsErrors
    {
        public static readonly Error NotFound = new(
            "Unit.NotFound", "الوحدة المطلوبة غير موجودة في النظام");
    }
}
