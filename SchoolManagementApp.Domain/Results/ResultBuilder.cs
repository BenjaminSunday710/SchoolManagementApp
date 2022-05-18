namespace SchoolManagementApp.Domain.Results
{
    public class ResultBuilder
    {
        public ResultBuilder SetSession(string session)
        {
            _session = session;
            return this;
        }

        public ResultBuilder SetTerm(Term term)
        {
            _term = term;
            return this;
        }
        public ResultBuilder SetContinuousAssessmentScore(double score)
        {
            _continuousAssessment = score;
            return this;
        }
        public ResultBuilder SetExamScore(double score)
        {
            _examination = score;
            return this;
        }
        public ResultBuilder SetGrade(Grade grade)
        {
            _grade = grade;
            return this;
        }

        public ResultBuilder SetRemark(Remark remark)
        {
            _remark = remark;
            return this;
        }

        public Result Build()
        {
            return new Result()
            {
                Remark = _remark,
                Grade = _grade,
                ContinuousAssessment = _continuousAssessment,
                Examination = _examination,
                Session = _session,
                Term = _term,
                Total = _continuousAssessment + _examination
            };
        }

        private string _session { get; set; }
        private Term _term { get; set; }
        private double _continuousAssessment { get; set; }
        private double _examination { get; set; }
        private Grade _grade { get; set; }
        private Remark _remark { get; set; }
    }
}
