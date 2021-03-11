using System.Collections.Generic;

namespace Models
{
    public class OperationResult<T>
    {
        public T Body { get; set; }
        public List<string> ErrorMessages { get; set; } = new();
        public bool IsSuccess { get; set; }
    }
}