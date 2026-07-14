import { Component, inject, signal } from '@angular/core';

import { Router } from '@angular/router';

import { AdminService } from '../services/admin';

import { User } from '../models/user.model';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [],
  templateUrl: './users.html',
  styleUrl: './users.css'
})
export class Users {

  private adminService = inject(AdminService);

  private router = inject(Router);

  protected users = signal<User[]>([]);

  protected loading = signal(true);

  constructor() {

    this.loadUsers();

  }

  loadUsers(): void {

    this.adminService
      .getUsers()
      .subscribe({

        next: response => {

          this.users.set(response);

          this.loading.set(false);

        },

        error: error => {

          console.error(error);

          this.loading.set(false);

        }

      });

  }

  createInstructor(): void {

    this.router.navigate([
      '/admin/create-instructor'
    ]);

  }

}