using System;

namespace Department {
    [Serializable]
    public class Question {
        public string ID;
        public string issueID;
        public string Text;
        public string Answer;
        public string State;
        public string Date;
        public string Department;
    }
}