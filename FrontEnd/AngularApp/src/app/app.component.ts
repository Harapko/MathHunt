import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HeaderComponent} from "./header/header.component";
import {RegisterComponent} from "./register/register.component";
import {LogInComponent} from "./log-in/log-in.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet , HeaderComponent , RegisterComponent , LogInComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'AngularApp';
  protected readonly HeaderComponent = HeaderComponent;
}
