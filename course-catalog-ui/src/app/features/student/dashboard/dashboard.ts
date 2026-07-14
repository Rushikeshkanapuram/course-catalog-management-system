import { Component, inject, signal } from '@angular/core';

import { StudentService } from '../services/student';
import { StudentDashboard } from '../models/student-dashboard.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {

  private studentService = inject(StudentService);

  protected dashboard = signal<StudentDashboard | null>(null);

  protected loading = signal(true);

  constructor() {

    this.studentService
      .getDashboard()
      .subscribe({

        next: response => {

          this.dashboard.set(response);

          this.loading.set(false);

        },

        error: error => {

          console.error(error);

          this.loading.set(false);

        }

      });

  }

}