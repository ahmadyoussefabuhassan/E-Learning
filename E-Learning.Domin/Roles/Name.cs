using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Roles
{
    public record Name
    {
        public string Value { get; init; }
        public readonly static Name Admin = new Name("Admin");
        public readonly static Name Teacher = new Name("Teacher");
        public readonly static Name Student = new Name("Student");
        private Name()
        {
            Value = string.Empty;
        }
        public Name(string value) => Value = value;
        public static readonly IReadOnlyCollection<Name> All = new[]
        {
            Admin,
            Teacher,
            Student
        };
        public static Name FromName(string name)
        {
            return All.FirstOrDefault(n => n.Value.Equals(name, StringComparison.OrdinalIgnoreCase))
                ?? throw new ArgumentException($"The role name {name} is invalid");
        }
    }
}
