//
// ─── TEACHER CONTRACTS ──────────────────────────────────────────────────────────
//

// Links
export const TeacherEntry = 'TeacherEntry'
// Relations
export const REL_TEACHER_CLASSES = '/relations#teacher-classes'
export const REL_TEACHER_COURSES = '/relations#teacher-courses'
// Actions
export const REMOVE_CLASS_TEACHER = 'remove-teacher-from-class'
export const ADD_CLASS_TEACHER = 'add-teacher-to-class'

//
// ─── STUDENT CONTRACTS ──────────────────────────────────────────────────────────
//

// Links
export const StudentEntry = 'StudentEntry'
// Relations
export const REL_STUDENT_CLASSES = '/relations#student-classes'
// Actions
export const REMOVE_CLASS_STUDENT = 'remove-student-from-class'
export const ADD_CLASS_STUDENT = 'add-student-to-class'

//
// ─── COURSE CONTRACTS ───────────────────────────────────────────────────────────
//

// Links
export const CourseList = 'CourseList'
export const CourseEntry = 'CourseEntry'
// Relations
export const REL_COURSE_CLASSES = 'relations#course-classes'
// Actions
export const ADD_CLASS_COURSE = 'add-class-to-course'

//
// ─── CLASS CONTRACTS ────────────────────────────────────────────────────────────
//

// Links
export const ClassEntry = 'ClassEntry'
export const ClassTeachersList = 'ClassTeachersList'
export const ClassStudentsList = 'ClassStudentsList'
export const ClassGroupsList = 'ClassGroupsList'

//
// ─── GROUP CONTRACTS ────────────────────────────────────────────────────────────
//

// Links
export const GroupEntry = 'GroupEntry'
// Actions
export const DELETE_GROUP = 'delete-group'
export const ADD_GROUP_CLASS = 'add-group-to-class'