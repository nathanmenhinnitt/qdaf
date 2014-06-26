namespace qdaf.Core
{
    using System.Collections.Generic;

    public class QdafValidationResult
    {
        public bool Valid { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
