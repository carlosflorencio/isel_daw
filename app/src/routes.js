import * as roles from './models/Roles'

import Teacher from './components/teachers/Teacher'
import Student from './components/students/Student'
import CourseClasses from './components/classes/CourseClasses'
import Class from './components/classes/ClassContainer'
import ClassGroups from './components/groups/ClassGroupsContainer'

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
    {
        path: '/courses/:id/classes',
        exact: true,
        component: CourseClasses,
        minRole: roles.STUDENT,
        routes: []
    },
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
]