using System;
using System.Collections.Generic;

namespace Solver {
    public class Issue {
        public string Title;
        public string ID;
        public string Description;
        public string State;
        public string Date;
        public string Creator;
        public Dictionary<String, Question> Questions;

        public Issue() {
            Questions = new Dictionary<String, Question>();
        }
    }
}