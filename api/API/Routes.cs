﻿using System;
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

        // CoursesController
        public const string CourseList = nameof(CourseList);
        public const string CourseEntry = nameof(CourseEntry);
        public const string CourseCreate = nameof(CourseCreate);
        public const string CourseEdit = nameof(CourseEdit);
        public const string CourseDelete = nameof(CourseDelete);

        // ClassesController
        public const string ClassList = nameof(ClassList);
        public const string ClassEntry = nameof(ClassEntry);
        public const string ClassCreate = nameof(ClassCreate);
        public const string ClassEdit = nameof(ClassEdit);
        public const string ClassDelete = nameof(ClassDelete);
        public const string ClassGroupsList = nameof(ClassGroupsList);
        public const string ClassParticipantsList = nameof(ClassParticipantsList);
        public const string ClassParticipantAdd = nameof(ClassParticipantAdd);
        public const string ClassGroupAdd = nameof(ClassGroupAdd);

        // SemestersController
        public const string SemesterEntry = nameof(SemesterEntry);

        // GroupsController
        public const string GroupStudentsList = nameof(GroupStudentsList);
        public const string GroupEntry = nameof(GroupEntry);
    }
}
