import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { StudentDashboard } from '../models/student-dashboard.model';
import { Course } from '../models/course.model';
import { Enrollment } from '../models/enrollment.model';
import { CreateEnrollmentRequest } from '../models/create-enrollment.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private http = inject(HttpClient);

  getCourses(): Observable<Course[]> {

    return this.http.get<Course[]>(
      `${environment.apiUrl}/course`
    );

  }

  enroll(request: CreateEnrollmentRequest) {

    return this.http.post(
      `${environment.apiUrl}/enrollment`,
      request
    );

  }

  getMyEnrollments(): Observable<Enrollment[]> {

    return this.http.get<Enrollment[]>(
      `${environment.apiUrl}/enrollment/my-enrollments`
    );

  }

  dropCourse(enrollmentId: number) {

    return this.http.delete(
      `${environment.apiUrl}/enrollment/${enrollmentId}`
    );

  }

  getDashboard() {

  return this.http.get<StudentDashboard>(
    `${environment.apiUrl}/dashboard/student`
  );

}

}