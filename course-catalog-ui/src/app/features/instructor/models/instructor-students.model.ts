export interface Student {

  studentId: number;

  fullName: string;

  email: string;

  enrollmentDate: string;

}

export interface InstructorStudents {

  courseId: number;

  courseTitle: string;

  students: Student[];

}