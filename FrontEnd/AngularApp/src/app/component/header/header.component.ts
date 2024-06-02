import {Component, OnInit, signal, SimpleChange} from '@angular/core';
import {LogInComponent} from "../log-in/log-in.component";
import {RouterLink} from "@angular/router";
import {UserService} from "../../service/userService/user.service";
import {UserResponse} from "../../models/user/user-response";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    LogInComponent,
    RouterLink
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit{
  user!: UserResponse;
  constructor(public userService: UserService) {
  }



  ngOnInit(): void {
    this.user = this.userService;
    }

  showLogin = signal(false);
  loginPosition = signal({ top: 0, left: 0 });
  isVisible: any;
  resetTimer = signal<any | null>(null);

  toggleLogin(event: MouseEvent) {
    this.showLogin.set(!this.showLogin());
    if (this.showLogin()) {
      const loginElement = document.querySelector('.login') as HTMLElement;
      const loginRect = loginElement.getBoundingClientRect();
      const offsetLeft = loginElement.offsetLeft;
      this.loginPosition.set({
        top: loginRect.bottom + 30,
        left: loginRect.left - 222
      });
    }
  }

  startTimer() {

  }
}
