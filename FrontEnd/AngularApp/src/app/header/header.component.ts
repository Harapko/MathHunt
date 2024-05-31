import {Component, signal} from '@angular/core';
import {LogInComponent} from "../log-in/log-in.component";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    LogInComponent
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

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
