using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Classes
{
    public static class ClassesErrors
    {
        public static readonly Error NotFound = new(
            "Classes.not found" , "لم يتم العثور على الفئة المحددة.");
        public static readonly Error AlreadyExists = new(
            "Class.AlreadyExists", "هذا الصف موجود مسبقاً، يرجى اختيار اسم آخر.");
        public static readonly Error HasRelatedData = new(
           "Class.HasRelatedData", "لا يمكن حذف هذا الصف لوجود بيانات مرتبطة به.");

    }
}
