/**
 * Default Roles
 */
export const GUEST = 'GUEST'
export const STUDENT = 'Student'
export const TEACHER = 'Teacher'
export const ADMIN = 'Administrator'

export const defaultRoles = {
  [ADMIN]: [TEACHER, STUDENT, GUEST],
  [TEACHER]: [STUDENT, GUEST],
  [STUDENT]: [GUEST],
  [GUEST]: []
}
