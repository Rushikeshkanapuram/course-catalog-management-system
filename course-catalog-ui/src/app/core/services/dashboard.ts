import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';

import {
  AdminDashboard,
  InstructorDashboard,
  StudentDashboard
} from '../models/dashboard.models';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private http = inject(HttpClient);

  getAdminDashboard() {

    return this.http.get<AdminDashboard>(
      `${environment.apiUrl}/dashboard/admin`
    );

  }

  getInstructorDashboard() {

    return this.http.get<InstructorDashboard>(
      `${environment.apiUrl}/dashboard/instructor`
    );

  }

  getStudentDashboard() {

    return this.http.get<StudentDashboard>(
      `${environment.apiUrl}/dashboard/student`
    );

  }

}