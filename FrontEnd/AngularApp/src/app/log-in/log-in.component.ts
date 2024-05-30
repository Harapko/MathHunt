import { Component, computed, HostListener, Input } from '@angular/core';
import { debounceTime, Subject } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {NgIf} from "@angular/common";
import {UserService} from "../service/authorize/user.service";

@Component({
  selector: 'app-log-in',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink,
    NgIf
  ],
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.scss'
})
export class LogInComponent {
  loginForm!: FormGroup;
  isLoading: boolean = false;
  message: string = '';

  @Input() position!: { top: number, left: number };


  isVisible = true;
  private userActivity = new Subject<void>();
  private timerStarted = false;

  constructor(private http: HttpClient,
              private router: Router,
              private userService: UserService) { // Use Router for navigation
    this.userActivity.pipe(
      debounceTime(1000)
    ).subscribe(() => {
      this.isVisible = false;
    });
    this.initForm();
  }

  private initForm() {
    this.loginForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      this.isLoading = true;
      const formData = this.loginForm.value;

      this.http.post('http://localhost:5117/login', formData).subscribe(
        (response: any) => {
          this.isLoading = false;
          this.userService.setUserName(this.loginForm.value.userName);
          this.message = 'Login successful!';
          console.log('User logged in', response);
          this.router.navigate(['/profile']);  // Навигация после успешного входа
        },
        error => {
          this.isLoading = false;
          this.message = 'Login failed. Please try again.';
          console.error('Login error', error);
        }
      );
    } else {
      this.message = 'Please fill out all fields correctly.';
    }
  }
  @HostListener('mouseenter')
  @HostListener('mousemove')
  @HostListener('mousedown')
  @HostListener('touchstart')
  resetTimer() {
    this.isVisible = true;
    if (this.timerStarted) {
      this.userActivity.next();
    } else {
      this.startTimer();
    }
  }

  startTimer() {
    this.timerStarted = true;
    this.userActivity.next();
  }

}



