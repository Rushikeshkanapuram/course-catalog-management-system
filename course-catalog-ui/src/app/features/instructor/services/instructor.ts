import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { CourseStudent } from '../models/course-student.model';
import { environment } from '../../../../environments/environment';
import { CreateCourseRequest } from '../models/create-course.model';
import { Course } from '../models/course.model';
import { InstructorStudents } from '../models/instructor-students.model';
@Injectable({
  providedIn: 'root'
})
export class InstructorService {

  private http = inject(HttpClient);

  getMyCourses(): Observable<Course[]> {

    return this.http.get<Course[]>(
      `${environment.apiUrl}/instructor/my-courses`
    );

  }

  createCourse(request: CreateCourseRequest) {

  return this.http.post(
    `${environment.apiUrl}/course`,
    request
  );

  

}
getCourseById(id: number) {

  return this.http.get<Course>(
    `${environment.apiUrl}/course/${id}`
  );

}

updateCourse(id: number, request: any) {

  return this.http.put(
    `${environment.apiUrl}/course/${id}`,
    request
  );

}

getCourseStudents(courseId: number) {

  return this.http.get<CourseStudent[]>(
    `${environment.apiUrl}/instructor/course/${courseId}/students`
  );

}

deleteCourse(courseId: number) {

  return this.http.delete(
    `${environment.apiUrl}/course/${courseId}`
  );

}

getAllStudents() {

  return this.http.get<InstructorStudents[]>(
    `${environment.apiUrl}/instructor/students`
  );

}
}