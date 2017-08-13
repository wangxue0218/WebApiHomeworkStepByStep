using System.Collections.Generic;
using MyApplication;

namespace Application.Test
{
    public class FakeLogger : IMyLogger
    {
        public readonly List<string> storage = new List<string>();

        public void Info(string message)
        {
            storage.Add(message);
        }
    }
}