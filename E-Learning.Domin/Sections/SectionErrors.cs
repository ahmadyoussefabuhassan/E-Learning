using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Sections
{
    public static class SectionErrors
    {
        public static readonly Error NotFound = new(
            "Section.NotFound", "القسم المطلوب غير موجود في النظام");
        public static readonly Error HasRelatedData = new(
            "Section.HasRelatedData", "لا يمكن حذف هذا قسم لوجود بيانات مرتبطة به.");
    }
}
