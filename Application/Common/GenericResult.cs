using System.Collections.Generic;
using System.Linq;

namespace Application.Common
{
    public class GenericResult<TData>
    {
        public GenericResult(bool succeeded, IEnumerable<string> errors, TData data)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Data = data;
        }

        public TData Data { get; private set; }
        public bool Succeeded { get; private set; }
        public string[] Errors { get; private set; }
        public static GenericResult<TData> Success(TData data)
        {
            return new GenericResult<TData>(true, new string[] { }, data);
        }

        public static GenericResult<TData> Failure(IEnumerable<string> errors)
        {
            return new GenericResult<TData>(false, errors, default);
        }
        
        public static GenericResult<TData> Failure(params string[] errors)
        {
            return new GenericResult<TData>(false, errors, default);
        }

        public static GenericResult<TData> Failure()
        {
            return Failure("General system error");
        }
    }
}
