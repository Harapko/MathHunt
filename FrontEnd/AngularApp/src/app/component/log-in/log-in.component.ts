import {Component, HostListener, Input, OnInit} from '@angular/core';
import { debounceTime, Subject } from "rxjs";
import {FormGroup, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {Route, Router, RouterLink} from "@angular/router";
import {NgIf} from "@angular/common";
import {AuthService} from "../../service/authorize/auth.service";
import {LoginRequest} from "../../models/login/login-request";
import {UserService} from "../../service/userService/user.service";
import {UserResponse} from "../../models/user/user-response";

@Component({
  selector: 'app-log-in',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink,
    NgIf,
    FormsModule
  ],
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.scss'
})
export class LogInComponent implements OnInit{
  message: string = '';
  user!: UserResponse;

  @Input() position!: { top: number, left: number };

  isVisible = true;
  private userActivity = new Subject<void>();
  private timerStarted = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private userService: UserService
              ) {
    this.userActivity.pipe(
      debounceTime(1000)
    ).subscribe(() => {
      this.isVisible = false;
    });
  }



  ngOnInit(): void {
    this.isLoggedIn();
    }

  credential: LoginRequest = {
    email: '',
    password: ''
  }

  isLoggedIn(){
    return this.authService.isLoggedIn();
  }

  login(){
    this.authService.login(this.credential)
      .subscribe(() => {
        this.userService.getUserInfo(this.credential.email)
        console.log("Login successfully");
        this.router.navigate(['/home-page'])

      },
      error => {
        console.log(error);

      console.log("error", error)
      })

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



