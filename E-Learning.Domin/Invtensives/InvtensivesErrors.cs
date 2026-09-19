using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Invtensives
{
    public static class InvtensivesErrors
    {
        public static readonly Error NotFound = new(
                        "Invtensives.not found", "لم يتم العثور على الدورة المكثفة المحددة.");
    }
}
