import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';

import { AdminDashboard } from '../models/admin-dashboard.model';
import { User } from '../models/user.model';
import { CreateInstructorRequest } from '../models/create-instructor.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private http = inject(HttpClient);

  getDashboard(): Observable<AdminDashboard> {

    return this.http.get<AdminDashboard>(
      `${environment.apiUrl}/dashboard/admin`
    );

  }

  getUsers(): Observable<User[]> {

    return this.http.get<User[]>(
      `${environment.apiUrl}/user`
    );

  }

  createInstructor(request: CreateInstructorRequest) {

    return this.http.post(
      `${environment.apiUrl}/user/instructor`,
      request
    );

  }

 

}

