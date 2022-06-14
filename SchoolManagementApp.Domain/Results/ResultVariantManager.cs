using Shared.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SchoolManagementApp.Domain.Results
{
    public class ResultVariantManager:BaseEntity<Guid>
    {
        protected ResultVariantManager() { }

        public ResultVariantManager(string session, Term term)
        {
            Session = session;
            Term = term;
        }

        public virtual void AddResult(Result result)
        {
            result.ResultVariantManager = this;
            _results.Add(result);
        }

        public virtual void RemoveResult(Result result)
        {
            _results.Remove(result);
        }

        public virtual string Session { get; set; }
        public virtual Term Term { get; set; }

        private ISet<Result> _results = new HashSet<Result>();
        public virtual IEnumerable<Result> Results { get; set; }
    }
}
