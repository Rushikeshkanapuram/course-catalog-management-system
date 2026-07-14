import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { InstructorService } from '../services/instructor';
import { DatePipe } from '@angular/common';
import {
  InstructorStudents,
  Student
} from '../models/instructor-students.model';

@Component({
  selector: 'app-students',
  standalone: true,
 imports: [
    DatePipe
  ],  templateUrl: './students.html',
  styleUrl: './students.css'
})
export class Students {

  private instructorService = inject(InstructorService);

  private route = inject(ActivatedRoute);

  protected loading = signal(true);

  protected courseGroups = signal<InstructorStudents[]>([]);

  protected students = signal<Student[]>([]);

  protected courseTitle = signal('');

  constructor() {

    const courseId = this.route.snapshot.paramMap.get('courseId');

    if (courseId) {

      this.loadCourseStudents(Number(courseId));

    }
    else {

      this.loadAllStudents();

    }

  }

  private loadAllStudents(): void {

    this.instructorService
      .getAllStudents()
      .subscribe({

        next: response => {

          this.courseGroups.set(response);

          this.loading.set(false);

        },

        error: error => {

          console.error(error);

          this.loading.set(false);

        }

      });

  }

  private loadCourseStudents(courseId: number): void {

    this.instructorService
      .getCourseStudents(courseId)
      .subscribe({

        next: response => {

          this.students.set(response);

          const group: InstructorStudents = {

            courseId,

            courseTitle: 'Students',

            students: response

          };

          this.courseGroups.set([group]);

          this.loading.set(false);

        },

        error: error => {

          console.error(error);

          this.loading.set(false);

        }

      });

  }

}