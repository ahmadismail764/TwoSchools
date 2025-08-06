# API Testing Guide

## Testing Order (Most Important First):

### 1. Schools (Foundation)

- `GET /api/School` - See existing schools
- `POST /api/School` - Create a new school (if needed)

### 2. School Years

- `GET /api/SchoolYear` - See existing school years
- `POST /api/SchoolYear` - Create a school year for your school

### 3. Terms

- `GET /api/Term` - See existing terms
- `POST /api/Term` - Create a term for your school year

### 4. Subjects

- `GET /api/Subject` - See existing subjects
- `POST /api/Subject` - Create some subjects (Math, English, Science, etc.)

### 5. Teachers

- `GET /api/Teacher` - See existing teachers
- `POST /api/Teacher` - Create teachers for your school

### 6. Students

- `GET /api/Student/school/{schoolId}` - See students in a school
- `POST /api/Student` - Create students for your school

### 7. Enrollments (The Main Feature)

- `POST /api/Enrollment` - Enroll students in subjects with teachers
- `GET /api/Enrollment/student/{studentId}` - See a student's enrollments
- `PUT /api/Enrollment/{id}/grade` - Add grades to enrollments

## Quick Test Sequence (5 minutes)

1. `GET /api/School` (see what schools exist)
2. `GET /api/Subject` (see what subjects exist)
3. `GET /api/Student/school/1` (see students in school 1)
4. `GET /api/Teacher` (see what teachers exist)
5. `POST /api/Enrollment` (try enrolling a student)

**Note:** Start with the GET endpoints to see what data is already seeded, then try creating new records if needed. The enrollment endpoint is the most complex and interesting one to test!
