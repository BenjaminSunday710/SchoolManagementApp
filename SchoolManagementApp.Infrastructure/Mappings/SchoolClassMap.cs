﻿using SchoolManagementApp.Domain.SchoolClasses;
using Shared.Infrastructure.Mappings;
using System;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class SchoolClassMap: BaseMap<Guid, SchoolClass>
    {
        public SchoolClassMap()
        {
            Table("SchoolClasses");
            Map(x => x.Name);
            References(x => x.School);
            References(x => x.ClassTeacher);
            HasMany(x => x.Students);
            HasMany(x => x.Subjects).Cascade.AllDeleteOrphan();
            HasMany(x => x.Results).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
