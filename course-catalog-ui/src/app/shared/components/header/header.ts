import { Component, inject } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { DatePipe } from '@angular/common';
import { AuthService } from '../../../core/services/auth';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatIconModule,
    DatePipe
  ],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header {

  protected authService = inject(AuthService);

  today = new Date();

  getInitials(): string {

    const name = this.authService.getFullName();

    if (!name) {
      return '';
    }

    return name
      .split(' ')
      .map(x => x[0])
      .join('')
      .toUpperCase();

  }

}