import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  LoginRequest,
  LoginResponse,
  RegisterRequest
} from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private http = inject(HttpClient);

  currentUser = signal<LoginResponse | null>(null);

  login(request: LoginRequest): Observable<LoginResponse> {

    return this.http.post<LoginResponse>(
      `${environment.apiUrl}/auth/login`,
      request
    );

  }

  register(request: RegisterRequest): Observable<void> {

    return this.http.post<void>(
      `${environment.apiUrl}/auth/register`,
      request
    );

  }

  saveUser(user: LoginResponse): void {

    localStorage.setItem('user', JSON.stringify(user));

    this.currentUser.set(user);

  }

loadUser(): void {

  const user = localStorage.getItem('user');

  if (user) {

    this.currentUser.set(JSON.parse(user));

  }

}

  logout(): void {

    localStorage.removeItem('user');

    this.currentUser.set(null);

  }

  isLoggedIn(): boolean {

    return this.currentUser() !== null;

  }

  getRole(): string {

  return this.currentUser()?.role ?? '';

}

getFullName(): string {

  return this.currentUser()?.fullName ?? '';

}

isAdmin(): boolean {

  return this.getRole() === 'Admin';

}

isInstructor(): boolean {

  return this.getRole() === 'Instructor';

}

isStudent(): boolean {

  return this.getRole() === 'Student';

}

}