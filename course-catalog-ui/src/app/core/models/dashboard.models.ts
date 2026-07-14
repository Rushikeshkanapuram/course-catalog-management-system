export interface AdminDashboard {

  totalUsers: number;

  totalStudents: number;

  totalInstructors: number;

  totalCourses: number;

  totalEnrollments: number;

}

export interface InstructorDashboard {

  myCourses: number;

  totalStudents: number;

  totalEnrollments: number;

}

export interface StudentDashboard {

  enrolledCourses: number;

  availableCourses: number;

}