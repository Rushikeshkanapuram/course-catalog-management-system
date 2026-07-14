export interface Course {

  id: number;

  code: string;

  title: string;

  description: string;

  category: string;

  instructorName?: string;

  instructorId?: number;

  startDate: string;

  endDate: string;

  capacity: number;

}