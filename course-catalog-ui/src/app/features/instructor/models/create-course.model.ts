export interface CreateCourseRequest {

  code: string;

  title: string;

  description: string;

  category: string;

  instructorId: number;

  startDate: string;

  endDate: string;

  capacity: number;

}