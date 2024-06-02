import { Component } from '@angular/core';
import {Route, Router, RouterLink, RouterOutlet} from "@angular/router";
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from "@angular/forms";
import { HttpClient } from "@angular/common/http";
import {NgIf} from "@angular/common";
import {RegisterService} from "../../service/authorize/register.service";
import {AuthService} from "../../service/authorize/auth.service";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    RouterLink,
    RouterOutlet,
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm!: FormGroup;
  isLoading: boolean = false;
  message: string = '';

  constructor(
    private registerService: RegisterService,
    private router: Router
    ) {
    this.initForm();
  }

  private initForm() {
    this.registerForm = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      phoneNumber: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      role: new FormControl('', Validators.required)
    });
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      this.isLoading = true;
      const formData = this.registerForm.value;
      this.registerService.registerUser(formData).subscribe(
          response => {
            this.isLoading = false;
            this.message = 'User registered successfully!';
            console.log('User registered', response);
            this.router.navigate(['/login'])
          },
          error => {
            this.isLoading = false;
            this.message = 'Registration failed. Please try again.';
            console.error('Registration error', error);
          }
      );
    } else {
      this.message = 'Please fill out all fields correctly.';
    }
  }
}
