import * as roles from './models/Roles'

import Teacher from './components/teachers/TeacherContainer'
import Student from './components/students/StudentContainer'
//import CourseClasses from './components/classes/CourseClasses'
import Class from './components/classes/ClassContainer'
import ClassGroups from './components/groups/ClassGroupsContainer'
import Group from './components/groups/Group'
import ClassStudents from './components/students/ClassStudentsContainer'

export default [
    {
        path: '/teachers/:id',
        exact: true,
        component: Teacher,
        minRole: roles.TEACHER,
        routes: []
    },
    {
        path: '/students/:id',
        exact: true,
        component: Student,
        minRole: roles.STUDENT,
        routes: []
    },
    // {
    //     path: '/courses/:id/classes',
    //     exact: true,
    //     component: CourseClasses,
    //     minRole: roles.STUDENT,
    //     routes: []
    // },
    {
        path: '/classes/:id',
        exact: true,
        component: Class,
        minRole: roles.STUDENT,
        routes: []
    },
    {
        path: '/classes/:id/groups',
        exact: true,
        component: ClassGroups,
        minRole: roles.STUDENT,
        routes: []
    },
    {
        path: '/classes/:id/groups/:groupId',
        exact: true,
        component: Group,
        minRole: roles.STUDENT,
        routes: []
    },
    {
        path: '/classes/:id/students',
        exact: true,
        component: ClassStudents,
        minRole: roles.STUDENT,
        routes: []
    },
]