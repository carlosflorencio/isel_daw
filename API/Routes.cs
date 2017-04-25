using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class Routes
    {
        // HomeController
        public const string Index = nameof(Index);
        public const string Error = nameof(Error);

        // StudentsController
        public const string StudentList = nameof(StudentList);
        public const string StudentEntry = nameof(StudentEntry);
        public const string StudentClassList = nameof(StudentClassList);
        public const string StudentGroupList = nameof(StudentGroupList);
        public const string StudentCreate = nameof(StudentCreate);
        public const string StudentEdit = nameof(StudentEdit);
        public const string StudentDelete = nameof(StudentDelete);

        // TeachersController
        public const string TeacherList = nameof(TeacherList);
        public const string TeacherEntry = nameof(TeacherEntry);
        public const string TeacherCreate = nameof(TeacherCreate);
        public const string TeacherEdit = nameof(TeacherEdit);
        public const string TeacherDelete = nameof(TeacherDelete);
        public const string TeacherClassList = nameof(TeacherClassList);

    }
}
